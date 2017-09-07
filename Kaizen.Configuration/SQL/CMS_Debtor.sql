﻿declare @ViewID int;
delete Kaizen00010 where ScreenID = 6
insert into Kaizen00010(ScreenID,ScreenName) values (6,'CMS_Debtor')
insert into Kaizen00011(ScreenID,ViewName,ViewDescription,IsDefault) values (6,'Default','Default View',1)
select @ViewID = Scope_Identity()

insert into KaizenGridViewAccess(ViewID,UserName,IsDefault) values (@ViewID,'admin',1);

truncate table GridCM00100
delete  GridCM00101
insert into  GridCM00101(FieldID,ColumnName) values (1,'DebtorID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (1,1,@ViewID,'DebtorID',20,1,1,1,1);
insert into  GridCM00101(FieldID,ColumnName) values (2,'ParentDebtor') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (2,2,@ViewID,'ParentDebtor',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (3,'NationalityName') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (3,3,@ViewID,'NationalityName',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (4,'PriorityName') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (4,4,@ViewID,'PriorityName',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (5,'GroupName') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (5,5,@ViewID,'GroupName',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (6,'Pnone01') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (6,6,@ViewID,'Pnone01',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (7,'Pnone02') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (7,7,@ViewID,'Pnone02',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (8,'CUSTCLASNAME') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (8,8,@ViewID,'CUSTCLASNAME',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (9,'NationalityID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (9,9,@ViewID,'NationalityID',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (10,'AddressName') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (10,10,@ViewID,'AddressName',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (11,'SiteID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (11,11,@ViewID,'SiteID',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (12,'Pnone01') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (12,12,@ViewID,'Pnone01',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (13,'Pnone02') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (13,13,@ViewID,'Pnone02',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (14,'Pnone03') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (14,14,@ViewID,'Pnone03',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (15,'Pnone04') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (15,15,@ViewID,'Pnone04',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (16,'MobileNo1') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (16,16,@ViewID,'MobileNo1',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (17,'MobileNo2') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (17,17,@ViewID,'MobileNo2',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (18,'MobileNo3') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (18,18,@ViewID,'MobileNo3',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (19,'MobileNo4') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (19,19,@ViewID,'MobileNo4',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (20,'POBox') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (20,20,@ViewID,'POBox',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (21,'Other01') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (21,21,@ViewID,'Other01',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (22,'Other02') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (22,22,@ViewID,'Other02',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (23,'Other03') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (23,23,@ViewID,'Other03',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (24,'Other04') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (24,23,@ViewID,'Other04',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (25,'Address1') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (25,23,@ViewID,'Address1',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (26,'Address2') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (26,23,@ViewID,'Address2',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (27,'Address3') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (27,23,@ViewID,'Address3',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (28,'Address4') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (28,23,@ViewID,'Address4',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (29,'Email01') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (29,23,@ViewID,'Email01',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (30,'Email02') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (30,23,@ViewID,'Email02',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (31,'Email03') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (31,23,@ViewID,'Email03',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (32,'Email04') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (32,23,@ViewID,'Email04',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (33,'ShipAddressName') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (33,23,@ViewID,'ShipAddressName',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (34,'ShipSiteID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (34,23,@ViewID,'ShipSiteID',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (35,'ShipPnone01') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (35,23,@ViewID,'ShipPnone01',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (36,'ShipPnone02') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (36,23,@ViewID,'ShipPnone02',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (37,'ShipPnone03') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (37,23,@ViewID,'ShipPnone03',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (38,'ShipPnone04') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (38,23,@ViewID,'ShipPnone04',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (39,'ShipMobileNo1') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (39,23,@ViewID,'ShipMobileNo1',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (40,'ShipMobileNo2') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (40,23,@ViewID,'ShipMobileNo2',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (41,'ShipMobileNo3') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (41,23,@ViewID,'ShipMobileNo3',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (42,'ShipMobileNo4') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (42,23,@ViewID,'ShipMobileNo4',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (43,'ShipPOBox') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (43,23,@ViewID,'ShipPOBox',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (44,'ShipOther01') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (44,23,@ViewID,'ShipOther01',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (45,'ShipOther02') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (45,23,@ViewID,'ShipOther02',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (46,'ShipOther03') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (46,23,@ViewID,'ShipOther03',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (47,'ShipOther04') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (47,23,@ViewID,'ShipOther04',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (48,'ShipAddress1') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (48,23,@ViewID,'ShipAddress1',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (49,'UPR00100ID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (49,23,@ViewID,'UPR00100ID',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (50,'ShipAddress2') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (50,23,@ViewID,'ShipAddress2',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (51,'ShipAddress3') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (51,23,@ViewID,'ShipAddress3',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (52,'ShipAddress4') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (52,23,@ViewID,'ShipAddress4',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (53,'ShipEmail01') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (53,23,@ViewID,'ShipEmail01',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (54,'ShipEmail02') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (54,23,@ViewID,'ShipEmail02',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (55,'ShipEmail03') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (55,23,@ViewID,'ShipEmail03',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (56,'ShipEmail04') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (56,23,@ViewID,'ShipEmail04',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (57,'BillAddressName') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (57,23,@ViewID,'BillAddressName',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (58,'BillSiteID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (58,23,@ViewID,'BillSiteID',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (59,'BillPnone01') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (59,23,@ViewID,'BillPnone01',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (60,'BillPnone02') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (60,23,@ViewID,'BillPnone02',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (61,'BillPnone03') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (61,23,@ViewID,'BillPnone03',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (62,'BillPnone04') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (62,23,@ViewID,'BillPnone04',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (63,'BillMobileNo1') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (63,23,@ViewID,'BillMobileNo1',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (64,'BillMobileNo2') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (64,23,@ViewID,'BillMobileNo2',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (65,'BillMobileNo3') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (65,23,@ViewID,'BillMobileNo3',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (66,'BillMobileNo4') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (66,23,@ViewID,'BillMobileNo4',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (67,'BillPOBox') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (67,23,@ViewID,'BillPOBox',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (68,'BillOther01') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (68,23,@ViewID,'BillOther01',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (69,'BillOther02') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (69,23,@ViewID,'BillOther02',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (70,'BillOther03') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (70,23,@ViewID,'BillMobileNo3',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (71,'BillOther04') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (71,23,@ViewID,'BillOther04',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (72,'BillAddress1') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (72,23,@ViewID,'BillAddress1',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (73,'BillAddress2') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (73,23,@ViewID,'BillAddress2',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (74,'BillAddress3') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (74,23,@ViewID,'BillMobileNo3',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (75,'BillAddress4') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (75,23,@ViewID,'BillAddress4',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (76,'BillEmail01') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (76,23,@ViewID,'BillEmail01',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (77,'BillEmail02') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (77,23,@ViewID,'BillEmail02',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (78,'BillEmail03') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (78,23,@ViewID,'BillEmail03',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (79,'BillEmail04') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (79,23,@ViewID,'BillEmail04',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (80,'StatMobileNo1') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (80,23,@ViewID,'StatMobileNo1',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (81,'StatMobileNo2') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (81,23,@ViewID,'StatMobileNo2',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (82,'StatMobileNo3') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (82,23,@ViewID,'StatMobileNo3',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (83,'StatMobileNo4') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (83,23,@ViewID,'StatMobileNo4',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (84,'StatPnone01') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (84,23,@ViewID,'StatPnone01',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (85,'StatPnone02') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (85,23,@ViewID,'StatPnone02',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (86,'StatPnone03') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (86,23,@ViewID,'StatPnone03',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (87,'StatPnone04') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (87,23,@ViewID,'StatPnone04',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (88,'StatSiteID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (88,23,@ViewID,'StatSiteID',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (89,'StatAddressName') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (89,23,@ViewID,'StatAddressName',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (90,'StatEmail01') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (90,23,@ViewID,'StatEmail01',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (91,'StatEmail02') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (91,23,@ViewID,'StatEmail02',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (92,'StatEmail03') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (92,23,@ViewID,'StatEmail03',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (93,'StatEmail04') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (93,23,@ViewID,'StatEmail04',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (94,'StatAddress1') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (94,23,@ViewID,'StatAddress1',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (95,'StatAddress2') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (95,23,@ViewID,'StatAddress2',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (96,'StatAddress3') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (96,23,@ViewID,'StatAddress3',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (97,'StatAddress4') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (97,23,@ViewID,'StatAddress4',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (98,'StatOther04') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (98,23,@ViewID,'StatOther04',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (99,'StatOther03') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (99,23,@ViewID,'StatOther03',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (100,'StatOther02') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (100,23,@ViewID,'StatOther02',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (101,'StatOther01') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (101,23,@ViewID,'StatOther01',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (102,'StatPOBox') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (102,23,@ViewID,'StatPOBox',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (103,'AddressCode') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (103,23,@ViewID,'AddressCode',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (104,'DebtorName') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (104,23,@ViewID,'DebtorName',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (105,'GeneralName') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (105,23,@ViewID,'GeneralName',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (106,'ShortName') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (106,23,@ViewID,'ShortName',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (107,'DebtorDescription') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (107,23,@ViewID,'DebtorDescription',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (108,'DebtorStatusName') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (108,23,@ViewID,'DebtorStatusName',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (109,'CountryID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (109,23,@ViewID,'CountryID',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (110,'CityID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (110,23,@ViewID,'CityID',20,1,1,0,1);

insert into  GridCM00101(FieldID,ColumnName) values (111,'CUSTCLAS') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (111,23,@ViewID,'CUSTCLAS',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (112,'GroupID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (112,23,@ViewID,'GroupID',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (113,'StatementToCityID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (113,23,@ViewID,'StatementToCityID',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (116,'IsHold') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (116,23,@ViewID,'IsHold',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (117,'IsActive') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (117,23,@ViewID,'IsActive',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (118,'PhotoExtension') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (118,23,@ViewID,'PhotoExtension',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (119,'CPRCRNo') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (119,23,@ViewID,'CPRCRNo',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (120,'EmployerName') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (120,23,@ViewID,'EmployerName',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (121,'ShipTo') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (121,23,@ViewID,'ShipTo',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (122,'BillTo') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (122,23,@ViewID,'BillTo',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (123,'StatementTo') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (123,23,@ViewID,'StatementTo',20,1,1,0,1);

insert into  GridCM00101(FieldID,ColumnName) values (124,'Occupation') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (124,24,@ViewID,'Occupation',20,1,1,0,1);

insert into  GridCM00101(FieldID,ColumnName) values (125,'ShipToCountryID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (126,24,@ViewID,'ShipToCountryID',20,1,1,0,1);

insert into  GridCM00101(FieldID,ColumnName) values (126,'ShipToCityID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (126,24,@ViewID,'ShipToCityID',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (127,'BillToCountryID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (127,24,@ViewID,'BillToCountryID',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (128,'BillToCityID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (128,24,@ViewID,'BillToCityID',20,1,1,0,1);
insert into  GridCM00101(FieldID,ColumnName) values (129,'StatementToCountryID') ;
insert into  GridCM00100(FieldID,ColumnOrder,ViewID,ColumnTitle,ColumnWidth,IsVisible,IsActive,Locked,Lockable) values (129,24,@ViewID,'StatementToCountryID',20,1,1,0,1);





