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
    public class NotecardsController : Controller
    {
        private Flashcard_web_appContext db = new Flashcard_web_appContext();

        public ActionResult Notecards(int? id)
        {
            Session["DeckID"] = id;
            return View(db.Notecards.ToList());
        }

        public ActionResult Quiz(int? id)
        {
            Session["DeckID"] = id;
            return View(db.Notecards.ToList());
        }


        public ActionResult Create(int? id) {
            Session["DeckID"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Notecard model) {
            if (ModelState.IsValid) {
                Notecard notecard = new Notecard() { DeckID = (int)Session["DeckID"], Question = model.Question, Answer = model.Answer };
                db.Notecards.Add(notecard);
                Deck deck = db.Decks.Find((int)Session["DeckID"]);
                deck.notecards.Add(notecard);
                db.SaveChanges();
                return RedirectToAction("Notecards", new { id = (int)Session["DeckID"] });
            }

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notecard notecard = db.Notecards.Find(id);
            if (notecard == null)
            {
                return HttpNotFound();
            }
            return View(notecard);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notecard notecard = db.Notecards.Find(id);
            if (notecard == null)
            {
                return HttpNotFound();
            }
            return View(notecard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DeckID,Question,Answer")] Notecard notecard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notecard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Notecards", new { id = (int)Session["DeckID"] });
            }
            return View(notecard);
        }

        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notecard notecard = db.Notecards.Find(id);
            if (notecard == null) {
                return HttpNotFound();
            }
            return View(notecard);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Notecard notecard = db.Notecards.Find(id);
            db.Notecards.Remove(notecard);
            db.SaveChanges();
            return RedirectToAction("Notecards", new { id = (int)Session["DeckID"] });
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
