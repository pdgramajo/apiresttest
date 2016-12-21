using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBAPI.Models
{
    public class Users
    {
        public int id { get; set; }
        public string name { get; set; }
        public string age { get; set; }
        public bool isAnonimus { get; set; }        

    }
}