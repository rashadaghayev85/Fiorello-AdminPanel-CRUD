using FiorelloMVC.Data;
using FiorelloMVC.Models;
using FiorelloMVC.Services;
using FiorelloMVC.Services.Interfaces;
using FiorelloMVC.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorelloMVC.Areas.Admin.Controllers
{
    [Area("admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly AppDBContext _context;
        public BlogController(IBlogService blogService,
                                   AppDBContext context)
        {
            _blogService = blogService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetAllAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool existBlog = await _blogService.ExistAsync(blog.Title);
            if (existBlog)
            {
                ModelState.AddModelError("Title", "This title already exist");
                return View();
            }
            await _blogService.CreateAsync(new Blog { Title = blog.Title,Description=blog.Description,Image= blog.Image="blog-feature-img-1.jpg" });
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var blog = await _blogService.GetByIdAsync((int)id);
            if (blog is null) return NotFound();
            await _blogService.DeleteAsync(blog);
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            
            Blog blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);
            return View(blog);
        }

    }
}
