using log4net;
using Strimm.Data.Interfaces;
using Strimm.Model.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Strimm.Data.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductRepository));

        public ProductRepository()
            : base()
        {

        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                products = this.StrimmDbConnection.Query<Product>("strimm.GetAllProducts", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
            }

            return products;
        }

        public Product FindProductById(int productId)
        {
            Contract.Requires(productId > 0, "Product id should be greater then 0");

            Product product = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<Product>("strimm.GetProductById", new { ProductId = productId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                product = results.FirstOrDefault();
            }

            return product;
        }

        public Product GetProductForOrderByOrderNumber(string orderNumber)
        {
            Contract.Requires(!String.IsNullOrEmpty(orderNumber), "Invalid order number specified for this request");

            Product product = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<Product>("strimm.GetProductForOrderByOrderNumber", new { OrderNumber = orderNumber }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                product = results.FirstOrDefault();
            }

            return product;
        }

        public ProductPo GetAvailableProductOptionsByProductIdAndUserId(int productId, int userId, bool isAnnual)
        {
            Contract.Requires(productId > 0, "Invalid product id specified");
            Contract.Requires(userId > 0, "Invalid user id specified");

            ProductPo product = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<ProductPo>("strimm.GetAvailableProductOptionsByUserIdAndProductId", new { 
                    ProductId = productId, 
                    UserId = userId,
                    IsAnnual = isAnnual
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                product = results.FirstOrDefault();
            }

            return product;
        }


        public List<ProductPo> GetAvailableProducts()
        {
            List<ProductPo> products = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                products = this.StrimmDbConnection.Query<ProductPo>("strimm.GetAvailableProducts", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
            }

            return products;
        }
    }
}
