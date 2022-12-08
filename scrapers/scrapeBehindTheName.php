<?php

ini_set('display_errors', 'On');

include "libAlphabet.php";
include "scrapeUtilities.php";

//can only run this from command prompt
if(count($argv) <= 1)
{
	exit(0);
}

$sourceName = "BehindTheName";
$sourceUrl = "https://www.behindthename.com";
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

$baseUrl = "https://www.behindthename.com";
$byLetterUrl = "https://www.behindthename.com/names/letter/";
scrapePages($byLetterUrl);

echo "done\n\n";

function scrapePages($url)
{
	for($a = 'a'; $a <= 'z'; $a++)
	{
		if(strlen($a) > 1) //for some dumbass reason, the loop goes X,Y,Z,AA,AB,AC...and finally breaks at ZZ
			break;
		
		$letterUrl = $url.$a."/";
		
		$pageNumber = 1;
		$foundAName = true;
		while($foundAName)
		{
			$numberLetterUrl = $letterUrl.$pageNumber;
			$foundAName = scrapePage($numberLetterUrl);
			$pageNumber += 1;
		}
	}
}

function scrapePage($url)
{
	global $updateDb;
	global $sourceId;
	global $sleepBetweenWebCalls;
	global $existingNames;

	$foundAName = false;
	
	echo $url."\n";
	
	sleep($sleepBetweenWebCalls);
	$content = file_get_contents($url);

$file = fopen('temp.txt', 'w');
fwrite($file, $content);
fclose($file);	
	
	preg_match_all('/<div class="browsename">(.*)<\/div>/sU', $content, $matchDivs);
	foreach($matchDivs[1] as $div)
	{
		preg_match_all('/<span class="listname">(.*?)<\/span>.*<span class="listgender">(.*?)<\/span>.*<span class="listusage">(.*?)<\/span>(.*)/s', $div, $matchFields);
		for($i = 0; $i < count($matchFields[0]); $i++)
		{
			$name = $matchFields[1][$i];
			$gender = $matchFields[2][$i];
			$origin = $matchFields[3][$i];
			$meaning = $matchFields[4][$i];
			
			$name = removeTags($name, 'a');
			$name = removeTags($name, 'span');
			$name = removeParentheses($name);
			$name = decodeHTML($name);
			$name = formatName($name);
			
			$gender = removeTags($gender, 'span');
			$gender = trim($gender);
			
			$origin = removeTags($origin, 'a');
			$origin = trim($origin);

			$meaning = decodeHTML($meaning);
			$meaning = removeTags($meaning, 'span');
			$meaning = removeTags($meaning, 'br');
			$meaning = removeTags($meaning, 'a');
			$meaning = removeTags($meaning, 'i');
			$meaning = trim($meaning);
			
			$foundAName = true;

			if(arrayContainsNameOrigin($existingNames, $name, $origin))
			{
				continue;
			}
			
			$record = new Record($name);
			switch($gender)
			{
				case "m & f":
					$record->{'isBoy'} = true;
					$record->{'isGirl'} = true;
					break;
				case "m":
					$record->{'isBoy'} = true;
					break;
				case "f":
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
	}
	
	return $foundAName;
}

//format name - first letter capital, the rest lower case
function formatName($name)
{
	$name = trim($name);
	if($name[0] == "'")
		return strtoupper($name[0].$name[1]).customToLowerCase(substr($name, 2));
	return strtoupper($name[0]).customToLowerCase(substr($name, 1));
}

//convert all html codes to unicode
function decodeHTML($text)
{
	return html_entity_decode($text);
}

//remove parenthetical clauses
function removeParentheses($text)
{
	while(strpos($text, '(') !== false && strpos($text, ')') !== false)
	{
		preg_match_all('/(.*)\((.*?)\)(.*)/', $text, $matches);
		$text = $matches[1][0].$matches[3][0];
	}
	while(strpos($text, '(') !== false)
	{
		preg_match_all('/(.*)\(.*/', $text, $matches);
		$text = $matches[1][0];
	}
	return $text;
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