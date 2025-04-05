using doğuş.Models.Repositories.Abstracts;
using doğuş.Models.Repositories.Concretes;
using doğuş.Models.Repositories.Entities;
using doğuş.Models.Services.Abstracts;
using doğuş.Models.Services.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace doğuş.Models.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public void Create(CreateProductViewModel createProductViewModel)
        {
            //formdan gelen veriyi kaydediyor 
            var product = new Product
            {
                Name = createProductViewModel.Name,
                Description = createProductViewModel.Description,
                Price = createProductViewModel.Price.Value,
                Stock = createProductViewModel.Stock!.Value,
                CategoryId = createProductViewModel.CategoryId!.Value,
            };

             _repository.Add(product);
        }

        public CreateProductViewModel CreateViewModel()
        {
            //Form ilk açıldığında kullanıcıya seçim yapılabilecek kategori listesini sunuyor
            var categories = _categoryRepository.GetAll();
            var createProductViewModel = new CreateProductViewModel();
            createProductViewModel.CategoryList = new SelectList(categories, "Id", "Name");
            return createProductViewModel;
        }

        public CreateProductViewModel CreateViewModel(CreateProductViewModel createProductViewModel)
        {
            //Kullanıcı formda bir hata yaptıysa ve form tekrar gösterilecekse, daha önce girilen bilgileri kaybetmeden kategori listesini tekrar dolduruyor
            var categories = _categoryRepository.GetAll();
            createProductViewModel.CategoryList = new SelectList(categories, "Id", "Name", createProductViewModel.CategoryId);
            return createProductViewModel;
        }

        public EditProductViewModel? EditViewModel(int id)
        {
            var product = _repository.GetById(id);
            if (product == null) 
                return null;
            var categories = _categoryRepository.GetAll();
            var editProductViewModel = new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId
            };
            editProductViewModel.CategoryList = new SelectList(categories, "Id", "Name", editProductViewModel.CategoryId);
            return editProductViewModel;

        }

        public EditProductViewModel? EditViewModel(EditProductViewModel editProductViewModel)
        {
            var categories = _categoryRepository.GetAll();
            editProductViewModel.CategoryList = new SelectList(categories, "Id", "Name", editProductViewModel.CategoryId);
            return editProductViewModel;
        }

        public List<ProductViewModel> GetAll()
        {
            var products=_repository.GetAll();

            var productListViewModel = new List<ProductViewModel>();

            foreach (var product in products)
            {
                var productViewModel = new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = (product.Price * 1.20m).ToString("C"),
                    Stock = product.Stock,
                    CategoryName=product.Category.Name,
                };
                productListViewModel.Add(productViewModel);
            }

            return productListViewModel;
        }

        public ProductViewModel? GetById(int id)
        {
            var product=_repository.GetById(id);
            if (product == null) return null!;
            var productViewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = (product.Price * 1.20m).ToString("C"),
                Stock = product.Stock,
                CategoryName = product.Category.Name
            };
            return productViewModel;

        }

        public void Remove(int id)
        {
            var product = _repository.GetById(id);
            if (product != null) 
                _repository.Delete(product);
        }

        public void Update(EditProductViewModel editProductViewModel)
        {
            var product = _repository.GetById(editProductViewModel.Id);
            if (product == null)
                return;

            product.Name = editProductViewModel.Name!;
            product.Description = editProductViewModel.Description;
            product.Price = editProductViewModel.Price!.Value;
            product.Stock = editProductViewModel.Stock!.Value;
            product.CategoryId = editProductViewModel.CategoryId!.Value;
            _repository.Update(product);
        }
    }
}
