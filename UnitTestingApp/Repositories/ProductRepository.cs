using UnitTestingApp.Models;

namespace UnitTestingApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = [
            new() 
            { 
                ProductId = 1, 
                Name = "Laptop", 
                Price = 1000 
            },
            new() 
            { 
                ProductId = 2, 
                Name = "Phone", 
                Price = 600 
            }
        ];

        public List<Product> GetAll()
        {
            return _products;
        }

        public Product GetById(int id)
        {
            return _products.FirstOrDefault(p => p.ProductId == id);
        }

        public Product Add(Product product)
        {
            product.ProductId = _products.Count + 1;
            _products.Add(product);
            return product;
        }

        public bool Update(Product product)
        {
            var existing = GetById(product.ProductId);

            if (existing == null)
                return false;

            existing.Name = product.Name;
            existing.Price = product.Price;

            return true;
        }

        public bool Delete(int id)
        {
            var product = GetById(id);

            if (product == null)
                return false;

            _products.Remove(product);
            return true;
        }
    }
}