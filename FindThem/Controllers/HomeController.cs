﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FindThem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult App()
        {
            return File("~/index.html", "text/html");
        }
    }
}