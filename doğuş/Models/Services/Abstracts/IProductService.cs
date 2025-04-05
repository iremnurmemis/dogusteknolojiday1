using doğuş.Models.Services.ViewModels;

namespace doğuş.Models.Services.Abstracts
{
    public interface IProductService
    {
        List<ProductViewModel> GetAll();
        CreateProductViewModel CreateViewModel();
        CreateProductViewModel CreateViewModel(CreateProductViewModel createProductViewModel);
        void Create(CreateProductViewModel createProductViewModel);
        ProductViewModel? GetById(int id);
        void Remove(int id);
        EditProductViewModel? EditViewModel(int id);
        EditProductViewModel? EditViewModel(EditProductViewModel editProductViewModel);
        void Update(EditProductViewModel editProductViewModel);
    }
}
