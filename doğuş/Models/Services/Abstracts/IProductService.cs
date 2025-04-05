using doğuş.Models.Services.ViewModels;

namespace doğuş.Models.Services.Abstracts
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetAll();
        Task<CreateProductViewModel> CreateViewModel();
        Task<CreateProductViewModel> CreateViewModel(CreateProductViewModel createProductViewModel);
        Task Create(CreateProductViewModel createProductViewModel);
        Task<ProductViewModel?> GetById(int id);
        Task Remove(int id);
        Task<EditProductViewModel?> EditViewModel(int id);
        Task<EditProductViewModel?> EditViewModel(EditProductViewModel editProductViewModel);
        Task Update(EditProductViewModel editProductViewModel);
    }
}
