using ApiWithEntity.Models;
using ApiWithEntity.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWithEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private ApiWithEntityContext _dbContext;
        public ContactsController(ApiWithEntityContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Contact>>> FindAll()
        {
            // Find all contacts
            List<Contact> contacts = await _dbContext.Contacts
                .Include(contact => contact.ContactType)
                .ToListAsync();

            return Ok(contacts);
        }

        [HttpGet("{id}")]
        [ActionName(nameof(FindById))]
        public async Task<ActionResult<Contact>> FindById([FromRoute] int id)
        {
            // Find contact by id
            Contact? contact = await _dbContext.Contacts
                .Include(contact => contact.ContactType)
                .Where(contact => contact.Id == id)
                .FirstOrDefaultAsync();

            // Contact not found
            if (contact == null) return NotFound();

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactDto contactDto)
        {
            // Find contact type for inserting in the contact that will be created
            int? id = contactDto.ContactTypeId;
            ContactType? contactType = await _dbContext.ContactTypes.FindAsync(id);

            // Contact type not found
            if (contactType == null) return NotFound("Contact type not found");

            // Create contact
            Contact contact = new Contact
            {
                Name = contactDto.Name,
                Description = contactDto.Description,
                Phone = contactDto.Phone,
                ContactTypeId = contactType.Id
            };

            // Save new contact and update contact type in database
            await _dbContext.Contacts.AddAsync(contact);
            int entriesWritten = await _dbContext.SaveChangesAsync();

            // No entries written
            if (entriesWritten == 0) Problem();

            // Setup response parameters
            string actionName = nameof(FindById);
            object routeValues = new { id = contact.Id };

            return CreatedAtAction(actionName, routeValues, contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById([FromRoute] int id, [FromBody] ContactDto contactDto)
        {
            // Find contact by id
            Contact? contact = await _dbContext.Contacts.FindAsync(id);

            // Contact not found
            if (contact == null) return NotFound();

            // Update contact
            contact.Name = contactDto.Name ?? contact.Name;
            contact.Description = contactDto.Description ?? contact.Description;
            contact.Phone = contactDto.Phone ?? contact.Phone;
            contact.ContactTypeId = contactDto.ContactTypeId ?? contact.ContactTypeId;
            int entriesWritten = await _dbContext.SaveChangesAsync();

            // No entries written
            if (entriesWritten == 0) return Problem();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            // Find contact by id
            Contact? contact = await _dbContext.Contacts.FindAsync(id);

            // Contact not found
            if (contact == null) return NotFound();

            // Delete contact from database
            _dbContext.Contacts.Remove(contact);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
