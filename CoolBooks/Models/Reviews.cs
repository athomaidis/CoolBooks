namespace CoolBooks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reviews
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Books")]
        public int BookId { get; set; }

        [Required]
        [StringLength(128)]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public Users User { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(4000)]
        public string Text { get; set; }

        [Range(0,5)]
        public byte? Rating { get; set; }

        [DisplayFormat(DataFormatString = "{0:d MMM \\'yy}")]
        public DateTime Created { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Books Books { get; set; }
    }
}
