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
    public class TwoForAmountStrategy : IOfferStrategy
    {
        private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");


        public bool CanApplyOffer(SpecialOfferType offerType)
        {
            return offerType == SpecialOfferType.TwoForAmount;
        }

        public Discount CalculateDiscount(Product product, Offer offer, int quantity, double unitPrice)
        {
            var requiredItemsForOffer = 2;
            Discount? discount = null;
            if (quantity >= 2)
            {
                var total = offer.Argument * (quantity / requiredItemsForOffer) + quantity % 2 * unitPrice;
                var discountN = unitPrice * quantity - total;
                discount = new Discount(product, "2 for " + PrintPrice(offer.Argument), -discountN);
            }

            return discount;
        }

        private string PrintPrice(double price)
        {
            return price.ToString("N2", Culture);
        }
    }
}
