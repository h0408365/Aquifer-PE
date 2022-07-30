using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.LicenseVerification
{
    public class LicenseVerificationUpdateRequest : LicenseVerificationAddRequest
    {
        public int Id { get; set; }
    }
}
