﻿namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class SecurityContext : DbContext
    {
        // Контекст настроен для использования строки подключения "SecurityContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "DataAccess.SecurityContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "SecurityContext" 
        // в файле конфигурации приложения.
        public SecurityContext()
            : base("name=SecurityContext")
        {
            Database.SetInitializer(new DataInitializer());
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet <User> Users { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}