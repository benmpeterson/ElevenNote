using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Note
    {
        [Key]
        public int NoteId {get; set;}

        [Required]
        //Globally unique identifier
        //Massive Data Structure. Identity framework identities users as a Guide type        
        public Guid Owner { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]  
        public string Content { get; set; }

        [DefaultValue(false)]
        public bool IsStarred { get; set; }

        [Required]
        //Better than DateTime because it takes into account Time Zone
        public DateTimeOffset CreatedUtc { get; set; }

        //Adding so value for DateTimeOffset can be null
        //Needed for reference type not value type
        public DateTimeOffset? ModifiedUTC { get; set; }


    }
}
