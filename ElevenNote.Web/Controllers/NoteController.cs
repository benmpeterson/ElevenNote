using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.Web.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        public ActionResult Index()
        {
            //TODO ASK DAVE HOW THIS IS CONNECTING TO NOTE VIEWS
            var model = new NoteListItem[0];
            return View(model); 
            
        }
    }
}