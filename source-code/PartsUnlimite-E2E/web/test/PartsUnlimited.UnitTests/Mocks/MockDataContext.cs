using FakeDbSet;
using PartsUnlimited.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Threading;

namespace PartsUnlimited.UnitTests.Mocks
{
    public class MockDataContext : IPartsUnlimitedContext
    {
        public MockDataContext()
        {
            InitProducts();
            InitOrders();
        }

        public IDbSet<CartItem> CartItems { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<OrderDetail> OrderDetails { get; set; }

        public IDbSet<Order> Orders { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Raincheck> RainChecks { get; set; }

        public IDbSet<Store> Stores { get; set; }

        public IDbSet<ApplicationUser> Users { get; set; }

        public void Dispose()
        {
            // do nothing
        }

        public DbEntityEntry Entry(object entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken requestAborted)
        {
            int changes = 0;
            changes += DbSetHelper.IncrementPrimaryKey(p => p.ProductId, Products);
            changes += DbSetHelper.IncrementPrimaryKey(o => o.OrderId, Orders);

            return Task.FromResult(changes);
        }

        #region init sets
        #region Products
        public void InitProducts()
        {
            Products = new TestDbSet<Product>();
            Products.Add(CreateProduct(1, 5));
            Products.Add(CreateProduct(2, 4));
            Products.Add(CreateProduct(3, 3));
            Products.Add(CreateProduct(4, 2));
            Products.Add(CreateProduct(5, 1));
            Products.Add(CreateProduct(6, 0));
        }

        private static Product CreateProduct(int id, int orderCount)
        {
            var product = new Product()
            {
                ProductId = id,
                Title = $"Product {id}",
                Description = $"Product {id} description",
                Inventory = 2,
                LeadTime = 10,
                Price = 125,
                ProductDetails = $"Product {id} details",
                CategoryId = 1,
                SkuNumber = $"SKU-{id}",
                OrderDetails = new List<OrderDetail>()
            };

            AddProductOrders(product, orderCount);

            return product;
        }

        private static void AddProductOrders(Product product, int orderCount)
        {
            var orders = new List<OrderDetail>();
            for (int i = 0; i < orderCount; i++)
            {
                orders.Add(new OrderDetail()
                {
                    OrderDetailId = (product.ProductId * 100) + i,
                    Count = 1,
                    UnitPrice = 125,
                    ProductId = product.ProductId
                });
            }
            product.OrderDetails.AddRange(orders);
        }
        #endregion

        #region Orders
        public void InitOrders()
        {
            Orders = new TestDbSet<Order>();
            Orders.Add(new Order()
            {
                OrderId = 1,
                Username = "bob",
                OrderDate = new DateTime(2016, 1, 2),
                OrderDetails = new List<OrderDetail>()
                {
                    new OrderDetail()
                    {
                        OrderDetailId = 101,
                        ProductId = 1,
                        Count = 1,
                        UnitPrice = 125,
                    }
                }
            });
            Orders.Add(new Order()
            {
                OrderId = 2,
                Username = "bob",
                OrderDate = new DateTime(2016, 12, 29),
                OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            OrderDetailId = 102,
                            ProductId = 2,
                            Count = 1,
                            UnitPrice = 125,
                        }
                    }
            });
            Orders.Add(new Order()
            {
                OrderId = 3,
                Username = "sue",
                OrderDate = new DateTime(2016, 1, 2),
                OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            OrderDetailId = 103,
                            ProductId = 3,
                            Count = 1,
                            UnitPrice = 125,
                        }
                    }
            });

            // populate the OrderDetails table with the details from the orders
            OrderDetails = new TestDbSet<OrderDetail>();
            Orders.ToList().ForEach(o => o.OrderDetails.ForEach(d => OrderDetails.Add(d)));
        }
        #endregion
        #endregion
    }
}
