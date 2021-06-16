using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class SkillArea
    {
        [Key]
        public int SkillAreaId { get; set; }

        [StringLength(100)]
        public string Area { get; set; }

        public string AreaDetails { get; set; }

        public ICollection<Talent> Talent { get; set; }
    }
}
