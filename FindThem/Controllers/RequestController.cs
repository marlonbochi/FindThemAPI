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
    [Route("api/request")]
    [ApiController]
    [Authorize]
    public class RequestController : ControllerBase
    {
        [HttpGet("{requestID}")]
        public IActionResult get(Int64 requestID)
        {
            var request = new Request();
            using (var db = new FindThemContext())
            {
                try
                {
                    request = db.Requests
                                .Where(x => x.id == requestID)
                                .FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok(request);
        }

        [HttpGet("findAll")]
        public IActionResult FindAll()
        {
            var requests = new List<Request>();
            using (var db = new FindThemContext())
            {
                try
                {
                    requests = db.Requests
                                 .Where(x => x.enabled == true)
                                 .ToList();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok(requests);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]Request request)
        {
            using (var db = new FindThemContext())
            {
                try
                {
                    db.Requests.Add(request);
                    db.SaveChanges();
                } catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok(request);
        }
    }
}