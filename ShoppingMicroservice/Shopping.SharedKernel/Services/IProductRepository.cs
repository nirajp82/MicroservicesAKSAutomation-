using Shopping.Model;
using System.Collections.Generic;

namespace Shopping.SharedKernel
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetPreconfiguredProducts();
    }
}