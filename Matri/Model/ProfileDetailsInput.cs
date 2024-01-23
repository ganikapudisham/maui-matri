using System;
using System.Collections.Generic;
using System.Text;

namespace Matri.Model
{
    public class ProfileDetailsInput
    {
        public ProfileDetailsInput() { }

        public Guid LoggedInId { get; set; }

        public Guid TargetProfileId { get; set; }
    }
}
