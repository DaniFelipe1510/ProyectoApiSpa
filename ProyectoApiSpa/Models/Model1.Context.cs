﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoApiSpa.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SPADBEntities : DbContext
    {
        public SPADBEntities()
            : base("name=SPADBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Carrito> Carrito { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
