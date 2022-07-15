using Sabio.Models.Domain.Listings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.ListingVerification
{
    public class ListingVerificationData
    {
		public int Id { get; set; }
		public string InternalName { get; set; }
		public string Title { get; set; }
		public string ShortDescription { get; set; }
		public string Description { get; set; }
		public short BedRooms { get; set; }
		public float Baths { get; set; }
		public HousingType HousingType { get; set; }
		public  AccessType AccessType { get; set; }
		public short GuestCapacity { get; set; }
		public int CostPerNight { get; set; }
		public int CostPerWeek { get; set; }
		public TimeSpan CheckInTime { get; set; }
		public TimeSpan CheckOutTime { get; set; }
		public int DaysAvailable { get; set; }
		public int LocationId { get; set; }
		public Boolean HasVerifiedOwnership { get; set; }
		public Boolean IsActive { get; set; }
		public int CreatedBy { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
	}
}
