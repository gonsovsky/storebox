using System;
using Contacts.Helpers;
using Contacts.Models;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;

namespace Contacts.Controllers
{
    public class ContactController : Controller
    {
        public ContactList Contacts => ContactList.Contacts;

        public ActionResult GetContacts()
        {
            return Json(Contacts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddContact(Contact contact)
        {
            try
            {
                if (!contact.IsValid)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Contacts.Add(contact);
                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
        }

        public ActionResult UpdateContact(Contact contact)
        {
            try
            {
                if (!contact.IsValid)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Contacts.ById(contact.Id).Assign(contact);
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
        }

        public ActionResult DeleteContact(int id)
        {
            var contact = Contacts.ById(id);
            try
            {
                Contacts.Remove(contact);
                return new HttpStatusCodeResult(HttpStatusCode.OK); 
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
        }

        public ActionResult GetPhoto(int id)
        {
            var data = Contacts.ById(id).Photo;
            return new FileContentResult(data,"image/jpeg");
        }

        [HttpPost]
        public string UploadPhoto(int id)
        {
            var c = Contacts.ById(id);
            var aFile = Request.Files[0];
            if (aFile == null)
                return $"Ошибка загрузки картинки для {c.Fio}.";
            c.Photo = new byte[aFile.ContentLength];
            aFile.InputStream.Read(c.Photo, 0, c.Photo.Length);
            return $"Картинка для {c.Fio} загружена.";
        }
    }
}