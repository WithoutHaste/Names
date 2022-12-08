<?php

$results = array();
$gender = $_GET["gender"];
$letters = $_GET["letters"];
$letters = urldecode($letters);
$letters = explode(",", $letters);
$origins = $_GET["origins"];
$origins = explode(",", $origins);

$connection = getConnection();

$sql = getQuery($connection, $gender, $letters, $origins);
$sql->execute();
$sql->store_result();
$sql->bind_result($name, $isBoy, $isGirl, $origins);
while($sql->fetch())
{
	$record = new stdClass(); //default empty object
	$record->name = $name;
	$record->isBoy = $isBoy;
	$record->isGirl = $isGirl;
	$record->origins = $origins;
	array_push($results, $record);
}

$sql->close();
mysqli_close($connection);

echo json_encode($results);

function getQuery($connection, $gender, $letters, $origins)
{
	$whereStarted = false;
	$queryText = @"
	SELECT 
		N.Name, 
		Sub.IsBoy,
		Sub.IsGirl,
		Sub.Origins
	FROM Name as N
	INNER JOIN (
		SELECT 
			N.NameId, 
			MAX(D.IsBoy) as IsBoy, 
			MAX(D.IsGirl) as IsGirl ,
			GROUP_CONCAT(DISTINCT D.Origin SEPARATOR ', ') as Origins
		FROM Name as N LEFT JOIN Detail as D on D.fkNameId = N.NameId  ";

	if(count($origins) > 0 && $origins[0] != "All")
	{
		$queryText = $queryText."WHERE D.Origin IN (".implode(",", cleanOrigins($connection, $origins)).") ";
	}
		
	$queryText = $queryText." GROUP BY N.NameId) as Sub on Sub.NameId = N.NameId "; 
	if($gender == "Boy")
	{
		$queryText = $queryText." WHERE IsBoy > 0 ";
		$whereStarted = true;
	}
	else if($gender == "Girl")
	{
		$queryText = $queryText." WHERE IsGirl > 0 ";
		$whereStarted = true;
	}

	$cleanLetters = array();
	foreach($letters as $letter)
	{
		if(strlen($letter) != 1 && strlen($letter) != 2) //2 because unusual letters are stored as 2 characters
			continue;
		if(strpos($letter, "'") !== false)
			continue;
		array_push($cleanLetters, "'".$letter."'");
	}
	if(count($letters) > 0 && $letters[0] != "All" && count($cleanLetters) > 0)
	{
		if($whereStarted)
			$queryText = $queryText." AND ";
		else
			$queryText = $queryText." WHERE ";
		$whereStarted = true;
		$queryText = $queryText." N.FirstLetter IN (".implode(",", $cleanLetters).") ";
	}
	
	$queryText = $queryText." ORDER BY N.Name ASC";

	$sql = mysqli_prepare($connection, $queryText);
	if(!$sql)
	{
		throw new Exception("Could not prepare query statement.");
	}
	//	$sql->bind_param("s", $sourceName);
	
	return $sql;
}

function cleanOrigins($connection, $origins)
{
	$sql = mysqli_prepare($connection, 'SELECT Category, SuperCategory FROM Category');
	if(!$sql)
	{
		throw new Exception("Could not prepare query statement.");
	}
	$sql->execute();
	$sql->store_result();
	$dbCategories = array();
	$dbOrigins = array();
	$sql->bind_result($category, $superCategory);
	while($sql->fetch())
	{
		$record = new stdClass(); //default empty object
		$record->category = $category;
		$record->superCategory = $superCategory;
		array_push($dbCategories, $record);
		array_push($dbOrigins, $category);
	}
	$sql->close();
	
	$cleanOrigins = array();
	foreach($origins as $origin)
	{
		if(in_array($origin, $dbOrigins))
		{
			array_push($cleanOrigins, $origin);
		}
	}
	
	$changeMade = true;
	while($changeMade)
	{
		$changeMade = false;
		foreach($cleanOrigins as $cleanOrigin)
		{
			foreach($dbCategories as $dbCategory)
			{
				if($cleanOrigin != $dbCategory->superCategory)
					continue;
				if(in_array($dbCategory->category, $cleanOrigins))
					continue;
				array_push($cleanOrigins, $dbCategory->category);
				$changeMade = true;
			}
		}
	}
	
	for($i = 0; $i < count($cleanOrigins); $i++)
	{
		$cleanOrigins[$i] = "'".$cleanOrigins[$i]."'";
	}
	
	return $cleanOrigins;
}

function getConnection()
{
	$connection = mysqli_connect('localhost', 'withouth_website', '9m3B*Q353Hb', 'withouth_names');
	mysqli_set_charset($connection, "utf8"); //for correct saving of accented characters
	if ($connection->connect_error) 
	{
		throw new Exception("Database connection failed.");
	}
	return $connection;
}

?>