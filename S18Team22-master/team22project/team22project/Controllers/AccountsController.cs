using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using team22project.DAL;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

//TODO: Change this using statement to match your project
using team22project.Models;

//TODO: Change this namespace to match your project
namespace team22project.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private AppUserManager _userManager;
        private AppDbContext db = new AppDbContext();

        public AccountsController()
        {
        }

        //NOTE: This creates a user manager and a sign-in manager every time someone creates a request to this controller
        public AccountsController(AppUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // NOTE:  This is the logic for the login page
        // GET: /Accounts/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated) //user has been redirected here from a page they're not authorized to see
            {
                return View("Error", new string[] { "Access Denied" });
            }
            AuthenticationManager.SignOut(); //this removes any old cookies hanging around
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Accounts/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

       
        //
        // GET: /Accounts/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // NOTE: Here is your logic for registering a new user
        // POST: /Accounts/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: Add fields to user here so they will be saved to do the database
                var user = new AppUser {
                    UserName = model.Email,
                    Email = model.Email,
                    //Firstname is an example - you will need to add the rest
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Birthday = model.Birthday
                                 
                };
                var result = await UserManager.CreateAsync(user, model.Password);

                //TODO:  Once you get roles working, you may want to add users to roles upon creation
                await UserManager.AddToRoleAsync(user.Id, "Customer");
                // --OR--
                // await UserManager.AddToRoleAsync(user.Id, "Employee");


                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    var body = "Thank you for registering with Longhorn Cinema!";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(user.Email));  // replace with valid value 
                    message.From = new MailAddress("longhorncinema22@gmail.com");  // replace with valid value
                    message.Subject = "Team 22 - Account Registration";
                    message.Body = string.Format(body);
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = "longhorncinema22@gmail.com",  // replace with valid value
                            Password = "Abcd123!"  // replace with valid value
                        };
                        smtp.Credentials = credential;
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(message);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Home");
                    }
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //GET: Accounts/Index
        public ActionResult Index()
        {
            IndexViewModel ivm = new IndexViewModel();

            //get user info
            String id = User.Identity.GetUserId();
            AppUser user = db.Users.Find(id);

            //populate the view model
            ivm.Email = user.Email;
            ivm.HasPassword = true;
            ivm.UserID = user.Id;
            ivm.UserName = user.UserName;
            ivm.Address = user.Address;
            ivm.PhoneNumber = user.PhoneNumber;


            return View(ivm);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            EditViewModel evm = new EditViewModel();

            String id = User.Identity.GetUserId();
            AppUser user = db.Users.Find(id);

            if (ModelState.IsValid)
            {
                user.Address = evm.Address;
                user.PhoneNumber = evm.PhoneNumber;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }


        //public static bool ValidateCreditCard(string CreditCard)
        //{
        //    bool Result = new bool();

        //    var cardCheck = new Regex(@"^(4|54|6)([\-\s]?[0-9]{4}){3}$");
        //    var amexCheck = new Regex(@"^\d{15}$");

        //    if (cardCheck.IsMatch(CreditCard))
        //    {
        //        Result = true;
               
        //    }
        //    else if (amexCheck.IsMatch(CreditCard))
        //    {
        //        Result = true;
        //    }

        //    else
        //    {
        //        Result = false;
        //    }

        //    return Result;
        //}

        //public ActionResult StoreCreditCard()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult StoreCreditCard(String CreditCard1, String CreditCard2)
        //{
        //    String id = User.Identity.GetUserId();
        //    AppUser user = db.Users.Find(id);

        //    var result = ValidateCreditCard(CreditCard1);
        //    if (result == true)
        //    {
        //        user.CreditCard1 = CreditCard1;
        //        return View();

        //    }

        //    return View();

        //}

        //Logic for change password
        // GET: /Accounts/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Accounts/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", "Home");
            }
            AddErrors(result);
            return View(model);
        }

        

        // POST: /Accounts/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region 

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}