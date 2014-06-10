using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using translator.Models;
using translator.Models.DbOperationException;

namespace translator.Controllers
{
    public class HomeController : Controller
    {
        private readonly String succesAddWordMessage = "Add the word to be successful!";
        private readonly String failAddWordMessage = "Addition of the word was a failure";
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

        [HttpPost]
        public ActionResult AddWord(String word, String lang)
        {
            ActionResult result = null;
            dbAdapter = new Translator();
            try
            {
                dbAdapter.AddWord(word, lang);
                result = Content(succesAddWordMessage);
            }
            catch (UndefineLangException e)
            {
                //result = new HttpStatusCodeResult(500, failAddWordMessage);
                result = Content(failAddWordMessage);
            }
            return result;
        }

    }
}
