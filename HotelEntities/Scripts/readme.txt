tables.sql 是由老的数据库直接生成的脚本,可以创建所有的表,但是不包含存储过程,视图,用户自定义函数和用户.
如果需要,继续创建procedure.sql,view.sql,function.sql以及user.sql

注意：因zd_zdfj表id列不能正常生成，对脚本作了处理
本来情况：id列默认为零，可为空，identity
因sql 2008 不支持identity可为null
修改为：id 以1递增，不可为null

数据库创建步骤：
create db named HotelDB
create user U：HotelAdmin P：User@123 role db_owner
run tables.sql
