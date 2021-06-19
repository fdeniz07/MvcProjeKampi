using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class MessageController : Controller
    {

        MessageManager messageManager = new MessageManager(new EfMessageDal());
        MessageValidator messagerValidator = new MessageValidator();

        ContactManager contactManager = new ContactManager(new EfContactDal());

        [Authorize]
        public ActionResult Inbox()
        {

            var messageListInbox = messageManager.GetListInbox();
            return View(messageListInbox);
        }

        public ActionResult Sendbox()
        {
            var messageListSendbox = messageManager.GetListSendbox();
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

            //Yeni Mesaj sayfasındaki buton isimlerine göre kontroller aşagıdaki gibi yapılır

            //Eğer kullanıcı Gönder tuşuna basarsa;
            if (menuName == "send")
            {
                if (results.IsValid)
                {
                    message.SenderMail = "atilla@yahoo.com"; // Session'a baglanacak
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
                    message.SenderMail = "atilla@yahoo.com"; // Session'a baglanacak
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
            var result = messageManager.IsDraft();
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