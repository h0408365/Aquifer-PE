using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.LicenseVerification
{
    public class LicenseVerificationAddRequest
    {
        [Required]
        public int LicenseTypesId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int LocationsId { get; set; }
        [Required]
        public int LicenseStateId { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int DateExpires { get; set; }
        public int ModifiedBy { get; set; }
    }
}
