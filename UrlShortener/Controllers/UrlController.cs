using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Raven.Abstractions.Data;
using Raven.Client;
using UrlShortener.Business;
using UrlShortener.Business.Domain;
using UrlShortener.Business.UI.Builders;
using UrlShortener.Business.UI.Models;

namespace UrlShortener.Controllers
{
    public partial class UrlController : Controller
    {
        private readonly IDocumentStore _documentStore = null;

        public UrlController(IDocumentStore documentStore)
        {
            if (documentStore == null) throw new ArgumentNullException("documentStore");

            _documentStore = documentStore;
        }

        //
        // GET: /Url/
        public virtual ActionResult Index()
        {
            var builder = new UrlIndexViewModelBuilder(_documentStore);
            var model = builder.Build();

            return View(model);
        }

        //
        // GET: /Url/Details/5
        public virtual ActionResult Details(string id)
        {
            try
            {
                ShortLink shortLink = null;

                using (var session = _documentStore.OpenSession())
                {
                    shortLink = session.Load<ShortLink>(id);
                }

                if (shortLink == null)
                {
                    return View(Views.NotFound);
                }

                var model = Mapper.Map<UrlDetailsViewModel>(shortLink);

                return View(Views.Details, model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //
        // GET: /Url/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Url/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(UrlCreateViewModel model)
        {
            try
            {
                using (var session = _documentStore.OpenSession())
                {
                    // Does a shortened url already exist with the specified key?
                    //var results = from urls in session.Query<UrlModel>()
                    //              where urls.Key == urlModel.Key
                    //              select urls;
                    var results = session.Query<ShortLink>().Where(url => url.Key == model.Key);

                    var existingUrlModel = results.FirstOrDefault();

                    if (existingUrlModel != null)
                    {
                        // TODO: Error page and try again.

                        return View();
                    }

                    var shortLink = Mapper.Map<ShortLink>(model);
                    shortLink.Created = DateTimeOffset.UtcNow;

                    session.Store(shortLink);
                    session.SaveChanges();
                }

                return RedirectToAction(Actions.Index());
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Url/Edit/5
        public virtual ActionResult Edit(string id)
        {
            try
            {
                ShortLink shortLink = null;

                using (var session = _documentStore.OpenSession())
                {
                    shortLink = session.Load<ShortLink>(id);
                }

                if (shortLink == null)
                {
                    return View(Views.NotFound);
                }

                var model = Mapper.Map<UrlEditViewModel>(shortLink);

                return View(Views.Edit, model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //
        // POST: /Url/Edit/5
        [HttpPost]
        public virtual ActionResult Edit(string id, UrlEditViewModel model)
        {
            try
            {
                // TODO: Validate that the new key is not in use.

                _documentStore.DatabaseCommands.Patch(
                    model.Id,
                    new[]
                        {
                            new PatchRequest
                            {
                                Type = PatchCommandType.Set,
                                Name = "Key",
                                Value = model.Key
                            },
                            new PatchRequest
                            {
                                Type = PatchCommandType.Set,
                                Name = "Url",
                                Value = model.Url.ToString()
                            },
                            new PatchRequest
                            {
                                Type=PatchCommandType.Set,
                                Name="Updated",
                                Value = DateTimeOffset.UtcNow
                            }
                        }
                    );

                return RedirectToAction(Actions.Index());
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Url/Delete/5
        public virtual ActionResult Delete(string id)
        {
            ShortLink shortLink = null;

            try
            {
                using (var session = _documentStore.OpenSession())
                {
                    shortLink = session.Load<ShortLink>(id);
                }

                if (shortLink == null)
                {
                    return HttpNotFound();
                }

                var model = Mapper.Map<UrlDeleteViewModel>(shortLink);

                return View(Views.Delete, model);
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
        public virtual ActionResult Delete(UrlDeleteViewModel model)
        {
            try
            {
                using (var session = _documentStore.OpenSession())
                {
                    var item = session.Load<ShortLink>(model.Id);
                    session.Delete(item);
                    session.SaveChanges();
                }

                return RedirectToAction(Actions.Index());
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Url/DeleteAll
        public virtual ActionResult DeleteAll()
        {
            return View();
        }

        //
        // POST: /Url/DeleteAll
        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DoDeleteAll()
        {
            string indexName = "ShortLinks/ByKey";

            try
            {
                _documentStore.DatabaseCommands.DeleteByIndex(indexName,
                    new IndexQuery()
                    );

                return RedirectToAction(MVC.Home.Index());
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
