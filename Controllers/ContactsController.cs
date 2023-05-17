using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Api_Assignment_049_.Data;
using Web_Api_Assignment_049_.Models;

namespace Web_Api_Assignment_049_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactAPIDbContext dbContext;

        public ContactsController(ContactAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
           return Ok(await dbContext.Contacts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if(contact == null) 
            {
                return NotFound("Contact List is Empty. Plz Enter a valid id");
            }
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest) 
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                PhoneNumber = addContactRequest.PhoneNumber

            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContext([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact=await  dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.FullName= updateContactRequest.FullName;
                contact.Email= updateContactRequest.Email;
                contact.Address= updateContactRequest.Address;
                contact.PhoneNumber= updateContactRequest.PhoneNumber;
                
                await dbContext.SaveChangesAsync();

                return Ok(contact);
            }
            return NotFound();

        }

        [HttpDelete]
        [Route("{id:guid}")]
    public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if(contact != null) 
            {
             dbContext.Remove(contact);
             await dbContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound("Not Found!!!");
        }
    
    
    }
}
