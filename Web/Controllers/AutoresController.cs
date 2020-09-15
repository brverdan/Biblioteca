using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context.Repository;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Web.Models;

namespace Web.Controllers
{
    public class AutoresController : Controller
    {
        private readonly BibliotecaContext _bibliotecaContext;

        public AutoresController(BibliotecaContext bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        // GET: AutoresController
        public ActionResult Index()
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:44343/api/autores", DataFormat.Json);
            var response = client.Get<List<Autor>>(request);

            return View(response.Data);
        }

        // GET: AutoresController/Details/5
        public ActionResult Details(Guid id)
        {

            var client = new RestClient();
            var request = new RestRequest("https://localhost:44343/api/autores/" + id, DataFormat.Json);
            var response = client.Get<Autor>(request);

            return View(response.Data);
        }

        // GET: AutoresController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutoresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Autor autor)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var client = new RestClient();

                    var request = new RestRequest("https://localhost:44343/api/autores", DataFormat.Json);

                    request.AddJsonBody(autor);
                    var response = client.Post<Autor>(request);

                    return Redirect("/");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutoresController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:44343/api/autores/" + id, DataFormat.Json);

            var response = client.Get<Autor>(request);

            return View(response.Data);
        }

        // POST: AutoresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Autor autor)
        {
            try
            {
                var client = new RestClient();

                var request = new RestRequest("https://localhost:44343/api/autores/" + id, DataFormat.Json);

                request.AddJsonBody(autor);
                var response = client.Put<Autor>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutoresController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:44343/api/autores/" + id, DataFormat.Json);

            var response = client.Get<Autor>(request);

            return View(response.Data);
        }

        // POST: AutoresController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Autor autor)
        {
            var client = new RestClient();
            var request = new RestRequest("https://localhost:44343/api/autores/" + id, DataFormat.Json);
            var response = client.Get<Autor>(request);

            autor = response.Data;

            using (var transaction = _bibliotecaContext.Database.BeginTransaction())
            {
                try
                {

                    foreach (var item in autor.Livros)
                    {
                        var requestLivro = new RestRequest("https://localhost:44343/api/livros/" + item.Id, DataFormat.Json);

                        var responseLivro = client.Delete<Livro>(requestLivro);
                    }

                    request = new RestRequest("https://localhost:44343/api/autores/" + id, DataFormat.Json);

                    response = client.Delete<Autor>(request);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
            return Redirect("/");
        }
    }
}

