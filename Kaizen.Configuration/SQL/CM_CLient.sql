declare @ViewID int;
delete Kaizen00010 where ScreenID = 10
insert into Kaizen00010(ScreenID,ScreenName) values (10,'CM_Client')
insert into Kaizen00011(ScreenID,ViewName,ViewDescription,IsDefault) values (10,'Default','Default View',1)
select @ViewID = Scope_Identity()

insert into KaizenGridViewAccess(ViewID,UserName,IsDefault) values (@ViewID,'admin',1);
truncate table GridCM00110
delete  GridCM00111
insert into  GridCM00111(FieldID,ColumnName) values (1,'ClientID') ;
insert into  GridCM00111(FieldID,ColumnName) values (2,'ClientName') ;
insert into  GridCM00111(FieldID,ColumnName) values (3,'ShortName') ;
insert into  GridCM00111(FieldID,ColumnName) values (4,'ClientDescription') ;
insert into  GridCM00111(FieldID,ColumnName) values (5,'ContactPerson') ;
insert into  GridCM00111(FieldID,ColumnName) values (6,'CUSTCLAS') ;
insert into  GridCM00111(FieldID,ColumnName) values (7,'StatusName') ;
insert into  GridCM00111(FieldID,ColumnName) values (8,'PhotoExtension') ;
insert into  GridCM00111(FieldID,ColumnName) values (9,'ParentVendor') ;
insert into  GridCM00111(FieldID,ColumnName) values (10,'AddressCode') ;
insert into  GridCM00111(FieldID,ColumnName) values (11,'BillAddressCode') ;
insert into  GridCM00111(FieldID,ColumnName) values (12,'RemitToAddressCode') ;
insert into  GridCM00111(FieldID,ColumnName) values (13,'AddressName') ;
insert into  GridCM00111(FieldID,ColumnName) values (14,'CountryID') ;
insert into  GridCM00111(FieldID,ColumnName) values (15,'Pnone01') ;
insert into  GridCM00111(FieldID,ColumnName) values (16,'Pnone02') ;
insert into  GridCM00111(FieldID,ColumnName) values (17,'Pnone03') ;
insert into  GridCM00111(FieldID,ColumnName) values (18,'Pnone04') ;
insert into  GridCM00111(FieldID,ColumnName) values (19,'MobileNo1') ;
insert into  GridCM00111(FieldID,ColumnName) values (20,'MobileNo2') ;
insert into  GridCM00111(FieldID,ColumnName) values (21,'MobileNo3') ;
insert into  GridCM00111(FieldID,ColumnName) values (22,'MobileNo4') ;
insert into  GridCM00111(FieldID,ColumnName) values (23,'POBox') ;
insert into  GridCM00111(FieldID,ColumnName) values (24,'Other01') ;
insert into  GridCM00111(FieldID,ColumnName) values (25,'Other02') ;
insert into  GridCM00111(FieldID,ColumnName) values (26,'Other03') ;
insert into  GridCM00111(FieldID,ColumnName) values (27,'Other04') ;
insert into  GridCM00111(FieldID,ColumnName) values (28,'Address1') ;
insert into  GridCM00111(FieldID,ColumnName) values (29,'Address2') ;
insert into  GridCM00111(FieldID,ColumnName) values (30,'Address3') ;
insert into  GridCM00111(FieldID,ColumnName) values (31,'Address4') ;
insert into  GridCM00111(FieldID,ColumnName) values (32,'Email01') ;
insert into  GridCM00111(FieldID,ColumnName) values (33,'Email02') ;
insert into  GridCM00111(FieldID,ColumnName) values (34,'Email03') ;
insert into  GridCM00111(FieldID,ColumnName) values (35,'Email04') ;
insert into  GridCM00111(FieldID,ColumnName) values (36,'CityID') ;
insert into  GridCM00111(FieldID,ColumnName) values (37,'WebSite') ;
insert into  GridCM00111(FieldID,ColumnName) values (38,'CUSTCLASNAME') ;
insert into  GridCM00111(FieldID,ColumnName) values (39,'LastClientID') ;
insert into  GridCM00111(FieldID,ColumnName) values (40,'StatusName') ;
truncate table GridCM00110


insert into GridCM00110(ViewID, FieldID,ColumnTitle,ColumnWidth,ColumnOrder,IsVisible,IsActive,Locked,Lockable)
select @ViewID,FieldID,ColumnName,100,1,1,1,0,1 from  GridCM00111
where ColumnName <> 'PhotoExtension'
insert into GridCM00110(ViewID, FieldID,ColumnTitle,ColumnWidth,ColumnOrder,IsVisible,IsActive,Locked,Lockable)
select @ViewID,FieldID,ColumnName,100,1,1,1,1,1 from  GridCM00111
where ColumnName = 'PhotoExtension'
