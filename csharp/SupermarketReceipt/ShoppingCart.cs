using SupermarketReceipt.Interfaces;
using SupermarketReceipt.Strategies;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SupermarketReceipt
{
    public class ShoppingCart
    {
        private readonly List<ProductQuantity> _items = new List<ProductQuantity>();
        private readonly Dictionary<Product, double> _productQuantities = new Dictionary<Product, double>();
        private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");
        private IEnumerable<IOfferStrategy> strategies;

        public ShoppingCart()
        {
            var offerStrategy = new OfferStrategy();
            strategies = offerStrategy.Strategies;
        }

        public List<ProductQuantity> GetItems()
        {
            return new List<ProductQuantity>(_items);
        }

        public void AddItem(Product product)
        {
            AddItemQuantity(product, 1.0);
        }


        public void AddItemQuantity(Product product, double quantity)
        {
            _items.Add(new ProductQuantity(product, quantity));
            if (_productQuantities.ContainsKey(product))
            {
                var newAmount = _productQuantities[product] + quantity;
                _productQuantities[product] = newAmount;
            }
            else
            {
                _productQuantities.Add(product, quantity);
            }
        }

        public void HandleOffers(Receipt receipt, Dictionary<Product, Offer> offers, SupermarketCatalog catalog)
        {
            foreach (var p in _productQuantities.Keys)
            {
                var quantity = _productQuantities[p];
                var quantityAsInt = (int) quantity;
                if (offers.ContainsKey(p))
                {
                    var offer = offers[p];
                    var unitPrice = catalog.GetUnitPrice(p);
                    Discount discount = null;

                    var startegy = strategies.FirstOrDefault(s => s.CanApplyOffer(offer.OfferType));
                    if (startegy == null)
                        continue;

                    discount = startegy.CalculateDiscount(p, offer, quantityAsInt, unitPrice);

                    if (discount != null)
                        receipt.AddDiscount(discount);
                }
            }
        }
    }
}