declare @ViewID int;
delete Kaizen00010 where ScreenID = 12
insert into Kaizen00010(ScreenID,ScreenName) values (12,'CMS_Case')
insert into Kaizen00011(ScreenID,ViewName,ViewDescription,IsDefault) values (12,'Default','Default View',1)
select @ViewID = Scope_Identity()

insert into KaizenGridViewAccess(ViewID,UserName,IsDefault) values (@ViewID,'admin',1);
truncate table GridCM00203
delete  CM00203
insert into  CM00203(FieldID,ColumnName) values (1,'CaseRef') ;
insert into  CM00203(FieldID,ColumnName) values (2,'TRXTypeID') ;
insert into  CM00203(FieldID,ColumnName) values (3,'TrxTypeName') ;
insert into  CM00203(FieldID,ColumnName) values (4,'ContractID') ;
insert into  CM00203(FieldID,ColumnName) values (5,'ClientID') ;
insert into  CM00203(FieldID,ColumnName) values (6,'JecketsID') ;
insert into  CM00203(FieldID,ColumnName) values (7,'JecketName') ;
insert into  CM00203(FieldID,ColumnName) values (8,'DebtorID') ;
insert into  CM00203(FieldID,ColumnName) values (9,'ProductID') ;
insert into  CM00203(FieldID,ColumnName) values (10,'CaseTreeID') ;
insert into  CM00203(FieldID,ColumnName) values (11,'CaseAddess') ;
insert into  CM00203(FieldID,ColumnName) values (12,'CaseContractDetail') ;
insert into  CM00203(FieldID,ColumnName) values (13,'AddressCode') ;
insert into  CM00203(FieldID,ColumnName) values (14,'CaseAccountNo') ;
insert into  CM00203(FieldID,ColumnName) values (15,'ClosingDate') ;
insert into  CM00203(FieldID,ColumnName) values (16,'BookingDate') ;
insert into  CM00203(FieldID,ColumnName) values (17,'CaseStatusname') ;
insert into  CM00203(FieldID,ColumnName) values (18,'ClaimAmount') ;
insert into  CM00203(FieldID,ColumnName) values (19,'PrincipleAmount') ;
insert into  CM00203(FieldID,ColumnName) values (20,'CreatedDate') ;
insert into  CM00203(FieldID,ColumnName) values (21,'EmployeeID') ;
insert into  CM00203(FieldID,ColumnName) values (22,'AssignDate') ;
insert into  CM00203(FieldID,ColumnName) values (23,'ActionPlanName') ;
insert into  CM00203(FieldID,ColumnName) values (24,'DebtorName') ;
insert into  CM00203(FieldID,ColumnName) values (25,'Occupation') ;
insert into  CM00203(FieldID,ColumnName) values (26,'GeneralName') ;
insert into  CM00203(FieldID,ColumnName) values (27,'ShortName') ;
insert into  CM00203(FieldID,ColumnName) values (28,'DebtorDescription') ;
insert into  CM00203(FieldID,ColumnName) values (29,'CPRCRNo') ;
insert into  CM00203(FieldID,ColumnName) values (30,'EmployerName') ;
insert into  CM00203(FieldID,ColumnName) values (31,'NationalityID') ;
insert into  CM00203(FieldID,ColumnName) values (32,'LastPaymentDate') ;
insert into  CM00203(FieldID,ColumnName) values (33,'LastCallactionAMT') ;
insert into  CM00203(FieldID,ColumnName) values (34,'LastPaymentBy') ;
insert into  CM00203(FieldID,ColumnName) values (35,'LastActionDate') ;
insert into  CM00203(FieldID,ColumnName) values (36,'LastActionDescription') ;
insert into  CM00203(FieldID,ColumnName) values (37,'LastActionBy') ;
insert into  CM00203(FieldID,ColumnName) values (38,'PromissToPayAmount') ;
insert into  CM00203(FieldID,ColumnName) values (39,'AddressName') ;
insert into  CM00203(FieldID,ColumnName) values (40,'WebSite') ;
insert into  CM00203(FieldID,ColumnName) values (41,'CountryID') ;
insert into  CM00203(FieldID,ColumnName) values (42,'CityID') ;
insert into  CM00203(FieldID,ColumnName) values (43,'Pnone01') ;
insert into  CM00203(FieldID,ColumnName) values (44,'Pnone02') ;
insert into  CM00203(FieldID,ColumnName) values (45,'Pnone03') ;
insert into  CM00203(FieldID,ColumnName) values (46,'Pnone04') ;
insert into  CM00203(FieldID,ColumnName) values (47,'MobileNo1') ;
insert into  CM00203(FieldID,ColumnName) values (48,'MobileNo2') ;
insert into  CM00203(FieldID,ColumnName) values (49,'MobileNo3') ;
insert into  CM00203(FieldID,ColumnName) values (50,'MobileNo4') ;
insert into  CM00203(FieldID,ColumnName) values (51,'POBox') ;
insert into  CM00203(FieldID,ColumnName) values (52,'Other01') ;
insert into  CM00203(FieldID,ColumnName) values (53,'Other02') ;
insert into  CM00203(FieldID,ColumnName) values (54,'Other03') ;
insert into  CM00203(FieldID,ColumnName) values (55,'Other04') ;
insert into  CM00203(FieldID,ColumnName) values (56,'Address1') ;
insert into  CM00203(FieldID,ColumnName) values (57,'Address2') ;
insert into  CM00203(FieldID,ColumnName) values (58,'Address3') ;
insert into  CM00203(FieldID,ColumnName) values (59,'Address4') ;
insert into  CM00203(FieldID,ColumnName) values (60,'Email01') ;
insert into  CM00203(FieldID,ColumnName) values (61,'Email02') ;
insert into  CM00203(FieldID,ColumnName) values (62,'Email03') ;
insert into  CM00203(FieldID,ColumnName) values (63,'Email04') ;

truncate table GridCM00203


insert into GridCM00203(ViewID, FieldID,ColumnTitle,ColumnWidth,ColumnOrder,IsVisible,IsActive,Locked,Lockable)
select @ViewID,FieldID,ColumnName,100,1,1,1,0,1 from  CM00203
where ColumnName <> 'CaseRef'
insert into GridCM00203(ViewID, FieldID,ColumnTitle,ColumnWidth,ColumnOrder,IsVisible,IsActive,Locked,Lockable)
select @ViewID,FieldID,ColumnName,100,1,1,1,1,1 from  CM00203
where ColumnName = 'CaseRef'