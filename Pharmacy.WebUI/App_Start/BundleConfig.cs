using System.Web;
using System.Web.Optimization;

namespace Pharmacy.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/mysite").Include(
               "~/content/member/assets/css/bulma.css").Include(
               "~/content/member/assets/css/core.css").Include(
               "~/content/member/assets/js/slick/slick.css").Include(
               "~/content/member/assets/js/slick/slick-theme.css").Include(
               "~/content/member/assets/js/webuipopover/jquery.webui-popover.min.css").Include(
               "~/content/member/assets/js/izitoast/css/iziToast.min.css").Include(
               "~/content/member/assets/js/zoom/zoom.css").Include(
               "~/content/member/assets/js/jpcard/card.css").Include(
               "~/content/member/assets/css/chosen/chosen.css").Include(
               "~/content/member/assets/css/icons.min.css"));
            bundles.Add(new ScriptBundle("~/js/mysite").Include(
                        "~/content/member/assets/js/jquery.js").Include(
                        "~/content/member/assets/js/app.js").Include(
                        "~/content/member/assets/js/nephos.js"));

        }
    }
}
