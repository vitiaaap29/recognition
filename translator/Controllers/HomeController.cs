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

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Recognize(String word)
        {
            Translator translator = new Translator(word);
            translator.CalculatePercentReliability();
            return PartialView("PersentTablePartial", translator);
        }

    }
}
