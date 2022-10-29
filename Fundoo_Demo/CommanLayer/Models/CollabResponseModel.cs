using System;
using System.Collections.Generic;
using System.Text;

namespace CommanLayer.Models
{
    public class CollabResponseModel
    {
        public long CollaboratorID { get; set; }

        public string CollaboratedEmail { get; set; }

        public long noteID { get; set; }

        public long UserId { get; set; }
    }
}
