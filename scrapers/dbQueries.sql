--recent records
SELECT Name.Name, Detail.* FROM Detail INNER JOIN Name ON Name.NameId = Detail.fkNameId ORDER BY DetailId DESC

--do not use stored procedures on website
--a simple select query stalled the database for an hour

--set Name.FirstLetter for basic alphabet
update Name inner join (
	SELECT NameId, Name, Substring(Name, 1, 1) as Letter FROM `Name`
	where FirstLetter is null ) as Sub on Sub.NameId = Name.NameId
set FirstLetter = Letter
where Letter IN ('A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z')

--set Name.FirstLetter for "starts with ' mark"
--50 rows
update Name inner join ( 
	select NameId, Name, Letter, Substring(Name, 2, 1) as Letter2 from ( 
		SELECT NameId, Name, Substring(Name, 1, 1) as Letter FROM `Name` where FirstLetter is null 
	) as Sub 
) as Sub on Sub.NameId = Name.NameId 
set FirstLetter = Letter2 
where Sub.Letter = "'"

--there aren't many weird letters, so just add them in
--129 rows
update Name inner join (
	SELECT NameId, Name, Substring(Name, 1, 1) as Letter FROM `Name`
	where FirstLetter is null ) as Sub on Sub.NameId = Name.NameId
set FirstLetter = Letter
