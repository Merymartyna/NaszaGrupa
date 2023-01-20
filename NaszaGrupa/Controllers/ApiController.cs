using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NaszaGrupa.Models;

namespace NaszaGrupa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly MessageContext _context;

        public ApiController(MessageContext context)
        {
            _context = context;

            if (_context.Messages.Count() == 0)
            {
                _context.Messages.Add(new MessagesModel { Header = "Product 1" });
                _context.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<MessagesModel> GetProducts()
        {
            return _context.Messages.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<MessagesModel> GetProduct(int id)
        {
            var Product = _context.Messages.Find(id);

            if (Product == null)
            {
                return NotFound();
            }

            return Product;
        }
        [HttpPut("{id}")]
        public IActionResult PutMessage(int id, MessagesModel message)
        {
            if (id != message.MessageId)
            {
                return BadRequest();
            }

            _context.Messages.Update(message);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id, MessagesModel message)
        {
            if (id != message.MessageId)
            {
                return BadRequest();
            }

            _context.Messages.Update(message);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

