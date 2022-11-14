using System;

namespace CouponFollow.TestTask.PageObjects.DomainObjects
{
    public class StaffPickCoupon
    {
        public string MerchantName { get; init; }
        public Deal Deal { get; init; }

        public override bool Equals(object obj)
        {
            return obj is StaffPickCoupon other && other.GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MerchantName, Deal);
        }

        public override string ToString()
        {
            return string.Format("Deal for store {0} is {1}", MerchantName, Deal);
        }
    }
}