<?php

ini_set('display_errors', 'On');

include "scrapeUtilities.php";

//can only run this from command prompt
if(count($argv) <= 1)
{
	exit(0);
}

$sourceName = "Nameberry";
$sourceUrl = "https://nameberry.com";
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

$records = array();
$boyUrl = "https://nameberry.com/search/boys_names/";
$girlUrl = "https://nameberry.com/search/girls_names/";
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

		$simpleNames = scrapeSimpleNames($baseUrl.$a);
		foreach($simpleNames as $simpleName)
		{
			if(arrayContainsName($existingNames, $simpleName))
			{
				continue;
			}
			$record = scrapeDetailedName($simpleName);
			$record->display();
			if($updateDb)
			{
				$record->saveToDatabase($sourceId);
			}
		}
	}
}

//returns all simple names found at url
function scrapeSimpleNames($url)
{
	global $sleepBetweenWebCalls;

	sleep($sleepBetweenWebCalls);
	$content = file_get_contents($url);
	preg_match_all('/<a.*babyname\/(\w*)".*>/', $content, $matches);
	//array[0] has the full <a...> matches, array[1] has the sub (\w*) matches
	return $matches[1];
}

//returns Record of name with details
function scrapeDetailedName($name)
{
	global $sleepBetweenWebCalls;

	$url = "https://nameberry.com/babyname/".$name;

	sleep($sleepBetweenWebCalls);
	$content = file_get_contents($url);
	
	$record = new Record($name);

	//page exists for both genders
	if(strpos($content, '"/babyname/'.$name.'/girl"') !== false && strpos($content, '"/babyname/'.$name.'/boy"') !== false)
	{
		$record->{'isBoy'} = true;
		$record->{'isGirl'} = true;
		$content = file_get_contents($url."/boy");
	}
/*	
$file = fopen('temp.txt', 'w');
fwrite($file, $content);
fclose($file);
*/
	$matches = array();

	//gender
	preg_match_all('/<a class="(boy|girl)" href="\/(boys|girls)\-names">(.*)<\/a>/', $content, $matches);
	$match = $matches[3][0];
	if($match == "Male")
	{
		$record->{'isBoy'} = true;
	}
	if($match == "Female")
	{
		$record->{'isGirl'} = true;
	}
	
	//origin
	preg_match_all('/Origin of '.$name.': ?<\/span> ?(.*)<\/span>/', $content, $matches);
	if(count($matches) >= 2 && count($matches[1]) >= 1)
	{
		$origin = trim($matches[1][0]);
		$origin = flattenAnchors($origin);
		$record->{'origin'} = $origin;
	}
	
	//meaning
	preg_match_all('/Meaning of '.$name.': ?<\/span> ?"(.*)"<\/span>/', $content, $matches);
	if(count($matches) >= 2 && count($matches[1]) >= 1)
	{
		$meaning = trim($matches[1][0]);
		$meaning = flattenAnchors($meaning);
		$record->{'meaning'} = $meaning;
	}
	
	return $record;
}

//replace all anchors with just their inner text
function flattenAnchors($text)
{
	while(strpos($text, '<a') !== false && strpos($text, '</a>') !== false)
	{
		preg_match_all('/(.*)<a.*>(.*)<\/a>(.*)/', $text, $matches);
		$text = $matches[1][0].$matches[2][0].$matches[3][0];
	}
	return $text;
}

?>