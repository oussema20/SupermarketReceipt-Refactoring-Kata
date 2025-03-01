using SupermarketReceipt.Entities;
using SupermarketReceipt.Enums;
using SupermarketReceipt.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SupermarketReceipt.Strategies
{
    public class TwoForAmountStartegy:IOfferStrategy
    {
        private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

        public Discount CalculateDiscount(Product product, double quantity, double unitPrice, Offer offer)
        {
            Discount discount = null;
            var requiredItemsForOffer = 2;
            var quantityAsInt = (int)quantity;
            var numberOfrequiredItemsForOffers = quantityAsInt / requiredItemsForOffer;

            if (quantityAsInt >= requiredItemsForOffer)
            {
                var total = offer.Argument * (quantityAsInt / requiredItemsForOffer) + quantityAsInt % 2 * unitPrice;
                var discountN = unitPrice * quantity - total;
                discount = new Discount(product, "2 for " + PrintPrice(offer.Argument), -discountN);
            }

            return discount;
        }

        public bool CanHandle(SpecialOfferType offerType)
        {
            return offerType.Equals(SpecialOfferType.TwoForAmount);
        }

        private string PrintPrice(double price)
        {
            return price.ToString("N2", Culture);
        }
    }
}
