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
    public class QuizzyUsersController : Controller
    {
        private Flashcard_web_appContext db = new Flashcard_web_appContext();

        // GET: QuizzyUsers
        public ActionResult Index()
        {
            return View(db.QuizzyUsers.ToList());
        }

        // GET: QuizzyUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizzyUser quizzyUser = db.QuizzyUsers.Find(id);
            if (quizzyUser == null)
            {
                return HttpNotFound();
            }
            return View(quizzyUser);
        }

        // GET: QuizzyUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuizzyUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,Password")] QuizzyUser quizzyUser)
        {
            if (ModelState.IsValid)
            {
                db.QuizzyUsers.Add(quizzyUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quizzyUser);
        }

        // GET: QuizzyUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizzyUser quizzyUser = db.QuizzyUsers.Find(id);
            if (quizzyUser == null)
            {
                return HttpNotFound();
            }
            return View(quizzyUser);
        }

        // POST: QuizzyUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password")] QuizzyUser quizzyUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quizzyUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quizzyUser);
        }

        // GET: QuizzyUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizzyUser quizzyUser = db.QuizzyUsers.Find(id);
            if (quizzyUser == null)
            {
                return HttpNotFound();
            }
            return View(quizzyUser);
        }

        // POST: QuizzyUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuizzyUser quizzyUser = db.QuizzyUsers.Find(id);
            db.QuizzyUsers.Remove(quizzyUser);
            db.SaveChanges();
            return RedirectToAction("Index");
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
