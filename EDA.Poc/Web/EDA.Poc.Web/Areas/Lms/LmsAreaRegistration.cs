using System.Web.Mvc;

namespace EDA.Poc.Web.Areas.Lms
{
    public class LmsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Lms";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Lms_default",
                "Lms/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
