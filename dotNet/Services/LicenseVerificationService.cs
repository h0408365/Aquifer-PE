using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models.Domain;
using Sabio.Models.Domain.LicenseVerification;
using Sabio.Models.Requests.LicenseVerification;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services.LicenseVerification
{
    public class LicenseVerificationService : ILicenseVerificationService
    {
        private IAuthenticationService<int> _authenticationService;
        private IDataProvider _data; 
        IUserProfileMapper _profileMapper = null;
        ILookUpService _lookUp = null;
        public LicenseVerificationService(IAuthenticationService<int> authService, IDataProvider data, IUserProfileMapper profileMapper, ILookUpService lookUp)
        {
            _authenticationService = authService;
            _data = data;
            _profileMapper = profileMapper;
            _lookUp = lookUp;
            
        }

        #region - SelectbyUserLicenseId -
        public UserLicense SelectbyUserLicenseId(int Id)
        {
            string procName = "[dbo].[LicenseVerification_SelectBy_UserLicenseId]"; 
            UserLicense userLicense = null;
            _data.ExecuteCmd(procName, 
                delegate (SqlParameterCollection inputParams) 
                {
                    inputParams.AddWithValue("@Id", Id); 
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0; 
                    userLicense = MapUserLicenseData(reader, ref startingIndex);
                });
            return userLicense;
        }
        #endregion

        #region - Update -
        public void Update(LicenseVerificationUpdateRequest model, int id) 
        {
            string procName = "[dbo].[LicenseVerification_Update]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParams(model, col);
                col.AddWithValue("@Id", id);

            }, returnParameters: null);
        }
        #endregion

        #region MapUserLicense
        private UserLicense MapUserLicenseData(IDataReader reader, ref int startingIndex)
        {
            UserLicense userLicense = new UserLicense(); 


            userLicense.Id = reader.GetInt32(startingIndex++);
            userLicense.LicenseTypesId = reader.GetInt32(startingIndex++);
            userLicense.LicenseType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            userLicense.UserId = reader.GetInt32(startingIndex++);
            userLicense.CreatedBy = _profileMapper.Map(reader, ref startingIndex);
            userLicense.LocationsId = reader.GetInt32(startingIndex++);
            userLicense.LicenseStateId = reader.GetInt32(startingIndex++);  
            userLicense.Url = reader.GetString(startingIndex++);
            userLicense.DateExpires = reader.GetInt32(startingIndex++);
            userLicense.DateCreated = reader.GetDateTime(startingIndex++);
            userLicense.DateModified = reader.GetDateTime(startingIndex++);

            return userLicense;
        } 
        #endregion

        #region - AddCommonParams -
        private static void AddCommonParams(LicenseVerificationAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@LicenseTypesId", model.LicenseTypesId);
            col.AddWithValue("@LocationsId", model.LocationsId);
            col.AddWithValue("@LicenseStateId", model.LicenseStateId);
            col.AddWithValue("@Url", model.Url);
            col.AddWithValue("@DateExpires", model.DateExpires);

        }
        #endregion
    }

}
