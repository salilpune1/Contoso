
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Contoso.Domain;
using Contoso.Entity.Context;
using Contoso.Domain.Service;
using Microsoft.Extensions.Logging;
using Serilog;
using Contoso.Entity;

namespace Contoso.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactService<ContactViewModel, Contact> _ContactService;
        public ContactController(ContactService<ContactViewModel, Contact> ContactService)
        {
            _ContactService = ContactService;
        }

        //get all
        //[Authorize]
        [HttpGet]
        public IEnumerable<ContactViewModel> GetAll()
        {
           
            var items = _ContactService.GetAll();
            return items;
        }

        //get by predicate example
        //get all active by Contactname
        //[Authorize]
        [HttpGet("GetActiveByFirstName/{firstname}")]
        public IActionResult GetActiveByFirstName(string firstname)
        {
            var items = _ContactService.Get(a => a.FirstName == firstname);
            return Ok(items);
        }

        //get one
        //[Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _ContactService.GetOne(id);
            if (item == null)
            {
                Log.Error("GetById({ ID}) NOT FOUND", id);
                return NotFound();
            }

            return Ok(item);
        }

        //add
        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Create([FromBody] ContactViewModel Contact)
        {
            if (Contact == null)
                return BadRequest();

            var id = _ContactService.Add(Contact);
            return Created($"api/Contact/{id}", id);  //HTTP201 Resource created
        }

        //update
        //[Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ContactViewModel Contact)
        {
            if (Contact == null || Contact.Id != id)
                return BadRequest();

            int retVal = _ContactService.Update(Contact);
            if (retVal == 0)
                return StatusCode(304);  //Not Modified
            else if (retVal == -1)
                return StatusCode(412, "DbUpdateConcurrencyException");  //412 Precondition Failed  - concurrency
            else
                return Accepted(Contact);
        }

        //delete
        //[Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int retVal = _ContactService.Remove(id);
            if (retVal == 0)
                return NotFound();  //Not Found 404
            else if (retVal == -1)
                return StatusCode(412, "DbUpdateConcurrencyException");  //Precondition Failed  - concurrency
            else
                return NoContent();          //No Content 204
        }

    }
}




