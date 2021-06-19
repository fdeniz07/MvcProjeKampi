using System.Web.Mvc;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;

namespace MvcProjeKampi.Controllers.WriterPanelContollers
{
    public class WriterPanelMessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageDal());
        MessageValidator messagerValidator = new MessageValidator();

        public ActionResult Inbox()
        {
            var messageListInbox = messageManager.GetListInbox();
            return View(messageListInbox);
        }
    }
}