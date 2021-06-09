using System.Linq;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System.Web.Mvc;
using BusinessLayer.ValidationRules;

namespace MvcProjeKampi.Controllers
{
    public class ContactController : Controller
    {
        ContactManager contactManager = new ContactManager(new EfContactDal());
        MessageManager messageManager = new MessageManager(new EfMessageDal());
        

        ContactValidator contactValidator = new ContactValidator();

        public ActionResult Index()
        {
            var contactValues = contactManager.GetList();
            return View(contactValues);
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactValues = contactManager.GetByIdContact(id);
            return View(contactValues);
        }

        public PartialViewResult MessageListMenu()
        {
            var contact = contactManager.GetList().Count();
            ViewBag.contact = contact;

            var sendMail = messageManager.GetListSendbox().Count();
            ViewBag.sendMail = sendMail;

            var receiverMail = messageManager.GetListInbox().Count();
            ViewBag.receiverMail = receiverMail;

            var draftMail = messageManager.GetListSendbox().Where(m => m.IsDraft == true).Count();
            ViewBag.draftMail = draftMail;

            return PartialView();
        }
    }
}