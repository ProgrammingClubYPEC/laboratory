//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace laboratory.database
{
    using System;
    using System.Collections.Generic;
    
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            this.analyzer = new HashSet<analyzer>();
            this.order = new HashSet<order>();
            this.service_rendered = new HashSet<service_rendered>();
            this.issued_invoices = new HashSet<issued_invoices>();
            this.service = new HashSet<service>();
            this.order1 = new HashSet<order>();
        }
        public override string ToString()
        {
            return $"{surname} {name} {midname}";
        }
        public string login { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string midname { get; set; }
        public Nullable<System.DateTime> bithday { get; set; }
        public long code_role { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<analyzer> analyzer { get; set; }
        public virtual login login1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> order { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<service_rendered> service_rendered { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<issued_invoices> issued_invoices { get; set; }
        public virtual user_confidential_data user_confidential_data { get; set; }
        public virtual user_contact user_contact { get; set; }
        public virtual user_role user_role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<service> service { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> order1 { get; set; }
    }
}
