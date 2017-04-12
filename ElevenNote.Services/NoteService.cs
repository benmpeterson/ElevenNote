using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {

        //backingfield
        private readonly Guid _userId;

        //Since making this constructor now you HAVE to get it a user ID
        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateNote(NoteCreate model)
        {
            var entity = new Note
            {
                Owner = _userId,
                Title = model.Title,
                Content = model.Content,
                CreatedUtc = DateTimeOffset.UtcNow           
            };

            //Ask about this using statement and what Note create is being used
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);

                return ctx.SaveChanges() == 1;
            }

        }
        //TODO ASK ABOUT USING
        public IEnumerable<NoteListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Notes.Where(e => e.Owner == _userId)
                    .Select(
                        e =>
                            new NoteListItem
                            {
                                NoteId = e.NoteId,
                                Title = e.Title,
                                CreatedUtc = e.CreatedUtc
                            }
                        );

                return query.ToArray();
            }
        }




    }
}
