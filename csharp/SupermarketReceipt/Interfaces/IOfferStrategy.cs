using SupermarketReceipt.Entities;
using SupermarketReceipt.Enums;

namespace SupermarketReceipt.Interfaces
{
    public interface IOfferStrategy
    {
        bool CanHandle(SpecialOfferType offerType);
        Discount? CalculateDiscount(
            Product product,
            double quantity,
            double unitPrice,
            Offer offer
        );
    }
}
