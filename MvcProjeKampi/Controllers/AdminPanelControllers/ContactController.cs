using System.Data.Entity.Core.Metadata.Edm;
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
        
        public ActionResult Index()
        {
            var contactValues = contactManager.GetList();
            return View(contactValues);
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactValues = contactManager.GetByIdContact(id);

           // contactManager.GetByIdContactAndSetRead(id,true); //<-- Buraya, mail acildiginda IsRead alanina true (okundu) yazacak kod gelecek!!!
            return View(contactValues);
        }

        public PartialViewResult PartialMessageMenu()
        {
            string session = (string)Session["AdminMail"];

            var contact = contactManager.GetList().Count();
            ViewBag.contact = contact;

            var sendMail = messageManager.GetListSendbox(session).Count();
            ViewBag.sendMail = sendMail;

            var receiverMail = messageManager.GetListInbox(session).Count();
            ViewBag.receiverMail = receiverMail;

            var draftMail = messageManager.GetListDraft(session).Count(); //GetListSendbox().Where(m => m.IsDraft == true).Count();
            ViewBag.draftMail = draftMail;

            var trashMail = messageManager.GetListTrash().Count();
            ViewBag.trashMail = trashMail;

            var readMail = messageManager.GetReadList(session).Count;
            ViewBag.readMail = readMail;

            var unReadMail = messageManager.GetUnReadList(session).Count;
            ViewBag.unReadMail = unReadMail;

            var importantMail = messageManager.GetListImportant(session).Count();
            ViewBag.importantMail = importantMail;

            var spamMail = messageManager.GetListSpam(session).Count();
            ViewBag.spamMail = spamMail;

            return PartialView();
        }

        public PartialViewResult PartialMessageList()
        {
            return PartialView();
        }

        public PartialViewResult PartialMessageListFooter()
        {
            return PartialView();
        }

        public PartialViewResult PartialMessageListFooterButton()
        {
            return PartialView();
        }

        public ActionResult IsRead(int id) //Bu alan sistem mesajlarindaki okundu butonundan gelen degeri DB yazar
        {
            var contactValue = contactManager.GetByIdContact(id);
           
            if (contactValue.IsRead)
            {
                contactValue.IsRead = false;
            }
            else
            {
                contactValue.IsRead = true;
            }

            contactManager.ContactUpdate(contactValue);
            return RedirectToAction("Index");
        }


        public ActionResult IsImportant(int id) //Bu alan sistem mesajlarindaki önemli butonundan gelen degeri DB yazar
        {
            var contactValue = contactManager.GetByIdContact(id);

            if (contactValue.IsImportant)
            { 
                contactValue.IsImportant = false;
            }
            else
            {
                contactValue.IsImportant = true;
            }

            contactManager.ContactUpdate(contactValue);
            return RedirectToAction("Index");
        }

    }
}