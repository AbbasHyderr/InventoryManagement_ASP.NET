using Octdaily.Models;

namespace Octdaily.interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts(  );

        Product GetProduct( string id );
        bool CreateProduct(  Product product );



        bool ProductExists(string id);

        bool UpdateProduct(Product product );

        bool DeleteProduct(Product product );
        bool Save();
    }
}
