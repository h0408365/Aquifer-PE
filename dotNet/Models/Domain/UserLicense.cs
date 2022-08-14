using Sabio.Models.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.LicenseVerification
{
    public class UserLicense
    {
        public int Id { get; set; }
        public int LicenseTypesId { get; set; }
        public LookUp LicenseType { get; set; }
        public int UserId { get; set; }
        public UserProfileBase CreatedBy { get; set; }
        public int LocationsId { get; set; }
        public int LicenseStateId { get; set; }
        public string Url { get; set; }
        public int DateExpires { get; set; }
        public DateTime DateCreated { get; set; }   
        public DateTime DateModified { get; set; }

    }
}
