using System;
using ScientificStudyWeb.DataObjects;

namespace ScientificStudyWeb.DataObjects
{
    public class UserDataToReturn
    {
        public string AccessToken { get; set; }
        
        public DateTime ExpiresAt { get; set; }

        public BasicUser User { get; set; }
    }
}
