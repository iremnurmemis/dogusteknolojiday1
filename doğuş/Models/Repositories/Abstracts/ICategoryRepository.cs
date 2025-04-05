using doğuş.Models.Repositories.Entities;

namespace doğuş.Models.Repositories.Abstracts
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
    }
}
