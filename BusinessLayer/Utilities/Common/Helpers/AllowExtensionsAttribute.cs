using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessLayer.Utilities.Common.Helpers
{
    /// <summary>  
    /// File extensions attribute class  
    /// </summary>  

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AllowExtensionsAttribute : ValidationAttribute
    {
        #region Public / Protected Properties  

        /// <summary>  
        /// Gets or sets extensions property.  
        /// </summary>  
        public string Extensions { get; set; } = "png,jpg,jpeg,gif";

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
            List<string> allowedExtensions = this.Extensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            // Verification.  
            if (file != null)
            {
                // Initialization.  
                var fileName = file.FileName;

                // Settings.  
                isValid = allowedExtensions.Any(y => fileName.EndsWith(y));
            }

            // Info  
            return isValid;
        }

        #endregion

        /*
        Detayli bilgiler icin asagidaki aciklamalardan ya da kaynak adreslerinden yararlanabilirsiniz.

        In ASP.NET MVC 5, creating customized data annotations/attributes is one of the cool features. 
        The ASP.NET MVC 5 platform already contains a default FileExtensions attribute, but, 
        the issue with this pre-built data annotation/attribute is that it is applicable only on string type view model properties and in my case, 
        I am uploading the files via "HttpPostedFileBase" data type view model property. 
        This means that the pre-built data annotation/attribute does not have any means to know the data type of the file(s) 
        that I am uploading which will have  no effect on the limitation that is considered to be applied on the uploaded file type extensions.
        Of course, there are many other tricks or workarounds to go through while working with the pre-built FileExtensions attribute, but, 
        I prefer the custom data annotation/attribute mechanism, which is much simpler.

        So, in the above code, I have created a new class "AllowExtensionsAttribute" 
        (by following the naming convention of custom attribute class) and inherited the ValidationAttribute class. 
        Then, I have created a public property "Extensions" and set the default value with image file type extensions, 
        which means that my custom attribute will accept only image file type to be uploaded. So, in order to allow the required file type extensions, 
        this property will be updated at the time of my custom attribute utilization accordingly. 
        Finally, I have overridden the "IsValid(....)" method which will receive my uploaded file as "HttpPostedFileBase" data type and from this,
        I will extract the file type extension of the uploaded file and then validate whether it is according to either default file type extension restriction 
        or according to my provided file type extensions.

        https://www.c-sharpcorner.com/article/asp-net-mvc5-limit-upload-file-type-extensions-via-custom-data-annotationattr/
         */
    }
}
