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
        public IActionResult Index()
        {
            var products = _productService.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var createProductViewModel=_productService.CreateViewModel();
            return View(createProductViewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateProductViewModel createProductViewModel)
        {
            if (!ModelState.IsValid)
                return View(_productService.CreateViewModel(createProductViewModel));

            _productService.Create(createProductViewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _productService.Remove(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_productService.EditViewModel(id));
        }

        [HttpPost]
        public IActionResult Edit(EditProductViewModel editProductViewModel)
        {
            if (!ModelState.IsValid) 
                return View(_productService.EditViewModel(editProductViewModel));

            _productService.Update(editProductViewModel);
            return RedirectToAction("Index");
        }


    }
}
