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
    
    public partial class rendered
    {
        public long render_code { get; set; }
        public long analyzer_code { get; set; }
        public string emploee_login { get; set; }
        public string patient_login { get; set; }
        public long render_type { get; set; }
        public System.DateTime date_of_service { get; set; }
        public int result { get; set; }
        public long service_code { get; set; }
        public long order_code { get; set; }
    
        public virtual analyzer analyzer { get; set; }
        public virtual user user { get; set; }
        public virtual user user1 { get; set; }
        public virtual rendered_type rendered_type { get; set; }
        public virtual service service { get; set; }
        public virtual order order { get; set; }
    }
}
