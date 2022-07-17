using Sabio.Models.Domain.FAQs.Listings;
using Sabio.Models.Domain.Listings;
using Sabio.Models.Domain.ListingVerification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.ListingVerification
{
    public class ListingVerification
    {
        public int Id { get; set; }
        public string WiFiDocumentUrl { get; set; }
        public string InsuranceDocumentUrl { get; set; }
        public string LocationDocumentUrl { get; set; }
        public int ApprovedBy { get; set; }  
        public int CreatedBy { get; set; }   
        public string Notes { get; set; }   
        public LocationTypes LocationTypes  { get; set; }  
        public Listing Listing { get; set; }
     
    }
}
