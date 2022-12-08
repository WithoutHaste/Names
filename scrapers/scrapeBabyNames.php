<?php

ini_set('display_errors', 'On');

include "scrapeUtilities.php";

//can only run this from command prompt
if(count($argv) <= 1)
{
	exit(0);
}

$sourceName = "BabyNames";
$sourceUrl = "http://www.babynames.net";
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

$baseUrl = "http://www.babynames.net";
$categoriesUrl = "http://www.babynames.net/categories?page=1"; //finds drop down list of all ethnicities
scrapeAllOrigins($categoriesUrl);

echo "done\n\n";

function scrapeAllOrigins($url)
{
	global $baseUrl;
	global $sleepBetweenWebCalls;
	
	echo $url."\n";

	sleep($sleepBetweenWebCalls);
	$content = file_get_contents($url);

	$foundAnchors = array();
	preg_match_all('/<a href="(\/all\/.*)">(.*)<\/a>/sU', $content, $matchAnchors);
	for($i = 0; $i < count($matchAnchors[0]); $i++)
	{
		$anchor = $baseUrl.$matchAnchors[1][$i];
		$origin = $matchAnchors[2][$i];
		$origin = removeTags($origin, "span");
		$origin = trim($origin);
		if(strpos($anchor, "starts-with") !== false)
			continue;
		if(in_array($anchor, $foundAnchors))
			continue;
		array_push($foundAnchors, $anchor);
		scrapeOrigin($anchor, $origin);
	}
}

function scrapeOrigin($url, $origin)
{
	$pageNumber = 1;
	$foundAName = true;
	while($foundAName)
	{
		$foundAName = scrapeNames($url."?page=".$pageNumber, $origin);
		$pageNumber += 1;
	}
}

function scrapeNames($url, $origin)
{	
	global $updateDb;
	global $sourceId;
	global $sleepBetweenWebCalls;
	global $existingNames;

	$foundAName = false;
	
	echo $url."\n";
	
	sleep($sleepBetweenWebCalls);
	$content = file_get_contents($url);

	//http://www.babynames.net/all/chinese?page=2
	//getting corrupted data here, but it works online
$file = fopen('temp.txt', 'w');
fwrite($file, $content);
fclose($file);	
	
	preg_match_all('/<li.*>.*<a href="\/names\/.*">.*<span class="result-gender (.*)">.*<\/span>.*<span class="result-name">(.*)<\/span>.*<span class="result-desc">(.*)<\/span>.*<\/li>/sU', $content, $matches);
	for($i = 0; $i < count($matches[0]); $i++)
	{
		$gender = $matches[1][$i];
		$name = $matches[2][$i];
		$meaning = $matches[3][$i];
		$foundAName = true;
		
		if(arrayContainsNameOrigin($existingNames, $name, $origin))
		{
			continue;
		}
		
		$record = new Record($name);
		switch($gender)
		{
			case "boygirl":
				$record->{'isBoy'} = true;
				$record->{'isGirl'} = true;
				break;
			case "boy":
				$record->{'isBoy'} = true;
				break;
			case "girl":
				$record->{'isGirl'} = true;
				break;
		}
		$record->{'origin'} = $origin;
		$record->{'meaning'} = $meaning;
		
		$record->display();
		
		if($updateDb)
		{
			$record->saveToDatabase($sourceId);
		}
	}
	
	return $foundAName;
}

//remove matches html tags
//does not match across lines
function removeTags($text, $tag)
{
	while(strpos($text, '<'.$tag) !== false && strpos($text, '</'.$tag.'>') !== false)
	{
		preg_match_all('/(.*)<'.$tag.'.*?>(.*)<\/'.$tag.'>(.*)/', $text, $matches);
		$text = $matches[1][0].$matches[2][0].$matches[3][0];
	}
	while(strpos($text, '<'.$tag) !== false) //remove unmatches tags
	{
		preg_match_all('/(.*)<'.$tag.'.*?>(.*)/', $text, $matches);
		$text = $matches[1][0].$matches[2][0];
	}
	while(strpos($text, '</'.$tag.'>') !== false) //remove unmatches tags
	{
		preg_match_all('/(.*)<\/'.$tag.'>(.*)/', $text, $matches);
		$text = $matches[1][0].$matches[2][0];
	}
	return $text;
}


?>