truncate table GridUPR00100
delete  GridUPR00101

declare @ViewID int;
delete Kaizen00010 where ScreenID = 7
insert into Kaizen00010(ScreenID,ScreenName) values (7,'HumanResources_Employee')
insert into Kaizen00011(ScreenID,ViewName,ViewDescription,IsDefault) values (7,'Default','Default View',1)
select @ViewID = Scope_Identity()

insert into KaizenGridViewAccess(ViewID,UserName,IsDefault) values (@ViewID,'admin',1);


insert into  GridUPR00101(FieldID,ColumnName) values (1,'EmployeeID') ;
insert into  GridUPR00101(FieldID,ColumnName) values (2,'SupervisorID') ;
insert into  GridUPR00101(FieldID,ColumnName) values (3,'MidName') ;
insert into  GridUPR00101(FieldID,ColumnName) values (4,'FirstName') ;
insert into  GridUPR00101(FieldID,ColumnName) values (5,'InactiveEmployee') ;
insert into  GridUPR00101(FieldID,ColumnName) values (6,'LastName') ;
insert into  GridUPR00101(FieldID,ColumnName) values (7,'CategoryID') ;
insert into  GridUPR00101(FieldID,ColumnName) values (8,'DivisionID') ;
insert into  GridUPR00101(FieldID,ColumnName) values (9,'DepartmentID') ;
insert into  GridUPR00101(FieldID,ColumnName) values (10,'PositionID') ;
insert into  GridUPR00101(FieldID,ColumnName) values (11,'LocationID') ;
insert into  GridUPR00101(FieldID,ColumnName) values (12,'LastWorkingDate') ;
insert into  GridUPR00101(FieldID,ColumnName) values (13,'BirthDate') ;
insert into  GridUPR00101(FieldID,ColumnName) values (14,'HireDate') ;
insert into  GridUPR00101(FieldID,ColumnName) values (15,'NationalityID') ;
insert into  GridUPR00101(FieldID,ColumnName) values (16,'HolidayGroupID') ;
insert into  GridUPR00101(FieldID,ColumnName) values (17,'AttendanceGroupID') ;
insert into  GridUPR00101(FieldID,ColumnName) values (18,'PhotoExtension') ;
insert into  GridUPR00101(FieldID,ColumnName) values (19,'SignatureExtension') ;
insert into  GridUPR00101(FieldID,ColumnName) values (20,'GradeID') ;
insert into  GridUPR00101(FieldID,ColumnName) values (21,'PayrollCategoryID') ;
insert into  GridUPR00101(FieldID,ColumnName) values (22,'PhoneNumber') ;
insert into  GridUPR00101(FieldID,ColumnName) values (23,'PhoneExtension') ;
insert into  GridUPR00101(FieldID,ColumnName) values (24,'KaizenDescription') ;
insert into  GridUPR00101(FieldID,ColumnName) values (25,'AddressName') ;
insert into  GridUPR00101(FieldID,ColumnName) values (26,'CountryID') ;
insert into  GridUPR00101(FieldID,ColumnName) values (27,'CityID') ;
insert into  GridUPR00101(FieldID,ColumnName) values (28,'WebSite') ;
insert into  GridUPR00101(FieldID,ColumnName) values (29,'Pnone01') ;
insert into  GridUPR00101(FieldID,ColumnName) values (30,'Pnone02') ;
insert into  GridUPR00101(FieldID,ColumnName) values (31,'Pnone03') ;
insert into  GridUPR00101(FieldID,ColumnName) values (32,'Pnone04') ;
insert into  GridUPR00101(FieldID,ColumnName) values (33,'MobileNo1') ;
insert into  GridUPR00101(FieldID,ColumnName) values (34,'MobileNo2') ;
insert into  GridUPR00101(FieldID,ColumnName) values (35,'MobileNo3') ;
insert into  GridUPR00101(FieldID,ColumnName) values (36,'MobileNo4') ;
insert into  GridUPR00101(FieldID,ColumnName) values (37,'POBox') ;
insert into  GridUPR00101(FieldID,ColumnName) values (38,'Other01') ;
insert into  GridUPR00101(FieldID,ColumnName) values (39,'Other02') ;
insert into  GridUPR00101(FieldID,ColumnName) values (40,'Other03') ;
insert into  GridUPR00101(FieldID,ColumnName) values (41,'Other04') ;
insert into  GridUPR00101(FieldID,ColumnName) values (42,'Address1') ;
insert into  GridUPR00101(FieldID,ColumnName) values (43,'Address2') ;
insert into  GridUPR00101(FieldID,ColumnName) values (44,'Address3') ;
insert into  GridUPR00101(FieldID,ColumnName) values (45,'Address4') ;
insert into  GridUPR00101(FieldID,ColumnName) values (46,'Email01') ;
insert into  GridUPR00101(FieldID,ColumnName) values (47,'Email02') ;
insert into  GridUPR00101(FieldID,ColumnName) values (48,'Email03') ;
insert into  GridUPR00101(FieldID,ColumnName) values (49,'Email04') ;
insert into  GridUPR00101(FieldID,ColumnName) values (50,'PrefixName') ;
insert into  GridUPR00101(FieldID,ColumnName) values (51,'SuffixINam') ;
insert into  GridUPR00101(FieldID,ColumnName) values (52,'MaritalName') ;
insert into  GridUPR00101(FieldID,ColumnName) values (53,'CategoryName') ;
insert into  GridUPR00101(FieldID,ColumnName) values (54,'GenderName') ;
insert into  GridUPR00101(FieldID,ColumnName) values (55,'DivisionName') ;
insert into  GridUPR00101(FieldID,ColumnName) values (56,'DepartmentName') ;
insert into  GridUPR00101(FieldID,ColumnName) values (57,'PositionName') ;
insert into  GridUPR00101(FieldID,ColumnName) values (58,'LocationName') ;
insert into  GridUPR00101(FieldID,ColumnName) values (59,'NationalityName') ;
insert into  GridUPR00101(FieldID,ColumnName) values (60,'HolidayGroupName') ;
insert into  GridUPR00101(FieldID,ColumnName) values (61,'AttendanceGroupName') ;


insert into GridUPR00100(ViewID, FieldID,ColumnTitle,ColumnWidth,ColumnOrder,IsVisible,IsActive,Locked,Lockable)
select @ViewID,FieldID,ColumnName,100,1,1,1,0,1 from  GridUPR00101
where ColumnName <> 'PhotoExtension'
insert into GridUPR00100(ViewID, FieldID,ColumnTitle,ColumnWidth,ColumnOrder,IsVisible,IsActive,Locked,Lockable)
select @ViewID,FieldID,ColumnName,100,1,1,1,1,1 from  GridUPR00101
where ColumnName = 'PhotoExtension'