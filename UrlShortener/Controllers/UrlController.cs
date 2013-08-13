using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Raven.Json.Linq;
using UrlShortener.Builders.Url;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    public class UrlController : Controller
    {
        private readonly IDocumentStore _documentStore = null;

        public UrlController(IDocumentStore documentStore)
        {
            if (documentStore == null) throw new ArgumentNullException("documentStore");

            _documentStore = documentStore;
        }

        //
        // GET: /Url/
        public ActionResult Index()
        {
            IndexModelBuilder builder = new IndexModelBuilder(_documentStore);
            var model = builder.Build();

            return View(model);
        }

        //
        // GET: /Url/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                using (var session = _documentStore.OpenSession())
                {
                    var urlModel = session.Load<UrlModel>(id);

                    return View("Details", urlModel);
                }
            }
            catch (Exception)
            {
                throw;
            }
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(UrlModel urlModel)
        {
            try
            {
                using (var session = _documentStore.OpenSession())
                {
                    // Does a shortened url already exist with the specified key?
                    //var results = from urls in session.Query<UrlModel>()
                    //              where urls.Key == urlModel.Key
                    //              select urls;
                    var results = session.Query<UrlModel>().Where(url => url.Key == urlModel.Key);

                    var existingUrlModel = results.FirstOrDefault();

                    if (existingUrlModel != null)
                    {
                        // TODO: Error page and try again.

                        return View();
                    }

                    urlModel.Created = DateTimeOffset.UtcNow;
                    session.Store(urlModel);
                    session.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Url/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                UrlModel urlModel = null;

                using (var session = _documentStore.OpenSession())
                {
                    urlModel = session.Load<UrlModel>(id);
                }

                return View("Edit", urlModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //
        // POST: /Url/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, UrlModel urlModel)
        {
            try
            {
                // TODO: Validate that the new key is not in use.

                _documentStore.DatabaseCommands.Patch(
                    urlModel.Id,
                    new[]
                        {
                            new PatchRequest
                            {
                                Type = PatchCommandType.Set,
                                Name = "Key",
                                Value = urlModel.Key
                            },
                            new PatchRequest
                            {
                                Type = PatchCommandType.Set,
                                Name = "Url",
                                Value = urlModel.Url.ToString()
                            },
                            new PatchRequest
                            {
                                Type=PatchCommandType.Set,
                                Name="Updated",
                                Value = DateTimeOffset.UtcNow
                            }
                        }
                    );

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Url/Delete/5
        public ActionResult Delete(string id)
        {
            UrlModel urlModel = null;

            try
            {
                using (var session = _documentStore.OpenSession())
                {
                    urlModel = session.Load<UrlModel>(id);
                }

                if (urlModel == null)
                {
                    return HttpNotFound();
                }

                return View("Delete", urlModel);
            }
            catch (Exception)
            {
                throw;
            }

            return View();
        }

        //
        // POST: /Url/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UrlModel urlModel)
        {
            try
            {
                using (var session = _documentStore.OpenSession())
                {
                    var item = session.Load<UrlModel>(urlModel.Id);
                    session.Delete(item);
                    session.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Url/DeleteAll
        public ActionResult DeleteAll()
        {
            return View();
        }

        //
        // POST: /Url/DeleteAll
        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public ActionResult DoDeleteAll()
        {
            try
            {
                _documentStore.DatabaseCommands.DeleteByIndex("Auto/UrlModels/ByKey",
                    new IndexQuery()
                    );

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
