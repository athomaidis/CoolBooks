namespace CoolBooks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Genres
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Genres()
        {
            Books = new HashSet<Books>();
        }

        public int Id { get; set; }

        // max length 50
        
        [StringLength(50)]
        [Display(Name = "Genre name")]
        public string Name { get; set; }

        // max length 2000
        [StringLength(2000)]
        public string Description { get; set; }

        public DateTime Created { get; set; }

        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Books> Books { get; set; }
    }
}
