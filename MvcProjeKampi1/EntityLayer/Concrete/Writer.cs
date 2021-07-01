using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Writer
    {
        [Key]
        public int WriterId { get; set; }

        [StringLength(50)]
        public string WriterUserName { get; set; }

        [StringLength(50)]
        public string WriterName { get; set; }

        [StringLength(50)]
        public string WriterSurName { get; set; }

        [StringLength(250)]
        public string WriterImage { get; set; }

        [StringLength(250)]
        public string WriterAbout { get; set; }

        public string WriterMail { get; set; }
        //public byte[] WriterMail { get; set; } //--> mail icin hash leme session de sorun yaratiyor

        public byte[] WriterPasswordHash { get; set; }

        public byte[] WriterPasswordSalt { get; set; }

        [StringLength(50)]
        public string WriterTitle { get; set; }

        public bool WriterStatus { get; set; }

        public string WriterRole { get; set; }

        public ICollection<Heading> Headings { get; set; } // Bir yazarin birden fazla basligi olabilir
        public ICollection<Content> Contents { get; set; } // Bir yazarin birden fazla icerigi olabilir
    }
}
