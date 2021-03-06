﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
                                .Include(x => x.client)
                                    .ThenInclude(x => x.user)
                                .Include(x => x.provider)
                                    .ThenInclude(x => x.user)
                                .Include(x => x.service)
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
                                 .Include(x => x.client)
                                    .ThenInclude(x => x.user)
                                 .Include(x => x.provider)
                                    .ThenInclude(x => x.user)
                                 .Include(x => x.service)
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
                                    .Include(x => x.user)
                                .FirstOrDefault();

                    var provider = db.Providers
                                .Where(x => x.id == request.provider.id)
                                    .Include(x => x.user)
                                .FirstOrDefault();

                    var service = db.Services
                                .Where(x => x.id == request.service.id)
                                    .Include(x => x.provider)
                                        .ThenInclude(x => x.user)
                                .FirstOrDefault();

                    request.client = client;
                    request.provider = provider;
                    request.service = service;

                    db.Requests.Add(request);

                    db.Entry(request.client).State = EntityState.Unchanged;
                    db.Entry(request.provider).State = EntityState.Unchanged;
                    db.Entry(request.service).State = EntityState.Unchanged;

                    db.SaveChanges();
                } catch (Exception ex)
                {
                    return Ok(new { success = false, message = ex.InnerException.Message });
                }
            }

            return Ok(new { success = true, message = "Request created with success", data = request });
        }
    }
}