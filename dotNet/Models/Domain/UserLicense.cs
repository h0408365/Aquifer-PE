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
        public LookUp LicenseType { get; set; }
        public UserProfile UserProfile { get; set; }
        public Location Location { get; set; }
        public State State { get; set; }
        public string Url { get; set; }
        public int DateExpires { get; set; }
        public UserProfileBase CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }   
        public DateTime DateModified { get; set; }

    }
}
