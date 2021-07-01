using EntityLayer.Concrete;
using System.Data.Entity;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        // Buraya EntityLayer daki sayfalar geliyor ve isimlendirmelerinin sonu cogul ek alacak!
        public DbSet<About> Abouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Heading> Headings { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ImageFile> ImageFiles { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Talent> Talents { get; set; }
        public DbSet<SkillArea> SkillAreas { get; set; }
        public DbSet<ScreenShot> ScreenShots { get; set; }
    }
}
