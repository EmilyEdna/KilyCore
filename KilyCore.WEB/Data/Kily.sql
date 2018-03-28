USE [Kily]
GO
INSERT [dbo].[CompanyIdent] ([Id], [AuditType], [CommunityCode], [CompanyId], [CompanyName], [CompanyType], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [IdentNo], [IdentStar], [IdentYear], [IsDelete], [LinkPhone], [Remark], [Representative], [RepresentativeCard], [SendCard], [SendPerson], [UpdateTime], [UpdateUser]) VALUES (N'f7c3dd2f-2275-43ed-b4a9-f98f34ac8ab6', 40, NULL, N'afb6d62a-acee-46f8-ad56-f5e443f2c7c1', N'成都研成科技', 50, CAST(N'2018-03-15T10:11:47.200' AS DateTime), N'49FEAB9B-B59C-4C78-9B58-F42D2206BFC4', NULL, NULL, N'V20180315101239', 10, 1, 0, N'83412331', N'四川省第一家溯源企业', NULL, NULL, NULL, NULL, CAST(N'2018-03-21T11:13:34.567' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b')
INSERT [dbo].[CompanyInfo] ([Id], [AuditType], [Certification], [CommunityCode], [CompanyAccount], [CompanyAddress], [CompanyName], [CompanyPhone], [CompanyType], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [Discription], [HonorCertification], [IsDelete], [NetAddress], [PassWord], [ProductionAddress], [SellerAddress], [TypePath], [UpdateTime], [UpdateUser], [VideoAddress]) VALUES (N'afb6d62a-acee-46f8-ad56-f5e443f2c7c1', 20, NULL, NULL, N'yckj', N'四川省成都市高新区天府五街美年广场', N'成都研成科技', N'83412331', 50, CAST(N'2018-03-06T11:27:38.047' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, N'全国第一家溯源企业', NULL, 0, N'www.cfdacx.com', N'admin123', NULL, NULL, N'89d4479c-6428-4d5e-b19c-c7c47db7fb8e,50e5588e-a466-49e1-ac5a-a2052792cf82,52bf3e24-0704-4be5-9314-183b08cb55f5,2bffa1e3-014e-4a42-a27b-b814b97fcc16', CAST(N'2018-03-13T16:00:11.443' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL)
INSERT [dbo].[SystemAdmin] ([Id], [Account], [AccountType], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [Email], [IdCard], [IsDelete], [PassWord], [Phone], [RoleAuthorType], [TrueName], [TypePath], [UpdateTime], [UpdateUser]) VALUES (N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', N'admin', 10, CAST(N'2018-02-23T17:24:58.670' AS DateTime), NULL, NULL, NULL, N'admin@163.com', N'362526200012017266', 0, N'admin', N'18987112298', N'ef5071b8-6471-4fbd-8d6f-6e51f05314fc', N'超级管理员', N'89d4479c-6428-4d5e-b19c-c7c47db7fb8e,50e5588e-a466-49e1-ac5a-a2052792cf82,52bf3e24-0704-4be5-9314-183b08cb55f5,2bffa1e3-014e-4a42-a27b-b814b97fcc16', NULL, NULL)
INSERT [dbo].[SystemAdminAttach] ([Id], [AdminId], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [EndTime], [IsDelete], [IsPay], [Money], [PayUser], [StartTime], [UpdateTime], [UpdateUser]) VALUES (N'2fd7c9a5-bb5b-4033-bd11-cfd2ae4275a7', N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', CAST(N'2018-03-27T16:44:35.917' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, CAST(N'2028-03-27T16:44:00.000' AS DateTime), 0, 1, CAST(5000000.00 AS Decimal(18, 2)), N'超级管理员', CAST(N'2018-03-27T16:43:27.000' AS DateTime), NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'25ccc7d6-7871-45f8-497f-08d52b62443c', CAST(N'2018-03-01T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 1, 0, 10, NULL, N'fa fa-cog', N'5f50d155-3f44-48bb-99d9-c94d6c06a873', N'系统管理', NULL, NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'd140628e-f23f-42b4-4980-08d52b62443c', CAST(N'2018-03-02T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/SysManage/System/SysMenu', NULL, N'38723482-f56b-45d0-a745-503af297dfa0', N'菜单管理', N'5f50d155-3f44-48bb-99d9-c94d6c06a873', NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'fe4a7ac4-1bda-406e-5ac1-08d52e67e1cc', CAST(N'2018-03-04T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/SysManage/System/SysRole', NULL, N'36174afd-bd13-4bfa-ad5b-7d2e6964b805', N'角色管理', N'5f50d155-3f44-48bb-99d9-c94d6c06a873', NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'a5418686-90a2-4a9e-9a03-0d7b113cc9af', CAST(N'2018-03-16T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/ComManage/Company/ReviewCode', NULL, N'fc4b7647-7de1-4405-a32c-c97b6102b604', N'追溯码', N'ae4a4662-4e1c-4280-bad9-e3e841348790', NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'8fc57926-4d8e-421a-815d-0df7035024d4', CAST(N'2018-03-08T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 1, 0, 10, NULL, N'fa fa-cny', N'd2796bca-2434-46b3-8d80-0e90deedcf5c', N'财务管理', NULL, NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'9e8ac760-e7df-43e1-b151-174b52e88c42', CAST(N'2018-03-06T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/SysManage/System/SysUser', NULL, N'ac10e21c-0c77-4c82-a003-c039071c4f28', N'用户管理', N'5f50d155-3f44-48bb-99d9-c94d6c06a873', NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'54efc365-3b4e-4d3b-ad92-1e9ec7c80c8c', CAST(N'2018-03-15T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/ComManage/Company/IdentAudit', NULL, N'baac8120-d191-45bf-9c6b-7b6597220257', N'认证审核', N'ae4a4662-4e1c-4280-bad9-e3e841348790', NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'8eaf7e5f-6566-42bf-8901-286852fd0e8c', CAST(N'2018-03-10T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/ComManage/Company/AuditProduct', NULL, N'7f381b36-b5bc-40ac-825f-3f97407b91b1', N'产品审核', N'ae4a4662-4e1c-4280-bad9-e3e841348790', NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'b99bc802-4dcc-46a2-8d9a-5eeafe70b8d7', CAST(N'2018-03-14T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/MoneyManage/Finance/IdentPay', NULL, N'57d8a93e-f278-4d8d-a71e-e60bb188e057', N'认证缴费', N'd2796bca-2434-46b3-8d80-0e90deedcf5c', NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'178b9a07-9cf2-42b7-b96d-77bf0e03b616', CAST(N'2018-03-05T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/SysManage/System/SysCompanyRole', NULL, N'278c6a45-0497-42c7-a4cd-45c5203041ce', N'企业角色', N'5f50d155-3f44-48bb-99d9-c94d6c06a873', NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'734a56ca-451c-4d4c-82af-7808a79d88f6', CAST(N'2018-03-13T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/ComManage/Company/AuditData', NULL, N'0b50ec8b-0dc5-4e31-941f-c789ab0191f6', N'资料审核', N'ae4a4662-4e1c-4280-bad9-e3e841348790', NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'ef8af488-0892-4aa6-a287-9f1b12a3e787', CAST(N'2018-03-03T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/SysManage/System/SysCompanyMenu', NULL, N'fcc1c859-7d1a-441e-b98a-9f554830a00a', N'企业菜单', N'5f50d155-3f44-48bb-99d9-c94d6c06a873', NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'7cadd24c-bfa1-41fd-8dbc-bdeebe148dbe', CAST(N'2018-03-07T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/SysManage/System/SysQuartz', NULL, N'ee3d6966-d4df-4c0b-a49f-c13e3bd2e43e', N'任务调度', N'5f50d155-3f44-48bb-99d9-c94d6c06a873', NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'765c133a-59d4-40a1-9fba-da97c6a85f22', CAST(N'2018-03-09T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/MoneyManage/Finance/JoinPay', NULL, N'267ac168-395c-406e-93ee-e694a0170111', N'加盟缴费', N'd2796bca-2434-46b3-8d80-0e90deedcf5c', NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'6784548b-dadc-4e3a-a0f1-df87f9697570', CAST(N'2018-03-12T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 1, 0, 10, NULL, N'fa fa-home', N'ae4a4662-4e1c-4280-bad9-e3e841348790', N'企业管理', NULL, NULL, NULL)
INSERT [dbo].[SystemMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'edec6394-e74c-46af-8e1b-f0e528e8beb7', CAST(N'2018-03-11T00:00:00.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/MoneyManage/Finance/GoodsPay', NULL, N'86676fb5-8987-4cef-9886-2f61e7ba5ebf', N'物码缴费', N'd2796bca-2434-46b3-8d80-0e90deedcf5c', NULL, NULL)
INSERT [dbo].[SystemRoleAuthor] ([Id], [AuthorMenuPath], [AuthorName], [AuthorRoleLvId], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [IsDelete], [TypePath], [UpdateTime], [UpdateUser]) VALUES (N'ef5071b8-6471-4fbd-8d6f-6e51f05314fc', NULL, N'超级管理员', N'19e9c88d-10ee-456b-b70c-ed105b87502e', CAST(N'2018-03-05T15:01:32.953' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, N'dc8a5e05-7508-4d20-9c2d-431c670d3987,b3427dab-3c92-4ae9-88bc-cba081b4fd10,25f29524-a78e-42f0-90b2-391c39ebbf9d', NULL, NULL)
INSERT [dbo].[SystemRoleLevel] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [IsDelete], [LvName], [RoleLv], [UpdateTime], [UpdateUser]) VALUES (N'b0bc6379-1f62-4460-879a-2264a28411c3', CAST(N'2018-02-06T14:22:58.750' AS DateTime), NULL, NULL, NULL, 0, N'市级运营中心', 40, NULL, NULL)
INSERT [dbo].[SystemRoleLevel] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [IsDelete], [LvName], [RoleLv], [UpdateTime], [UpdateUser]) VALUES (N'b0c231b3-bf4f-4409-81ff-2a5bacdc7341', CAST(N'2018-02-06T14:22:27.263' AS DateTime), NULL, NULL, NULL, 0, N'省级运营中心', 30, NULL, NULL)
INSERT [dbo].[SystemRoleLevel] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [IsDelete], [LvName], [RoleLv], [UpdateTime], [UpdateUser]) VALUES (N'7cbf21db-765a-4104-bd0e-4eed44f71396', CAST(N'2018-02-06T14:22:10.710' AS DateTime), NULL, NULL, NULL, 0, N'全国运营中心', 20, NULL, NULL)
INSERT [dbo].[SystemRoleLevel] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [IsDelete], [LvName], [RoleLv], [UpdateTime], [UpdateUser]) VALUES (N'624ced1a-8ebf-4769-ac07-742d6b73c0bf', CAST(N'2018-02-06T14:23:23.847' AS DateTime), NULL, NULL, NULL, 0, N'乡镇运营中心', 60, NULL, NULL)
INSERT [dbo].[SystemRoleLevel] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [IsDelete], [LvName], [RoleLv], [UpdateTime], [UpdateUser]) VALUES (N'769c360c-62cd-4a17-8d76-86ef603186d8', CAST(N'2018-02-06T14:23:14.643' AS DateTime), NULL, NULL, NULL, 0, N'区级运营中心', 50, NULL, NULL)
INSERT [dbo].[SystemRoleLevel] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [IsDelete], [LvName], [RoleLv], [UpdateTime], [UpdateUser]) VALUES (N'19e9c88d-10ee-456b-b70c-ed105b87502e', CAST(N'2018-02-06T14:21:48.530' AS DateTime), NULL, NULL, NULL, 0, N'超级管理员', 10, NULL, NULL)
INSERT [dbo].[SystemCompanyMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'180b3988-b266-4502-a542-00249cd331b5', CAST(N'2018-03-28T17:09:06.337' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 1, 0, 10, NULL, N'fa fa-cog', N'19e77674-4b45-49ce-8e8e-4715ca8ee36a', N'基础管理', NULL, NULL, NULL)
INSERT [dbo].[SystemCompanyMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'e8822cfd-59eb-4f2f-85ef-092eefceb300', CAST(N'2018-03-28T17:16:04.273' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/WebCompanyManage/Base/UserManage', NULL, N'60a7153a-2c9b-4073-9151-a645a9ce27fa', N'人员管理', N'19e77674-4b45-49ce-8e8e-4715ca8ee36a', NULL, NULL)
INSERT [dbo].[SystemCompanyMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'fffbfeed-c1bb-4320-85a6-43fe6f52414d', CAST(N'2018-03-28T17:29:39.807' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/WebCompanyManage/Develop/Planting', NULL, N'021f12fb-5e02-4930-85ed-b1171f02882c', N'施养管理', N'd8f415fa-bb60-4159-ac65-ffa9eb081bd6', NULL, NULL)
INSERT [dbo].[SystemCompanyMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'45ccd704-8e55-4130-8212-4e527031228b', CAST(N'2018-03-28T17:10:09.647' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/WebCompanyManage/Base/CompanyData', NULL, N'9ce2cede-2491-4b9e-82dc-b9914d0b5ef3', N'企业资料', N'19e77674-4b45-49ce-8e8e-4715ca8ee36a', NULL, NULL)
INSERT [dbo].[SystemCompanyMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'1d9eb61d-002e-46a1-91ce-5c4818679b49', CAST(N'2018-03-28T17:23:46.000' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 1, 0, 10, NULL, N'fa fa-leaf', N'd8f415fa-bb60-4159-ac65-ffa9eb081bd6', N'成长档案', NULL, NULL, NULL)
INSERT [dbo].[SystemCompanyMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'067a7bf5-4737-40f0-9ceb-6c873d14274a', CAST(N'2018-03-28T17:30:40.343' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/WebCompanyManage/Develop/Drug', NULL, N'97061aec-336b-4422-9585-50f7936859ac', N'农药疫情', N'd8f415fa-bb60-4159-ac65-ffa9eb081bd6', NULL, NULL)
INSERT [dbo].[SystemCompanyMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'c541c859-6382-4518-a4e7-c8673d5a86af', CAST(N'2018-03-28T17:24:23.357' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/WebCompanyManage/Develop/Ambient', NULL, N'32c6d7ad-fdfd-4fbe-ba09-623ca7304008', N'环境检测', N'd8f415fa-bb60-4159-ac65-ffa9eb081bd6', NULL, NULL)
INSERT [dbo].[SystemCompanyMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'8c832493-13ba-4eb2-afdb-dcaf91689dfd', CAST(N'2018-03-28T17:10:51.447' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/WebCompanyManage/Base/CompanyIdent', NULL, N'd2a4588a-06d4-4cd3-8945-8dc98b523045', N'我要认证', N'19e77674-4b45-49ce-8e8e-4715ca8ee36a', NULL, NULL)
INSERT [dbo].[SystemCompanyMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'db517f85-8bab-4f5a-9cb2-eba00d16e81e', CAST(N'2018-03-28T17:25:52.693' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/WebCompanyManage/Develop/Diary', NULL, N'e69b5f2f-9f3f-4e6b-9945-5b7e5785cf84', N'成长日记', N'd8f415fa-bb60-4159-ac65-ffa9eb081bd6', NULL, NULL)
INSERT [dbo].[SystemCompanyMenu] ([Id], [CreateTime], [CreateUser], [DeleteTime], [DeleteUser], [HasChildrenNode], [IsDelete], [Level], [MenuAddress], [MenuIcon], [MenuId], [MenuName], [ParentId], [UpdateTime], [UpdateUser]) VALUES (N'bb8bd8af-caeb-4e19-81c3-f60307a07034', CAST(N'2018-03-28T17:18:11.623' AS DateTime), N'ab88e16f-41f2-4d4f-9699-98a4d4bccd0b', NULL, NULL, 0, 0, 20, N'/WebCompanyManage/Base/GroupAccount', NULL, N'948d5c2f-307b-4670-abfb-fb44b604e98c', N'集团账户', N'19e77674-4b45-49ce-8e8e-4715ca8ee36a', NULL, NULL)
