//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UPPos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Results
    {
        public int id { get; set; }
        public Nullable<int> id_user { get; set; }
        public Nullable<int> id_lab { get; set; }
        public Nullable<int> id_service { get; set; }
        public string result { get; set; }
        public string data { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Service Service { get; set; }
        public virtual Users Users { get; set; }
    }
}
