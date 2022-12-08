<?php
//Utilities and data types for web scraper

mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT); // Set MySQLi to throw exceptions

$sleepBetweenWebCalls = 4; //seconds to wait between loading pages - avoid denial of service

class Record
{
	public $name;
	public $isBoy;
	public $isGirl;
	public $isFirstName;
	public $isLastName;
	public $origin;
	public $meaning;
	
	public function __construct($name) 
	{
		$this->{'name'} = $name;
	}
	
	public function display()
	{
		echo $this->{'name'}."\t".$this->{'isBoy'}."\t".$this->{'isGirl'}."\t".$this->{'origin'}."\t".$this->{'meaning'}."\t".$this->{'isFirstName'}."\t".$this->{'isLastName'}."\n";
	}
	
	public static function displayHeaders()
	{
		echo "Name \tIsBoy \tIsGirl \tOrigin \tMeaning \tFirstName \tLastName \n";
	}
	
	public function saveToDatabase($sourceId)
	{
		$nameId = saveGetNameId($this->{'name'});
		
		$connection = getConnection();
		//prep query
		$sql = mysqli_prepare($connection, "INSERT INTO Detail(fkSourceId,fkNameId,IsBoy,IsGirl,Origin,Meaning,IsFirstName,IsLastName,CreatedDate) VALUES(?,?,?,?,?,?,?,?,now())");
		if(!$sql)
		{
			throw new Exception("Could not prepare query statement in ".__FUNCTION__.".");
		}
		$sql->bind_param("iiiissii", $sourceId, $nameId, intval($this->{'isBoy'}), intval($this->{'isGirl'}), $this->{'origin'}, $this->{'meaning'}, intval($this->{'isFirstName'}), intval($this->{'isLastName'}));
		$sql->execute();
		$sql->close();
		//cleanup
		mysqli_close($connection);
	}
}

//returns true if this combination is found
function arrayContainsNameOrigin($arrayOfRecords, $name, $origin)
{
	foreach($arrayOfRecords as $record)
	{
		if($record->{'name'} == $name)
		{
			if($record->{'origin'} == $origin)
				return true;
			if($record->{'origin'} == null && ($origin == null || strlen($origin) == 0))
				return true;
		}
	}
	return false;
}

//returns true if this name is found
function arrayContainsName($arrayOfRecords, $name)
{
	foreach($arrayOfRecords as $record)
	{
		if($record->{'name'} == $name)
		{
			return true;
		}
	}
	return false;
}

//if new name, save it
//either way, return name id
function saveGetNameId($name)
{
	$connection = getConnection();
	$nameId = getNameId($name, $connection);
	if($nameId == null)
	{
		$sql = mysqli_prepare($connection, "INSERT INTO Name(Name) VALUES(?)");
		if(!$sql)
		{
			throw new Exception("Could not prepare query statement in ".__FUNCTION__.".");
		}
		$sql->bind_param("s", $name);
		$sql->execute();
		mysqli_close($connection);
		$connection = getConnection();
		$nameId = getNameId($name, $connection);
	}
	mysqli_close($connection);
	return $nameId;
}

//return name id, or null
//does not close connection
function getNameId($name, $connection)
{
	$sql = mysqli_prepare($connection, "SELECT NameId FROM Name WHERE Name.Name = ?");
	if(!$sql)
	{
		throw new Exception("Could not prepare query statement in ".__FUNCTION__.".");
	}
	$sql->bind_param("s", $name);
	$sql->execute();
	$sql->store_result();
	$nameId = null;
	if($sql->num_rows > 0)
	{
		$sql->bind_result($nameId);
		$sql->fetch();
	}
	$sql->close();
	return $nameId;
}

//create source if it doesn't exist
//return source id
function saveGetSourceId($sourceName, $sourceUrl)
{
	$connection = getConnection();
	$sourceId = getSourceId($sourceName, $connection);
	if($sourceId == null)
	{
		$sql = mysqli_prepare($connection, "INSERT INTO Source(Name,Url) VALUES(?,?)");
		if(!$sql)
		{
			throw new Exception("Could not prepare query statement in ".__FUNCTION__.".");
		}
		$sql->bind_param("ss", $sourceName, $sourceUrl);
		$sql->execute();
		$sourceId = getSourceId($sourceName, $connection);
	}
	mysqli_close($connection);
	return $sourceId;
}

//return source id, or null
//does not close connection
function getSourceId($sourceName, $connection)
{
	$sql = mysqli_prepare($connection, "SELECT SourceId FROM Source WHERE Source.Name = ?");
	if(!$sql)
	{
		throw new Exception("Could not prepare query statement in ".__FUNCTION__.".");
	}
	$sql->bind_param("s", $sourceName);
	$sql->execute();
	$sql->store_result();
	$sourceId = null;
	if($sql->num_rows > 0)
	{
		$sql->bind_result($sourceId);
		$sql->fetch();
	}
	$sql->close();
	return $sourceId;
}

//return open database connection
function getConnection()
{
	$connection = mysqli_connect('localhost', 'withouth_names', 'h$RX76mb7L!', 'withouth_names');
	mysqli_set_charset($connection, "utf8"); //for correct saving of accented characters
	if ($connection->connect_error) 
	{
		throw new Exception("Connection failed: " . $connection->connect_error);
	}
	return $connection;
}

//load all name/origins by source
function loadNamesBySource($sourceId)
{
	$names = array();
	$connection = getConnection();
	//prep query
	$sql = mysqli_prepare($connection, "SELECT DISTINCT N.Name, D.Origin FROM Name AS N INNER JOIN Detail as D ON D.fkNameId = N.NameId WHERE D.fkSourceId = ?");
	if(!$sql)
	{
		throw new Exception("Could not prepare query statement in ".__FUNCTION__.".");
	}
	$sql->bind_param("i", $sourceId);
	$sql->execute();
	$sql->bind_result($name, $origin);
	while ($sql->fetch()) 
	{
		$record = new Record($name);
		$record->{'origin'} = $origin;
		array_push($names, $record);
	}
	$sql->close();
	//cleanup
	mysqli_close($connection);

	return $names;
}

//remove all data related to source from database
function clearSourceData($sourceId)
{
	$connection = getConnection();
	//prep query
	$sql = mysqli_prepare($connection, "DELETE FROM Detail WHERE fkSourceId = ?");
	if(!$sql)
	{
		throw new Exception("Could not prepare query statement in ".__FUNCTION__.".");
	}
	$sql->bind_param("i", $sourceId);
	$sql->execute();
	$sql->close();
	//cleanup
	mysqli_close($connection);
}

?>