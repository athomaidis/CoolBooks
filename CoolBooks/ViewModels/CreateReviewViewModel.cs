using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolBooks.Models
{
    public class CreateReviewViewModel
    {
       public Users User { get; set; }
       public Reviews Review { get; set; }
    }

}