﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HPV_Datos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class HPVEntities : DbContext
    {
        public HPVEntities()
            : base("name=HPVEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<T12DBC_PERIODOS> T12DBC_PERIODOS { get; set; }
    
        public virtual int PK_ALI_INSCRIPCION_PR_OBTENERPERIODO(ObjectParameter p_NOMPERIODO)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PK_ALI_INSCRIPCION_PR_OBTENERPERIODO", p_NOMPERIODO);
        }
    }
}
