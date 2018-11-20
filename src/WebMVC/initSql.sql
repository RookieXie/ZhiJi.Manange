CREATE TABLE `Admin_Menu` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(6) NOT NULL,
  `IsDisplay` bit(1) NOT NULL,
  `Name` varchar(20) DEFAULT NULL,
  `OrderNum` int(11) NOT NULL,
  `ParentId` int(11) NOT NULL,
  `Status` int(11) NOT NULL,
  `StatusTime` datetime(6) NOT NULL,
  `Type` int(11) DEFAULT NULL,
  `Url` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;

CREATE TABLE `Admin_Role` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(6) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `Status` int(11) NOT NULL,
  `StatusTime` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

CREATE TABLE `Admin_RoleAndMenu` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Delete` bit(1) DEFAULT NULL,
  `MenuId` int(11) NOT NULL,
  `Modify` bit(1) DEFAULT NULL,
  `Query` bit(1) DEFAULT NULL,
  `RoleId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Admin_RoleAndMenu_MenuId` (`MenuId`),
  KEY `IX_Admin_RoleAndMenu_RoleId` (`RoleId`),
  CONSTRAINT `FK_Admin_RoleAndMenu_Admin_Menu_MenuId` FOREIGN KEY (`MenuId`) REFERENCES `Admin_Menu` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Admin_RoleAndMenu_Admin_Role_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Admin_Role` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8;

CREATE TABLE `Admin_User` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(6) NOT NULL,
  `Name` varchar(16) DEFAULT NULL,
  `NickName` varchar(16) DEFAULT NULL,
  `Password` varchar(16) DEFAULT NULL,
  `Phone` varchar(15) DEFAULT NULL,
  `Type` int(11) DEFAULT NULL,
  `PwdFlag` varchar(10) DEFAULT NULL,
  `RealName` varchar(50) DEFAULT NULL,
  `Status` int(11) NOT NULL,
  `StatusTime` datetime(6) NOT NULL,
  `Gender` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

CREATE TABLE `Admin_UserAndRole` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Admin_UserAndRole_RoleId` (`RoleId`),
  KEY `IX_Admin_UserAndRole_UserId` (`UserId`),
  CONSTRAINT `FK_Admin_UserAndRole_Admin_Role_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Admin_Role` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Admin_UserAndRole_Admin_User_UserId` FOREIGN KEY (`UserId`) REFERENCES `Admin_User` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*业务*/
CREATE TABLE `zj_balance` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `FId` varchar(100) DEFAULT NULL,
  `UserId` varchar(100) DEFAULT NULL,
  `AdverseId` varchar(100) DEFAULT NULL,
  `ChangeMoney` decimal(18,2) DEFAULT '0.00',
  `BeforeMoney` decimal(18,2) DEFAULT '0.00',
  `AfterMoney` decimal(18,2) DEFAULT '0.00',
  `CreateTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `IsDelete` int(1) DEFAULT '0' COMMENT '0-未删，1-已删',
  PRIMARY KEY (`Id`),
  KEY `index_FId` (`FId`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `zj_chat` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `FId` varchar(100) DEFAULT NULL,
  `QuestionId` varchar(100) DEFAULT NULL,
  `AnswerId` varchar(100) DEFAULT NULL,
  `Status` int(1) DEFAULT '0',
  `CreateTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `StartTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `EndTime` datetime DEFAULT NULL,
  `IsDelete` int(1) DEFAULT '0' COMMENT '0-未删，1-已删',
  PRIMARY KEY (`Id`),
  KEY `index_FId` (`FId`)
) ENGINE=InnoDB AUTO_INCREMENT=62 DEFAULT CHARSET=utf8mb4;
CREATE TABLE `zj_pay_order` (
  `Id` int(12) NOT NULL AUTO_INCREMENT,
  `FId` varchar(100) DEFAULT NULL COMMENT 'Id主键',
  `UserId` int(12) NOT NULL COMMENT '用户Id',
  `ChatId` int(10) DEFAULT NULL COMMENT '聊天Id',
  `OrderNo` varchar(100) DEFAULT NULL COMMENT '订单号',
  `SerialNo` varchar(100) DEFAULT NULL COMMENT '流水号',
  `TransType` int(10) DEFAULT NULL COMMENT '交易类型（1）',
  `PayType` int(10) DEFAULT NULL COMMENT '支付方式（1-支付宝，2-微信）',
  `FeeType` int(10) DEFAULT NULL COMMENT '计费方式',
  `Money` decimal(12,2) DEFAULT '0.00' COMMENT '交易金额',
  `Status` int(2) DEFAULT NULL COMMENT '状态（0-失败，1-成功）',
  `EndTime` datetime DEFAULT NULL COMMENT '交易完成时间',
  `PrivateInfo` varchar(3000) DEFAULT NULL COMMENT '商户私有信息',
  `Remarks` varchar(300) DEFAULT NULL COMMENT '备注',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间',
  `Qudao` varchar(45) DEFAULT NULL,
  `Version` int(11) DEFAULT NULL,
  `IsDelete` int(1) DEFAULT '0' COMMENT '0-未删，1-已删',
  PRIMARY KEY (`Id`),
  KEY `index_UserId` (`UserId`),
  KEY `index_OrderNo` (`OrderNo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='支付订单表';

CREATE TABLE `zj_user` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `FId` varchar(100) DEFAULT NULL,
  `NickName` varchar(45) DEFAULT NULL,
  `HeadImg` varchar(400) DEFAULT NULL,
  `Sex` int(2) DEFAULT '0' COMMENT '性别（0-未知 1-男，2-女）',
  `Age` int(3) DEFAULT '18' COMMENT '年龄',
  `OpenID` varchar(400) DEFAULT NULL COMMENT 'OpenId',
  `UnionID` varchar(400) DEFAULT NULL COMMENT '微信唯一标识',
  `BoundSource` int(2) DEFAULT NULL COMMENT '绑定来源（1-微信）',
  `Professional` varchar(400) DEFAULT NULL,
  `Introduction` varchar(400) DEFAULT NULL,
  `UserType` int(1) DEFAULT '0' COMMENT '0-用户，1-答主',
  `AnswerPrice` decimal(18,2) DEFAULT '0.00',
  `AnswerTime` decimal(18,2) DEFAULT '0.00',
  `TotalMoney` decimal(18,2) DEFAULT '0.00',
  `Sign` int(2) DEFAULT '0',
  `CreateTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `IsDelete` int(1) DEFAULT '0' COMMENT '0-未删，1-已删',
  `Numbers` varchar(400) DEFAULT NULL COMMENT '知几号',
  PRIMARY KEY (`Id`),
  KEY `index_FId` (`FId`)
) ENGINE=InnoDB AUTO_INCREMENT=10034 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `zj_wxcommon_user` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `FId` varchar(100) DEFAULT NULL,
  `commonOpenId` varchar(100) DEFAULT NULL COMMENT '公众号openId',
  `wxappOpenId` varchar(100) DEFAULT NULL COMMENT '小程序opendId',
  `UnionID` varchar(400) DEFAULT NULL COMMENT '微信唯一标识',
  `CreateTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `IsDelete` int(1) DEFAULT '0' COMMENT '0-未删，1-已删',
  PRIMARY KEY (`Id`),
  KEY `index_FId` (`FId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4;