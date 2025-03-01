using SupermarketReceipt.Entities;
using SupermarketReceipt.Enums;
using SupermarketReceipt.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketReceipt.Strategies
{
    public class FiveForAmountStrategy : IOfferStrategy
    {
        private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

        public Discount CalculateDiscount(Product product, double quantity, double unitPrice, Offer offer)
        {
            Discount discount = null;
            var requiredItemsForOffer = 5;
            var quantityAsInt = (int)quantity;
            var numberOfrequiredItemsForOffers = quantityAsInt / requiredItemsForOffer;

            if (quantityAsInt >= requiredItemsForOffer)
            {
                var discountTotal = unitPrice * quantity - (offer.Argument * numberOfrequiredItemsForOffers + quantityAsInt % 5 * unitPrice);
                discount = new Discount(product, requiredItemsForOffer + " for " + PrintPrice(offer.Argument), -discountTotal);
            }

            return discount;
        }

        public bool CanHandle(SpecialOfferType offerType)
        {
            return offerType.Equals(SpecialOfferType.FiveForAmount);
        }

        private string PrintPrice(double price)
        {
            return price.ToString("N2", Culture);
        }
    }
}
