using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreCachingDemo.Services;
using Microsoft.AspNet.Mvc;

namespace AspNetCoreCachingDemo.Controllers
{
    public class HomeController : Controller
    {
	    private readonly IXyRepository xyRepository;

	    public HomeController(IXyRepository xyRepository)
	    {
		    this.xyRepository = xyRepository;
	    }

		public IActionResult Index()
	    {
		    ViewBag.Data = xyRepository.GetData();

			Thread.Sleep(50);

			return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
