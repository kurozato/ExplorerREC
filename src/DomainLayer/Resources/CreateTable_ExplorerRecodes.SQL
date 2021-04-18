/*** ExplorerRecodes ***/
CREATE TABLE IF NOT EXISTS ExplorerRecodes(
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT,
    Path TEXT,
    DataSrt INTEGER
);

/*** Index of ExplorerRecodes ***/
CREATE TABLE IF NOT EXISTS ExplorerRecodes_INDEX (
    Recode_ID INTEGER 
);
