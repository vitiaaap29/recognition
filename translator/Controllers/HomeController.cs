using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using translator.Models;

namespace translator.Controllers
{
    public class HomeController : Controller
    {
        public readonly String k = "dd";
        private Translator dbAdapter;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Recognize(String word)
        {
            dbAdapter = new Translator();
            dbAdapter.CalculatePercentReliability(word);
            return PartialView("PersentTablePartial", dbAdapter);
        }

    }
}
