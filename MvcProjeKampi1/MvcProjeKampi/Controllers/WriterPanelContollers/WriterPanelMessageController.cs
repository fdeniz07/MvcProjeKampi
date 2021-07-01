using System;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;

namespace MvcProjeKampi.Controllers.WriterPanelContollers
{
    public class WriterPanelMessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageDal());
        MessageValidator messagerValidator = new MessageValidator();

        public ActionResult Inbox(int? page)
        {
            string session = (string)Session["WriterMail"];
            var messageListInbox = messageManager.GetListInbox(session).ToPagedList(page ?? 1, 8); //? işaretleri boş gelme/boş olma durumuna karşı önlem                                                                                  amaçlı,kacinci sayfadan baslasin, sayfada kac deger olsun anlamina gelmektedir..
            return View(messageListInbox);
        }

        public PartialViewResult PartialMessageMenu()
        {
            string session = (string)Session["WriterMail"];
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

        public ActionResult Sendbox()
        {
            string session = (string)Session["WriterMail"];
            var messageListSendbox = messageManager.GetListSendbox(session);
            return View(messageListSendbox);
        }

        public ActionResult GetInboxMessageDetails(int id)
        {
            var values = messageManager.GetByIdMessage(id);
            return View(values);
        }

        public ActionResult GetSendboxMessageDetails(int id)
        {
            var values = messageManager.GetByIdMessage(id);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult NewMessage(Message message, string menuName)
        {
            ValidationResult results = messagerValidator.Validate(message);
            string session = (string)Session["WriterMail"];

            //Yeni Mesaj sayfasındaki buton isimlerine göre kontroller aşagıdaki gibi yapılır

            //Eğer kullanıcı Gönder tuşuna basarsa;
            if (menuName == "send")
            {
                if (results.IsValid)
                {
                    message.SenderMail = session; 
                    //message.IsDraft = false;
                    message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    messageManager.MessageAdd(message);
                    return RedirectToAction("Sendbox");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            //Eğer kullanıcı Taslaklara Kaydet tuşuna basarsa;
            else if (menuName == "draft")
            {
                if (results.IsValid)
                {
                    message.SenderMail = session; 
                    message.IsDraft = true;
                    message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    messageManager.MessageAdd(message);
                    return RedirectToAction("DraftMessages");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            //Eğer kullanıcı İptal tuşuna basarsa;
            else if (menuName == "cancel")
            {
                return RedirectToAction("NewMessage");
            }
            return View();
        }

        public ActionResult DeleteMessage(int id) //Bu alan gelen mesajlarindaki silindi butonundan gelen degeri DB yazar --> Henüz inbox da bu buton eklenmedi !!!
        {
            var result = messageManager.GetByIdMessage(id);
            if (result.Trash == true)
            {
                result.Trash = false;
            }
            else
            {
                result.Trash = true;
            }
            messageManager.MessageDelete(result);
            return RedirectToAction("Inbox");
        }

        public ActionResult DraftMessages()
        {
            string session = (string)Session["WriterMail"];
            var result = messageManager.IsDraft(session);
            return View(result);
        }

        public ActionResult GetDraftDetails(int id)
        {
            var result = messageManager.GetByIdMessage(id);
            return View(result);
        }

        public ActionResult IsRead(int id) //Bu alan gelen mesajlarindaki okundu butonundan gelen degeri DB yazar
        {
            var messageValue = messageManager.GetByIdMessage(id);

            if (messageValue.IsRead)
            {
                messageValue.IsRead = false;
            }
            else
            {
                messageValue.IsRead = true;
            }

            messageManager.MessageUpdate(messageValue);
            return RedirectToAction("Inbox");
        }


        public ActionResult IsImportant(int id) //Bu alan gelen mesajlarindaki önemli butonundan gelen degeri DB yazar
        {
            var messageValue = messageManager.GetByIdMessage(id);

            if (messageValue.IsImportant)
            {
                messageValue.IsImportant = false;
            }
            else
            {
                messageValue.IsImportant = true;
            }

            messageManager.MessageUpdate(messageValue);
            return RedirectToAction("Inbox");
        }
    }
}