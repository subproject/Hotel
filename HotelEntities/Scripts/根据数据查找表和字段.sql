DECLARE @what varchar(800) 
SET @what='标准间暗' --要搜索的字符串 

DECLARE @sql varchar(8000) 

DECLARE TableCursor CURSOR LOCAL FOR 
SELECT sql='IF EXISTS ( SELECT 1 FROM ['+o.name+'] WHERE ['+c.name+'] LIKE ''%'+@what+'%'' ) 
PRINT ''所在的表及字段：['+o.name+'].['+c.name+']''' 
FROM syscolumns c JOIN sysobjects o ON c.id=o.id 

-- 175=char 56=int 可以查 select * from sys.types 
WHERE o.xtype='U' AND c.status>=0 AND c.xusertype IN (175, 239, 231, 167 ) 

OPEN TableCursor 

FETCH NEXT FROM TableCursor INTO @sql 
WHILE @@FETCH_STATUS=0 
BEGIN 
EXEC( @sql ) 
FETCH NEXT FROM TableCursor INTO @sql 
END 

CLOSE TableCursor 

-- 删除游标引用 
DEALLOCATE TableCursor 