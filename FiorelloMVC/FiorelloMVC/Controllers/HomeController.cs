
using FiorelloMVC.Services;
using FiorelloMVC.Services.Interfaces;
using FiorelloMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FiorelloMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
       private readonly IBlogService _blogService;
        private readonly IExpertService _expertService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public HomeController(ISliderService sliderService,
                              IBlogService blogService,
                              IExpertService expertService,
                              ICategoryService categoryService,
                              IProductService productService)
        {
            _blogService = blogService;
            _sliderService = sliderService;
            _expertService = expertService;
            _categoryService = categoryService;
            _productService = productService;

        }
        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
              Sliders=await _sliderService.GetAllAsync(),
              SliderInfo=await _sliderService.GetSliderInfoAsync(),
              Blogs=await _blogService.GetAllAsync(3),
              Experts=await  _expertService.GetAllAsync(),
              Categories=await _categoryService.GetAllAsync(),
              Products=await _productService.GetAllAsync(),
            };
            return View(model);
        }

      
     
    }
}
