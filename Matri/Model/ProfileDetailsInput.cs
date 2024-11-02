using System;
using System.Collections.Generic;
using System.Text;

namespace Matri.Model
{
    public class ProfileDetailsInput
    {
        public ProfileDetailsInput() { }

        public string LoggedInId { get; set; }

        public Guid TargetProfileId { get; set; }

        public bool Blocked { get; set; }

        public bool Favourite { get; set; }

        public bool ReceivedPhotoRequest { get; set; }

        public bool ReceivedInterest { get; set; }
    }
}
