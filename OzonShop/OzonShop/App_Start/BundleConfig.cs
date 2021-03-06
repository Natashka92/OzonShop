﻿using System.Web;
using System.Web.Optimization;

namespace OzonShop
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-1.*"));


            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap*"));

            bundles.Add(new StyleBundle("~/Content/css")
                    .Include("~/Content/site.css")
                    .Include("~/Content/bootstrap*")
                    .Include("~/Content/themes/base/jquery-ui.css")
                    .Include("~/Content/jquery.tagit.css"));
        }
    }
}