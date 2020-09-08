using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WBSProekt_Visualization.Helpers;

namespace WBSProekt_Visualization.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            var games = RDFHelper.GetRatingOverWeight();
            return View(games);
        }

        public ActionResult RatingWeight() {
            return View("RatingWeightChart", RDFHelper.GetRatingOverWeight());
        }

        public ActionResult WeightGroup() {
            return View("WeightGroupingChart", RDFHelper.GetGroupedWeights());
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}