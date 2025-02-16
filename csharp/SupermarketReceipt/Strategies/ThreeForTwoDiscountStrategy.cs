using SupermarketReceipt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketReceipt.Strategies
{
    public class ThreeForTwoDiscountStrategy : IOfferStrategy
    {
        public bool CanApplyOffer(SpecialOfferType offerType)
        {
            return offerType == SpecialOfferType.ThreeForTwo;
        }

        public Discount CalculateDiscount(Product product, Offer offer, int quantity, double unitPrice)
        {
            var requiredItemsForOffer = 3;
            var multiplier = quantity / requiredItemsForOffer;
            Discount? discount = null;
            if (quantity >= 3)
            {
                var discountAmount = quantity * unitPrice - (multiplier * 2 * unitPrice + quantity % 3 * unitPrice);
                discount = new Discount(product, "3 for 2", -discountAmount);
            }

            return discount;
        }
    }
}
