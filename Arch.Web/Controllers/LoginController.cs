using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Arch.Service.Interfaces;
using Arch.Data.UnitofWork;
using System.Web.Security;
using System;
using System.IO;
using Arch.Utilities.Manager;
using Newtonsoft.Json;
using System.Web;
using Arch.Web.Framework.Enums;
using Arch.Dto.ParamDto;
using Arch.Core.Constants;
using Arch.Core;
using Arch.Dto.SingleDto;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Arch.Web.Controllers
{
    public class LoginController : PublicController
    {
        private readonly IWorkService _workService;
        private readonly IPersonService _personService;
        private readonly IUtilityService _utilityService;
        public LoginController(
             IWorkService workService,
            IUnitofWork uow,
            ILogService logService,
            IPersonService personService,
            IUtilityService utilityService
            )
            : base(uow, logService)
        {
            _personService = personService;
            _utilityService = utilityService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginControl(LoginParamDto model)
        {
            //login girişi yapmış olan tekrar ajaxla giriş yapmaya çalışmasını engellemek içinm bunu yapıyoruz
            if (Accesses.IsLogin() != ForbiddenAccessTypes.UnForbidden || Accesses.IsLogin() == ForbiddenAccessTypes.IsLogout)
            {
                var person = _personService.GetPerson(model.UserName, model.Password);
                //var serdar = _personService.FindPerson(790);
                //string sifre = EnDeCode.Decrypt(serdar.Password, StaticParams.SifrelemeParametresi);

                if (person == null)
                    return AjaxMessage("Uyarı", "Yanlış kullanıcı adı veya şifre", MessageTypes.danger);

                // cookie için bi ticket oluşturuyoruz. oturum bilgilerini tutmak için 
                var ticket = new FormsAuthenticationTicket(1,
                  EnDeCode.Encrypt(person.Id.ToString(), StaticParams.SifrelemeParametresi),
                  DateTime.Now,
                  model.RememberMe ? DateTime.Now.AddYears(1) : DateTime.Now.AddDays(1),
                  model.RememberMe,
                  EnDeCode.Encrypt(person.Id.ToString(), StaticParams.SifrelemeParametresi),
                  FormsAuthentication.FormsCookiePath);
                //oluşturulan ticketı encrypt yaparak kaydediyoruz. cookie de şifreli tutmak için 
                string encTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(".u", encTicket); //adı .u olan bir cookie oluşturduk 
                var personInitials = new HttpCookie("_initials", UtilityManager.Base64Encode(person.Initials));
                var personFullName = new HttpCookie("_fullname", UtilityManager.Base64Encode(person.Name + " " + person.Surname));
                var personEmail = new HttpCookie("_email", UtilityManager.Base64Encode(person.Email));
                var unitId = new HttpCookie("_ui", UtilityManager.Base64Encode(person.UnitId.ToString()));

                cookie.HttpOnly = true;
                personInitials.HttpOnly = true;
                personFullName.HttpOnly = true;
                personEmail.HttpOnly = true;
                unitId.HttpOnly = true;

                cookie.Expires = model.RememberMe ? DateTime.Now.AddYears(1) : DateTime.Now.AddDays(1); // cookie için zaman veriyoruz
                personInitials.Expires = model.RememberMe ? DateTime.Now.AddYears(1) : DateTime.Now.AddDays(1);
                personFullName.Expires = model.RememberMe ? DateTime.Now.AddYears(1) : DateTime.Now.AddDays(1);
                personEmail.Expires = model.RememberMe ? DateTime.Now.AddYears(1) : DateTime.Now.AddDays(1);
                unitId.Expires = model.RememberMe ? DateTime.Now.AddYears(1) : DateTime.Now.AddDays(1);

                Response.Cookies.Add(cookie);
                Response.Cookies.Add(personInitials);
                Response.Cookies.Add(personFullName);
                Response.Cookies.Add(personEmail);
                Response.Cookies.Add(unitId);
                return Json("/Dashboard", JsonRequestBehavior.AllowGet);
            }
            else { return AjaxMessage(MessageTitleTypes.Uyari, "Yanlış kullanıcı adı veya şifre",MessageTypes.danger ); }
        }

        public ActionResult Logout(string r)
        {
            // cookie leri temizliyoruz 
            string[] myCookies = Request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
            FormsAuthentication.SignOut();
            // çıkış yaptıktan sonra bir ajax çalıştırmışsak yine bizi logout edecek. 
            if (Request.IsAjaxRequest())
                return AjaxMessage("Hata", "window.location.reload()", MessageTypes.func);
            Response.Redirect("/login?redirectUrl=" + r);
            return Json("");
        }


        public ActionResult Register()
        {
            return View();
        }

        public ActionResult GetUnitProjectList(int unitId)
        {
            return Json(_workService.GetUnitProjectList(unitId), JsonRequestBehavior.AllowGet);
        }
        //public ActionResult Register(LoginParamDto model)
        //{
        //    return View();
        //}
    }
}