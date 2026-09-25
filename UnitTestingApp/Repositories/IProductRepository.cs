using UnitTestingApp.Models;

namespace UnitTestingApp.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetById(int id);
        Product Add(Product product);
        bool Update(Product product);
        bool Delete(int id);
    }
}