using SupermarketReceipt.Interfaces;
using System.Globalization;

namespace SupermarketReceipt.Strategies
{
    public class FiveForAmountDiscountStrategy : IOfferStrategy
    {
        private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");


        public bool CanApplyOffer(SpecialOfferType offerType)
        {
            return offerType == SpecialOfferType.FiveForAmount;
        }

        public Discount CalculateDiscount(Product product, Offer offer, int quantity, double unitPrice)
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

        private string PrintPrice(double price)
        {
            return price.ToString("N2", Culture);
        }
    }
}
