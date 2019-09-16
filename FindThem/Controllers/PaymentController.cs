using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FindThem.Models;

namespace FindThem.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        [HttpGet("{id}")]
        public IActionResult Get(Int64 id)
        {
            var payment = new Payment();

            using (var db = new FindThemContext())
            {
                payment = db.Payments
                            .Where(x => x.enabled == true && x.id == id)
                           .FirstOrDefault(x => x.id == id);

            }

            return Ok(payment);
        }
    }
}