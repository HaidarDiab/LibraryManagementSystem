using System.Web;
using System.Web.Optimization;

namespace LibraryManagementSystem
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/lib").Include(
						"~/Scripts/jquery-{version}.js",
						"~/Scripts/bootstrap.js",
					  "~/Scripts/bootbox.js",
					  "~/Scripts/bootbox.min.js",
					  "~/Scripts/DataTables/jquery.dataTables.js",
					  "~/Scripts/DataTables/dataTables.bootstrap.js",
						"~/Scripts/PreviewSelectedImage.js",
						"~/Scripts/WaitingUntilLoadingPage.js",
						"~/Scripts/toastr.js",
						"~/Scripts/sweetalert.min.js",
						"~/Scripts/underscore-umd-min.js"
						));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));


			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/bootstrap-icons.css",
					  "~/Content/site.css",
					  "~/Content/Animate.css",
					  "~/Content/toastr.css",
					  "~/Content/sweetalert/sweetalert1.1.3.min.css",
					  "~/Content/DataTables/css/dataTables.bootstrap.css"));
		}
	}
}
