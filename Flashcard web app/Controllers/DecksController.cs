using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Flashcard_web_app.Models;

namespace Flashcard_web_app.Controllers
{
    [Authorize]
    public class DecksController : Controller
    {
        public ActionResult Index() {
            return View(db.Decks.ToList());
        }
        private Flashcard_web_appContext db = new Flashcard_web_appContext();

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = db.Decks.Find(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Deck model)
        {
            if (ModelState.IsValid)
            {

                Deck deck = new Deck() { DeckName = model.DeckName, QuizzyUserID = (int)Session["UserID"], isPublic = model.isPublic};
                db.Decks.Add(deck);
                db.SaveChanges();
                return RedirectToAction("Dashboard", "Account");
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = db.Decks.Find(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DeckName,QuizzyUserID,isPublic")]Deck deck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deck).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Dashboard","Account");
            }
            return View(deck);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = db.Decks.Find(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deck deck = db.Decks.Find(id);
            db.Decks.Remove(deck);
            foreach (Notecard card in db.Notecards.Where(a => a.DeckID == id))
                db.Notecards.Remove(card);
            db.SaveChanges();
            return RedirectToAction("Dashboard", "Account");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
