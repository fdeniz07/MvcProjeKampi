using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        [StringLength(200)]
        public string CategoryDescription { get; set; }

        public DateTime CategoryDate { get; set; }

        //public bool CategoryStatus { get; set; }
        public int? StatusId { get; set; }

        public virtual Status Status { get; set; }

        public ICollection<Heading> Headings { get; set; } //Bire cok bir iliski kurulacak (Heading alani ile iliski kuracak)
    }
}
