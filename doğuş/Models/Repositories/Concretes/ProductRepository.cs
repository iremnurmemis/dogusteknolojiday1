using doğuş.Models.Repositories.Abstracts;
using doğuş.Models.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace doğuş.Models.Repositories.Concretes
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Product product)
        {
           _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context?.Products.Remove(product);
            _context?.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return _context.Products.Include(p=>p.Category).ToList();
        }

        public Product? GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public void Update(Product product)
        {
           _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
