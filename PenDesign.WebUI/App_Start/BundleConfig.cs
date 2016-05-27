using System.Web;
using System.Web.Optimization;

namespace PenDesign.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                      "~/Scripts/angularjs/angular.min.js",
                      "~/Scripts/angularjs/angular-animate.min.js",
                      "~/Scripts/angularjs/angular-resource.min.js",
                      "~/Scripts/angularjs/angular-sanitize.min.js",
                      "~/Scripts/angularjs/angular-messages.min.js",
                      "~/Scripts/angularjs/toaster.min.js",
                      "~/Scripts/app/app.js",
                      "~/Scripts/angularjs/angular-recaptcha.js"));



            bundles.Add(new ScriptBundle("~/bundles/PenDesignScripts").Include(
                        "~/Scripts/PenDesign/script.js",
                        "~/Scripts/PenDesign/superfish.js",
                        "~/Scripts/PenDesign/jquery.ui.totop.js",
                        "~/Scripts/PenDesign/jquery.equalheights.js",
                        "~/Scripts/PenDesign/jquery.mobilemenu.js",
                        "~/Scripts/PenDesign/jquery.easing.1.3.js",
                        "~/Scripts/PenDesign/jquery.tooltipster.js",
                        "~/Scripts/PenDesign/classie.js"
                        ));


            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/css/camera.css",
                      "~/Content/css/component.css",
                      "~/Content/css/tooltipster.css",
                      "~/Content/css/style.css",
                      "~/Content/css/reset.css",
                      "~/Content/css/skeleton.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/css/superfish.css",
                      "~/Content/main.css"));
            
            //Admin -----------------------------------------------------------------------------------------------
            /*
             * Login App
             */
            bundles.Add(new ScriptBundle("~/bundles/loginPageJs").Include(
                       "~/Areas/Admin/Scripts/jquery/jquery-2.1.3.min.js",
                       "~/Areas/Admin/Scripts/angularjs/angular.min.js",
                       "~/Areas/Admin/Scripts/appScripts/account/loginApp.js"
                       ));
            /*
             * AdminLayout
             */
            bundles.Add(new StyleBundle("~/bundles/AdminIndexPageCss").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/backToTop/backTop.css",
                      "~/Content/fonts.css",
                      "~/Areas/Admin/Scripts/jquery/jqueryPlugin/iCheck/square/blue.css",
                      "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularToaster/toaster.css",
                      "~/Areas/Admin/Scripts/jquery/jqueryPlugin/datatables/dataTables.bootstrap.css",
                      "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsDataTable/plugins/tabletools/dataTables.tableTools.css",
                      "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsDataTable/plugins/responsive/dataTables.responsive.css",
                      "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularBtsUI/dialog/dialogs.css",
                      "~/Areas/Admin/Content/AdminLTE.css",
                      "~/Areas/Admin/Content/editInPlace.css",
                      "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsSortAble/ng-sortable.style.min.css",
                      "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsSortAble/ng-sortable.min.css",
                      "~/Areas/Admin/Content/styles.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/AdminIndexPageJs").Include(
                        "~/Areas/Admin/Scripts/jquery/jquery-1.10.2.min.js",
                        "~/Areas/Admin/Scripts/jquery/jquery-ui.1.10.2.min.js",
//                        //dataTable jqueury
                        "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsDataTable/jquery.dataTables.min.js",
                        "~/Areas/Admin/Scripts/jquery/jqueryPlugin/datatables/dataTables.bootstrap.js",
                        "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsDataTable/plugins/tabletools/dataTables.tableTools.js",
                        "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsDataTable/plugins/responsive/dataTables.responsive.js",
//
                        "~/Areas/Admin/Scripts/jquery/jqueryPlugin/iCheck/icheck.min.js",
                        "~/Areas/Admin/Content/bootstrap/js/bootstrap.min.js",
//                        // <!--angularjs-->
                        "~/Areas/Admin/Scripts/angularjs/angular.min.js",
                        "~/Areas/Admin/Scripts/angularjs/angular-route.min.js",
                        "~/Areas/Admin/Scripts/angularjs/angular-ui-router.min.js",
                        "~/Areas/Admin/Scripts/angularjs/angular-cookies.min.js",
                        "~/Areas/Admin/Scripts/angularjs/angular-resource.min.js",
                        "~/Areas/Admin/Scripts/angularjs/angular-animate.min.js",
                        "~/Areas/Admin/Scripts/angularjs/angular-sanitize.min.js",
                        "~/Areas/Admin/Scripts/angularjs/angular-messages.min.js",
//                        // <!--angular plugin-->
//                        // <!--bootstrap UI-->
                        "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularBtsUI/ui-bootstrap-tpls-0.13.2.min.js",
                        "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularBtsUI/dialog/dialogs.min.js",
//                        //<!--datatables-->
                        "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsDataTable/angular-datatables.min.js",
                        "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsDataTable/angular-datatables.bootstrap.js",
                        "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsDataTable/plugins/tabletools/angular-dt.tabletools.js",
//                        // other plugins
                        "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularToaster/toaster.js",
                        "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularMoment/moment.min.js",
                        "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularMoment/angular-moment.min.js",
                        "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularFileuUpload/angular-file-upload.js",
//                        //youtube API
                        "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsYoutube/angular-youtube-embed.js",
                        //<!--ngSortAble-->
                        "~/Areas/Admin/Scripts/angularjs/angularjsPlugin/angularjsSortAble/ng-sortable.min.js",
                        //<!--appScripts-->
                        "~/Areas/Admin/Scripts/appScripts/data.js",
                        "~/Areas/Admin/Scripts/appScripts/app.js",
                        "~/Areas/Admin/Scripts/appScripts/indexApp.js",
                        //<!--account-->
                        "~/Areas/Admin/Scripts/appScripts/account/accountController.js",
                        "~/Areas/Admin/Scripts/appScripts/account/accountService.js",
                        //<!--news-->
                        "~/Areas/Admin/Scripts/appScripts/news/newsService.js",
                        "~/Areas/Admin/Scripts/appScripts/news/newsMappingService.js",
                        "~/Areas/Admin/Scripts/appScripts/news/newsController.js",
                        //<!--project-->
                        "~/Areas/Admin/Scripts/appScripts/project/projectService.js",
                        "~/Areas/Admin/Scripts/appScripts/project/projectNewsService.js",
                        "~/Areas/Admin/Scripts/appScripts/project/projectImageService.js",
                        "~/Areas/Admin/Scripts/appScripts/project/projectController.js",
                        // <!--video-->
                        "~/Areas/Admin/Scripts/appScripts/video/videoService.js",
                        "~/Areas/Admin/Scripts/appScripts/video/videoMappingService.js",
                        "~/Areas/Admin/Scripts/appScripts/video/videoController.js",
                        //<!--construction-->
                        "~/Areas/Admin/Scripts/appScripts/construction/constructionService.js",
                        "~/Areas/Admin/Scripts/appScripts/construction/constructionController.js",
                        //<!--email-->
                        "~/Areas/Admin/Scripts/appScripts/email/emailService.js",
                        "~/Areas/Admin/Scripts/appScripts/email/emailController.js",
                        "~/Areas/Admin/Scripts/appScripts/email/contactEmailController.js",
                        //<!--banner-->
                        "~/Areas/Admin/Scripts/appScripts/banner/bannerService.js",
                        "~/Areas/Admin/Scripts/appScripts/banner/bannerMappingService.js",
                        "~/Areas/Admin/Scripts/appScripts/banner/bannerController.js",
                        // <!--config-->
                        "~/Areas/Admin/Scripts/appScripts/config/configService.js",
                        "~/Areas/Admin/Scripts/appScripts/config/configController.js"
                        ));


            /*
             * Index APP
             */
            bundles.Add(new StyleBundle("~/bundles/AdminAppIndexCss").Include(
                      "~/Areas/Admin/Content/fontAwesome/css/font-awesome.min.css",
                      "~/Areas/Admin/Scripts/jquery/jqueryPlugin/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                      "~/Areas/Admin/Content/skins/_all-skins.css",
                      "~/Areas/Admin/Scripts/jquery/jqueryPlugin/iCheck/all.css",
                      "~/Scripts/dropzone/basic.css",
                      "~/Scripts/dropzone/dropzone.min.css",
                      "~/Areas/Admin/Content/metroUI/metro-icons.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/AdminAppIndexJs").Include(
                        "~/Areas/Admin/Scripts/jquery/jqueryPlugin/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                        "~/Areas/Admin/Scripts/jquery/jqueryPlugin/fastclick/fastclick.min.js",
                        "~/Areas/Admin/Scripts/jquery/jqueryPlugin/slimScroll/jquery.slimscroll.min.js",
                        "~/Areas/Admin/Scripts/jquery/jqueryPlugin/daterangepicker/daterangepicker.js",
                        //"~/ckeditor/ckeditor.js",
                        //"~/ckfinder/ckfinder.js",
                        "~/Scripts/dropzone/dropzone.min.js",
                        "~/Areas/Admin/Scripts/LTEScripts/app.js",
                        "~/Areas/Admin/Scripts/LTEScripts/dashboard.js",
                        "~/Areas/Admin/Scripts/LTEScripts/demo.js"
                        ));

            //Account
            bundles.Add(new ScriptBundle("~/bundles/account").Include(
                        "~/Areas/Admin/Scripts/appScripts/account/accountController.js",
                        "~/Areas/Admin/Scripts/appScripts/account/accountService.js"
                        ));
        }
    }
}
