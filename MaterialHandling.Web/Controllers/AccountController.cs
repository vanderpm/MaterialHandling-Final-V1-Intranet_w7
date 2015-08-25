using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaterialHandling.Data;
using System.Web.Security;


namespace MaterialHandling.Web.Controllers
{
    
    public class AccountController : BaseController
    {
        /*This will display the login page when the website loads*/
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /*This will post the account username to the database
        *if the data is valid and matches the userName and password
        *in the database, the site will redirect to the create page
        *if not valid, site will prompt for another entry*/
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Account model, string returnUrl)
        {
            // Lets first check if the Model is valid or not
            if (ModelState.IsValid)
            {
                using (MaterialHandlingEntities3 entities = new MaterialHandlingEntities3())
                {
                    string username = model.userName;
                    string password = model.password;

                    // Now if our password was enctypted or hashed we would have done the
                    // same operation on the user entered password here, But for now
                    // since the password is in plain text lets just authenticate directly

                    bool userValid = entities.Accounts.Any(user => user.userName == username && user.password == password);

                    // User found in the database
                    if (userValid)
                    {

                        FormsAuthentication.SetAuthCookie(username, false);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Create", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /*This will log a user out of the website
        *This is not implimented for this site*/
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}