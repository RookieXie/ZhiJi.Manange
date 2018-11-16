using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YYLog.ClassLibrary;

namespace WebCore.Filter
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        //private readonly IHostingEnvironment _hostingEnvironment;
        //private readonly IModelMetadataProvider _modelMetadataProvider;

        //public CustomExceptionFilterAttribute(
        //    IHostingEnvironment hostingEnvironment,
        //    IModelMetadataProvider modelMetadataProvider)
        //{
        //    _hostingEnvironment = hostingEnvironment;
        //    _modelMetadataProvider = modelMetadataProvider;
        //}

        //private string _moduleName;

        //public CustomExceptionFilterAttribute()
        //{
        //}
        //public CustomExceptionFilterAttribute(string moduleName)
        //{
        //    _moduleName = moduleName;
        //}

        public override void OnException(ExceptionContext context)
        {
            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    // do nothing
            //    return;
            //}
            //var result = new ViewResult { ViewName = "CustomError" };
            //result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
            //result.ViewData.Add("Exception", context.Exception);
            // TODO: Pass additional detailed data via ViewData
            var path = context.HttpContext.Request.Path.ToString();
            Log.WriteErrorLog(path, context.Exception.Message);

            //if (context.Exception.HelpLink == "TokenOverdue")
            //{
            //    context.HttpContext.Response.Redirect("/error/TokenOverdue");
            //}
            //else if (context.Exception.HelpLink == "Inadmissibility")
            //{
            //    context.HttpContext.Response.Redirect("/error/Inadmissibility");
            //}
            //context.Result = error;
        }
    }
}
