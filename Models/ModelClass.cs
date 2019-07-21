using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudInMVC.Models
{
    public class ModelClass
    {
        StudentDBHandle dbhandle = new StudentDBHandle();
        public  object GetClass()
        {
            return   dbhandle.CreateModel();
        }
    }
}