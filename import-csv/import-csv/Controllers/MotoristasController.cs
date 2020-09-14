using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using import_csv.Models;

namespace import_csv.Controllers
{
    public class MotoristasController : Controller
    {
        private Model db = new Model();

        // GET: Motoristas
        public ActionResult Index()
        {
            return View(new List<Motorista>());
        }

        [HttpPost]
        public ActionResult GetMotoristas()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            var motoristas = db.Motoristas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                motoristas = db.Motoristas.Where(x =>
                    x.CEP.ToUpper().Contains(searchValue.ToUpper()) ||
                    x.Cidade.ToUpper().Contains(searchValue.ToUpper()) ||
                    x.CPFCNPJ.ToUpper().Contains(searchValue.ToUpper()) ||
                    x.Email.ToUpper().Contains(searchValue.ToUpper()) ||
                    x.Estado.ToString().ToUpper().Contains(searchValue.ToUpper()) ||
                    x.Nome.ToUpper().Contains(searchValue.ToUpper()) ||
                    x.Numero.ToString().Contains(searchValue.ToUpper()) ||
                    x.Rua.ToUpper().Contains(searchValue.ToUpper()) ||
                    x.Telefone.ToUpper().Contains(searchValue.ToUpper())
                    );
            }

            int totalMotoristas = db.Motoristas.Count();
            int totalFiltrado = motoristas.Count();

            motoristas = motoristas.OrderBy(sortColumnName + " " + sortDirection);            

            motoristas = motoristas.Skip(start).Take(length);

            return Json(new { data = new List<Motorista>(motoristas), draw = Request["draw"], recordTotal = totalMotoristas, recordsFiltered = totalFiltrado,  }, JsonRequestBehavior.AllowGet);                        
        }

        // GET: Motoristas/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motorista motorista = db.Motoristas.Find(id);
            if (motorista == null)
            {
                return HttpNotFound();
            }
            return View(motorista);
        }

        // GET: Motoristas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Motoristas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Telefone,Email,CPFCNPJ,CEP,Rua,Numero,Cidade,Estado")] Motorista motorista)
        {
            if (ModelState.IsValid)
            {
                motorista.Id = Guid.NewGuid();
                db.Motoristas.Add(motorista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(motorista);
        }

        // GET: Motoristas/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motorista motorista = db.Motoristas.Find(id);
            if (motorista == null)
            {
                return HttpNotFound();
            }
            return View(motorista);
        }

        // POST: Motoristas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Telefone,Email,CPFCNPJ,CEP,Rua,Numero,Cidade,Estado")] Motorista motorista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(motorista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(motorista);
        }

        // GET: Motoristas/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motorista motorista = db.Motoristas.Find(id);
            if (motorista == null)
            {
                return HttpNotFound();
            }
            return View(motorista);
        }

        // POST: Motoristas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Motorista motorista = db.Motoristas.Find(id);
            db.Motoristas.Remove(motorista);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
