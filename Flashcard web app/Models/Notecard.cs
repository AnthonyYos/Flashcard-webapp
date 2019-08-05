using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flashcard_web_app.Models
{
    public class Notecard
    {
       public int ID { get; set; }
       public int DeckID { get; set; }
       public string Question { get; set; }
       public string Answer { get; set; }
    }
}