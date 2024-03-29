﻿USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[LicenseVerification_SelectBy_UserLicenseId]    Script Date: 7/29/2022 21:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-------------------------------------------------
-- Author: Harold Tran
-- Create date: 20-July-2022
-- Description: Created procedure
-- Code Review 

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
--------------------------------------------------

CREATE PROCEDURE [dbo].[LicenseVerification_SelectBy_UserLicenseId]
				@Id int

AS

/* --Test Code--

DECLARE			@Id int = 10
EXECUTE LicenseVerification_SelectBy_UserLicenseId
				@Id

SELECT * FROM dbo.UserLicense

*/

BEGIN

SELECT			uv.Id as UserLicenseId
				,lt.Id AS LicenseTypesId
				,lt.Name LicensTypesName
				,up.Id AS UserProfileId
				,uv.UserId
				,up.FirstName
				,up.LastName
				,up.Mi
				,up.AvatarUrl
				,pt.Id AS ProfessionTypeId
				,pt.Name AS ProfessionTypeName
				,up.DOB
				,up.Email
				,up.Phone
				,up.LicenseNumber
				,up.YearsOfExperience
				,up.DesiredHourlyRate
				,up.IsActive
				,l.Id AS LocationsId
				,loct.Id AS LocationTypeId
				,loct.Name AS LocationTypeName
				,l.LineOne
				,l.LineTwo
				,l.City
				,l.Zip
				,l.Latitude
				,l.Longitude
				,s.Id AS StatesId
				,s.Code
				,S.Name
				,uv.[Url]
				,uv.DateExpires
				,up.Id AS UserProfileId
				,uv.[CreatedBy]
				,up.FirstName
				,up.LastName
				,up.Mi
				,up.AvatarUrl
				,uv.DateCreated
				,uv.DateModified

	
	FROM UserLicense AS uv INNER JOIN LicenseTypes AS lt
	ON lt.Id = uv.LicenseTypesId INNER JOIN UserProfiles AS up
	ON up.Id = uv.UserId INNER JOIN Locations AS l
	ON l.Id = uv.LocationsId INNER JOIN States AS s
	ON s.Id = uv.LicenseStateId INNER JOIN LocationTypes AS loct
	ON loct.Id = l.LocationTypeId INNER JOIN ProfessionTypes AS pt
	ON pt.Id = up.ProfessionTypeId
	WHERE uv.Id = @Id

END 
				

			
GO
