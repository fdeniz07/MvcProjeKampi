using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using MvcProjeKampi.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization; //Dinamik ögeler icin

namespace MvcProjeKampi.Controllers
{

    public class GalleryController : Controller
    {
        ImageFileManager imageFileManager = new ImageFileManager(new EfImageFileDal());

        //HttpPostedFileBase file;


        public ActionResult Index(int? page)
        {
            var files = imageFileManager.GetList().ToPagedList(page ?? 1, 18); //? işaretleri boş gelme/boş olma durumuna 
            return View(files);
        }


        #region Resim Ekleme Bölümü  Hakkinda

        /*Bu bölüm 5 asamadan olusmaktadir
         *
         * 1.Asama : Veriler ImageFile Entity'si üzerinden DB'ye kaydedilir
         * 2.Asama : Kontoller icin BLL katmanindaki Utilities --> Common --> Helpers class'lari tarafindan gerekli kütüphaneler referans alinir. Kaynak linkleri iclerinde mevcuttur.
         * 3.Asama : Webconfig dosyasindaki server e yükleme süresi ve dosya boyutu ile de bagi mevcuttur.
         * 4.Asama : UI katmanindaki Model klasörü altindaki ImageViewModel class'inda dosya türleri ve boyutlari sinirlari tanimlanmistir.
         * 5.Asama : UI katmanindaki controller kismindan, yani buradan olusmaktadir. Burada su kontroller yapilir:
         *  - Resimlerin kayit edilecegi yolun kontrolü
         *  - Reimlerin ayni isimde olup olmadiginin kontrolü (Burasi icin ileride Ajax yapisi kullanilarak resim isimlerinin yüklenme asamasinda isminin degistirilmesi)
         *  - Resim boyutunun en fazla 5MB olarak kontrolünün yapilmasi
         *  - Belirledigimiz resim dosya formatlarinin (.jpg,.jpeg,.png,.gif) uzantilarinin kontrolü
         *
         * Yukarida yapilanlar tamamiyle server tarafinda yasanacak sorunlari backend tarafinda cözülmesine istinaden yazilmistir.
         *
         * Bu bölümleri kullanacak arkadaslara simdiden kolay gelsin.
         */

        #endregion

        [HttpGet]
        public ActionResult AddImage()
        {
            ImageViewModel imageModel = new ImageViewModel() { FileAttach = null, Message = string.Empty, IsValid = false };

            try { }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return this.View(imageModel);
        }

        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult AddImage(ImageViewModel imageModel, ImageFile imageFile)
        {
            //Eger dosya yolu mevcut degilse,yeni dosya yolu olustur
            string folderPath = Server.MapPath("~/Images/Gallery/");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            try
            {
                // Dogrulama Islemleri
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileName(Request.Files[0].FileName);
                    string expansion = Path.GetExtension(Request.Files[0].FileName);
                    string path = "/Images/Gallery/" + fileName + expansion;

                    //Dosya yükleme istek kontrolü
                    if (Request.Files.Count > 0)
                    {
                        var fullPath = Server.MapPath("/Images/Gallery/") + fileName + expansion;
                        //Resim dosyasi isim kontrolü
                        if (System.IO.File.Exists(fullPath))
                        {
                            ViewBag.ActionMessage = "Bu dosya adında başka bir resim mevcuttur";
                        }
                        else
                        {
                            Request.Files[0].SaveAs(Server.MapPath(path));
                            imageFile.ImageName = fileName;
                            imageFile.ImagePath = "/Images/Gallery/" + fileName + expansion;
                            imageFile.ImageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                            imageFileManager.ImageAdd(imageFile);
                            //ViewBag.ActionMessage = "Resim yükleme başarıyla gerçekleşmiştir.";
                            imageModel.Message = "'" + imageModel.FileAttach.FileName + "' dosya yükleme başarılı.   ";
                            return RedirectToAction("Index");
                        }
                    }

                    // Ayarlar - Dogrulama gecerliyse 
                    //imageModel.Message = "'" + imageModel.FileAttach.FileName + "' dosya yükleme başarılı.   ";
                    imageModel.IsValid = true;
                }
                else
                {
                    // Ayarlar - Dogrulama gecersizse  
                    imageModel.Message = "'" + imageModel.FileAttach.FileName + "' dosya yükleme başarısız!   ";
                    imageModel.IsValid = false;
                }
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }
            return View(imageModel);
        }


        [HttpGet]
        public ActionResult UpdateImage(int id)
        {
            ImageViewModel updateModel = new ImageViewModel();
           

            List<SelectListItem> _valueImage = (from x in imageFileManager.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.ImageName,
                                                    Value = x.ImageId.ToString()
                                                }).ToList();
            ViewBag.valueImage = _valueImage;
            ViewData["Image"] = imageFileManager.GetByIdImageFile(id);
            ViewData["Models"] = updateModel;
            return View(_valueImage);
        }

        [HttpPost]
        public ActionResult UpdateImage(ImageFile imageFile, string[] DynamicTextBox)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ViewBag.Values = serializer.Serialize(DynamicTextBox);

            string message = "";
            
            foreach (string textboxValue in DynamicTextBox)
            {
                message = textboxValue ;
            }
            ViewBag.Message = message;
            imageFile.ImageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            imageFileManager.ImageUpdate(imageFile);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteImage(int Id)
        {
            var imageValues = imageFileManager.GetByIdImageFile(Id);
            imageFileManager.ImageDelete(imageValues);
            return RedirectToAction("Index");
        }

    }
}