using S2L5_Scarpe.Models;

namespace S2L5_Scarpe.Services
{
    public class ProductService : IProductService
    {
        private static List<Product> products = new List<Product>
        {
            
        };
        public int lastId = 0;
        public void CreateProduct(Product product)
        {
            product.Id = ++lastId;
            products.Add(product);
        }

        public void DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public Product GetProductById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }
    }
}
