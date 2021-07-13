using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [StringLength(50)]
        public string AdminUserName { get; set; }

        //public string  AdminMail { get; set; }
        public byte[] AdminMail { get; set; }

        public byte[] AdminPasswordHash { get; set; }

        public byte[] AdminPasswordSalt { get; set; }

        //public bool AdminStatus { get; set; }
        public int? StatusId { get; set; }

        public virtual Status Status { get; set; }
        
        public int? RoleId { get; set; }

        public  virtual Role Role { get; set; }


    }
}

//Ödev : Kullanici adi ve sifre Hash'lenerek Veritabanina kaydedilecek ev ayni sekilde cözülecek

