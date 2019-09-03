using Microsoft.AspNetCore.Mvc;
using FindThem.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;

namespace FindThem.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/client")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        [HttpGet("FindAll")]
        public IActionResult findAll()
        {
            var clients = new List<Client>();

            using (var db = new FindThemContext())
            {
                clients = db.Clients
                            .Where(x => x.enabled == true)
                            .Include(user => user.user)
                            .ToList();
            }

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult get(Int64 id)
        {
            var client = new Client();

            using (var db = new FindThemContext())
            {
                client = db.Clients
                            .Where(x => x.enabled == true)
                           .Include(user => user.user)
                           .FirstOrDefault(x => x.id == id);

                client.user.password = "";
            }

            return Ok(client);
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public IActionResult create([FromBody]Client client)
        {
            using (var db = new FindThemContext())
            {
                try
                {
                    var user = db.Users.FirstOrDefault(x => x.id == client.user.id);

                    if (user != null)
                    {
                        client.user = user;
                    }
                    else
                    {
                        client.user.password = Utils.GetMd5HashPassword(client.user.password);
                    }

                    db.Clients.Add(client);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok(client);
        }

        [HttpPost("edit/{id}")]
        public IActionResult edit(Int64 id, string key, string value)
        {
            Client client = new Client();

            if (key == "password")
            {
                value = Utils.GetMd5HashPassword(value);
            }

            using (var db = new FindThemContext())
            {
                client = db.Clients
                           .Include(user => user.user)
                           .FirstOrDefault(x => x.id == id);

                var arraykeys = key.Split(".");

                if (client == null)
                {
                    return NotFound("Client not found.");
                }

                try
                {
                    client = Client.Update(client, key, value);

                    db.Clients.Update(client);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }

            return Ok(client);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult delete(Int64 id)
        {
            using (var db = new FindThemContext())
            {
                try
                {
                    var client = db.Clients
                                .FirstOrDefault(x => x.id == id);

                    client.dateUpdated = DateTime.Now;
                    client.enabled = false;

                    db.Clients.Update(client);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok();
        }
    }
}