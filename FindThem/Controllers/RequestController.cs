using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FindThem.Models;
using System.Security.Claims;

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

            long id = 0;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                id = Convert.ToInt64(identity.FindFirst("userId").Value);
            }

            var kindUser = FindThem.Models.User.getKindUser(id);

            using (var db = new FindThemContext())
            {
                try
                {
                    requests = db.Requests
                                 .Where(x => x.enabled == true && x.client.user.id == id)
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
                    var client = db.Clients
                                .Where(x => x.id == request.client.id)
                                .FirstOrDefault();

                    var provider = db.Providers
                                .Where(x => x.id == request.provider.id)
                                .FirstOrDefault();

                    var service = db.Services
                                .Where(x => x.id == request.service.id)
                                .FirstOrDefault();

                    request.client = client;
                    request.provider = provider;
                    request.service = service;

                    db.Requests.Add(request);
                    db.SaveChanges();
                } catch (Exception ex)
                {
                    return Ok(new { success = false, message = ex.Message });
                }
            }

            return Ok(new { success = true, message = "Request created with success", data = request });
        }
    }
}