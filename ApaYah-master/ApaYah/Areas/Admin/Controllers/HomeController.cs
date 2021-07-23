using ApaYah.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApaYah.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext c)
        {
            _context = c;
        }        

        public IActionResult Index()
        {
            var data = _context.Blogs.ToList();            
            return View(data);
        }

        public IActionResult Published()
        {
            var data = _context.Blogs.ToList().Where(x=>x.Status==true);
            return View(data);
        }

        public IActionResult Draft()
        {
            var data = _context.Blogs.ToList().Where(x => x.Status == false);
            return View(data);
        }

        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogsForm data)
        {
            if (ModelState.IsValid)
            {
                var blog = new Blogs()
                {
                    Title = data.Title,
                    Content = data.Content,
                    Author = data.Author,
                    CreateDate = DateTime.Now,
                    Status = false
                };                

                _context.Add(blog);

                _context.SaveChanges();
                int id = _context.Blogs.OrderByDescending(x => x.Id)
                             .Take(1)
                             .Select(x => x.Id)
                             .ToList()
                             .FirstOrDefault();

                return RedirectToAction("Detail", new { id = id });
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            var GetBlogId = _context.Blogs.Find(id);
            return View(GetBlogId);
        }

        [HttpPost]
        public IActionResult Edit(Blogs data)
        {
            if (ModelState.IsValid)
            {
                var finduser = _context.Blogs.Find(data.Id);

                if (finduser == null)
                {
                    return NotFound();
                }

                finduser.Title = data.Title;
                finduser.Content = data.Content;
                finduser.Status = false;

                _context.Update(finduser);

                _context.SaveChanges();

                return RedirectToAction("Detail", new { id = data.Id });
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            var data = _context.Blogs.Find(id);            
            
            if (data == null)
            {
                return NotFound();
            }

            _context.Remove(data);
            

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id)
        {
            var GetBlogId = _context.Blogs.Find(id);
            return View(GetBlogId);
        }

        public IActionResult Unggah(Blogs data)
        {
            if (ModelState.IsValid)
            {
                var finduser = _context.Blogs.Find(data.Id);

                if (finduser == null)
                {
                    return NotFound();
                }

                finduser.Status = true;

                _context.Update(finduser);

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Takedown(Blogs data)
        {
            if (ModelState.IsValid)
            {
                var finduser = _context.Blogs.Find(data.Id);

                if (finduser == null)
                {
                    return NotFound();
                }

                finduser.Status = false;

                _context.Update(finduser);

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

    }
}