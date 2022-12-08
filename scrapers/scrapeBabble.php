<?php

ini_set('display_errors', 'On');

include "libAlphabet.php";
include "scrapeUtilities.php";

//can only run this from command prompt
if(count($argv) <= 1)
{
	exit(0);
}

$sourceName = "Babble";
$sourceUrl = "https://www.babble.com";
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

$boyNamesUrl = "https://www.babble.com/baby-names/baby-boy-names/";
$girlNamesUrl = "https://www.babble.com/baby-names/baby-girl-names/";
scrapeAllNames($boyNamesUrl, true, false);
scrapeAllNames($girlNamesUrl, false, true);

echo "done\n\n";

//get first page (different format) and all later pages
function scrapeAllNames($baseUrl, $isBoy, $isGirl)
{
	scrapePage($baseUrl, $isBoy, $isGirl);
	
	$pageNumber = 2;
	$foundAName = true;
	while($foundAName)
	{
		$foundAName = scrapePage($baseUrl."page/".$pageNumber, $isBoy, $isGirl);
		$pageNumber += 1;
	}
}

function scrapePage($url, $isBoy, $isGirl)
{
	global $sleepBetweenWebCalls;
	
	echo $url."\n";
	
	sleep($sleepBetweenWebCalls);
	$content = file_get_contents($url);

$file = fopen('temp.txt', 'w');
fwrite($file, $content);
fclose($file);	

	$foundAName = false;

	preg_match_all('/<div class="bnl-baby-name" id="(.*)">(.*)<\/div>/sU', $content, $matchDivs);
	for($i = 0; $i < count($matchDivs[0]); $i++)
	{
		$name = $matchDivs[1][$i];
		$name = formatName($name);
		$foundAName = true;
		
		preg_match_all('/<p>(.*)<\/p>/sU', $matchDivs[2][$i], $matchParagraphs);
		$meaning = null;
		$origin = null;
		foreach($matchParagraphs[1] as $paragraph)
		{
			if(strpos($paragraph, "<strong>") !== false)
			{				
				//has origin and maybe meaning
				preg_match_all('/<strong>(.*)<\/strong>(.*)/s', $paragraph, $matchFields);

				$origin = $matchFields[1][0];
				$origin = str_replace(":", "", $origin);
				$origin = str_replace("Origin", "", $origin);
				$origin = trim($origin);
				
				$meaning = $matchFields[2][0];
				$meaning = toPlainText($meaning);
				$meaning = str_replace("''", "'", $meaning);
				$meaning = deleteTags($meaning, "a");
				$meaning = trim($meaning);
				
				save($name, $isBoy, $isGirl, $origin, $meaning);
			}
			else if($meaning == null && $origin == null)
			{
				$meaning = $paragraph;
				$meaning = toPlainText($meaning);
				$meaning = str_replace("''", "'", $meaning);
				$meaning = deleteTags($meaning, "a");
				$meaning = trim($meaning);
				
				save($name, $isBoy, $isGirl, $origin, $meaning);
			}
		}
	}
	return $foundAName;
}

function save($name, $isBoy, $isGirl, $origin, $meaning)
{
	global $existingNames;
	global $updateDb;
	global $sourceId;

	if($origin == null)
	{
		if(arrayContainsName($existingNames, $name))
		{
			return;
		}
	}
	else
	{
		if(arrayContainsNameOrigin($existingNames, $name, $origin))
		{
			return;
		}
	}
	
	$record = new Record($name);
	if($isBoy)
	{
		$record->{'isBoy'} = true;
	}
	if($isGirl)
	{
		$record->{'isGirl'} = true;
	}
	$record->{'origin'} = $origin;
	$record->{'meaning'} = $meaning;
	
	$record->display();
	
	if($updateDb)
	{
		$record->saveToDatabase($sourceId);
	}
}

//convert all html codes to unicode
function decodeHTML($text)
{
	return html_entity_decode($text);
}

//format name - first letter capital, the rest lower case
function formatName($name)
{
	$name = trim($name);
	if($name[0] == "'")
		return strtoupper($name[0].$name[1]).customToLowerCase(substr($name, 2));
	return strtoupper($name[0]).customToLowerCase(substr($name, 1));
}

function toPlainText($text)
{
	$text = preg_replace('/[\s\n]+/', " ", $text);
	$text = decodeHTML($text);
	$text = removeTags($text, "p");
	$text = trim($text);
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

//remove matches html tags with their contents
//does not match across lines
function deleteTags($text, $tag)
{
	while(strpos($text, '<'.$tag) !== false && strpos($text, '</'.$tag.'>') !== false)
	{
		preg_match_all('/(.*)<'.$tag.'.*?>(.*)<\/'.$tag.'>(.*)/', $text, $matches);
		$text = $matches[1][0].$matches[3][0];
	}
	return $text;
}

?>