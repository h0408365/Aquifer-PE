using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.FAQs.Listings;
using Sabio.Models.Domain.Listings;
using Sabio.Models.Domain.ListingVerification;
using Sabio.Models.Requests.ListingVerifications;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class ListingVerificationService : IListingVerificationService
    {
        private IAuthenticationService<int> _authenticationService;
        private IDataProvider _data;
        public ListingVerificationService(IAuthenticationService<int> authService, IDataProvider data)
        {
            _authenticationService = authService;
            _data = data;
        }

        #region - Insert -
        public int Insert(ListingVerificationAddRequest model)
        {
            int id = 0;
            string procName = "[dbo].[ListingVerification_Insert]";
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
        public void Update(ListingVerificationUpdateRequest model, int id)
        {
            string procName = "[dbo].[ListingVerification_Update]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParams(model, col);
                col.AddWithValue("@Id", id);

            }, returnParameters: null);
        }
        #endregion

        #region - Paginate -
        public Paged<ListingVerification> GetPaginate(int pageIndex, int pageSize)
        {
            string procName = "[dbo].[ListingVerification_Pagination]";
            List<ListingVerification> data = null;
            Paged<ListingVerification> pagedList = null;
            int totalCount = 0;
            _data.ExecuteCmd(procName,
                 delegate (SqlParameterCollection inputParams)
                 {
                     inputParams.AddWithValue("@PageIndex", pageIndex);
                     inputParams.AddWithValue("@PageSize", pageSize);

                 },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    ListingVerification verificationData = MapSingleVerificationData(reader, ref startingIndex);
                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }

                    if (data == null)
                    {
                        data = new List<ListingVerification>();
                    }
                    data.Add(verificationData);
                }
            );

            if (data != null)
            {
                pagedList = new Paged<ListingVerification>(data, pageIndex, pageSize, totalCount);
            }

            return pagedList;
        }
        #endregion

        #region - GetCreatedBy -
        public Paged<ListingVerification> GetCreatedBy(int pageIndex, int pageSize, int createdBy)
        {
            Paged<ListingVerification> pagedResult = null;

            List<ListingVerification> listings = null;

            int totalCount = 0;

            string procName = "[dbo].[ListingVerification_Select_ByCreatedBy]";

            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@PageIndex", pageIndex);
                col.AddWithValue("@PageSize", pageSize);
                col.AddWithValue("@CreatedBy", createdBy);

            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;

                ListingVerification aListing = MapSingleVerificationData(reader, ref startingIndex);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }

                if (listings == null)
                {
                    listings = new List<ListingVerification>();
                }
                listings.Add(aListing);
            });
            if (listings != null)
            {
                pagedResult = new Paged<ListingVerification>(listings, pageIndex, pageSize, totalCount);
            }
            return pagedResult;
        }
        #endregion

        #region - GetById -
        public ListingVerification Get(int Id)
        {
            string procName = "[dbo].[ListingVerification_SelectById]";
            ListingVerification verificationData = null;
            _data.ExecuteCmd(procName,
                delegate (SqlParameterCollection inputParams)
                {
                    inputParams.AddWithValue("@Id", Id);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    verificationData = MapSingleVerificationData(reader, ref startingIndex);
                });
            return verificationData;
        } 
        #endregion

        #region - AddCommonParams -
        private static void AddCommonParams(ListingVerificationAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@ListingId", model.ListingId);
            col.AddWithValue("@WiFiDocumentUrl", model.WiFiDocumentUrl);
            col.AddWithValue("@InsuranceDocumentUrl", model.InsuranceDocumentUrl);
            col.AddWithValue("@LocationDocumentUrl", model.LocationDocumentUrl);
            col.AddWithValue("@ApprovedBy", model.ApprovedBy);
            col.AddWithValue("@CreatedBy", model.CreatedBy);
            col.AddWithValue("@Notes", model.Notes);

        }
        #endregion

        #region - MapSingleVerificationData -
        private static ListingVerification MapSingleVerificationData(IDataReader reader, ref int startingIndex)
        {
            ListingVerification verificationData = new ListingVerification();

            verificationData.Id = reader.GetInt32(startingIndex++);
            verificationData.WiFiDocumentUrl = reader.GetString(startingIndex++);
            verificationData.InsuranceDocumentUrl = reader.GetString(startingIndex++);
            verificationData.LocationDocumentUrl = reader.GetString(startingIndex++);
            verificationData.ApprovedBy = reader.GetInt32(startingIndex++);
            verificationData.CreatedBy = reader.GetInt32(startingIndex++);
            verificationData.Notes = reader.GetString(startingIndex++);

            verificationData.Listing = new Listing();
            verificationData.Listing.Id = reader.GetInt32(startingIndex++);
            verificationData.Listing.InternalName = reader.GetSafeString(startingIndex++);  
            verificationData.Listing.Title = reader.GetSafeString(startingIndex++);
            verificationData.Listing.ShortDescription = reader.GetSafeString(startingIndex++);
            verificationData.Listing.Description = reader.GetSafeString(startingIndex++);
            verificationData.Listing.BedRooms = reader.GetSafeInt16(startingIndex++);
            verificationData.Listing.Baths = reader.GetSafeFloat(startingIndex++);

            verificationData.Listing.HousingType = new HousingType();
            verificationData.Listing.HousingType.Id = reader.GetSafeInt32(startingIndex++);
            verificationData.Listing.HousingType.Name = reader.GetSafeString(startingIndex++);

            verificationData.Listing.AccessType = new AccessType();
            verificationData.Listing.AccessType.Id = reader.GetSafeInt32(startingIndex++);
            verificationData.Listing.AccessType.Name = reader.GetSafeString(startingIndex++);   
            verificationData.Listing.AccessType.Description = reader.GetSafeString(startingIndex++);

            verificationData.Listing.GuestCapacity = reader.GetSafeInt16(startingIndex++);
            verificationData.Listing.CostPerNight = reader.GetSafeInt32(startingIndex++);
            verificationData.Listing.CostPerWeek = reader.GetSafeInt32(startingIndex++);
            verificationData.Listing.CheckInTime = reader.GetSafeTimeSpan(startingIndex++);
            verificationData.Listing.CheckOutTime = reader.GetSafeTimeSpan(startingIndex++);
            verificationData.Listing.DaysAvailable = reader.GetSafeInt32(startingIndex++);

            verificationData.LocationTypes = new LocationTypes();
            verificationData.LocationTypes.Id = reader.GetSafeInt32(startingIndex++);
            verificationData.LocationTypes.Name = reader.GetSafeString(startingIndex++); 

            verificationData.Listing.HasVerifiedOwnership = reader.GetSafeBool(startingIndex++);
            verificationData.Listing.IsActive = reader.GetSafeBool(startingIndex++);
            verificationData.Listing.HousingImages = reader.DeserializeObject<List<HousingImage>>(startingIndex++);

            return verificationData;
        } 
        #endregion
    }
}
