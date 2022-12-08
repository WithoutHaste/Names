--database withouth_names
--mySQL

CREATE TABLE Source
(
	SourceId INT AUTO_INCREMENT NOT NULL,
	Name VARCHAR(50) NOT NULL,				--short designation
	Url VARCHAR(200) NOT NULL,				--home url, not specific page
	PRIMARY KEY pkSource(SourceId)
)

CREATE TABLE Name
(
	NameId INT AUTO_INCREMENT NOT NULL,
	Name VARCHAR(50) NOT NULL UNIQUE,
	FirstLetter CHAR(1) DEFAULT NULL,		--A to Z first letter
	IsFamiliar BIT DEFAULT NULL,			--a name I recognize, null=unknown
	PRIMARY KEY pkName(NameId)
)
CHARACTER SET utf8 COLLATE utf8_general_ci

CREATE TABLE Detail
(
	DetailId INT AUTO_INCREMENT NOT NULL,
	fkSourceId INT NOT NULL,
	fkNameId INT NOT NULL,
	IsBoy BIT DEFAULT NULL,					--can be a boy name, null=unknown
	IsGirl BIT DEFAULT NULL,				--can be a girl name, null=unknown
	IsFirstName BIT DEFAULT NULL,			--a first name, null=unknown
	IsLastName BIT DEFAULT NULL,			--a last/surname, null=unknown
	Origin VARCHAR(50),						--country or region of origin
	Meaning VARCHAR(200),
	CreatedDate DATETIME NOT NULL,
	PRIMARY KEY pkDetail(DetailId),
	FOREIGN KEY fkDetailSource(fkSourceId) REFERENCES Source(SourceId),
	FOREIGN KEY fkDetailName(fkNameId) REFERENCES Name(NameId)
)
CHARACTER SET utf8 COLLATE utf8_general_ci

--hierarchy of (mostly) origins
--expect duplicates in "Category" column, because some stuff can be listed in multiple super-categories
CREATE TABLE Category
(
	Category VARCHAR(50) NOT NULL,				--ex "England"
	SuperCategory VARCHAR(50) DEFAULT NULL,		--ex "Europe" b/c England is in Europe
	PRIMARY KEY pkCategory(Category, SuperCategory)
)
CHARACTER SET utf8 COLLATE utf8_general_ci

--nicknames, diminutives
CREATE TABLE NickName
(
	fkNickNameId INT NOT NULL,
	fkFullNameId INT NOT NULL,
	PRIMARY KEY pkNickName(fkNickNameId, fkFullNameId),
	FOREIGN KEY fkNickNameNickName(fkNickNameId) REFERENCES Name(NameId),
	FOREIGN KEY fkNickNameFullName(fkFullNameId) REFERENCES Name(NameId)
)
CHARACTER SET utf8 COLLATE utf8_general_ci

--spelling variations
--with the most common, hopefully, in the first column
CREATE TABLE Spelling
(
	fkCommonNameId INT NOT NULL,
	fkVariationNameId INT NOT NULL,
	PRIMARY KEY pkSpelling(fkCommonNameId, fkVariationNameId),
	FOREIGN KEY fkSpellingCommonName(fkCommonNameId) REFERENCES Name(NameId),
	FOREIGN KEY fkSpellingVariationName(fkVariationNameId) REFERENCES Name(NameId)
)
CHARACTER SET utf8 COLLATE utf8_general_ci
