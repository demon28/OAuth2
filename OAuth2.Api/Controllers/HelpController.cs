using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAuth2.Api.Controllers
{
    public class HelpController : Controller
    {
        //   help/protocols/registerProtocol
        public ActionResult Protocol(string id)
        {
            var proto = Winner.Protocols.Protocol.Get(id);
            if (proto == null)
            {
                return View(id);
            }
            return View(proto);
        }

        public ActionResult AboutUs()
        {
            return View();
        }
    }
}