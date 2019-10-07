using Microsoft.AspNetCore.Mvc;
using FindThem.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;

namespace FindThem.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpGet("{requestID}")]
        public ActionResult get (Int64 requestID)
        {
            var messages = new List<Message>();

            using (var db = new FindThemContext())
            {
                messages = db.Messages
                            .Where(x => x.request.id == requestID)
                           .ToList();
            }

            return Ok(messages);
        }

        [HttpPost("{requestID}")]
        public ActionResult set(Int64 requestID, [FromBody] Message message)
        {
            using (var db = new FindThemContext())
            {
                message = db.Messages
                            .Add(message).Entity;
            }

            return Ok(message);
        }
    }
}