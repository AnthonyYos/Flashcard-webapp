using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Flashcard_web_app.Models
{
    public class Deck
    {
        public int ID { get; set; }
        [Display(Name = "Deck Name")]
        public string DeckName { get; set; }
        public int QuizzyUserID { get; set; }
        [Display(Name = "Set as public")]
        public bool isPublic { get; set; }
        public ICollection<Notecard> notecards { get; set; }
    }
}