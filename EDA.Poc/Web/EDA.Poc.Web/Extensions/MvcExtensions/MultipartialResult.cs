using Glimpse.Mvc.AlternateType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDA.Poc.Web.Extensions.MvcExtensions
{
    public class MultipartialResult : JsonResult
    {
        private Controller mController;

        private List<View> views = new List<View>();

        public MultipartialResult(Controller controller)
        {
            mController = controller;
            JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet;
            //ContentType = "application/json";
        }

        public MultipartialResult AddView(string viewName, string propertyName, object model = null)
        {
            views.Add(new View() { Kind = ViewKind.View, ViewName = viewName, PropertyName = propertyName, Model = model });
            return this;
        }

        public MultipartialResult AddFirstView(string viewName, string propertyName, object model = null)
        {
            views.Insert(0, new View() { Kind = ViewKind.View, ViewName = viewName, PropertyName = propertyName, Model = model });
            return this;
        }

        public MultipartialResult AddContent(string content, string propertyName)
        {
            views.Add(new View() { Kind = ViewKind.Content, PropertyName = propertyName, Content = content });

            return this;
        }

        public MultipartialResult AddScript(string script)
        {
            views.Add(new View() { Kind = ViewKind.Script, Script = script });
            return this;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            //rendering views
            var data = new List<object>();

            foreach (var view in views)
            {
                string html = string.Empty;

                if (view.Kind == ViewKind.View)
                {
                    //view result
                    html = RenderPartialViewToString(mController, view.ViewName, view.Model);
                    data.Add(new { name = view.PropertyName, html = html });
                    //data.Add(new KeyValuePair<string, string>(view.PropertyName, html));
                }
                else if (view.Kind == ViewKind.Content)
                {
                    //content result
                    html = view.Content;
                    data.Add(new { name = view.PropertyName, html = html });
                }
                else if (view.Kind == ViewKind.Script)
                {
                    data.Add(new { script = view.Script });
                }

            }
            Data = new {
                Data = data,
                Sucesso = true
            };

            base.ExecuteResult(context);
        }

        public static string RenderPartialViewToString(Controller controller, string viewName, object model)
        {
            if (model != null)
                controller.ViewData.Model = model;
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                    if (viewResult.View == null) throw new Exception("View not found: " + viewName);
                    ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);

                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception)
            {
                //logging code
                throw;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public enum ViewKind { View, UIMessage, Content, Script }

        private class View
        {
            public ViewKind Kind { get; set; }

            public string ViewName { get; set; }
            public string Controllername { get; set; }
            public string PropertyName { get; set; }
            public object Model { get; set; }
            public string Script { get; set; }
            public string Content { get; set; }
        }
    }
}