using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImanolFotia.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Listar()
        {
            try
            {
                var db = Models.ContextoDb.obtenerInstancia();
                var cliList = db.Clientes.ToList();
                return View(cliList);
            }
            catch
            {
                return RedirectToAction("Listar");
            }
        }

        [HttpPost]
        public ActionResult Alta(Models.Cliente cli)
        {
            try
            {
                Models.ContextoDb ConRef = Models.ContextoDb.obtenerInstancia();
                if (cli.Nombre != null || cli.Apellido != null)
                if (ModelState.IsValid)
                {
                    ConRef = Models.ContextoDb.obtenerInstancia();
                    ConRef.Clientes.Add(cli);
                    ConRef.SaveChanges();
                    }
                var cliList = ConRef.Clientes.ToList();
                return View("Listar", cliList);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Alta()
        {
            Models.ContextoDb ConRef = Models.ContextoDb.obtenerInstancia();
            var cliList = ConRef.Clientes.ToList();
            return View();
        }


        public ActionResult Baja(int id)
        {
            try
            {
                var ConRef = Models.ContextoDb.obtenerInstancia();
                if (ModelState.IsValid)
                {
                    ConRef = Models.ContextoDb.obtenerInstancia();
                    var cli = ConRef.Clientes.FirstOrDefault(b => b.Id == id);
                    ConRef.Clientes.Remove(cli);
                    ConRef.SaveChanges();
                }

                var cliList = ConRef.Clientes.ToList();
                return View("Listar", cliList);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Modificar(int id)
        {
            try
            {
                var ConRef = Models.ContextoDb.obtenerInstancia();
                var cli = ConRef.Clientes.FirstOrDefault(b => b.Id == id);
                return View(cli);
            }
            catch
            {
                return RedirectToAction("Listar");
            }
        }

        [HttpPost]
        public ActionResult Modificar(Models.Cliente cli)
        {
            try
            {
                var ConRef = Models.ContextoDb.obtenerInstancia();
                var original = ConRef.Clientes.SingleOrDefault(b => b.Id == cli.Id);
                if (original != null)
                {
                    ConRef.Entry(original).CurrentValues.SetValues(cli);
                    ConRef.SaveChanges();
                }

                return RedirectToAction("Listar", ConRef.Clientes.ToList());
            }
            catch {
                return RedirectToAction("Listar");
            }

        }


        public ActionResult Index()
        {
            return new ViewResult();
        }
    }
}