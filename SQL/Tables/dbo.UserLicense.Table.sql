USE [CnmPro]
GO
/****** Object:  Table [dbo].[UserLicense]    Script Date: 7/29/2022 21:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLicense](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LicenseTypesId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[LocationsId] [int] NOT NULL,
	[LicenseStateId] [int] NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[DateExpires] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserLicense] ADD  CONSTRAINT [DF_UserLicense_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[UserLicense] ADD  CONSTRAINT [DF_UserLicense_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[UserLicense]  WITH CHECK ADD  CONSTRAINT [FK_UserLicense_LicenseTypes] FOREIGN KEY([LicenseTypesId])
REFERENCES [dbo].[LicenseTypes] ([Id])
GO
ALTER TABLE [dbo].[UserLicense] CHECK CONSTRAINT [FK_UserLicense_LicenseTypes]
GO
ALTER TABLE [dbo].[UserLicense]  WITH CHECK ADD  CONSTRAINT [FK_UserLicense_Locations] FOREIGN KEY([LocationsId])
REFERENCES [dbo].[Locations] ([Id])
GO
ALTER TABLE [dbo].[UserLicense] CHECK CONSTRAINT [FK_UserLicense_Locations]
GO
ALTER TABLE [dbo].[UserLicense]  WITH CHECK ADD  CONSTRAINT [FK_UserLicense_States] FOREIGN KEY([LicenseStateId])
REFERENCES [dbo].[States] ([Id])
GO
ALTER TABLE [dbo].[UserLicense] CHECK CONSTRAINT [FK_UserLicense_States]
GO
ALTER TABLE [dbo].[UserLicense]  WITH CHECK ADD  CONSTRAINT [FK_UserLicense_UserProfiles] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfiles] ([Id])
GO
ALTER TABLE [dbo].[UserLicense] CHECK CONSTRAINT [FK_UserLicense_UserProfiles]
GO
