using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class ScreenShot
    {
        [Key]
        public int ImageId { get; set; }

        [StringLength(100)]
        public string ImageName { get; set; }

        [StringLength(500)]
        public string ImagePath { get; set; }
    }
}
