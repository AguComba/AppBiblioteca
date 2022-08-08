using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AppBiblioteca.Data
{
    public class AppBibliotecaContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public AppBibliotecaContext() : base("name=AppBibliotecaContext")
        {
        }

        public System.Data.Entity.DbSet<AppBiblioteca.Models.Secciones> Secciones { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca.Models.Autores> Autores { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca.Models.Editoriales> Editoriales { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca.Models.Generos> Generos { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca.Models.Socios> Socios { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca.Models.Libros> Libros { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca.Models.Prestamos> Prestamos { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca.Models.PrestamosDetalle> PrestamosDetalle { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca.Models.PrestamosDetallesT> PrestamosDetallesT { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca.Models.Devoluciones> Devoluciones { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca.Models.DevolucionesDetalles> DevolucionesDetalles { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca.Models.DevolucionesDetallesT> DevolucionesDetallesT { get; set; }

    }
}
