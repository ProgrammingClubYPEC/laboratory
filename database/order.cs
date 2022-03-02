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
    
    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            this.rendered = new HashSet<rendered>();
        }
    
        public long order_code { get; set; }
        public System.DateTime creation_date { get; set; }
        public string customer_login { get; set; }
        public string employeer_login { get; set; }
        public long service_code { get; set; }
        public long tube_code { get; set; }
        public long order_status_code { get; set; }
        public long service_status_code { get; set; }
        public int days_to_complete { get; set; }
        public override string ToString()
        {
            return $"{customer_login.Trim()}:{service.name.Trim()}";
        }
        public virtual biomaterials_tube biomaterials_tube { get; set; }
        public virtual order_status order_status { get; set; }
        public virtual service service { get; set; }
        public virtual service_status service_status { get; set; }
        public virtual user user { get; set; }
        public virtual user user1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rendered> rendered { get; set; }
    }
}
