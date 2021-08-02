using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 2.Models;

namespace 2.1
{
    public class a : a
    {
        appleEntities ad = new appleEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your About page.";

            return View();
        }
        public ActionResult Gallery()
        {
            List<ProcGallerySelectSP_Result> ar = ad.ProcGallerySelectSP().ToList();
            return View(ar);
        }
        public ActionResult Social()
        {
            ViewBag.Message = "Your Social page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Accessories(int? id)
        {
            if (id == 0)
            {
                List<ProcAccessoriesDetailsSelectByAccessoriesIDSP_Result> menus = ad.ProcAccessoriesDetailsSelectByAccessoriesIDSP(0).ToList();
                return View(menus);
            }
            else
            {
                List<ProcAccessoriesDetailsSelectByAccessoriesIDSP_Result> menus = ad.ProcAccessoriesDetailsSelectByAccessoriesIDSP(id).ToList();
                return View(menus);
            }            
        }

        public ActionResult ShowAccessories()
        {
            List<ProcAccessoriesMasterSelectSP_Result> lst = ad.ProcAccessoriesMasterSelectSP().ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Mobiles(int? id)
        {
            if (id == 0)
            {
                List<ProcMobileDetailsSelectByMobileIDSP_Result> menus = ad.ProcMobileDetailsSelectByMobileIDSP(0).ToList();
                return View(menus);
            }
            else
            {
                List<ProcMobileDetailsSelectByMobileIDSP_Result> menus = ad.ProcMobileDetailsSelectByMobileIDSP(id).ToList();
                return View(menus);
            }
        }

        public ActionResult ShowMobile()
        {
            List<ProcMobileMasterSelectSP_Result> lst = ad.ProcMobileMasterSelectSP().ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
}
