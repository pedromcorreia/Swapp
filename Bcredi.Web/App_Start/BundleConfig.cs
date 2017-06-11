using System.Web;
using System.Web.Optimization;

namespace Bcredi.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        /*
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-1.11.3.min.js",
                       "~/Scripts/datepicker.js",
                       "~/Scripts/moment.js",
                       "~/Scripts/validacoes.js",
                       "~/Scripts/valida.cpf-cnpj.js"
                       //",~/Scripts/jquery-{version}.js"
                       ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                       "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(                      
                      //"~/Scripts/bootstrap.js",
                      //"~/Scripts/respond.js",
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css_nativo").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/comum.css",
                      "~/Content/css/cadastro.css"));
        }
        */
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include(
                        "~/Scripts/jquery-3.1.1.min.js",
                        "~/Scripts/jquery.maskedinput.js",
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/datepicker.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js",
                        "~/Scripts/bootstrap-toggle.min.js",
                         "~/Scripts/jquery.maskMoney.min.js",
                         "~/Scripts/moment.js",                         
                         "~/Scripts/alertify.min.js",
                        "~/Scripts/jquery.smartWizard.js",
                        "~/Scripts/jquery.icheck.js",
                        "~/Scripts/editorWYSIWYG.js",
                        "~/Scripts/validacoes.js",
                        "~/Scripts/comum.js",
                        "~/Scripts/bootstrap.min.js", 
                        "~/Scripts/jquery.loadingModal.js"
                )
            );

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            "~/Scripts/jquery.unobtrusive*",
            "~/Scripts/jquery.validate*"));

            //Este arquivo "~/Scripts/validacoes.js" é o próprio onesystem, use ele.
            //bundles.Add(new ScriptBundle("~/bundles/onesystem")
            //    .Include(
            //            "~/Scripts/systemOne.js"
            //    )
            //);

            /* Scripts para lidar com graficos */
            bundles.Add(new ScriptBundle("~/bundles/jquery-chart")
                .Include(
                    "~/Scripts/jquery.flot.js",
                    "~/Scripts/jquery.flot.time.js",
                    "~/Scripts/jquery.flot.pie.js",
                    "~/Scripts/jquery.flot.spline.min.js",
                    "~/Scripts/excanvas.js",
                    "~/Scripts/flot-data.js",
                    "~/Scripts/jquery.flot.resize.js",
                    "~/Scripts/canvasjs.min.js",
                    "~/Scripts/canvasjs.min.js",
                    "~/Scripts/jquery.flot.tooltip.js"
                )
            );

            /* Scripts para lidar com valores númericos e conversão para moeda */
            bundles.Add(new ScriptBundle("~/bundles/jquery-currency")
                .Include(
                    "~/Scripts/jquery.formatCurrency-1.4.0.min.js",
                    //"~/Scripts/jquery.formatCurrency.all.js",
                    "~/Scripts/jquery.formatCurrency.pt-BR.js"
                )
            );

            /* Scripts para lidar com valores númericos e conversão para % ou número formato (pt-br) */
            /* https://code.google.com/archive/p/jquery-numberformatter/ */
            bundles.Add(new ScriptBundle("~/bundles/jquery-numberformatter")
                .Include(
                    "~/Scripts/jquery.hashset.js",
                    "~/Scripts/jquery.hashtable.js",
                    "~/Scripts/jquery.numberformatter-1.2.4.min.js"
                )
            );

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include(
                      //"~/Scripts/bootstrap.js",
                      //"~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap-3.3.7.min.js",
                      "~/Scripts/respond.js",
                      //"~/Scripts/jquery.validate.js",
                      "~/Scripts/valida.cpf-cnpj.js",
                      "~/Scripts/bootstrap-wysiwyg.min.js",
                      "~/Scripts/jquery.hotkeys.js",
                      "~/Scripts/prettify.js",
                      "~/Scripts/daterangepicker.js",
                      "~/Scripts/fastclick.js",
                      "~/Scripts/nprogress.js"

                )
            );


            bundles.Add(new StyleBundle("~/Content/css")
                .Include(
                      "~/Content/dataTables.bootstrap.min.css",
                      "~/Content/responsive.bootstrap.min.css",
                      "~/Content/jquery-ui-1.12.1.min.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/bootstrap-toggle.min.css",
                      //"~/Content/font-awesome.min.css",
                      "~/Content/editorWYSIWYG.css",
                      "~/Content/SystemOne.css",
                      "~/Content/Sidebar.css",
                      "~/Content/responsive.table.min.css",
                      "~/Content/prettify.min.css",
                      "~/Content/yellow.css",
                      //"~/Content/font-awesome.css",
                      "~/Content/metisMenu.css",
                      "~/Content/morris.css",
                      "~/Content/sb-admin-2.css",
                      "~/Content/jquery.loadingModal.css",
                      "~/Content/alertify.core.css",
                      "~/Content/alertify.default.css",
                      "~/Content/css/comum.css",
                      "~/Content/css/login.css",
                      "~/Content/css/fileinput.css",
					  "~/Content/css/tooltip.css",
					  "~/Content/css/estrutura.css",
					  "~/Content/css/paginas.css",
					  "~/Content/css/responsivo.css",
                      "~/Content/css/cadastro.css"));


            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/css/bootstrap.min.css",
            //          "~/Content/css/comum.css",
            //          "~/Content/css/fileinput.css",
            //          "~/Content/css/login.css",
            //          "~/Content/css/cadastro.css"));

            bundles.Add(new StyleBundle("~/bundles/clicksign").Include(
                      "~/Scripts/clicksign/clicksign.js"
                ));
            bundles.Add(new StyleBundle("~/bundles/fileinput").Include(
                      "~/Scripts/fileinput/canvas-to-blob.js",
                "~/Scripts/fileinput/fileinput.js",
                "~/Scripts/fileinput/pt-BR.js",
                "~/Scripts/fileinput/purify.js",
               "~/Scripts/fileinput/sortable.js"

                ));
        }
    }
}
