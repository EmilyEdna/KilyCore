USE [Kily];
GO
--删除所有约束
DECLARE Temp CURSOR FOR
SELECT 'alter table [' + OBJECT_NAME(parent_obj) + '] drop constraint [' + name + ']; '
FROM sysobjects
WHERE xtype = 'F';
OPEN Temp;
DECLARE @Temp VARCHAR(8000);
FETCH NEXT FROM Temp
INTO @Temp;
WHILE (@@fetch_status = 0)
BEGIN
    EXEC (@Temp);
    FETCH NEXT FROM Temp
    INTO @Temp;
END;
CLOSE Temp;
DEALLOCATE Temp;
--删除数据库所有表
DECLARE @tname VARCHAR(8000);
SET @tname = '';
SELECT @tname = @tname + name + ','
FROM sysobjects
WHERE xtype = 'U';
SELECT @tname = 'drop table ' + LEFT(@tname, LEN(@tname) - 1);
EXEC (@tname);