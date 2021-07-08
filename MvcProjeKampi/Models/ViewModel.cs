using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MvcProjeKampi.Models
{
    public class ViewModel
    {
        public HttpPostedFileBase File { get; set; }
    }
}