using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using MvcProjeKampi.Models;
using PagedList;

namespace MvcProjeKampi.Controllers
{

    public class GalleryController : Controller
    {
        ImageFileManager imageFileManager = new ImageFileManager(new EfImageFileDal());

        HttpPostedFileBase file;


        public ActionResult Index(int? page)
        {
            var files = imageFileManager.GetList().ToPagedList(page ?? 1, 20); //? işaretleri boş gelme/boş olma durumuna 
            return View(files);
        }

        //[HttpGet]
        //public ActionResult AddImage()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult AddImage(ImageFile imageFile)
        //{
        //    if (Request.Files.Count > 0)
        //    {
        //        string fileName = Path.GetFileName(Request.Files[0].FileName);
        //        string expansion = Path.GetExtension(Request.Files[0].FileName);
        //        string path = "/Images/Gallery/" + fileName + expansion;
        //        Request.Files[0].SaveAs(Server.MapPath(path));
        //        imageFile.ImagePath = "/Images/Gallery/" + fileName + expansion;
        //        imageFileManager.ImageAdd(imageFile);
        //        return RedirectToAction("Index");

        //    }
        //    return View();
        //}

        [HttpGet]
        public ActionResult AddImage()
        {
            ImageViewModel imageModel = new ImageViewModel() { FileAttach = null, Message = string.Empty, IsValid = false };
            try
            {

            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Info.  
            return this.View(imageModel);

        }

        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult AddImage(ImageViewModel imageModel, ImageFile imageFile)//ImageFile imageFile
        {
            //Eger dosya yolu mevcut degilse,yeni dosya yolu olustur
            string folderPath = Server.MapPath("~/Images/Gallery/");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            try
            {
                // Dogrulama  
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileName(Request.Files[0].FileName);
                    string expansion = Path.GetExtension(Request.Files[0].FileName);
                    string path = "/Images/Gallery/" + fileName + expansion;

                    if (Request.Files.Count > 0)
                    {
                        var fullPath = Server.MapPath("/Images/Gallery/") + fileName + expansion;
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
            List<SelectListItem> _valueImage = (from x in imageFileManager.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.ImageName,
                                                    Value = x.ImageId.ToString()
                                                }).ToList();
            ViewBag.valueImage = _valueImage;
            var imageValues = imageFileManager.GetByIdImageFile(id);
            return View(imageValues);
        }

        [HttpPost]
        public ActionResult UpdateImage(ImageFile imageFile)
        {
            imageFileManager.ImageUpdate(imageFile);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteImage(int Id)
        {
            var imageValues = imageFileManager.GetByIdImageFile(Id);
            imageFileManager.ImageUpdate(imageValues);
            return RedirectToAction("Index");
        }
    }
}