﻿using Sabio.Data;
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

        #region - SelectbyUnexpiredLicenseType -
        public List<UserLicense> SelectbyUnexpiredLicenseType(int LicenseTypeId, int DateExpires)
        {
            string procName = "[dbo].[LicenseVerification_GetBy_UnexpiredLicenseType]";
            List<UserLicense> list = null;
            _data.ExecuteCmd(procName,
                delegate (SqlParameterCollection inputParams)
                {
                    inputParams.AddWithValue("@LicenseTypeId", LicenseTypeId);
                    inputParams.AddWithValue("@DateExpires", DateExpires);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    UserLicense aUserLicense = MapUserLicenseData(reader, ref startingIndex);

                    if (list == null)
                    {
                        list = new List<UserLicense>();
                    }
                    list.Add(aUserLicense);
                });
            return list;
        }
        #endregion

        #region - Create -
        public int Create(LicenseVerificationAddRequest model)
        {
            int id = 0;
            string procName = "[dbo].[LicenseVerification_Create]";
            _data.ExecuteNonQuery(procName,
                delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;
                    col.Add(idOut);
                },
                returnParameters: delegate (SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@Id"].Value;
                    int.TryParse(oId.ToString(), out id);
                });
            return id;
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
            userLicense.LicenseType = _lookUp.MapSingleLookUp(reader, ref startingIndex);

            userLicense.UserProfile = new UserProfile();
            userLicense.UserProfile.Id = reader.GetInt32(startingIndex++); 
            userLicense.UserProfile.UserId = reader.GetInt32(startingIndex++);
            userLicense.UserProfile.FirstName = reader.GetString(startingIndex++);
            userLicense.UserProfile.LastName = reader.GetString(startingIndex++);
            userLicense.UserProfile.Mi = reader.GetString(startingIndex++);
            userLicense.UserProfile.AvatarUrl = reader.GetString(startingIndex++);
            userLicense.UserProfile.ProfessionType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            userLicense.UserProfile.DOB = reader.GetDateTime(startingIndex++);
            userLicense.UserProfile.Email = reader.GetString(startingIndex++);
            userLicense.UserProfile.Phone = reader.GetString(startingIndex++);
            userLicense.UserProfile.LicenseNumber = reader.GetString(startingIndex++);
            userLicense.UserProfile.YearsOfExperience = reader.GetString(startingIndex++);
            userLicense.UserProfile.DesiredHourlyRate = reader.GetString(startingIndex++);
            userLicense.UserProfile.IsActive = reader.GetBoolean(startingIndex++);
           

            userLicense.Location = new Location();
            userLicense.Location.Id = reader.GetInt32(startingIndex++);
            userLicense.Location.LocationType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            userLicense.Location.LineOne = reader.GetString(startingIndex++);
            userLicense.Location.LineTwo = reader.GetString(startingIndex++);
            userLicense.Location.City = reader.GetString(startingIndex++);
            userLicense.Location.Zip = reader.GetString(startingIndex++);           
            userLicense.Location.Latitude = reader.GetSafeDouble(startingIndex++);
            userLicense.Location.Longitude = reader.GetSafeDouble(startingIndex++);

            userLicense.State = new State();
            userLicense.State.Id = reader.GetInt32(startingIndex++);    
            userLicense.State.Code = reader.GetString(startingIndex++);
            userLicense.State.Name = reader.GetString(startingIndex++);

            userLicense.Url = reader.GetString(startingIndex++);
            userLicense.DateExpires = reader.GetInt32(startingIndex++);
            userLicense.CreatedBy = _profileMapper.Map(reader, ref startingIndex);
            userLicense.DateCreated = reader.GetDateTime(startingIndex++);
            userLicense.DateModified = reader.GetDateTime(startingIndex++);

            return userLicense;
        } 
        #endregion

        #region - AddCommonParams -
        private static void AddCommonParams(LicenseVerificationAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@LicenseTypesId", model.LicenseTypesId);
            col.AddWithValue("@UserId", model.UserId);
            col.AddWithValue("@LocationsId", model.LocationsId);
            col.AddWithValue("@LicenseStateId", model.LicenseStateId);
            col.AddWithValue("@Url", model.Url);
            col.AddWithValue("@DateExpires", model.DateExpires);
            col.AddWithValue("@ModifiedBy", model.ModifiedBy);

        }
        #endregion
    }

}