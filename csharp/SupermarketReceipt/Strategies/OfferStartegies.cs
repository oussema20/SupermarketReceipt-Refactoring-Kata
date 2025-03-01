using SupermarketReceipt.Interfaces;
using System.Collections.Generic;

namespace SupermarketReceipt.Strategies
{
    public class OfferStartegies
    {
        public readonly List<IOfferStrategy> Strategies = new List<IOfferStrategy>() {
            new TenPercentDiscountStrategy(),
            new ThreeForTwoOfferStrategy(),
            new TwoForAmountStartegy(),
            new ThreeForTwoOfferStrategy(),
        };
    }
}
