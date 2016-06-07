using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stuff.DAta;

namespace MvcApplication29.Controllers
{
    public class StuffController : Controller
    {
        public ActionResult Index()
        {
            var manager = new StuffManager(Properties.Settings.Default.ConStr);
            return View(manager.GetStuff());
        }

        [HttpPost]
        public ActionResult Submit(string word, HttpPostedFileBase image)
        {
            string fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            image.SaveAs(Server.MapPath("~/Images/") + fileName);
            var manager = new StuffManager(Properties.Settings.Default.ConStr);
            manager.AddStuff(fileName, word);
            return Redirect("/stuff/index");
        }

    }
}
