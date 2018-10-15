using bugTracker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static bugTracker.ApplicationSignInManager;

namespace bugTracker.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!(User.Identity.IsAuthenticated))
            {
                return RedirectToAction("LoginRegister", "Account");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            EmailModel model = new EmailModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var message = "<p>Email From: <bold>{0}</bold>({1})</p><p> Message:</p><p>{2}</p> ";
                    //var from = "MyPortfolio<example@email.com>";
                    //model.Body = "This is a message from your portfolio site.  The name and the email of the contacting person is above.";

                    var testVar = ConfigurationManager.AppSettings["emailto"];

                    var email = new MailMessage(model.FromEmail, ConfigurationManager.AppSettings["emailto"])
                    {
                        Subject = model.Subject,
                        Body = string.Format(message, model.FromName, model.FromEmail, model.Body),
                        IsBodyHtml = true
                    };

                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);

                    ModelState.Clear();

                    TempData["status"] = "success";
                    return View(new EmailModel());
                }
                catch (Exception ex)
                {
                    TempData["status"] = "error";
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }

    }
}