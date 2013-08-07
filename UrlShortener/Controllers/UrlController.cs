using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    public class UrlController : Controller
    {
        //
        // GET: /Url/
        public ActionResult Index()
        {
            var urls = new List<UrlModel>();
            return View("Index", urls);
        }

        //
        // GET: /Url/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Url/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Url/Create
        [HttpPost]
        public ActionResult Create(UrlModel urlModel)
        {
            try
            {
                // TODO: does key already exist in db?
                // TODO: insert url into db.

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Url/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Url/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Url/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Url/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
