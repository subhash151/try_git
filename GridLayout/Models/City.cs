//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GridLayout.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class City
    {
        public int ID { get; set; }
        public string CityName { get; set; }
        public int StateID { get; set; }
    
        public virtual ICollection<AdData> AdDatas { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<Locality> Localities { get; set; }
    }
}
