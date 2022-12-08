<?php

ini_set('display_errors', 'On');

include "scrapeUtilities.php";

//can only run this from command prompt
if(count($argv) <= 1)
{
	exit(0);
}

$sourceName = "FamilyEducation";
$sourceUrl = "https://www.familyeducation.com";
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

$originUrl = "https://www.familyeducation.com/baby-names/browse-origin/first-name";
$isFirstName = true;
scrapeOrigins($sourceUrl, $originUrl, $isFirstName);

echo "done\n\n";

//get list of origins
function scrapeOrigins($baseUrl, $url, $isFirstName)
{
	$content = file_get_contents($url);
	preg_match_all('/<section.*babynamebybrowseoriginblock.*<\/section>/sU', $content, $matches);
	$section = $matches[0][0];
	preg_match_all('/<a href="(.*)">(.*)<\/a>/sU', $section, $matches);
	for($i = 0; $i < count($matches[0]); $i++)
	{
		$localPath = $matches[1][$i];
		if(strpos($localPath, 'browse-origin/first-name') !== false)
		{
			$origin = $matches[2][$i];
			scrapeOrigin($baseUrl, $baseUrl.$localPath, $origin, $isFirstName);
		}
	}
}

//get names for selected origin
function scrapeOrigin($baseUrl, $url, $origin, $isFirstName)
{
	$pageNumber = 1;
	$pagesAreNumbered = false;
	$foundAName = true;
	while($foundAName)
	{
		$foundAName = scrapeOriginOnePage($baseUrl, $url."?page=".$pageNumber, $origin, $isFirstName);
		if($foundAName)
		{
			$pagesAreNumbered = true;
		}
		$pageNumber += 1;
	}
	
	if(!$pagesAreNumbered)
	{
		scrapeOriginOnePage($baseUrl, $url, $origin, $isFirstName);
	}
}

//get names for selected origin
//returns true if any names were found
function scrapeOriginOnePage($baseUrl, $url, $origin, $isFirstName)
{
	global $existingNames;
	global $sleepBetweenWebCalls;
	
	$foundAName = false;
	
	sleep($sleepBetweenWebCalls);
	$content = file_get_contents($url);
	preg_match_all('/<a href="(\/baby-names\/name-meaning.*)">(.*)<\/a>/sU', $content, $matches);
	for($i = 0; $i < count($matches[0]); $i++)
	{
		$foundAName = true;
		$href = $matches[1][$i];
		$name = $matches[2][$i];
		
		if(arrayContainsNameOrigin($existingNames, $name, $origin))
		{
			continue;
		}
		
		scrapeName($baseUrl.$href, $name, $origin, $isFirstName);
	}
	
	return $foundAName;
}

function scrapeName($url, $name, $origin, $isFirstName)
{
	global $sourceId;
	global $updateDb;
	global $sleepBetweenWebCalls;

	$record = new Record($name);
	$record->{'origin'} = $origin;
	if($isFirstName)
	{
		$record->{'isFirstName'} = true;
	}
	else
	{
		$record->{'isLastName'} = true;
	}

	sleep($sleepBetweenWebCalls);
	$content = file_get_contents($url);
	if(strpos($content, 'Boy name origins') !== false)
	{
		$record->{'isBoy'} = true;
	}
	elseif(strpos($content, 'Girl name origins') !== false)
	{
		$record->{'isGirl'} = true;
	}

	preg_match_all('/<li>.* : (.*)<\/li>/', $content, $matchesMeanings);
	if(count($matchesMeanings[0]) > 0)
	{
		$record->{'meaning'} = $matchesMeanings[1][0];
	}
	
	$record->display();
	
	if($updateDb)
	{
		$record->saveToDatabase($sourceId);
	}
}

?>