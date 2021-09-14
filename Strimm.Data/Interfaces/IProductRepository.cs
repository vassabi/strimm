using Strimm.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();

        Product FindProductById(int productId);
    }
}
