﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIApplication.Models;

namespace WebAPIApplication.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly Context _context;
        
        public MessagesController(Context context)
        {
            _context = context;
            if (!_context.Messages.Any())
            {
                _context.Messages.Add(new Message {Text="TestMessage1", CreationDate=DateTime.UtcNow.ToString(), HostName= Dns.GetHostName().ToString(), HostIP= Dns.GetHostAddresses(Dns.GetHostName()).Where(address => address.AddressFamily == AddressFamily.InterNetwork).First().ToString() });
                 _context.SaveChanges();
            }
        }

        public string IdSort { get; set; }
        public string DateTimeSort { get; set; }
        public string HostNameSort { get; set; }
        public string HostIPSort { get; set; }
        public string CurrestSort { get; set; }
        public string CurrentFilter { get; set; }

        // GET: api/Messages
        [Authorize]
        [HttpGet]
        public IEnumerable<Message> GetMessages(/*string sortOrder*/)
        {
            //IdSort = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            //DateTimeSort = sortOrder == "DateTime" ? "datetime_desc" : "DateTime";
            //HostNameSort = sortOrder == "HostName" ? "hostname_desc" : "HostName";
            //HostIPSort = sortOrder == "HostIP" ? "hostip_desc" : "HostIP";

            //IQueryable<Message> messages = from m in _context.Messages
            //                                select m;
            //switch (sortOrder)
            //{
            //    case "id_desc":
            //        messages = messages.OrderByDescending(m => m.Id);
            //        break;
            //    case "DateTime":
            //        messages = messages.OrderBy(m => m.CreationDate);
            //        break;
            //    case "datetime_desc":
            //        messages = messages.OrderByDescending(m => m.CreationDate);
            //        break;
            //    case "HostName":
            //        messages = messages.OrderBy(m => m.HostName);
            //        break;
            //    case "hostname_desc":
            //        messages = messages.OrderByDescending(m => m.HostName);
            //        break;
            //    case "HostIP":
            //        messages = messages.OrderBy(m => m.HostIP);
            //        break;
            //    case "hostip_desc":
            //        messages = messages.OrderByDescending(m => m.HostIP);
            //        break;
            //    default:
            //        messages = messages.OrderBy(m => m.Id);
            //        break;
            //}

            //await messages.AsNoTracking().ToListAsync();
            return  _context.Messages.ToList();
        }

        // GET: api/Messages/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // PUT: api/Messages/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage([FromRoute] int id, [FromBody] Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.Id)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Messages
        
        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessage", new { id = message.Id }, message);
        }

        // DELETE: api/Messages/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return Ok(message);
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}