using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaterialHandling.Data;

namespace MaterialHandling.Web.Controllers
{
    public class BaseController : Controller
    {
        private MaterialHandlingEntities3 _database = null;

        public MaterialHandlingEntities3 Database
        {
            get
            {
                if (_database == null)
                    _database = new MaterialHandlingEntities3();

                return _database;
            }
        }
    }
}