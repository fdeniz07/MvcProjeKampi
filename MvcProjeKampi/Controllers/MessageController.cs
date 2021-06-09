using System;
using BusinessLayer.Concrete;
using System.Web.Mvc;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using BusinessLayer.ValidationRules;
using FluentValidation.Results;

namespace MvcProjeKampi.Controllers
{
    public class MessageController : Controller
    {

        MessageManager messageManager = new MessageManager(new EfMessageDal());
        MessageValidator messagerValidator = new MessageValidator();

        ContactManager contactManager = new ContactManager(new EfContactDal());

        public ActionResult Inbox()
        {
            var messageList = messageManager.GetListInbox();
            return View(messageList);
        }

        public ActionResult Sendbox()
        {
            var messageList = messageManager.GetListSendbox();
            return View(messageList);
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

        [HttpPost]
        public ActionResult NewMessage(Message message, string menuName)
        {
            ValidationResult results = messagerValidator.Validate(message);

            //Yeni Mesaj sayfasındaki buton isimlerine göre kontroller aşagıdaki gibi yapılır

            //Eğer kullanıcı Gönder tuşuna basarsa;
            if (menuName == "send")
            {
                if (results.IsValid)
                {
                    message.SenderMail = "admin@gmail.com";
                    message.IsDraft = false;
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
                    message.SenderMail = "admin@gmail.com";
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

        public ActionResult DeleteMessage(int id)
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

        //public PartialViewResult MessageMenuDetails(int id)
        //{
        //    messageCounter();
        //    var messageValues = messageManager.GetByIdMessage(id);
        //    return PartialView(messageValues.MessageContent);
        //}

        //public void messageCounter()
        //{
        //    ViewBag.counterOfSystemMessages = contactManager.GetList().Count;
        //    ViewBag.counterOfInboxMessages = messageManager.GetListInbox().Count;
        //    ViewBag.ocunterOfSendMessages = messageManager.GetListSendbox().Count;
        //}
    }

    //    if (results.IsValid)
    //{
    //    message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
    //    messageManager.MessageAdd(message);
    //    return RedirectToAction("Sendbox");
    //}
    //else
    //{
    //    foreach (var item in results.Errors)
    //    {
    //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
    //    }
    //}
    //return View();
}