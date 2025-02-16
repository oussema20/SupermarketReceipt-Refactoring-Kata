using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketReceipt.Interfaces
{
    public interface IOfferStrategy
    {
        bool CanApplyOffer(SpecialOfferType offerType);

        Discount CalculateDiscount(Product product, Offer offer, int quantity, double unitPrice);
    }
}
