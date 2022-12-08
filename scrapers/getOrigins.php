<?php

$results = array();

$connection = getConnection();
$sql = mysqli_prepare($connection, 'SELECT Category, SuperCategory FROM Category');
if(!$sql)
{
	throw new Exception("Could not prepare query statement.");
}
$sql->execute();
$sql->store_result();
$sql->bind_result($origin, $superOrigin);
while($sql->fetch())
{
	$record = new stdClass(); //default empty object
	$record->origin = $origin;
	$record->superOrigin = $superOrigin;
	array_push($results, $record);
}

$sql->close();
mysqli_close($connection);

echo json_encode($results);

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