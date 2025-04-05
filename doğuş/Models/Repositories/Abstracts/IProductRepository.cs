using doğuş.Models.Repositories.Entities;

namespace doğuş.Models.Repositories.Abstracts
{
    public interface IProductRepository 

    {
        List<Product> GetAll();
        Product? GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
