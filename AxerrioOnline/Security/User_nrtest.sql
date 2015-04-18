CREATE USER nrtest
	FOR LOGIN nrtest
	WITH DEFAULT_SCHEMA = dbo
GO
-- Add user to the database owner role
EXEC sp_addrolemember N'db_datareader', N'nrtest'
GO
EXEC sp_addrolemember N'db_datawriter', N'nrtest'
GO