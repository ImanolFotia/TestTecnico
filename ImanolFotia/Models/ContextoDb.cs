using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImanolFotia.Models
{

    public class ContextoDb : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public static ContextoDb Contexto;
        public static ContextoDb obtenerInstancia()
        {
            if (Contexto == null)
                Contexto = new ContextoDb();

            return Contexto;
        }
    }
}