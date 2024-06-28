using S2L5_Scarpe.Models;

namespace S2L5_Scarpe.Services
{
    public interface IProductService
    {

      public  IEnumerable<Product> GetAllProducts();
      public  Product GetProductById(int id);
      public  void CreateProduct(Product product);
      public  void DeleteProduct(int id);

    }
}
