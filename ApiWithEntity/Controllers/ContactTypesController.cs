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
        private ApiWithEntityContext _dbContext;
        public ContactTypesController(ApiWithEntityContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<ContactType>>> findAll()
        {
            // Find all contact types
            List<ContactType> contactTypes = await _dbContext.ContactTypes
                .Include(contactTypes => contactTypes.Contacts)
                .ToListAsync();

            return Ok(contactTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactType>> findById([FromRoute] int id)
        {
            // Find contact type by id
            ContactType? contactType = await _dbContext.ContactTypes
                .Include(contactTypes => contactTypes.Contacts)
                .Where(contactType => contactType.Id == id)
                .FirstOrDefaultAsync();

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
            await _dbContext.ContactTypes.AddAsync(contactType);
            int entriesWritten = await _dbContext.SaveChangesAsync();

            // No entries written
            if (entriesWritten == 0) return Problem();

            // Setup response parameters
            string actionName = nameof(findById);
            object routeValues = new { id = contactType.Id };

            return CreatedAtAction(actionName, routeValues, contactType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateById([FromRoute] int id, [FromBody] ContactTypeDto contactTypeDto)
        {
            // Find contact type by id
            ContactType? contactType = await _dbContext.ContactTypes.FindAsync(id);

            // Contact type not found
            if (contactType == null) return NotFound();

            // Update contact
            contactType.Description = contactTypeDto.Description;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteById([FromRoute] int id)
        {
            // Find contact type by id
            ContactType? contactType = await _dbContext.ContactTypes.FindAsync(id);

            // Contact type not found 
            if (contactType == null) return NotFound();

            // Remove contact type from database
            _dbContext.ContactTypes.Remove(contactType);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
