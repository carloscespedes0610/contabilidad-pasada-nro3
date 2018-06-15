using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace Contabilidad {

    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {

            var scriptBundle = new ScriptBundle("~/Scripts/bundle");
            var styleBundle = new StyleBundle("~/Content/bundle");

            // jQuery
            scriptBundle
                .Include("~/Scripts/jquery-2.2.3.js");

            // jQuery
            scriptBundle
                .Include("~/Scripts/jquery-ui.min.js");

            // Bootstrap
            scriptBundle
                .Include("~/Scripts/bootstrap.js");

            // AdminLTE App
            scriptBundle
                .Include("~/Content/dist/js/app.min.js");

            // AdminLTE for demo purposes
            scriptBundle
                .Include("~/Content/dist/js/demo.js");



            // Bootstrap
            styleBundle
                .Include("~/Content/bootstrap.css");

            // Custom site styles
            styleBundle
                .Include("~/Content/Site.css");

            // Theme style
            styleBundle
                .Include("~/Content/dist/css/AdminLTE.min.css");

            // AdminLTE Skins. Choose a skin from the css/skins
            // folder instead of downloading all of them to reduce the load.
            styleBundle
                .Include("~/Content/dist/css/skins/_all-skins.min.css");

            // Font Awesome
            styleBundle
                .Include("~/Content/font-awesome.min.css");

            // Ionicons
            styleBundle
                .Include("~/Content/ionicons.min.css");

            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);

#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}