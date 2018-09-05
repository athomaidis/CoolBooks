namespace CoolBooks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Users
    {
        [Key]
        public string UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        public DateTime? Birthdate { get; set; }

        [StringLength(512)]
        [Display(Name = "Image path")]
        public string ImagePath { get; set; }

        public byte[] Image { get; set; }

        public string MimeType { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(10)]
        public string ZipCode { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Info { get; set; }

        public DateTime Created { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Books> Books{ get; set; }

        

    }
}
