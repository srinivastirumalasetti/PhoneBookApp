using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using BusinessServices;
using BusinessServices.Interface;
using GoodExample.App_Start;

namespace GoodExample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
                        
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            Bootstrapper.Run();           

        }

        protected void Application_BeginRequest()
        {
            if (Request.Headers.GetValues("Origin") != null)
            {
                if (Request.HttpMethod == "OPTIONS")
                {
                    Response.SubStatusCode = (int)HttpStatusCode.OK;
                    Response.AppendHeader("Access-Control-Allow-Origin", Request.Headers.GetValues("Origin")[0]);
                    Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, userId");
                    Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                    Response.AddHeader("Access-Control-Allow-Credentials", "true");
                    Response.End();
                }
                else
                {
                    Response.AppendHeader("Access-Control-Allow-Origin", Request.Headers.GetValues("Origin")[0]);
                    Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, userId");
                    Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                    Response.AddHeader("Access-Control-Allow-Credentials", "true");                    
                }
            }
        }
    }
}