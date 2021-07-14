using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }

        public string StatusName { get; set; }

        public ICollection<Admin> Admins { get; set; }

        public ICollection<Heading> Headings { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
