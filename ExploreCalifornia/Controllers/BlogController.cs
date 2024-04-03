﻿using Microsoft.AspNetCore.Mvc;

namespace ExploreCalifornia.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("blog/{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            ViewBag.Title = "My blog post";
            ViewBag.Posted = DateTime.Now;
            ViewBag.Author = "Diaaeldin Elsherif";
            ViewBag.Body = "This is a great blog post.";
            return View();
        }
    }
}
