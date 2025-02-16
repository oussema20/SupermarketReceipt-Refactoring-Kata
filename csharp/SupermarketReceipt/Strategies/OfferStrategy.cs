using SupermarketReceipt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketReceipt.Strategies
{
    public class OfferStrategy
    {
        public IEnumerable<IOfferStrategy> Strategies = new List<IOfferStrategy>()
        {
            new TwoForAmountStrategy(),
            new ThreeForTwoDiscountStrategy(),
            new TenPercentDiscountStrategy(),
            new FiveForAmountDiscountStrategy()
        };
    }
}
