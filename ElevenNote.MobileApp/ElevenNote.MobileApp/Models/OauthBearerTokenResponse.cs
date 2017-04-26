using System;
using System.Collections.Generic;
using System.Text;

namespace ElevenNote.MobileApp.Models
{
    internal class OauthBearerTokenResponse
    {
        //Properties are named liked this 
        //because when the JSON comes back they
        //Will be in that format. And if named the same
        //We can use the values as is, not requiring translation
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string userName { get; set; }
    }
}
