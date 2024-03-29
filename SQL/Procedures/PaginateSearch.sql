﻿USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[LicenseVerification_SearchBy_UnexpiredLicenseType]    Script Date: 7/29/2022 21:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LicenseVerification_SearchBy_UnexpiredLicenseType]
				@PageIndex int
				,@PageSize int
				,@LicenseTypesId int
				,@DateExpires int

AS

/*
 
DECLARE			@PageIndex int = 0
				,@PageSize int = 1
				,@LicenseTypesId int = 1
				,@DateExpires int = 202207

EXECUTE LicenseVerification_SearchBy_UnexpiredLicenseType
				@PageIndex   
				,@PageSize 
				,@LicenseTypesId 
				,@DateExpires

*/

BEGIN

DECLARE			@offset int = @PageIndex * @PageSize

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
				,uv.Url
				,uv.DateExpires
				,up.Id AS UserProfileId
				,uv.[CreatedBy]
				,up.FirstName
				,up.LastName
				,up.Mi
				,up.AvatarUrl
				,uv.DateCreated
				,uv.DateModified
				,TotalCount = COUNT(1) OVER()

	
	FROM UserLicense AS uv INNER JOIN LicenseTypes AS lt
	ON lt.Id = uv.LicenseTypesId INNER JOIN UserProfiles AS up
	ON up.Id = uv.UserId INNER JOIN Locations AS l
	ON l.Id = uv.LocationsId INNER JOIN States AS s
	ON s.Id = uv.LicenseStateId INNER JOIN LocationTypes AS loct
	ON loct.Id = l.LocationTypeId INNER JOIN ProfessionTypes AS pt
	ON pt.Id = up.ProfessionTypeId
	
	WHERE uv.LicenseTypesId = @LicenseTypesId AND uv.DateExpires >= @DateExpires
	ORDER BY uv.Id
	OFFSET @offset ROWS
	FETCH NEXT @PageSize ROWS ONLY

 END
GO
