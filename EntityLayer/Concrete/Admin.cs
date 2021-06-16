using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [StringLength(50)]
        public string AdminUserName { get; set; }


        public byte[] AdminMail { get; set; }

        public byte[] AdminPasswordHash { get; set; }

        public byte[] AdminPasswordSalt { get; set; }

        [StringLength(1)]
        public string AdminRole { get; set; }
    }
}

//Ödev : Kullanici adi ve sifre Hash'lenerek Veritabanina kaydedilecek ev ayni sekilde cözülecek

