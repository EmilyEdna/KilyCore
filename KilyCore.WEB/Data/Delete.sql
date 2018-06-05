USE [Kily];
GO
--删除所有约束
DECLARE c1 CURSOR FOR
SELECT 'alter table [' + OBJECT_NAME(parent_obj) + '] drop constraint [' + name + ']; '
FROM sysobjects
WHERE xtype = 'F';
OPEN c1;
DECLARE @c1 VARCHAR(8000);
FETCH NEXT FROM c1
INTO @c1;
WHILE (@@fetch_status = 0)
BEGIN
    EXEC (@c1);
    FETCH NEXT FROM c1
    INTO @c1;
END;
CLOSE c1;
DEALLOCATE c1;
--删除数据库所有表
DECLARE @tname VARCHAR(8000);
SET @tname = '';
SELECT @tname = @tname + name + ','
FROM sysobjects
WHERE xtype = 'U';
SELECT @tname = 'drop table ' + LEFT(@tname, LEN(@tname) - 1);
EXEC (@tname);