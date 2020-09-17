using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Conta;
using RestSharp;
using Services.Account;
using Web.ViewModel;

namespace Web.Controllers
{
    public class ContasController : Controller
    {
        private IContaIdentityManager _contaIdentityManager;

        public ContasController(IContaIdentityManager contaIdentityManager)
        {
            _contaIdentityManager = contaIdentityManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                var result = await _contaIdentityManager.Login(model.Email, model.Password);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Login ou Senha inválidos");
                    return View(model);
                }

                var client = new RestClient();
                var request = new RestRequest("https://localhost:44343/api/authenticate/token", DataFormat.Json);
                request.AddJsonBody(model);
                var response = client.Post<string>(request);
                HttpContext.Session.SetString("Token", response.Data);

                if (!String.IsNullOrWhiteSpace(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return Redirect("/");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro, por favor tente mais tarde.");
                return View(model);
            }
        }

        public IActionResult Logout()
        {
            _contaIdentityManager.Logout();
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }
            return Redirect("/Contas/Login");
        }
    }
}
