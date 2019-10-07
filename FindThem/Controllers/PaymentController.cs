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
    [Authorize]
    public class PaymentController : ControllerBase
    {

        [HttpGet("{id}")]
        public IActionResult Get(Int64 id)
        {
            var payment = new Payment();

            using (var db = new FindThemContext())
            {
                try
                {
                    payment = db.Payments
                            .Where(x => x.enabled == true && x.id == id)
                           .FirstOrDefault(x => x.id == id);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok(payment);
        }



        [HttpPost("pay")]
        public IActionResult Pay([FromBody] Payment payment)
        {
            using (var db = new FindThemContext())
            {
                try
                {
                    payment = db.Payments.Add(payment).Entity;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok(payment);
        }
    }
}