using Microsoft.AspNetCore.Mvc;
using FindThem.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FindThem.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/client")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        [HttpGet("FindAll")]
        public IActionResult findAll(int page = 1)
        {
            if (page > 0)
            {
                page--;
            }

            var clients = new List<Client>();

            using (var db = new FindThemContext())
            {
                clients = db.Clients
                            .Where(x => x.enabled == true)
                            .Include(user => user.user)
                            .Skip(20 * page).Take(20)
                            .ToList();
            }

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult get(Int64 id = 0)
        {
            var client = new Client();

            if (id == 0)
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    id = Convert.ToInt64(identity.FindFirst("userId").Value);
                }
            }

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

        [HttpGet]
        public IActionResult getWithoutId()
        {
            var client = new Client();

            Int64 id = 0;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                id = Convert.ToInt64(identity.FindFirst("userId").Value);
            }

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
                    var user = db.Users.FirstOrDefault(x => x.email == client.user.email);

                    client.user.password = Utils.GetMd5HashPassword(client.user.password);

                    if (user == null)
                    {
                        client.user = db.Users.Add(client.user).Entity;
                        db.SaveChanges();
                    } else
                    {
                        user.email = client.user.email;
                        user.name = client.user.name;
                        user.password = client.user.password;
                        user.photo = client.user.photo;

                        db.Users.Update(user);
                        db.SaveChanges();

                        client.user = user;
                    }

                    db.Clients.Add(client);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Ok(new { success = false, message = ex.Message });
                }
            }

            return Ok(new { success = true, message = "Register created with success", data = client });
        }

        [HttpPost("edit")]
        public IActionResult edit([FromBody]Client client)
        {

            if (client.user.password != string.Empty)
            {
                client.user.password = Utils.GetMd5HashPassword(client.user.password);
            }

            using (var db = new FindThemContext())
            {
                try
                {
                    var user = db.Users.FirstOrDefault(x => x.id == client.user.id);

                    user.email = client.user.email;
                    user.name = client.user.name;
                    user.photo = client.user.photo;

                    db.Users.Update(user);
                    db.SaveChanges();

                    client.user = user;

                    db.Clients.Update(client);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    return Ok(new { success = false, message = ex.Message });
                }
            }

            return Ok(new { success = true, message = "Register edited with success", data = client });
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
                    return Ok(new { success = false, message = ex.Message });
                }
            }

            return Ok(new { success = true, message = "Register deleted with success" });
        }
    }
}