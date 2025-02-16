using SupermarketReceipt.Interfaces;
using SupermarketReceipt.Strategies;
using System.Collections.Generic;
using Xunit;

namespace SupermarketReceipt.Test
{
    public class SupermarketXUnitTest
    {
        public SupermarketXUnitTest()
        {
            var offerStrategy = new OfferStrategy();
            IEnumerable<IOfferStrategy> strategies = offerStrategy.Strategies;
        }

        [Fact]
        public void TenPercentDiscount()
        {
            // ARRANGE
            SupermarketCatalog catalog = new FakeCatalog();
            var toothbrush = new Product("toothbrush", ProductUnit.Each);
            catalog.AddProduct(toothbrush, 0.99);
            var apples = new Product("apples", ProductUnit.Kilo);
            catalog.AddProduct(apples, 1.99);

            var cart = new ShoppingCart();
            cart.AddItemQuantity(apples, 2.5);

            var teller = new Teller(catalog);
            teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, toothbrush, 10.0);

            // ACT
            var receipt = teller.ChecksOutArticlesFrom(cart);

            // ASSERT
            Assert.Equal(4.975, receipt.GetTotalPrice());
            Assert.Equal(new List<Discount>(), receipt.GetDiscounts());
            Assert.Single(receipt.GetItems());
            var receiptItem = receipt.GetItems()[0];
            Assert.Equal(apples, receiptItem.Product);
            Assert.Equal(1.99, receiptItem.Price);
            Assert.Equal(2.5 * 1.99, receiptItem.TotalPrice);
            Assert.Equal(2.5, receiptItem.Quantity);
        }

        [Fact]
        public void ThreeForTwoDiscount()
        {
            // ARRANGE
            SupermarketCatalog catalog = new FakeCatalog();
            var toothbrush = new Product("toothbrush", ProductUnit.Each);
            catalog.AddProduct(toothbrush, 1);

            var cart = new ShoppingCart();
            cart.AddItemQuantity(toothbrush, 3);

            var teller = new Teller(catalog);
            teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, toothbrush, 3);

            var discount = new Discount(toothbrush, "3 for 2", -1);
            var discounts = new List<Discount> { discount };

            // ACT
            var receipt = teller.ChecksOutArticlesFrom(cart);

            // ASSERT
            Assert.Equal(2, receipt.GetTotalPrice());
            Assert.Equivalent(discounts, receipt.GetDiscounts());
            Assert.Single(receipt.GetItems());
            var receiptItem = receipt.GetItems()[0];
            Assert.Equal(toothbrush, receiptItem.Product);
            Assert.Equal(1, receiptItem.Price);
            Assert.Equal(3, receiptItem.TotalPrice);
            Assert.Equal(3, receiptItem.Quantity);
        }

        [Fact]
        public void TwoForAmountDiscount()
        {
            // ARRANGE
            SupermarketCatalog catalog = new FakeCatalog();
            var toothbrush = new Product("toothbrush", ProductUnit.Each);
            catalog.AddProduct(toothbrush, 1);

            var cart = new ShoppingCart();
            cart.AddItemQuantity(toothbrush, 3);

            var teller = new Teller(catalog);
            teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, toothbrush, 2);

            var discount = new Discount(toothbrush, "2 for 2.00", -0);
            var discounts = new List<Discount> { discount };

            // ACT
            var receipt = teller.ChecksOutArticlesFrom(cart);

            // ASSERT
            Assert.Equal(3, receipt.GetTotalPrice());
            Assert.Equivalent(discounts, receipt.GetDiscounts());
            Assert.Single(receipt.GetItems());
            var receiptItem = receipt.GetItems()[0];
            Assert.Equal(toothbrush, receiptItem.Product);
            Assert.Equal(1, receiptItem.Price);
            Assert.Equal(3, receiptItem.TotalPrice);
            Assert.Equal(3, receiptItem.Quantity);
        }

        [Fact]
        public void FiveForDiscount()
        {
            // ARRANGE
            SupermarketCatalog catalog = new FakeCatalog();
            var toothbrush = new Product("toothbrush", ProductUnit.Each);
            catalog.AddProduct(toothbrush, 1);

            var cart = new ShoppingCart();
            cart.AddItemQuantity(toothbrush, 6);

            var teller = new Teller(catalog);
            teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, toothbrush, 5);

            var discount = new Discount(toothbrush, "5 for 5.00", -0);
            var discounts = new List<Discount> { discount };

            // ACT
            var receipt = teller.ChecksOutArticlesFrom(cart);

            // ASSERT
            Assert.Equal(6, receipt.GetTotalPrice());
            Assert.Equivalent(discounts, receipt.GetDiscounts());
            Assert.Single(receipt.GetItems());
            var receiptItem = receipt.GetItems()[0];
            Assert.Equal(toothbrush, receiptItem.Product);
            Assert.Equal(1, receiptItem.Price);
            Assert.Equal(6, receiptItem.TotalPrice);
            Assert.Equal(6, receiptItem.Quantity);
        }
    }
}