using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProjeKampi.Models;

namespace MvcProjeKampi.Controllers.AdminPanelControllers
{

    // Bu Controller sadece test amacli olusturulmsutur. Burayi almaniza gerek yoktur.
    /// <summary>  
    /// Image controller class.  
    /// </summary>  
    public class ImageTestController : Controller
    {
        #region Index view method.  

        #region Get: /Img/Index method.  

        /// <summary>  
        /// Get: /Img/Index method.  
        /// </summary>          
        /// <returns>Return index view</returns>  
        public ActionResult Index()
        {
            // Initialization/  
            ImageViewModel sizeModel = new ImageViewModel() { FileAttach = null, Message = string.Empty, IsValid = false };

            try
            {
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Info.  
            return this.View(sizeModel);
        }

        #endregion

        #region POST: /Img/Index  

        /// <summary>  
        /// POST: /Img/Index  
        /// </summary>  
        /// <param name="model">Model parameter</param>  
        /// <returns>Return - Response information</returns>  
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ImageViewModel model)
        {
            try
            {
                // Verification  
                if (ModelState.IsValid)
                {
                    // Settings.  
                    model.Message = "'" + model.FileAttach.FileName + "' dosya yükleme başarılı.   ";
                    model.IsValid = true;
                }
                else
                {
                    // Settings.  
                    model.Message = "'" + model.FileAttach.FileName + "' dosya yükleme başarısız!   ";
                    model.IsValid = false;
                }
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Info  
            return this.View(model);
        }

        #endregion

        #endregion

        [HttpGet]
        public ActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            //Eger dosya yolu mevcut degilse,yeni dosya yolu olustur
            string folderPath = Server.MapPath("~/Images/Gallery/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (file != null && file.ContentLength > 0)
            {
                if (file.ContentType == "image/jpeg" ||
                    file.ContentType == "image/jpg" ||
                    file.ContentType == "image/gif" ||
                    file.ContentType == "image/png")
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var userFolderPath = Path.Combine(Server.MapPath("~/Images/Gallery/"), fileName);
                    var fullPath = Server.MapPath("~/Images/Gallery/") + file.FileName;
                    if (System.IO.File.Exists(fullPath))
                    {
                        ViewBag.ActionMessage = "Bu dosya adında başka bir resim mevcuttur";
                    }
                    else
                    {
                        file.SaveAs(userFolderPath);
                        ViewBag.ActionMessage = "Resim yükleme başarıyla gerçekleşmiştir.";
                    }
                }
                else
                {
                    ViewBag.ActionMessage = "Lütfen sadece jpeg / jpg / gif / png uzantılı resimler yükleyiniz!)";
                }
            }
            return View();
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //[HttpPost]
        //public ActionResult Action(ViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var postedFile = Request.Files["File"];

        //        // now you can get and validate the file type:
        //        var isFileSupported = IsFileSupported(postedFile);
        //    }
        //}

        //public bool IsFileSupported(HttpPostedFileBase file)
        //{
        //    var isSupported = false;

        //    switch (file.ContentType)
        //    {

        //        case ("image/gif"):
        //            isSupported = true;
        //            break;

        //        case ("image/jpeg"):
        //            isSupported = true;
        //            break;

        //        case ("image/png"):
        //            isSupported = true;
        //            break;


        //        case ("image/jpg"):
        //            isSupported = true;
        //            break;

        //        case ("image/tiff"):
        //            isSupported = true;
        //            break;
        //    }

        //    return isSupported;
        //}
    }
}


