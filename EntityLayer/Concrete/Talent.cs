using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Talent
    {
        [Key]
        public int SkillId { get; set; }

        [StringLength(100)]
        public string SkillName { get; set; }

        public string SkillDetails { get; set; }

        public byte SkillLevel { get; set; }

        public int? SkillAreaId { get; set; }
        public virtual SkillArea SkillArea { get; set; }
    }
}
