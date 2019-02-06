using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LudoWebApp.Controllers
{
    public class LudoAppController : Controller
    {
        // GET: LudoApp
        public ActionResult Index()
        {
            return View();
        }

        // GET: LudoApp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LudoApp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LudoApp/Create
        [HttpPost("ludo")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LudoApp/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LudoApp/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LudoApp/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LudoApp/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}