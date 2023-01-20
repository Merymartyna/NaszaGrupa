using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NaszaGrupa.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace NaszaGrupa.Controllers
{
    public class MessagesController : Controller
    {
        private readonly MessageContext db;

        public MessagesController(MessageContext context)
        {
            db = context;
        }

        // GET: MessagesController
        public ActionResult Index()
        {
            return View("Index", db.Messages.OrderByDescending(c => c.MessageId));
        }

        // GET: MessagesController/Details/5
        public ActionResult Details(int id)
        {
            var Message = db.Messages.Find(id);
            if (Message == null)
                return NotFound();
            return View(Message);
        }

        // GET: MessagesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MessagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MessagesModel newmessageModel)
        {
            if (ModelState.IsValid)
            {
                db.Add(newmessageModel); //dodanie do proxy -> RAM
                db.SaveChanges();

                TempData["message"] = "Został dodany nowy komunikat:  " + newmessageModel.Header + ".";
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: MessagesController/Edit/5
        public ActionResult Edit(int id = 0)
        {
            MessagesModel messagesModel = db.Messages.Find(id);
            if (messagesModel == null)
            {
                return HttpNotFound();
            }
            return View(messagesModel);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        // POST: MessagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MessagesModel messagesModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messagesModel).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Komunikat: " + messagesModel.Header + " został edytowany";
                return RedirectToAction("Index");
            }
            return View(messagesModel);
        }

        // GET: MessagesController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessagesModel messagesModel = db.Messages.Find(id);
            if (messagesModel == null)
            {
                return HttpNotFound();
            }
            return View(messagesModel);
        }

        // POST: MessagesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            MessagesModel messagesModel = db.Messages.Find(id);
            db.Messages.Remove(messagesModel);
            db.SaveChanges();
            TempData["message"] = "Komunikat: " + messagesModel.Header + " został usunięty";
            return RedirectToAction("Index");
        }
    }
}
