using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Heading
    {
        [Key]
        public int HeadingId { get; set; }

        [StringLength(50)]
        public string HeadingName { get; set; }

        public DateTime HeadingDate { get; set; }

        //public bool HeadingStatus { get; set; }
        public int? StatusId { get; set; }

        public virtual Status Status { get; set; }

        public int CategoryId { get; set; } //Iliskili tablonun anahtar sütunu ile ayni isimde olacak!!!
                                            //(Iliskili sinif Category tablosu, en cok hata alinabilen alan)

        public virtual Category Category { get; set; } //Bir siniftan deger alacak

        public int WriterId { get; set; }
        public virtual Writer Writer { get; set; }

        public ICollection<Content> Contents { get; set; } //Baslik alani (Heading) da icerik alani (content) ile iliskili
    }
}
