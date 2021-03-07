using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Domain;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogsRepository _blogsRepo;
        public BlogsController(IBlogsRepository blogsRepo)
        {
            _blogsRepo = blogsRepo;
        }
        public IActionResult Index()
        {
            var list = _blogsRepo.GetBlogs();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Blog b)
        {
            if (_blogsRepo.GetBlogs().Where(x => x.Url == b.Url).Count() == 0)
            {
                _blogsRepo.AddBlog(b);
            }
            else TempData["warning"] = "Blog exists already";
            return RedirectToAction("Index");
        }
    }
}
