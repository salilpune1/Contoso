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
using System.Threading.Tasks;

namespace Contoso.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ContactAsyncController : ControllerBase
    {
        private readonly ContactServiceAsync<ContactViewModel, Contact> _ContactServiceAsync;
        public ContactAsyncController(ContactServiceAsync<ContactViewModel, Contact> ContactServiceAsync)
        {
            _ContactServiceAsync = ContactServiceAsync;
        }


        //get all
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<ContactViewModel>> GetAll()
        {
            var items = await _ContactServiceAsync.GetAll();
            return items;
        }

        //get by predicate example
        //get all active by Contactname
        [Authorize]
        [HttpGet("GetActiveByFirstName/{firstname}")]
        public async Task<IActionResult> GetByFirstName(string firstname)
        {
            var items = await _ContactServiceAsync.Get(a => a.FirstName == firstname);
            return Ok(items);
        }

        //get one
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _ContactServiceAsync.GetOne(id);
            if (item == null)
            {
                Log.Error("GetById({ ID}) NOT FOUND", id);
                return NotFound();
            }

            return Ok(item);
        }

        //add
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactViewModel Contact)
        {
            if (Contact == null)
                return BadRequest();

            var id = await _ContactServiceAsync.Add(Contact);
            return Created($"api/Contact/{id}", id);  //HTTP201 Resource created
        }

        //update
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ContactViewModel Contact)
        {
            if (Contact == null || Contact.Id != id)
                return BadRequest();

            int retVal = await _ContactServiceAsync.Update(Contact);
            if (retVal == 0)
                return StatusCode(304);  //Not Modified
            else if (retVal == -1)
                return StatusCode(412, "DbUpdateConcurrencyException");  //412 Precondition Failed  - concurrency
            else
                return Accepted(Contact);
        }

        //delete
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int retVal = await _ContactServiceAsync.Remove(id);
            if (retVal == 0)
                return NotFound();  //Not Found 404
            else if (retVal == -1)
                return StatusCode(412, "DbUpdateConcurrencyException");  //Precondition Failed  - concurrency
            else
                return NoContent();   	     //No Content 204
        }

    }
}


