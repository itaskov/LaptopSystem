using LaptopSystem.Data;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaptopSystem.Web.Controllers
{
    public class BaseController : Controller
    {
        //[Inject]
        //public IUowData Data {get; set;}

        protected IUowData Data { get; set; }

        public BaseController(IUowData data)
        {
            this.Data = data;
        }
    }
}