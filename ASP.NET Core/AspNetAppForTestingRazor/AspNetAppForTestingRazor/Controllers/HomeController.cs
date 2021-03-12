﻿using AspNetAppForTestingRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetAppForTestingRazor.Data;
using AspNetAppForTestingRazor.Services;
using AspNetAppForTestingRazor.ViewModels.Home;

namespace AspNetAppForTestingRazor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext dbContext;
        private readonly IShortStringService _shortStringService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IShortStringService shortStringService)
        {
            _logger = logger;
            this.dbContext = dbContext;
            _shortStringService = shortStringService;
        }

        public IActionResult Index(int id)
        {
            //// Not recommended
            ////Dictionary<string, object>
            //this.ViewData["Year"] = 2021;
            //this.ViewData["Name"] = "Aleks";
            //this.ViewData["UsersCount"] = this.dbContext.Users.Count();

            //// Not recommended
            ////dynamic
            //this.ViewBag.Name = "Aleks";
            //this.ViewBag.Year = "2021";
            //this.ViewBag.UsersCount = this.dbContext.Users.Count();
            //this.ViewBag.Calc = new Func<int>(() => 3);
            //this.ViewBag.Calc();

            var viewModel = new IndexViewModel()
            {
                Id = id,
                Name = "Aleks",
                Year = DateTime.UtcNow.Year,
                UsersCount = this.dbContext.Users.Count(),
                ProcessorsCount = Environment.ProcessorCount,
                Description = "Google LLC is an American multinational technology company that specializes in Internet-related services and products, which include online advertising technologies, a search engine, cloud computing, software, and hardware."
            };
            //viewModel.Description = this._shortStringService.GetShort(viewModel.Description, 200);

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}