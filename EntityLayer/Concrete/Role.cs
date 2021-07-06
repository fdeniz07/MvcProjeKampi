using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [StringLength(1)]
        public string RoleName { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public ICollection<Admin> Admins { get; set; }

    }
}
