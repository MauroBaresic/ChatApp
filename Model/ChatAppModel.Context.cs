﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ChatAppDBEntities : DbContext
    {
        public ChatAppDBEntities()
            : base("name=ChatAppDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<string> AllUsers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("AllUsers");
        }
    }
}