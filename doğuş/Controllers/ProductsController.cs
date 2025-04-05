using doğuş.Models.Services.Abstracts;
using doğuş.Models.Services.Concretes;
using doğuş.Models.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace doğuş.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAll();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var createProductViewModel = await _productService.CreateViewModel();
            return View(createProductViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel createProductViewModel)
        {
            if (!ModelState.IsValid)
                return View(await _productService.CreateViewModel(createProductViewModel));

            await _productService.Create(createProductViewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Remove(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await _productService.EditViewModel(id);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel editProductViewModel)
        {
            if (!ModelState.IsValid)
                return View(await _productService.EditViewModel(editProductViewModel));

            await _productService.Update(editProductViewModel);
            return RedirectToAction("Index");
        }


    }
}
