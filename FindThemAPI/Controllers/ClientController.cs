using Microsoft.AspNetCore.Mvc;
using FindThemAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FindThemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpGet("FindAll")]
        public List<Client> FindAll()
        {
            var clients = new List<Client>();

            using (var db = new FindThemContext())
            {
                clients = db.Clients
                            .Include(user => user.user)
                            .ToList();
            }

            return clients;
        }
        [HttpGet("{id}")]
        public Client Get(int id)
        {
            var client = new Client();

            using (var db = new FindThemContext())
            {
                client = db.Clients
                           .Include(user => user.user)
                           .First(x => x.id == id);
            }

            return client;
        }
    }
}