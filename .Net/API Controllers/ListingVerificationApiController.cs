using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain.ListingVerification;
using Sabio.Models.Requests.ListingVerifications;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/docverify")]
    [ApiController]
    public class ListingVerificationApiController : BaseApiController
    { 
        private IListingVerificationService _service = null;
        private IAuthenticationService<int> _authService = null;

        public ListingVerificationApiController(IListingVerificationService service, ILogger<PingApiController> logger, IAuthenticationService<int> authentication) : base(logger)
        {
            _service = service;
            _authService = authentication;
        }

        #region - Insert -
        [HttpPost]
        public ActionResult<ItemResponse<int>> Insert(ListingVerificationAddRequest model)
        {
            ObjectResult result = null;
            try
            {
                int id = _service.Insert(model);
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

        #region - Update -
        [HttpPut("{id:int}")]
        public ActionResult<SuccessResponse> Update(ListingVerificationUpdateRequest model, int id)
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

        #region - GetPaginate -
        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<ListingVerification>>> GetPaginate(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                Paged<ListingVerification> page = _service.GetPaginate(pageIndex, pageSize);
                response = new ItemResponse<Paged<ListingVerification>> { Item = page };

            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Internal Server Error; there was an issue with the server {ex.Message}");

            }
            return StatusCode(code, response);
        }
        #endregion

        #region - GetCreatedBy -
        [HttpGet("createdby")]
        public ActionResult<ItemResponse<Paged<ListingVerification>>> GetByCreatedBy(int pageIndex, int pageSize)
        {
            int code = 200;
            int userId = _authService.GetCurrentUserId();
            BaseResponse response = null;
            try
            {
                Paged<ListingVerification> page = _service.GetCreatedBy(pageIndex, pageSize, userId);
                response = new ItemResponse<Paged<ListingVerification>> { Item = page };

            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Internal Server Error; there was an issue with the server {ex.Message}");

            }
            return StatusCode(code, response);
        }
        #endregion

        #region - GetById -
        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<ListingVerification>> Get(int id)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                ListingVerification data = _service.Get(id);
                response = new ItemResponse<ListingVerification> { Item = data };
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Internal Server Error; there was an issue with the server {ex.Message}");
            }
            return StatusCode(code, response);
        } 
        #endregion

    }


}
