using SupermarketReceipt.Entities;
using SupermarketReceipt.Enums;
using SupermarketReceipt.Interfaces;
using System.Globalization;

namespace SupermarketReceipt.Strategies
{
    public class TenPercentDiscountStrategy : IOfferStrategy
    {
        private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

        public Discount CalculateDiscount(Product product, double quantity, double unitPrice, Offer offer)
        {
            Discount discount = null;

            discount = new Discount(product, offer.Argument + "% off", -quantity * unitPrice * offer.Argument / 100.0);

            return discount;
        }

        public bool CanHandle(SpecialOfferType offerType)
        {
            return offerType.Equals(SpecialOfferType.TenPercentDiscount);
        }
    }
}
