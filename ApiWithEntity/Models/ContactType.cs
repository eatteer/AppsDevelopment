using System;
using System.Collections.Generic;

namespace ApiWithEntity.Models
{
    public partial class ContactType
    {
        public ContactType()
        {
            Contacts = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
