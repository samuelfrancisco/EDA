using System.Web.Optimization;

namespace EDA.Poc.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/js/vendors/jquery/all").Include(
                "~/Content/js/vendors/jquery/jquery-3.1.1.min.js",
                "~/Content/js/vendors/jquery.validation/jquery.validate.min.js",
                "~/Content/js/vendors/jquery.validate.unobtrusive/jquery.validate.unobtrusive.min.js",
                "~/Content/js/vendors/jquery.numeric/jquery.numeric.js"));

            bundles.Add(new ScriptBundle("~/Content/js/vendors/jcarousellite/all").Include(
                "~/Content/js/vendors/jcarousellite/jegurucarousel.js"));

            bundles.Add(new ScriptBundle("~/Content/js/vendors/foolproof/all").Include(
                "~/Content/js/vendors/foolproof/mvcfoolproof.unobtrusive.min.js",
                "~/Content/js/vendors/foolproof/mvcfoolproof.unobtrusive.custom.min.js"));

            bundles.Add(new ScriptBundle("~/Content/js/vendors/pickadate/all").Include(
                "~/Content/js/vendors/pickadate/legacy.js",
                "~/Content/js/vendors/pickadate/picker.js",
                "~/Content/js/vendors/pickadate/picker.date.js",
                "~/Content/js/vendors/pickadate/picker.time.js",
                "~/Content/js/vendors/pickadate/translations/pt_BR.js"));

            bundles.Add(new StyleBundle("~/Content/js/vendors/pickadate/themes/all").Include(
                "~/Content/js/vendors/pickadate/themes/default.css",
                "~/Content/js/vendors/pickadate/themes/default.date.css"));

            bundles.Add(new ScriptBundle("~/Content/all").Include(
                "~/Content/js/geral.js"));
        }
    }
}
