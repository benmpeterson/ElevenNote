﻿using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Http;

namespace ElevenNote.Api.Controllers
{
    [Authorize]
    public class NotesController : ApiController
    {
        
        public IHttpActionResult GetAll()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var noteService = new NoteService(userId);
            var notes = noteService.GetNotes();

            return Ok(notes);
        }

        public IHttpActionResult Get(int id)
        {
            var noteService = new NoteService(Guid.Parse(User.Identity.GetUserId()));
            var note = noteService.GetNoteById(id);
            if (note == null) return NotFound();

            return Ok();
        }

        public IHttpActionResult Post(NoteCreate model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var noteService = new NoteService(Guid.Parse(User.Identity.GetUserId()));

            return Ok(noteService.CreateNote(model));
        }

        public IHttpActionResult Put(NoteEdit model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //Make sure the note exists
            var noteService = new NoteService(Guid.Parse(User.Identity.GetUserId()));
            var note = noteService.GetNoteById(model.NoteId);
            if (note == null) return NotFound();

            //Attempt to update, passing in the model 
            return Ok(noteService.UpdateNote(model));
        }

        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var noteService = new NoteService(Guid.Parse(User.Identity.GetUserId()));
            var note = noteService.GetNoteById(id);
            if (note == null) return NotFound();

            return Ok(noteService.DeleteNote(id));           
        }

    }
}
