namespace CoolBooks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    
    public partial class Books
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Books()
        {
            Reviews = new HashSet<Reviews>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        [ForeignKey("Users")]
        public string UserId { get; set; }
        public Users Users { get; set; }

        [Display(Name = "Author")]
        public int AuthorId { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [StringLength(256)]
        [Display(Name = "Alternative title")]
        public string AlternativeTitle { get; set; }

        public short? Part { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [StringLength(50)]
        [Index("IX_ISBN", 1, IsUnique = true)]
        [UniqueISBN(ErrorMessage = "The ISBN is not unique.")]
        public string ISBN { get; set; }

        [DisplayFormat(DataFormatString = "{0:d MMM \\'yy}")]
        public DateTime? PublishDate { get; set; }

        [StringLength(512)]
        [Display(Name = "Image path")]
        public string ImagePath { get; set; }

        public byte[] Image { get; set; }

        public string MimeType { get; set; }

        [DisplayFormat(DataFormatString = "{0:d MMM \\'yy}")]
        public DateTime Created { get; set; }

        [Display(Name = "Is deleted")]
        public bool IsDeleted { get; set; }

        public virtual Authors Authors { get; set; }

        public virtual Genres Genres { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reviews> Reviews { get; set; }
        
    }
}
