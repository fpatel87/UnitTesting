using UnitTestingApp.Models;
using UnitTestingApp.Repositories;

namespace UnitTestingApp.Services
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public Product Add(Product product)
        {
            return _productRepository.Add(product);
        }

        public bool Update(Product product)
        {
            return _productRepository.Update(product);
        }

        public bool Delete(int id)
        {
            return _productRepository.Delete(id);
        }
    }
}