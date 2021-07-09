using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BusinessLayer.Utilities.Common.Helpers
{
    /// <summary>  
    /// Allow file size attribute class  
    /// </summary>

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AllowFileSizeAttribute : ValidationAttribute
    {
        #region Public / Protected Properties  

        /// <summary>  
        /// Gets or sets file size property. Default is 1GB (the value is in Bytes).  
        /// </summary>  
        public int FileSize { get; set; } = 1 * 1024 * 1024 * 1024;

        #endregion

        #region Is valid method  

        /// <summary>  
        /// Is valid method.  
        /// </summary>  
        /// <param name="value">Value parameter</param>  
        /// <returns>Returns - true is specify extension matches.</returns>
        
        public override bool IsValid(object value)
        {
            // Initialization  
            HttpPostedFileBase file = value as HttpPostedFileBase;
            bool isValid = true;

            // Settings.  
            int allowedFileSize = this.FileSize;

            // Verification.  
            if (file != null)
            {
                // Initialization.  
                var fileSize = file.ContentLength;

                // Settings.  
                isValid = fileSize <= allowedFileSize;
            }

            // Info  
            return isValid;
        }

        /*
        Detayli bilgiler icin asagidaki aciklamalardan ya da kaynak adreslerinden yararlanabilirsiniz.

        In ASP.NET MVC5, creating customized data annotations/attributes is one of the cool features. In the above code, 
        I have created a new class "AllowFileSizeAttribute" (by following the naming convention of custom attribute class) and 
        inherited ValidationAttribute class. Then, I have created a public property "FileSize" and set its default value as 1 GB 
        in bytes which means that my custom attribute will accept only uploaded files with a maximum file size less than or equal to 1 GB. S
        o, in order to allow the required file size, this property will be updated at the time of my custom attribute utilization accordingly. 
        Finally, I have overridden the "IsValid(....)" method which will receive my uploaded file as "HttpPostedFileBase" data type and from this, 
        I will extract the file size of the upload file and then validated whether it is less than or equal to the default file size restriction or 
        according to my provided file size.

        https://www.c-sharpcorner.com/article/asp-net-mvc5-limit-upload-file-size/
         */

        #endregion
    }
}
