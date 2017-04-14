using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.Web.Controllers.WebAPI
{
    //Requring that anyone using this WebAPI controller is logged in
    //Routing is different in web Api. Uses attribute based routing
    [Authorize]
    //Sets up a seperate api that directs to api instead of MVC
    [RoutePrefix("api/Note")]
    public class NoteController : ApiController
    {
        private bool SetStarState(int noteId, bool newState)
        {
            //create the service
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);

            //Get the note
            var detail = service.GetNoteById(noteId);

            //Create the NoteEdit model instance with the new star state
            var updatedNote = new NoteEdit
            {
                NoteId = detail.NoteId,
                Title = detail.Title,
                Content = detail.Content,
                IsStarred = newState,
            };

            //Return a value indiciating whether the update succeeded
            return service.UpdateNote(updatedNote);

        }

        [Route("{id}/Star")]
        [HttpPut]
        //An endpoint is what is going to receive the request. An actual URL in our application
        //Web Api is talked about in terms of resources, common name "restful api"
        public bool ToggleStarOn(int id) => SetStarState(id, true);

        [Route("{id}/Star")]
        [HttpDelete]
        public bool ToggleStarOff(int id) => SetStarState(id, false);


    }
}
