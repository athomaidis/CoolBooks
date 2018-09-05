using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoolBooks.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public string UserFullName { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(4000)]
        public string Text { get; set; }

        public byte? Rating { get; set; }

        [DisplayFormat(DataFormatString = "{0:d MMM \\'yy}")]
        public DateTime Created { get; set; }
    }

}