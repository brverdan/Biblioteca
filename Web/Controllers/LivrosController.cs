using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Autor;
using RestSharp;
using Web.Models;

namespace Web.Controllers
{
    public class LivrosController : Controller
    {
        private AutorRepository _autorRepository;

        public LivrosController(AutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }


        // GET: LivrosController
        public ActionResult Index()
        {
            var client = new RestClient();

            var key = HttpContext.Session.GetString("Token");

            var request = new RestRequest("https://localhost:44343/api/livros", DataFormat.Json);
            var response = client.Get<List<Livro>>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

            return View(response.Data);
        }

        // GET: LivrosController/Details/5
        public ActionResult Details(Guid id)
        {
            var client = new RestClient();

            var key = HttpContext.Session.GetString("Token");

            var request = new RestRequest("https://localhost:44343/api/livros/" + id, DataFormat.Json);
            var response = client.Get<LivroResponse>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

            return View(response.Data);
        }

        // GET: LivrosController/Create
        public ActionResult Create()
        {
            var client = new RestClient();

            var key = HttpContext.Session.GetString("Token");

            var request =  new RestRequest("https://localhost:44343/api/autores", DataFormat.Json);
            var response = client.Get<List<Autor>>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

            ViewBag.Autores = response.Data;

            return View();
        }

        // POST: LivrosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Livro livro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var client = new RestClient();
                    var key = HttpContext.Session.GetString("Token");

                    var request = new RestRequest("https://localhost:44343/api/livros", DataFormat.Json);

                    request.AddJsonBody(livro);

                    var responseLivro = client.Post<Livro>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

                    return Redirect("/");
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LivrosController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var client = new RestClient();
            var key = HttpContext.Session.GetString("Token");
            var request = new RestRequest("https://localhost:44343/api/livros/" + id, DataFormat.Json);
            var response = client.Get<Livro>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

            var requestAutor = new RestRequest("https://localhost:44343/api/autores", DataFormat.Json);
            var responseAutor = client.Get<List<Autor>>(requestAutor.AddHeader("Authorization", "Bearer " + KeyValue(key)));

            ViewBag.Autores = responseAutor.Data;

            return View(response.Data);
        }

        // POST: LivrosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Livro livro)
        {
            try
            {
                var client = new RestClient();
                var key = HttpContext.Session.GetString("Token");
                var request = new RestRequest("https://localhost:44343/api/livros/" + id, DataFormat.Json);

                request.AddJsonBody(livro);
                var response = client.Put<Livro>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LivrosController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var client = new RestClient();
            var key = HttpContext.Session.GetString("Token");
            var request = new RestRequest("https://localhost:44343/api/livros/" + id, DataFormat.Json);
            var response = client.Get<Livro>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

            return View(response.Data);
        }

        // POST: LivrosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Livro livro)
        {
            try
            {
                var client = new RestClient();
                var key = HttpContext.Session.GetString("Token");
                var request = new RestRequest("https://localhost:44343/api/livros/" + id, DataFormat.Json);

                var response = client.Delete<Livro>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public string KeyValue(string key)
        {
            string[] peloAmorDeDeusFunciona = key.Split(":");
            string[] agrVai = peloAmorDeDeusFunciona[1].Split("\\");
            string[] agrVaiPF = agrVai[0].Split("}");
            string[] agrVaiPF2 = agrVaiPF[0].Split('"');

            return agrVaiPF2[1];
        }
    }
}
