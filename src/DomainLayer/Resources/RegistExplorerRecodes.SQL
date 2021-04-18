/*Regist Model*/
Delete From ExplorerRecodes Where Path = '@Path';

Update ExplorerRecodes Set 
    DataSrt = DataSrt + 1;

Insert Into ExplorerRecodes(
    Name, 
    Path, 
    DataSrt
)
Values(
    '@Name',
    '@Path',
     0);

--make Index 
Delete From ExplorerRecodes_INDEX;

Insert Into ExplorerRecodes_INDEX(
    Recode_ID
)
Select
    ID
From
    ExplorerRecodes
Order By
    DataSrt;

--delete 30 recodes over
Delete From ExplorerRecodes
Where ID In(
    Select 
        Main.ID 
    From 
        ExplorerRecodes Main 
    Inner join ExplorerRecodes_INDEX Main_Index On 
        Main_Index.Recode_ID = Main.ID 
    Where 
        Main_Index.ROWID > 30);