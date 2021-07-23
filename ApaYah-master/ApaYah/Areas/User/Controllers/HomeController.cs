using ApaYah.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApaYah.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext c)
        {
            _context = c;
        }

        public IActionResult Index()
        {
            var data = _context.Blogs.Where(x => x.Status==true).ToList();
            var contents = new List<Blogs>();
            /*foreach(var item in data)
            {
                var conten = new Blogs
                {
                    CreateDate: item.CreateDate.
                }
            }  */ 
            return View(data);
        }

        public IActionResult Detail(int id)
        {
            var GetBlogId = _context.Blogs.Find(id);
            return View(GetBlogId);
        }
    }
}