using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    //Always make this public right away
    public class NoteDetail
    {
        public int NoteId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }

        public override string ToString() => $"[{NoteId}] {Title}";

    }
}
