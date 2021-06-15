using System;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string UserMail { get; set; }

        [StringLength(50)]
        public string Subject { get; set; }

        public DateTime ContactDate { get; set; }

        public string Message { get; set; }

        public bool IsRead { get; set; }

        public bool IsImportant { get; set; }
    }
}
