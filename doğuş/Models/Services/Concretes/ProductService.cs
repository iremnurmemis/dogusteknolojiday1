using doğuş.Models.Repositories;
using doğuş.Models.Repositories.Entities;
using doğuş.Models.Services.Abstracts;
using doğuş.Models.Services.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace doğuş.Models.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly AppDbContext _context; // Include işlemi için DbContext

        public ProductService(
            IGenericRepository<Product> productRepository,
            IGenericRepository<Category> categoryRepository,
            AppDbContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }

      

        public async Task<CreateProductViewModel> CreateViewModel()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var createProductViewModel = new CreateProductViewModel
            {
                CategoryList = new SelectList(categories, "Id", "Name")
            };
            return createProductViewModel;
        }

        public async Task<CreateProductViewModel> CreateViewModel(CreateProductViewModel createProductViewModel)
        {
            var categories = await _categoryRepository.GetAllAsync();
            createProductViewModel.CategoryList = new SelectList(categories, "Id", "Name", createProductViewModel.CategoryId);
            return createProductViewModel;
        }

        public async Task<EditProductViewModel?> EditViewModel(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return null;

            var categories = await _categoryRepository.GetAllAsync();
            var editProductViewModel = new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryList = new SelectList(categories, "Id", "Name", product.CategoryId)
            };
            return editProductViewModel;
        }

        public async Task<EditProductViewModel?> EditViewModel(EditProductViewModel editProductViewModel)
        {
            var categories = await _categoryRepository.GetAllAsync();
            editProductViewModel.CategoryList = new SelectList(categories, "Id", "Name", editProductViewModel.CategoryId);
            return editProductViewModel;
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            // Category dahil edilerek getiriliyor
            var products = await _context.Products.Include(p => p.Category).ToListAsync();

            return products.Select(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = (product.Price * 1.20m).ToString("C"),
                Stock = product.Stock,
                CategoryName = product.Category?.Name
            }).ToList();
        }

        public async Task<ProductViewModel?> GetById(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return null;

            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = (product.Price * 1.20m).ToString("C"),
                Stock = product.Stock,
                CategoryName = product.Category?.Name
            };
        }

        public async Task Create(CreateProductViewModel createProductViewModel)
        {
            var product = new Product
            {
                Name = createProductViewModel.Name,
                Description = createProductViewModel.Description,
                Price = createProductViewModel.Price.Value,
                Stock = createProductViewModel.Stock!.Value,
                CategoryId = createProductViewModel.CategoryId!.Value,
            };

            await _productRepository.AddAsync(product);
        }

        public async Task Remove(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task Update(EditProductViewModel editProductViewModel)
        {
            var product = await _productRepository.GetByIdAsync(editProductViewModel.Id);
            if (product == null)
                return;

            product.Name = editProductViewModel.Name!;
            product.Description = editProductViewModel.Description;
            product.Price = editProductViewModel.Price!.Value;
            product.Stock = editProductViewModel.Stock!.Value;
            product.CategoryId = editProductViewModel.CategoryId!.Value;

            await _productRepository.UpdateAsync(product);
        }

    }
}
