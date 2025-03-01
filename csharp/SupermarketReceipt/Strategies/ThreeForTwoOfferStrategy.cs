using SupermarketReceipt.Entities;
using SupermarketReceipt.Enums;
using SupermarketReceipt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketReceipt.Strategies
{
    public class ThreeForTwoOfferStrategy : IOfferStrategy
    {
        public Discount CalculateDiscount(Product product, double quantity, double unitPrice, Offer offer)
        {
            Discount discount = null;
            var requiredItemsForOffer = 3;
            var quantityAsInt = (int)quantity;
            var numberOfrequiredItemsForOffers = quantityAsInt / requiredItemsForOffer;

            if (quantityAsInt >= requiredItemsForOffer)
            {
                var discountAmount = quantity * unitPrice - (numberOfrequiredItemsForOffers * 2 * unitPrice + quantityAsInt % 3 * unitPrice);
                discount = new Discount(product, "3 for 2", -discountAmount);
            }

            return discount;
        }

        public bool CanHandle(SpecialOfferType offerType)
        {
            return offerType.Equals(SpecialOfferType.ThreeForTwo);
        }
    }
}
