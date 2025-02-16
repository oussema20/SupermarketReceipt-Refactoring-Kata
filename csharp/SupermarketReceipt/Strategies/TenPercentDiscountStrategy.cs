using SupermarketReceipt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketReceipt.Strategies
{
    public class TenPercentDiscountStrategy : IOfferStrategy
    {
        public bool CanApplyOffer(SpecialOfferType offerType)
        {
            return offerType == SpecialOfferType.TenPercentDiscount;
        }

        public Discount CalculateDiscount(Product product, Offer offer, int quantity, double unitPrice)
        {
            var discount = new Discount(product, offer.Argument + "% off", -quantity * unitPrice * offer.Argument / 100.0);
            return discount;
        }
    }
}
