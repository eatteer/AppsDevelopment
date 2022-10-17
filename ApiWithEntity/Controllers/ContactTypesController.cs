using ApiWithEntity.DTOs;
using ApiWithEntity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWithEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactTypesController : ControllerBase
    {
        ApiWithEntityContext dbContext;
        public ContactTypesController(ApiWithEntityContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<ContactType>>> findAll()
        {
            // Find contact type by id
            List<ContactType> contactTypes = await dbContext.ContactTypes.ToListAsync();

            return Ok(contactTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactType>> getById([FromRoute] int id)
        {
            // Find contact type by id
            ContactType contactType = await dbContext.ContactTypes.FindAsync(id);

            // Contact type not found
            if (contactType == null) return NotFound();

            return Ok(contactType);
        }

        [HttpPost]
        public async Task<IActionResult> create([FromBody] ContactTypeDto contactTypeDto)
        {
            // Create contact type from DTO
            ContactType contactType = new ContactType { Description = contactTypeDto.Description };

            // Save contact type in database
            await dbContext.AddAsync(contactType);
            int entriesWritten = await dbContext.SaveChangesAsync();

            // No entries written
            if (entriesWritten == 0) return BadRequest();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateById([FromRoute] int id, [FromBody] ContactTypeDto contactTypeDto)
        {
            // Find contact type by id
            ContactType contactType = await dbContext.ContactTypes.FindAsync(id);

            // Contact type not found
            if (contactType == null) return NotFound();

            contactType.Description = contactTypeDto.Description;

            // Save updated contact type in database
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteById([FromRoute] int id)
        {
            // Find contact type by id
            ContactType contactType = await dbContext.ContactTypes.FindAsync(id);

            // Contact type not found 
            if (contactType == null) return NotFound();

            // Remove contact type from database
            dbContext.Remove(contactType);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
