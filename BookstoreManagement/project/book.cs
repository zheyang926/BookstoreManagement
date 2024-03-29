//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace project
{
    using System;
    using System.Collections.Generic;
    
    public partial class book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public book()
        {
            this.bookOrders = new HashSet<bookOrder>();
        }
    
        public int BookID { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public double UnitPrice { get; set; }
        public System.DateTime YearPublished { get; set; }
        public int QOH { get; set; }
        public int AuthorID { get; set; }
        public int PublishID { get; set; }
        public int CategoryID { get; set; }
    
        public virtual author author { get; set; }
        public virtual category category { get; set; }
        public virtual publisher publisher { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bookOrder> bookOrders { get; set; }
    }
}
