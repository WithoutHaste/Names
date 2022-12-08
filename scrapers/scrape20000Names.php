<?php

ini_set('display_errors', 'On');

include "libAlphabet.php";
include "scrapeUtilities.php";

//can only run this from command prompt
if(count($argv) <= 1)
{
	exit(0);
}

$sourceName = "20000Names";
$sourceUrl = "http://www.20000-names.com";
$updateDb = false;
$sourceId = null;
$existingNames = array();
switch($argv[1])
{
	case "text": 
		$updateDb = false; 
		$sourceId = saveGetSourceId($sourceName, $sourceUrl);
		if($sourceId != null)
		{
			$existingNames = loadNamesBySource($sourceId); //these names will not be looked up again
		}
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

$baseUrl = "http://www.20000-names.com/";
$allOriginsUrl = "http://www.20000-names.com/index.htm";

/*
$africanOrigin = "African";
$africanMaleUrl = "http://www.20000-names.com/male_african_names.htm";
$africanFemaleUrl = "http://www.20000-names.com/female_african_names.htm";
scrapeOrigin($africanOrigin, $africanMaleUrl, $africanFemaleUrl);

$akkadianOrigin = "Akkadian, Chaldean, Semitic Babylonian";
$akkadianMaleUrl = "http://www.20000-names.com/male_akkadian_names.htm";
$akkadianFemaleUrl = "http://www.20000-names.com/female_akkadian_names.htm";
scrapeOrigin($akkadianOrigin, $akkadianMaleUrl, $akkadianFemaleUrl);

$albanianOrigin = "Albanian";
$albanianMaleUrl = "http://www.20000-names.com/male_albanian_names.htm";
$albanianFemaleUrl = "http://www.20000-names.com/female_albanian_names.htm";
scrapeOrigin($albanianOrigin, $albanianMaleUrl, $albanianFemaleUrl);

$angloOrigin = "Anglo-Saxon";
$angloMaleUrl = "http://www.20000-names.com/male_anglo_saxon_names.htm";
$angloFemaleUrl = "http://www.20000-names.com/female_anglo_saxon_names.htm";
scrapeOrigin($angloOrigin, $angloMaleUrl, $angloFemaleUrl);

$arabianOrigin = "Arabian, Arabic";
$arabianMaleUrl = "http://www.20000-names.com/male_arabian_names.htm";
$arabianFemaleUrl = "http://www.20000-names.com/female_arabian_names.htm";
scrapeOrigin($arabianOrigin, $arabianMaleUrl, $arabianFemaleUrl);

$aramaicOrigin = "Aramaic";
$aramaicMaleUrl = "http://www.20000-names.com/male_aramaic_names.htm";
$aramaicFemaleUrl = "http://www.20000-names.com/female_aramaic_names.htm";
scrapeOrigin($aramaicOrigin, $aramaicMaleUrl, $aramaicFemaleUrl);

$armenianOrigin = "Armenian";
$armenianMaleUrl = "http://www.20000-names.com/male_armenian_names.htm";
$armenianFemaleUrl = "http://www.20000-names.com/female_armenian_names.htm";
scrapeOrigin($armenianOrigin, $armenianMaleUrl, $armenianFemaleUrl);

$arthurianOrigin = "Arthurian Legend";
$arthurianMaleUrl = "http://www.20000-names.com/male_arthurian_legend_names.htm";
$arthurianFemaleUrl = "http://www.20000-names.com/female_arthurian_legend_names.htm";
scrapeOrigin($arthurianOrigin, $arthurianMaleUrl, $arthurianFemaleUrl);

$assyrianOrigin = "Assyrian";
$assyrianMaleUrl = "http://www.20000-names.com/male_assyrian_names.htm";
$assyrianFemaleUrl = "http://www.20000-names.com/female_assyrian_names.htm";
scrapeOrigin($assyrianOrigin, $assyrianMaleUrl, $assyrianFemaleUrl);

$aztecOrigin = "Aztec, Nahuatl";
$aztecMaleUrl = "http://www.20000-names.com/male_nahuatl_names.htm";
$aztecFemaleUrl = "http://www.20000-names.com/female_nahuatl_names.htm";
scrapeOrigin($aztecOrigin, $aztecMaleUrl, $aztecFemaleUrl);

$babylonianOrigin = "Babylonian";
$babylonianMaleUrl =     "http://www.20000-names.com/male_babylonian_names.htm";
$babylonianFemaleUrl = "http://www.20000-names.com/female_babylonian_names.htm";
scrapeOrigin($babylonianOrigin, $babylonianMaleUrl, $babylonianFemaleUrl);

$basqueOrigin = "Basque";
$basqueMaleUrl =     "http://www.20000-names.com/male_basque_names.htm";
$basqueFemaleUrl = "http://www.20000-names.com/female_basque_names.htm";
scrapeOrigin($basqueOrigin, $basqueMaleUrl, $basqueFemaleUrl);

$bulgarianOrigin = "Bulgarian";
$bulgarianMaleUrl =     "http://www.20000-names.com/male_bulgarian_names.htm";
$bulgarianFemaleUrl = "http://www.20000-names.com/female_bulgarian_names.htm";
scrapeOrigin($bulgarianOrigin, $bulgarianMaleUrl, $bulgarianFemaleUrl);

$celticOrigin = "Celtic";
$celticMaleUrl =     "http://www.20000-names.com/male_celtic_names.htm";
$celticFemaleUrl = "http://www.20000-names.com/female_celtic_names.htm";
scrapeOrigin($celticOrigin, $celticMaleUrl, $celticFemaleUrl);

$chamoruOrigin = "Chamoru";
$chamoruMaleUrl =     "http://www.20000-names.com/male_chamoru_names.htm";
$chamoruFemaleUrl = "http://www.20000-names.com/female_chamoru_names.htm";
scrapeOrigin($chamoruOrigin, $chamoruMaleUrl, $chamoruFemaleUrl);

$chineseOrigin = "Chinese";
$chineseMaleUrl =     "http://www.20000-names.com/male_chinese_names.htm";
$chineseFemaleUrl = "http://www.20000-names.com/female_chinese_names.htm";
scrapeOrigin($chineseOrigin, $chineseMaleUrl, $chineseFemaleUrl);

$cornishOrigin = "Cornish";
$cornishMaleUrl =     "http://www.20000-names.com/male_cornish_names.htm";
$cornishFemaleUrl = "http://www.20000-names.com/female_cornish_names.htm";
scrapeOrigin($cornishOrigin, $cornishMaleUrl, $cornishFemaleUrl);

$croatianOrigin = "Croatian";
$croatianMaleUrl =     "http://www.20000-names.com/male_croatian_names.htm";
$croatianFemaleUrl = "http://www.20000-names.com/female_croatian_names.htm";
scrapeOrigin($croatianOrigin, $croatianMaleUrl, $croatianFemaleUrl);

$czechOrigin = "Czech, Slovak, Bohemian";
$czechMaleUrl =     "http://www.20000-names.com/male_czechoslovakian_names.htm";
$czechFemaleUrl = "http://www.20000-names.com/female_czechoslovakian_names.htm";
scrapeOrigin($czechOrigin, $czechMaleUrl, $czechFemaleUrl);

$danishOrigin = "Danish";
$danishMaleUrl =     "http://www.20000-names.com/male_danish_names.htm";
$danishFemaleUrl = "http://www.20000-names.com/female_danish_names.htm";
scrapeOrigin($danishOrigin, $danishMaleUrl, $danishFemaleUrl);

$dutchOrigin = "Dutch";
$dutchMaleUrl =     "http://www.20000-names.com/male_dutch_names.htm";
$dutchFemaleUrl = "http://www.20000-names.com/female_dutch_names.htm";
scrapeOrigin($dutchOrigin, $dutchMaleUrl, $dutchFemaleUrl);

$egyptianOrigin = "Egyptian";
$egyptianMaleUrl =     "http://www.20000-names.com/male_egyptian_names.htm";
$egyptianFemaleUrl = "http://www.20000-names.com/female_egyptian_names.htm";
scrapeOrigin($egyptianOrigin, $egyptianMaleUrl, $egyptianFemaleUrl);

$englishOrigin = "English";
$englishMaleUrl =     "http://www.20000-names.com/male_english_names.htm";
$englishFemaleUrl = "http://www.20000-names.com/female_english_names.htm";
scrapeOrigin($englishOrigin, $englishMaleUrl, $englishFemaleUrl);

$esperantoOrigin = "Esperanto";
$esperantoMaleUrl =     "http://www.20000-names.com/male_esperanto_names.htm";
$esperantoFemaleUrl = "http://www.20000-names.com/female_esperanto_names.htm";
scrapeOrigin($esperantoOrigin, $esperantoMaleUrl, $esperantoFemaleUrl);

$finnishOrigin = "Finnish";
$finnishMaleUrl =     "http://www.20000-names.com/male_finnish_names.htm";
$finnishFemaleUrl = "http://www.20000-names.com/female_finnish_names.htm";
scrapeOrigin($finnishOrigin, $finnishMaleUrl, $finnishFemaleUrl);

$frenchOrigin = "French";
$frenchMaleUrl =     "http://www.20000-names.com/male_french_names.htm";
$frenchFemaleUrl = "http://www.20000-names.com/female_french_names.htm";
scrapeOrigin($frenchOrigin, $frenchMaleUrl, $frenchFemaleUrl);

$gaelicOrigin = "Gaelic";
$gaelicMaleUrl =     "http://www.20000-names.com/male_gaelic_names.htm";
$gaelicFemaleUrl = "http://www.20000-names.com/female_gaelic_names.htm";
scrapeOrigin($gaelicOrigin, $gaelicMaleUrl, $gaelicFemaleUrl);

$germanOrigin = "German";
$germanMaleUrl =     "http://www.20000-names.com/male_german_names.htm";
$germanFemaleUrl = "http://www.20000-names.com/female_german_names.htm";
scrapeOrigin($germanOrigin, $germanMaleUrl, $germanFemaleUrl);

$gothicOrigin = "Gothic";
$gothicMaleUrl =     "http://www.20000-names.com/male_gothic_names_visigothic_ostrogothic.htm";
$gothicFemaleUrl = "http://www.20000-names.com/female_gothic_names_visigothic_ostrogothic.htm";
scrapeOrigin($gothicOrigin, $gothicMaleUrl, $gothicFemaleUrl);

$greekOrigin = "Greek";
$greekMaleUrl =     "http://www.20000-names.com/male_greek_names.htm";
$greekFemaleUrl = "http://www.20000-names.com/female_greek_names.htm";
scrapeOrigin($greekOrigin, $greekMaleUrl, $greekFemaleUrl);

$hawaiianOrigin = "Hawaiian";
$hawaiianMaleUrl =     "http://www.20000-names.com/male_hawaiian_names.htm";
$hawaiianFemaleUrl = "http://www.20000-names.com/female_hawaiian_names.htm";
scrapeOrigin($hawaiianOrigin, $hawaiianMaleUrl, $hawaiianFemaleUrl);

$hebrewOrigin = "Hebrew";
$hebrewMaleUrl =     "http://www.20000-names.com/male_hebrew_names.htm";
$hebrewFemaleUrl = "http://www.20000-names.com/female_hebrew_names.htm";
scrapeOrigin($hebrewOrigin, $hebrewMaleUrl, $hebrewFemaleUrl);

$hindiOrigin = "Hindi, Indian";
$hindiMaleUrl =     "http://www.20000-names.com/male_hindi_names.htm";
$hindiFemaleUrl = "http://www.20000-names.com/female_hindi_names.htm";
scrapeOrigin($hindiOrigin, $hindiMaleUrl, $hindiFemaleUrl);

$hungarianOrigin = "Hungarian";
$hungarianMaleUrl =     "http://www.20000-names.com/male_hungarian_names.htm";
$hungarianFemaleUrl = "http://www.20000-names.com/female_hungarian_names.htm";
scrapeOrigin($hungarianOrigin, $hungarianMaleUrl, $hungarianFemaleUrl);

$icelandicOrigin = "Icelandic";
$icelandicMaleUrl =     "http://www.20000-names.com/male_icelandic_names.htm";
$icelandicFemaleUrl = "http://www.20000-names.com/female_icelandic_names.htm";
scrapeOrigin($icelandicOrigin, $icelandicMaleUrl, $icelandicFemaleUrl);

$iranianOrigin = "Persian, Iranian";
$iranianMaleUrl =     "http://www.20000-names.com/male_persian_names.htm";
$iranianFemaleUrl = "http://www.20000-names.com/female_persian_names.htm";
scrapeOrigin($iranianOrigin, $iranianMaleUrl, $iranianFemaleUrl);

$irishOrigin = "Irish";
$irishMaleUrl =     "http://www.20000-names.com/male_irish_names.htm";
$irishFemaleUrl = "http://www.20000-names.com/female_irish_names.htm";
scrapeOrigin($irishOrigin, $irishMaleUrl, $irishFemaleUrl);

$italianOrigin = "Italian";
$italianMaleUrl =     "http://www.20000-names.com/male_italian_names.htm";
$italianFemaleUrl = "http://www.20000-names.com/female_italian_names.htm";
scrapeOrigin($italianOrigin, $italianMaleUrl, $italianFemaleUrl);

$japaneseOrigin = "Japanese";
$japaneseMaleUrl =     "http://www.20000-names.com/male_japanese_names.htm";
$japaneseFemaleUrl = "http://www.20000-names.com/female_japanese_names.htm";
scrapeOrigin($japaneseOrigin, $japaneseMaleUrl, $japaneseFemaleUrl);

$latinOrigin = "Latin, Roman";
$latinMaleUrl =     "http://www.20000-names.com/male_latin_names.htm";
$latinFemaleUrl = "http://www.20000-names.com/female_latin_names.htm";
scrapeOrigin($latinOrigin, $latinMaleUrl, $latinFemaleUrl);

$nativeAmericanOrigin = "Native American";
$nativeAmericanMaleUrl =     "http://www.20000-names.com/male_native_american_names.htm";
$nativeAmericanFemaleUrl = "http://www.20000-names.com/female_native_american_names.htm";
scrapeOrigin($nativeAmericanOrigin, $nativeAmericanMaleUrl, $nativeAmericanFemaleUrl);

$norseOrigin = "Norse";
$norseMaleUrl =     "http://www.20000-names.com/male_norse_names.htm";
$norseFemaleUrl = "http://www.20000-names.com/female_norse_names.htm";
scrapeOrigin($norseOrigin, $norseMaleUrl, $norseFemaleUrl);

$norwegianOrigin = "Norwegian";
$norwegianMaleUrl =     "http://www.20000-names.com/male_norwegian_names.htm";
$norwegianFemaleUrl = "http://www.20000-names.com/female_norwegian_names.htm";
scrapeOrigin($norwegianOrigin, $norwegianMaleUrl, $norwegianFemaleUrl);

$polishOrigin = "Polish";
$polishMaleUrl =     "http://www.20000-names.com/male_polish_names.htm";
$polishFemaleUrl = "http://www.20000-names.com/female_polish_names.htm";
scrapeOrigin($polishOrigin, $polishMaleUrl, $polishFemaleUrl);

$portugueseOrigin = "Portuguese";
$portugueseMaleUrl =     "http://www.20000-names.com/male_portuguese_names.htm";
$portugueseFemaleUrl = "http://www.20000-names.com/female_portuguese_names.htm";
scrapeOrigin($portugueseOrigin, $portugueseMaleUrl, $portugueseFemaleUrl);

$romanianOrigin = "Romanian";
$romanianMaleUrl =     "http://www.20000-names.com/male_romanian_names.htm";
$romanianFemaleUrl = "http://www.20000-names.com/female_romanian_names.htm";
scrapeOrigin($romanianOrigin, $romanianMaleUrl, $romanianFemaleUrl);

$russianOrigin = "Russian";
$russianMaleUrl =     "http://www.20000-names.com/male_russian_names.htm";
$russianFemaleUrl = "http://www.20000-names.com/female_russian_names.htm";
scrapeOrigin($russianOrigin, $russianMaleUrl, $russianFemaleUrl);

$scandinavianOrigin = "Scandinavian";
$scandinavianMaleUrl =     "http://www.20000-names.com/male_scandinavian_names.htm";
$scandinavianFemaleUrl = "http://www.20000-names.com/female_scandinavian_names.htm";
scrapeOrigin($scandinavianOrigin, $scandinavianMaleUrl, $scandinavianFemaleUrl);

$scottishOrigin = "Scottish";
$scottishMaleUrl =     "http://www.20000-names.com/male_scottish_names.htm";
$scottishFemaleUrl = "http://www.20000-names.com/female_scottish_names.htm";
scrapeOrigin($scottishOrigin, $scottishMaleUrl, $scottishFemaleUrl);

$serbianOrigin = "Serbian";
$serbianMaleUrl =     "http://www.20000-names.com/male_serbian_names.htm";
$serbianFemaleUrl = "http://www.20000-names.com/female_serbian_names.htm";
scrapeOrigin($serbianOrigin, $serbianMaleUrl, $serbianFemaleUrl);

$slavicOrigin = "Slavic";
$slavicMaleUrl =     "http://www.20000-names.com/male_slavic_names.htm";
$slavicFemaleUrl = "http://www.20000-names.com/female_slavic_names.htm";
scrapeOrigin($slavicOrigin, $slavicMaleUrl, $slavicFemaleUrl);

$spanishOrigin = "Spanish";
$spanishMaleUrl =     "http://www.20000-names.com/male_spanish_names.htm";
$spanishFemaleUrl = "http://www.20000-names.com/female_spanish_names.htm";
scrapeOrigin($spanishOrigin, $spanishMaleUrl, $spanishFemaleUrl);

$swedishOrigin = "Swedish";
$swedishMaleUrl =     "http://www.20000-names.com/male_swedish_names.htm";
$swedishFemaleUrl = "http://www.20000-names.com/female_swedish_names.htm";
scrapeOrigin($swedishOrigin, $swedishMaleUrl, $swedishFemaleUrl);

$swissOrigin = "Swiss";
$swissMaleUrl =     "http://www.20000-names.com/male_swiss_names.htm";
$swissFemaleUrl = "http://www.20000-names.com/female_swiss_names.htm";
scrapeOrigin($swissOrigin, $swissMaleUrl, $swissFemaleUrl);

$welshOrigin = "Welsh";
$welshMaleUrl =     "http://www.20000-names.com/male_welsh_names.htm";
$welshFemaleUrl = "http://www.20000-names.com/female_welsh_names.htm";
scrapeOrigin($welshOrigin, $welshMaleUrl, $welshFemaleUrl);
*/

$yiddishOrigin = "Yiddish";
$yiddishMaleUrl =     "http://www.20000-names.com/male_yiddish_names.htm";
$yiddishFemaleUrl = "http://www.20000-names.com/female_yiddish_names.htm";
scrapeOrigin($yiddishOrigin, $yiddishMaleUrl, $yiddishFemaleUrl);

echo "done\n\n";

//scrape all pages related to this origin
function scrapeOrigin($origin, $maleUrl, $femaleUrl)
{
	global $arabianOrigin;
	global $armenianOrigin;
	global $assyrianOrigin;
	global $babylonianOrigin;
	global $basqueOrigin;
	global $czechOrigin;
	
	$allowSingleDigit = ($origin == $arabianOrigin || $origin == $armenianOrigin || $origin == $assyrianOrigin || $origin == $babylonianOrigin || $origin == $czechOrigin);
	
	scrapeNamePage($maleUrl, $origin, true, false);
	for($i = 2; $i < 100; $i++)
	{
		$num = (($i < 10 && !$allowSingleDigit) ? "0".$i : $i);
		$nextUrl = str_replace(".htm", "_".$num.".htm", $maleUrl);
		$success = scrapeNamePage($nextUrl, $origin, true, false);
		if(!$success)
			break;
	}
	
	scrapeNamePage($femaleUrl, $origin, false, true);
	if($origin == $basqueOrigin)
	{
		return;
	}
	for($i = 2; $i < 100; $i++)
	{
		$num = (($i < 10 && !$allowSingleDigit) ? "0".$i : $i);
		$nextUrl = str_replace(".htm", "_".$num.".htm", $femaleUrl);
		$success = scrapeNamePage($nextUrl, $origin, false, true);
		if(!$success)
			break;
	}
}

//scrape one page related to an origin
//return true if a page of names was found, false if not
function scrapeNamePage($url, $origin, $isBoy, $isGirl)
{
	global $sleepBetweenWebCalls;
	/*
	global $africanOrigin;
	global $akkadianOrigin;
	global $albanianOrigin;
	global $angloOrigin;
	global $arabianOrigin;
	global $aramaicOrigin;
	global $armenianOrigin;
	global $arthurianOrigin;
	global $assyrianOrigin;
	global $aztecOrigin;
	global $babylonianOrigin;
	global $basqueOrigin;
	global $bulgarianOrigin;
	global $celticOrigin;
	global $chamoruOrigin;
	global $chineseOrigin;
	global $cornishOrigin;
	global $croatianOrigin;
	global $czechOrigin;
	*/
	
	sleep($sleepBetweenWebCalls);	
	$content = file_get_contents($url);
	if(!$content)
	{
		//$error = error_get_last();
		//echo "Error: ".$error['message']."\n";
		return false;
	}
	if(strpos($content, "Sorry our mistake, not what your looking for?") !== false)
	{
		return false; //page does not exist
	}
	echo "Page: ".$url."\n";
	
	parsePage_A($content, $origin, $isBoy, $isGirl); 
	/*
	switch($origin)
	{
		case $africanOrigin: 
		case $akkadianOrigin:
		case $albanianOrigin:
		case $angloOrigin:
		case $arabianOrigin:
		case $aramaicOrigin:
		case $armenianOrigin:
		case $arthurianOrigin:
		case $assyrianOrigin:
		case $aztecOrigin:
		case $babylonianOrigin:
		case $basqueOrigin:
		case $bulgarianOrigin:
		case $celticOrigin:
		case $chamoruOrigin:
		case $chineseOrigin:
		case $cornishOrigin:
		case $croatianOrigin:
		case $czechOrigin:
			parsePage_A($content, $origin, $isBoy, $isGirl); 
			break;
		default:
			echo "Unhandled origin: ".$origin."\n";
			exit(0);
	}
	*/
	
	return true;
}

//parse page with pattern A
function parsePage_A($content, $origin, $isBoy, $isGirl)
{	
	global $updateDb;
	global $sourceId;
	global $existingNames;

/*	
$file = fopen('temp.txt', 'w');
fwrite($file, $content);
fclose($file);
*/

	preg_match_all('/<ol.*?>(.*)<\/ol>/s', $content, $matchesList); //biggest lists on the page
	foreach($matchesList[1] as $list)
	{
		preg_match_all('/<li.*?>(.*?)<\/li>/s', $list, $matchesLine); //smallest lines in list
		foreach($matchesLine[1] as $line)
		{
			$line = toPlainText($line);
			$fields = preg_split('/(,|:|\.)/', $line);
			$name = array_shift($fields);
			$name = removeParentheses($name);
			$name = formatName($name);
			if($fields[count($fields)-1] == null)
				array_pop($fields);
			$meaning = implode(',', $fields);
			$meaning = trim($meaning);
			
			if(strpos($name, "�") !== false || strpos($name, ";") !== false || strlen($name) > 50)
			{
				continue; //impossible name
			}
			
			if(arrayContainsNameOrigin($existingNames, $name, $origin))
			{
				continue;
			}
			
			$record = new Record($name);
			$record->{'origin'} = $origin;
			$record->{'meaning'} = $meaning;
			$record->{'isFirst'} = true;
			if($isBoy)
				$record->{'isBoy'} = true;
			if($isGirl)
				$record->{'isGirl'} = true;

			$record->display();
			
			if($updateDb)
			{
				$record->saveToDatabase($sourceId);
			}
		}
	}
}

//format name - first letter capital, the rest lower case
function formatName($name)
{
	$name = trim($name);
	if($name[0] == "'")
		return strtoupper($name[0].$name[1]).customToLowerCase(substr($name, 2));
	return strtoupper($name[0]).customToLowerCase(substr($name, 1));
}

//remove all extra whitespace, and all html tags, from text
function toPlainText($text)
{
	$text = preg_replace('/[\s\n]+/', " ", $text);
	$text = decodeHTML($text);
	$text = deleteTags($text, 'li');
	$text = removeTags($text, 'p');
	$text = removeTags($text, 'font');
	$text = removeTags($text, 'ol');
	$text = removeTags($text, 'li');
	$text = removeTags($text, 'a');
	$text = removeTags($text, 'b');
	$text = removeTags($text, 'i');
	$text = trim($text);
	return $text;
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