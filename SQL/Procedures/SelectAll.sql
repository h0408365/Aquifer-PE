USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[LicenseTypes_SelectAll]    Script Date: 7/29/2022 21:30:02 ******/
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

CREATE PROCEDURE [dbo].[LicenseTypes_SelectAll]

AS

/*

EXECUTE dbo.LicenseTypes_GetAll

*/

BEGIN

SELECT			[Id]
				,[Name]

FROM dbo.LicenseTypes

END

GO
