using Sabio.Models;
using Sabio.Models.Domain.ListingVerification;
using Sabio.Models.Requests.ListingVerifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services.Interfaces
{
    public interface IListingVerificationService
    {
        int Insert(ListingVerificationAddRequest model);
        void Update(ListingVerificationUpdateRequest model, int id);
        Paged<ListingVerification> GetPaginate(int pageIndex, int pageSize);
        Paged<ListingVerification> GetCreatedBy(int pageIndex, int pageSize, int createdBy);
        ListingVerification Get(int Id);
    }
}
