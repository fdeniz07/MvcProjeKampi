using BusinessLayer.Concrete;
using System.Web.Mvc;
using DataAccessLayer.EntityFramework;

namespace MvcProjeKampi.Controllers
{
    public class MessageController : Controller
    {

       MessageManager messageManager = new MessageManager(new EfMessageDal());


        public ActionResult Inbox()
        {
            var messageList = messageManager.GetList();
            return View(messageList);
        }
    }
}