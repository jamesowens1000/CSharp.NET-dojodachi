using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Dojodachi.Models;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Dojodachi()
        {
            if (HttpContext.Session.GetObjectFromJson<Dachi>("MyDojodachi") == null)
            {
                HttpContext.Session.SetObjectAsJson("MyDojodachi", new Dachi());
            }
            ViewBag.DachiInfo = HttpContext.Session.GetObjectFromJson<Dachi>("MyDojodachi");

            return View("Dojodachi");
        }

        [HttpGet("feed")]
        public IActionResult Feed()
        {
            Dachi myDachi = HttpContext.Session.GetObjectFromJson<Dachi>("MyDojodachi");
            myDachi.Feed();
            HttpContext.Session.SetObjectAsJson("MyDojodachi",myDachi);
            return RedirectToAction("Dojodachi");
        }

        [HttpGet("play")]
        public IActionResult Play()
        {
            Dachi myDachi = HttpContext.Session.GetObjectFromJson<Dachi>("MyDojodachi");
            myDachi.Play();
            HttpContext.Session.SetObjectAsJson("MyDojodachi",myDachi);
            return RedirectToAction("Dojodachi");
        }

        [HttpGet("work")]
        public IActionResult Work()
        {
            Dachi myDachi = HttpContext.Session.GetObjectFromJson<Dachi>("MyDojodachi");
            myDachi.Work();
            HttpContext.Session.SetObjectAsJson("MyDojodachi",myDachi);
            return RedirectToAction("Dojodachi");
        }

        [HttpGet("sleep")]
        public IActionResult Sleep()
        {
            Dachi myDachi = HttpContext.Session.GetObjectFromJson<Dachi>("MyDojodachi");
            myDachi.Sleep();
            HttpContext.Session.SetObjectAsJson("MyDojodachi",myDachi);
            return RedirectToAction("Dojodachi");
        }

        [HttpGet("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Dojodachi");
        }
    }

    public static class SessionExtensions
    {
        // We can call ".SetObjectAsJson" just like our other session set methods, by passing a key and a value
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            // This helper function simply serializes the object to JSON and stores it as a string in session
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        
        // generic type T is a stand-in indicating that we need to specify the type on retrieval
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            // Upon retrieval the object is deserialized based on the type we specified
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}