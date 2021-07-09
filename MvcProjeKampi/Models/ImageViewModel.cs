using System.Collections.Generic;
using BusinessLayer.Utilities.Common.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Web;
using EntityLayer.Concrete;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace MvcProjeKampi.Models
{
    /// <summary>  
    /// Image view model class.  
    /// </summary>  
    public class ImageViewModel
    {
        #region Properties  

        [Required]
        [Display(Name = "Upload File")]
        [AllowFileSize(FileSize = 5 * 1024 * 1024, ErrorMessage = "İzin verilen dosya boyutu : 5 MB")] //Resim boyutu en fazla 5MB
        [AllowExtensions(Extensions = "png,jpg,jpeg,gif,PNG,JPG,JPEG,GIF", ErrorMessage = "Desteklenen dosya uzantıları :  .png | .jpg | .jpeg | .gif")] //Desteklenen formatlar
        public HttpPostedFileBase FileAttach { get; set; }

        public string Message { get; set; }

        public bool IsValid { get; set; }

        /*
        Detayli bilgiler icin asagidaki aciklamalardan ya da kaynak adreslerinden yararlanabilirsiniz.


        In the above code, I have created my View Model which I will attach with my View. Here, 
        I have created HttpPostedFileBase type file attachment property which will capture the uploaded image/file data from the end-user. 
        Then, I have also applied my custom "AllowFileSize" attribute to the FileAttach property and provided the default file size as 5 MB that 
        have allowed my system to accept. Then, I have created two more properties, i.e., Message of data type string and isValid of data type Boolean for processing purpose.
        https://www.c-sharpcorner.com/article/asp-net-mvc5-limit-upload-file-size/


        In the above code, I have created my view model which I will attach with my view. Here, 
        I have created HttpPostedFileBase type file attachment property which will capture uploaded image/file data from the end-user, 
        then I have also applied my custom "AllowExtensions" attribute to the FileAttach property and 
        provide the list of file type extensions separated by a comma (,) that I have allowed my system to accept. 
        Then, I have created two more properties; i.e., Message of data type string and isValid of data type Boolean for processing purpose.
        https://www.c-sharpcorner.com/article/asp-net-mvc5-limit-upload-file-type-extensions-via-custom-data-annotationattr/
         */

        #endregion



        // UpdateGallery sayfasinda coklu model ihtiyacindan bu sekilde cözüm üretildi.

        /* Kaynak icin buradan yararlanabilirsiniz:
         *
         *https://www.c-sharpcorner.com/UploadFile/ff2f08/multiple-models-in-single-view-in-mvc/
         *
         */
    }
}