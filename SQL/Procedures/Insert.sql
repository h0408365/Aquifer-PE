USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[LicenseVerification_Create]    Script Date: 7/29/2022 21:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Harold Tran
-- Create date: 20-July-2022
-- Description: Created Procedure
-- Code Reviewer: 

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note: 
-- =============================================

CREATE PROCEDURE [dbo].[LicenseVerification_Create]
					@LicenseTypesId int
					,@UserId int
					,@LocationsId int
					,@LicenseStateId int
					,@Url nvarchar(Max)
					,@DateExpires int
					,@ModifiedBy int
					,@Id int OUTPUT
					
AS

/* --TEST CODE --

Declare				@LicenseTypesId int = 3
					,@UserId int = 12
					,@LocationsId int = 4
					,@LicenseStateId int = 4
					,@Url nvarchar(Max) = 'https://sabio-training.s3-us-west-2.amazonaws.com/bbe84c23-2bbf-440d-b8bd-a6b519fe3344_API%20Help%20info.txthttps://sabio-training.s3-us-west-2.amazonaws.com/b1a079fb-6507-430f-a61f-06107fe1ff31_Test%201.pdf'
					,@DateExpires int = 1
					,@ModifiedBy int = 1
					,@Id int 

Execute dbo.LicenseVerification_Create
					@LicenseTypesId
					,@UserId 
					,@LocationsId 
					,@LicenseStateId 
					,@Url 
					,@DateExpires 
					,@ModifiedBy 
					,@Id OUTPUT

SELECT * FROM UserLicense


*/ --TEST CODE--

BEGIN

INSERT INTO dbo.UserLicense
					(
					[LicenseTypesId]
					,[UserId]
					,[LocationsId]
					,[LicenseStateId]
					,[Url]
					,[DateExpires]
					,[ModifiedBy]
					)

VALUES				(
					@LicenseTypesId
					,@UserId 
					,@LocationsId 
					,@LicenseStateId 
					,@Url 
					,@DateExpires 
					,@ModifiedBy 
					)

SET					@Id = SCOPE_IDENTITY();

END
GO
