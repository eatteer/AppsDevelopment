using System;
using System.Collections.Generic;

namespace ApiWithEntity.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Phone { get; set; }
        public int ContactTypeId { get; set; }

        public virtual ContactType? ContactType { get; set; }
    }
}
