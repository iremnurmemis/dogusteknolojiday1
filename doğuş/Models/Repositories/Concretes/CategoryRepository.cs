using doğuş.Models.Repositories.Abstracts;
using doğuş.Models.Repositories.Entities;

namespace doğuş.Models.Repositories.Concretes
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
    }
}
