using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Flashcard_web_app.Models
{
    public class Flashcard_web_appContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public Flashcard_web_appContext() : base("name=Flashcard_web_appContext")
        {
        }
        public DbSet<Flashcard_web_app.Models.QuizzyUser> QuizzyUsers { get; set; }
        public DbSet<Flashcard_web_app.Models.Deck> Decks { get; set; }
        public DbSet<Flashcard_web_app.Models.Notecard> Notecards { get; set; }
    }
}
