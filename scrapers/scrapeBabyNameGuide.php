<?php

ini_set('display_errors', 'On');

include "scrapeUtilities.php";

//can only run this from command prompt
if(count($argv) <= 1)
{
	exit(0);
}

$sourceName = "BabyNameGuide";
$sourceUrl = "https://www.babynameguide.com";
$updateDb = false;
$sourceId = null;
$existingNames = array();
switch($argv[1])
{
	case "text": 
		$updateDb = false;
		break;
	case "db": 
		$updateDb = true; 
		$sourceId = saveGetSourceId($sourceName, $sourceUrl);
		if(count($argv) >= 3 && $argv[2] == "restart")
		{
			clearSourceData($sourceId);
		}
		$existingNames = loadNamesBySource($sourceId); //these names will not be looked up again
		break;
	default: 
		echo "Usage:\n";
		echo "script.php text: display results as text only\n";
		echo "script.php db: save results to database, also display as text\n";
		echo "script.php db restart: clear old source results from database before pulling new results\n";
		exit(0);
}

$boyUrl = "https://www.babynameguide.com/boy-names.asp";
$girlUrl = "https://www.babynameguide.com/girl-names.asp";
scrapeNames($boyUrl);
scrapeNames($girlUrl);

echo "done\n\n";

function scrapeNames($baseUrl)
{
	global $sourceId;
	global $updateDb;
	global $existingNames;
	
	for($a = 'A'; $a <= 'Z'; $a++)
	{
		if(strlen($a) > 1) //for some dumbass reason, the loop goes X,Y,Z,AA,AB,AC...and finally breaks at ZZ
			break;

		$records = scrapeNamesByLetter($baseUrl.'?strAlpha='.$a);
		foreach($records as $record)
		{
			if(arrayContainsNameOrigin($existingNames, $record->{'name'}, $record->{'origin'}))
			{
				continue;
			}
			
			$record->display();
			
			if($updateDb)
			{
				$record->saveToDatabase($sourceId);
			}
		}
	}
}

//returns all records found at url
function scrapeNamesByLetter($url)
{
	global $sleepBetweenWebCalls;

	sleep($sleepBetweenWebCalls);
	$content = file_get_contents($url);

	$records = array();
	
	//table rows
	preg_match_all('/<tr>(.*)<\/tr>/sU', $content, $matchesRows);
	foreach($matchesRows[1] as $tableRow)
	{
		preg_match_all('/<th.*<\/th>/sU', $tableRow, $matchesCells);
		if(strpos($matchesCells[0][0], 'NameColumnsName') !== false)
		{
			$record = new Record(getCellContents($matchesCells[0][0]));
			if(strpos($matchesCells[0][1], '>Boy<') !== false)
			{
				$record->{'isBoy'} = true;
			}		
			elseif(strpos($matchesCells[0][1], '>Girl<') !== false)
			{
				$record->{'isGirl'} = true;
			}		
			$record->{'meaning'} = getCellContents($matchesCells[0][2]);
			$record->{'origin'} = getCellContents($matchesCells[0][3]);
			array_push($records, $record);
		}
	}
	
	return $records;
}

//return inner contents of table cell or header cell
function getCellContents($cell)
{
	preg_match_all('/<(th|td).*>(.*)<\/(th|td)>/sU', $cell, $matches);
	return trim($matches[2][0]);
}

?>