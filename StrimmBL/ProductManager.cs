using log4net;
using Strimm.Data.Repositories;
using Strimm.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL
{
    public static class ProductManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductManager));

        public static List<Product> GetAllProducts()
        {
            Logger.Debug("Requesting all system products");

            List<Product> products = null;

            using (var repository = new ProductRepository())
            {
                products = repository.GetAllProducts();
            }

            return products;
        }

        public static Product GetProductById(int productId)
        {
            Logger.Debug(String.Format("Requesting product using id={0}", productId));

            Product product = null;

            using (var repository = new ProductRepository())
            {
                product = repository.FindProductById(productId);
            }

            return product;
        }
    }
}
