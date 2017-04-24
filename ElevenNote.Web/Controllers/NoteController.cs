using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace ElevenNote.Web.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        //Refactored this method since both httpget Create and httppost use the same two lines of code
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }

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
                //TODO WHY COULDN'T YOU DO RETURN View(Index)
                return RedirectToAction("Index");
            }

            //If it fails the ModelState.AddModelError would display that the note was not created in the validation summary
            ModelState.AddModelError("", "Your note could not be create.");
            return View(model);

        }

        public ActionResult Details(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);
           

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            var model =
                new NoteEdit
                {
                    NoteId = detail.NoteId,
                    Title = detail.Title,
                    Content = detail.Content
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.NoteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateNoteService();

            if (service.UpdateNote(model))
            {
                //TempData is a dictionary that displays text per user in view then is removed only displaying the 
                //Value of the key
                TempData["SaveResult"] = "Your note was updated";
                //TODO WHY COULDN'T YOU DO RETURN View(Index)
                return RedirectToAction("Index");
            }

            //If it fails the ModelState.AddModelError would display that the note was not created in the validation summary
            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }


        public ActionResult Delete(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //Have to change name since there is already a ActionResult named DeletePost with int id parameters
        //ActionName will keep the route conistant which is looking for a httppost called "Delete"
        public ActionResult DeletePost(int id)
        {
            var service = CreateNoteService();

            //TODO: Handle failure See update post for reference 
            service.DeleteNote(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        } 

        
    }
} 