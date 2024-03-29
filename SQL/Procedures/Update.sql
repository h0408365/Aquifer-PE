﻿USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[LicenseVerification_Update]    Script Date: 7/29/2022 21:30:02 ******/
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

CREATE PROCEDURE [dbo].[LicenseVerification_Update]
					@LicenseTypesId int
					,@UserId int
					,@LocationsId int
					,@LicenseStateId int
					,@Url nvarchar(Max)
					,@DateExpires int
					,@ModifiedBy int
					,@Id int 

AS

/* --TEST CODE --

DECLARE				@Id int = 12

Declare				@LicenseTypesId int = 6
					,@UserId int = 32
					,@LocationsId int = 4
					,@LicenseStateId int = 4
					,@Url nvarchar(Max) = 'https://sabio-training.s3-us-west-2.amazonaws.com/bbe84c23-2bbf-440d-b8bd-a6b519fe3344_API%20Help%20info.txthttps://sabio-training.s3-us-west-2.amazonaws.com/b1a079fb-6507-430f-a61f-06107fe1ff31_Test%201.pdf'
					,@DateExpires int = 2
					,@ModifiedBy int = 1

Execute dbo.LicenseVerification_Update
					@LicenseTypesId
					,@UserId 
					,@LocationsId 
					,@LicenseStateId 
					,@Url 
					,@DateExpires 
					,@ModifiedBy 
					,@Id 

SELECT * FROM UserLicense
WHERE Id = @Id



*/ --TEST CODE--

BEGIN

DECLARE @datNow datetime2 = GETUTCDATE()

UPDATE dbo.UserLicense
SET					
					[LicenseTypesId] = @LicenseTypesId	
					,[UserId] = @UserId
					,[LocationsId] = @LocationsId
					,[LicenseStateId] = @LicenseStateId
					,[Url] = @Url
					,[DateExpires] = @DateExpires
					,[ModifiedBy] = @ModifiedBy
					,[DateModified] = @datNow
					

WHERE				Id = @Id AND [UserId] = @UserId 
	

END
GO
