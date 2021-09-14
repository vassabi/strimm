
--ALTER ROLE [db_owner] ADD MEMBER [MonoX];


--GO
ALTER ROLE [db_owner] ADD MEMBER [strimm_dbo];


GO
--ALTER ROLE [db_datareader] ADD MEMBER [MonoX];


--GO
ALTER ROLE [db_datareader] ADD MEMBER [strimm_dbo];


GO
--ALTER ROLE [db_datareader] ADD MEMBER [strimm_user];


--GO
--ALTER ROLE [db_datawriter] ADD MEMBER [MonoX];


--GO
ALTER ROLE [db_datawriter] ADD MEMBER [strimm_dbo];


GO
--ALTER ROLE [db_datawriter] ADD MEMBER [strimm_user];

