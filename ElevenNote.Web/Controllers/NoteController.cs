using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
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
            var service = CreateNoteService();
            var model = service.GetNotes(); 
            return View(model);             
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                //TempData is a dictionary that displays text per user in view then is removed only displaying the 
                //Value of the key
                TempData ["SaveResult"] = "Your note was created";
                return RedirectToAction("Index");
            }

            //If it fails the ModelState.AddModelError would display that the note was not created in the validation summary
            ModelState.AddModelError("", "Your note could not be create.");
            return View(model);

        }

        //Refactored this method since both httpget Create and httppost use the same two lines of code
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }
    }
} 