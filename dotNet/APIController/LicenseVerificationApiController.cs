using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models.Domain.LicenseVerification;
using Sabio.Models.Requests.LicenseVerification;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers.LicenseVerification
{
    [Route("api/licenses")]
    [ApiController]
    public class LicenseVerificationApiController : BaseApiController
    {
        private ILicenseVerificationService _service = null; 
        private IAuthenticationService<int> _authService = null;

        public LicenseVerificationApiController(ILicenseVerificationService service, ILogger<PingApiController> logger, IAuthenticationService<int> authentication) : base(logger)
        {
            _service = service;
            _authService = authentication;
        }

        #region - SelectbyUserLicenseId -
        [HttpGet("verify/{id:int}")]
        public ActionResult<ItemResponse<UserLicense>> SelectbyUserLicenseId(int id)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                UserLicense data = _service.SelectbyUserLicenseId(id);
                response = new ItemResponse<UserLicense> { Item = data };
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Internal Server Error; there was an issue with the server {ex.Message}");
            }
            return StatusCode(code, response);
        }
        #endregion

        #region - Update -
        [HttpPut("verify/{id:int}")]
        public ActionResult<SuccessResponse> Update(LicenseVerificationUpdateRequest model, int id)
        {
            int code = 200;
            int userId = _authService.GetCurrentUserId();
            BaseResponse response = null;
            try
            {
                _service.Update(model, id);
                response = new SuccessResponse();
            }

            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Generic Error {ex.Message}");
            }

            return StatusCode(code, response);
        }
        #endregion

        #region - SelectbyUnexpiredLicenseType -
        [HttpGet("filter")]
        public ActionResult<ItemsResponse<UserLicense>> SelectbyUnexpiredLicenseType(int LicenseTypeId, int DateExpires)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<UserLicense> list = _service.SelectbyUnexpiredLicenseType(LicenseTypeId, DateExpires);
                if (list == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource Not Found.");
                }
                else
                {
                    response = new ItemsResponse<UserLicense> { Items = list };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }
        #endregion

        #region - Create -
        [HttpPost]
        public ActionResult<ItemResponse<int>> Create(LicenseVerificationAddRequest model)
        {
            ObjectResult result = null;
            try
            {
                int id = _service.Create(model);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };
                result = Created201(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);
                result = StatusCode(500, response);
            }

            return result;
        }
        #endregion

        

    }
}
