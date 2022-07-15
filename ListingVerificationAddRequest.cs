using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.ListingVerifications
{
    public class ListingVerificationAddRequest
    {
        [Required]
        public int ListingId { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string WiFiDocumentUrl { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string InsuranceDocumentUrl { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string LocationDocumentUrl { get; set; }
        public string ApprovedBy { get; set; }
        public string CreatedBy { get; set; }
        public string Notes { get; set; }
    }
}
