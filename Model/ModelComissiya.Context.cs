﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Comissionka.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ComissiyaEntities : DbContext
    {
        public static ComissiyaEntities _context;
        public static ComissiyaEntities GetContext()
        {
            if (_context == null)
                _context = new ComissiyaEntities();
            return _context;
        }
        public ComissiyaEntities()
            : base("name=ComissiyaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Tovar> Tovar { get; set; }
        public virtual DbSet<TovarCategory> TovarCategory { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
