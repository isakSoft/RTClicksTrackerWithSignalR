﻿using System.Web;
using System.Web.Mvc;

namespace com.ai.ext.upwork.test1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
