using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplebiteMobiles_App.Models;

namespace ApplebiteMobiles_App.Controllers
{
    public class AdmnController : Controller
    {
        appleEntities ad = new appleEntities();
        // GET: Admn
        public String GetString()
        {
            try
            {
                if (HttpContext.Session != null)
                {
                    return "Welcome " + ((ProcUserValidateSP_Result)Session["LoginVal"]).UserName;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex) { return ""; }
        }
        public int GetUserID()
        {
            try
            {
                if (HttpContext.Session != null)
                {
                    return ((ProcUserValidateSP_Result)Session["LoginVal"]).UserID;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex) { return 0; }
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            try
            {
                if (HttpContext.Session != null)
                {
                    Session.Abandon();
                    Session.Clear();
                    Session.RemoveAll();
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(tblUser tu)
        {
            ProcUserValidateSP_Result ar = ad.ProcUserValidateSP(tu.UserName, tu.Password).FirstOrDefault();
            if (ar != null)
            {
                if (ar.UserID == -1 || ar.UserID == 0)
                {
                    ViewBag.Message = "Invalid Credential";
                    return View();
                }
                else
                {
                    Session["LoginVal"] = ar;
                    return RedirectToAction("Home");
                }
            }
            else
            {
                ViewBag.Message = "Invalid Credential";
                return View();
            }
        }
        public ActionResult Home(tblUser tu)
        {
            return View();
        }
        
        //Accessories Master Start
        public ActionResult Accessories()
        {
            List<ProcAccessoriesMasterSelectSP_Result> ar = ad.ProcAccessoriesMasterSelectSP().ToList();    
            return View(ar);
        }
        public ActionResult CreateA_Master()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateA_Master(tblAccessoriesMaster am, HttpPostedFileBase file)
        {
            var allowedExtensions = new[] {".Jpg", ".png", ".jpg", "jpeg"};
            var ext = Path.GetExtension(file.FileName);
            if (allowedExtensions.Contains(ext))
            {               
                string myfile = DateTime.Now.ToString("dd/mm/yyy hh:mm:ss tt").Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "") + ext;
                file.SaveAs(Server.MapPath(("../Img/AMaster/") + myfile));

                int userid = GetUserID();var returnid = new ObjectParameter("output", typeof(Int32));
                int result1 = ad.ProcAccessoriesMasterInsertSP(am.A_Name, am.A_Description, myfile, userid, DateTime.Now, returnid);

                if (result1 > 0)
                {
                    return RedirectToAction("Accessories");
                }
                else
                {
                    ViewBag.message = "accessories not added";
                    return View();
                }                
            }
            else
            {
                ViewBag.message = "Please choose only Image file";
                return View();
            }
        }
        public ActionResult EditA_Master(int id)
        {
            ProcAccessoriesMasterSelectByAccessoriesIDSP_Result ar = ad.ProcAccessoriesMasterSelectByAccessoriesIDSP(id).FirstOrDefault();
            return View(ar);
        }
        [HttpPost]
        public ActionResult EditA_Master(int id, ProcAccessoriesMasterSelectByAccessoriesIDSP_Result am, HttpPostedFileBase file)
        {
            string myfile = string.Empty;
            if (file != null)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg" };
                var ext = Path.GetExtension(file.FileName);
                if (allowedExtensions.Contains(ext))
                {
                    myfile = DateTime.Now.ToString("dd/mm/yyy hh:mm:ss tt").Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "") + ext;
                    file.SaveAs(Server.MapPath(("../../Img/AMaster/") + myfile));
                }
            }

            int userid = GetUserID();
            var returnId = new ObjectParameter("Output", typeof(Int32));
            int Result1 = ad.ProcAccessoriesMasterUpdateSP(id, am.A_Name, am.A_Description, (file != null ? myfile : am.A_Image), userid, DateTime.Now, returnId);
            if (Result1 > 0)
            {
                return RedirectToAction("Accessories");
            }
            else
            {
                ViewBag.Message = "Accessories Not Updated";
                return View();
            }
        }
        public ActionResult Delete_AMaster(int id)
        {
            int userid = GetUserID();
            var returnId = new ObjectParameter("Output", typeof(Int32));
            int Result1 = ad.ProcAccessoriesMasterDeleteSP(id, userid, DateTime.Now, returnId);

            if (Result1 > 0)
            {
                return RedirectToAction("Accessories");
            }
            else
            {
                ViewBag.Message = "Accessories Not Deleted";
                return View();
            }
        }
        //Accessories Master End

        //Accessories Details Start
        public ActionResult AccessoriesDetail()
        {
            List<ProcAccessoriesDetailsSelectSP_Result> ar = ad.ProcAccessoriesDetailsSelectSP().ToList();
            return View(ar);
        }

        public ActionResult CreateADetail_Master()
        {
            //ViewBag.CityList = ToSelectList();
            ViewBag.AccessoriesID = new SelectList(ad.ProcAccessoriesMasterSelectSP().ToList(), "AccessoriesID", "A_Name");
            return View();
        }
        [HttpPost]
        public ActionResult CreateADetail_Master(tblAccessoriesDetail am, HttpPostedFileBase file)
        {
            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg" };
            var ext = Path.GetExtension(file.FileName);
            if (allowedExtensions.Contains(ext))
            {
                string myfile = DateTime.Now.ToString("dd/mm/yyy hh:mm:ss tt").Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "") + ext;
                file.SaveAs(Server.MapPath(("../Img/ADetails/") + myfile));

                int userid = GetUserID(); var returnid = new ObjectParameter("output", typeof(Int32));
                int result1 = ad.ProcAccessoriesDetailsInsertSP(am.A_DName, am.AccessoriesID, myfile, userid, DateTime.Now, returnid);

                if (result1 > 0)
                {
                    return RedirectToAction("AccessoriesDetail");
                }
                else
                {
                    ViewBag.message = "accessories details not added";
                    return View();
                }
            }
            else
            {
                ViewBag.message = "Please choose only Image file";
                return View();
            }
        }
        public ActionResult EditADetail_Master(int id)
        {
            ProcAccessoriesDetailsSelectByA_DetailsIDSP_Result ar = ad.ProcAccessoriesDetailsSelectByA_DetailsIDSP(id).FirstOrDefault();
            ViewBag.AccessoriesID = new SelectList(ad.ProcAccessoriesMasterSelectSP().ToList(), "AccessoriesID", "A_Name",ar.AccessoriesID);
            //List<ProcAccessoriesMasterSelectSP_Result> am = ad.ProcAccessoriesMasterSelectSP().ToList();
            //ViewBag.AccessoriesID = new SelectList(am , "AccessoriesID", "A_Name", ar.AccessoriesID);
            return View(ar);
        }
        [HttpPost]
        public ActionResult EditADetail_Master(int id, ProcAccessoriesDetailsSelectByA_DetailsIDSP_Result am, HttpPostedFileBase file)
        {
            string myfile = string.Empty;
            if (file != null)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg" };
                var ext = Path.GetExtension(file.FileName);
                if (allowedExtensions.Contains(ext))
                {
                    myfile = DateTime.Now.ToString("dd/mm/yyy hh:mm:ss tt").Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "") + ext;
                    file.SaveAs(Server.MapPath(("../../Img/ADetails/") + myfile));
                }
            }

            int userid = GetUserID();
            var returnId = new ObjectParameter("Output", typeof(Int32));
            int Result1 = ad.ProcAccessoriesDetailsUpdateSP(id, am.A_DName, am.AccessoriesID, (file != null ? myfile : am.A_DImage) , userid, DateTime.Now, returnId);
            if (Result1 > 0)
            {
                return RedirectToAction("AccessoriesDetail");
            }
            else
            {
                ViewBag.Message = "Accessories Details Not Updated";
                return View();
            }
        }
        public ActionResult DeleteDetail_AMaster(int id)
        {
            int userid = GetUserID();
            var returnId = new ObjectParameter("Output", typeof(Int32));
            int Result1 = ad.ProcAccessoriesDetailsDeleteSP(id, userid, DateTime.Now, returnId);

            if (Result1 > 0)
            {
                return RedirectToAction("AccessoriesDetail");
            }
            else
            {
                ViewBag.Message = "Accessories Details Not Deleted";
                return View();
            }
        }
        //Accessories Details End


        //Gallery Start
        public ActionResult Gallery()
        {
            List<ProcGallerySelectSP_Result> ar = ad.ProcGallerySelectSP().ToList();
            return View(ar);
        }
        public ActionResult CreateA_Gallery()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateA_Gallery(tblGallery am, HttpPostedFileBase file)
        {
            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg" };
            var ext = Path.GetExtension(file.FileName);
            if (allowedExtensions.Contains(ext))
            {
                string myfile = DateTime.Now.ToString("dd/mm/yyy hh:mm:ss tt").Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "") + ext;
                file.SaveAs(Server.MapPath(("../Img/AGallery/") + myfile));

                int userid = GetUserID(); var returnid = new ObjectParameter("output", typeof(Int32));
                int result1 = ad.ProcGalleryInsertSP(am.GalleryName, myfile, userid, DateTime.Now, returnid);

                if (result1 > 0)
                {
                    return RedirectToAction("Gallery");
                }
                else
                {
                    ViewBag.message = "Gallery not added";
                    return View();
                }
            }
            else
            {
                ViewBag.message = "Please choose only Image file";
                return View();
            }
        }
        public ActionResult EditA_Gallery(int id)
        {
            ProcGallerySelectByGalleryIDSP_Result ar = ad.ProcGallerySelectByGalleryIDSP(id).FirstOrDefault();
            return View(ar);
        }
        [HttpPost]
        public ActionResult EditA_Gallery(int id, ProcGallerySelectByGalleryIDSP_Result am, HttpPostedFileBase file)
        {
            string myfile = string.Empty;
            if (file != null)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg" };
                var ext = Path.GetExtension(file.FileName);
                if (allowedExtensions.Contains(ext))
                {
                    myfile = DateTime.Now.ToString("dd/mm/yyy hh:mm:ss tt").Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "") + ext;
                    file.SaveAs(Server.MapPath(("../../Img/AGallery/") + myfile));
                }
            }

            int userid = GetUserID();
            var returnId = new ObjectParameter("Output", typeof(Int32));
            int Result1 = ad.ProcGalleryUpdateSP(id, am.GalleryName, (file != null ? myfile : am.GalleryImage), userid, DateTime.Now, returnId);
            if (Result1 > 0)
            {
                return RedirectToAction("Gallery");
            }
            else
            {
                ViewBag.Message = "Gallery Not Updated";
                return View();
            }
        }
        public ActionResult Delete_AGallery(int id)
        {
            int userid = GetUserID();
            var returnId = new ObjectParameter("Output", typeof(Int32));
            int Result1 = ad.ProcGalleryDeleteSP(id, userid, DateTime.Now, returnId);

            if (Result1 > 0)
            {
                return RedirectToAction("Gallery");
            }
            else
            {
                ViewBag.Message = "Gallery Not Deleted";
                return View();
            }
        }
        //Gallery End

        //Mobile Master Start
        public ActionResult Mobile()
        {
            List<ProcMobileMasterSelectSP_Result> ar = ad.ProcMobileMasterSelectSP().ToList();
            return View(ar);
        }
        public ActionResult CreateM_Master()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateM_Master(tblMobileMaster am, HttpPostedFileBase file)
        {
            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg" };
            var ext = Path.GetExtension(file.FileName);
            if (allowedExtensions.Contains(ext))
            {
                string myfile = DateTime.Now.ToString("dd/mm/yyy hh:mm:ss tt").Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "") + ext;
                file.SaveAs(Server.MapPath(("../Img/MMaster/") + myfile));

                int userid = GetUserID(); var returnid = new ObjectParameter("output", typeof(Int32));
                int result1 = ad.ProcMobileMasterInsertSP(am.M_Name, am.M_Description, myfile, userid, DateTime.Now, returnid);

                if (result1 > 0)
                {
                    return RedirectToAction("Mobile");
                }
                else
                {
                    ViewBag.message = "Mobile not added";
                    return View();
                }
            }
            else
            {
                ViewBag.message = "Please choose only Image file";
                return View();
            }
        }
        public ActionResult EditM_Master(int id)
        {
            ProcMobileMasterSelectByMobileIDSP_Result ar = ad.ProcMobileMasterSelectByMobileIDSP(id).FirstOrDefault();
            return View(ar);
        }
        [HttpPost]
        public ActionResult EditM_Master(int id, ProcMobileMasterSelectByMobileIDSP_Result am, HttpPostedFileBase file)
        {
            string myfile = string.Empty;
            if (file != null)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg" };
                var ext = Path.GetExtension(file.FileName);
                if (allowedExtensions.Contains(ext))
                {
                    myfile = DateTime.Now.ToString("dd/mm/yyy hh:mm:ss tt").Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "") + ext;
                    file.SaveAs(Server.MapPath(("../../Img/MMaster/") + myfile));
                }
            }

            int userid = GetUserID();
            var returnId = new ObjectParameter("Output", typeof(Int32));
            int Result1 = ad.ProcMobileMasterUpdateSP(id, am.M_Name, am.M_Description, (file != null ? myfile : am.M_Image), userid, DateTime.Now, returnId);
            if (Result1 > 0)
            {
                return RedirectToAction("Mobile");
            }
            else
            {
                ViewBag.Message = "Mobile Not Updated";
                return View();
            }
        }
        public ActionResult Delete_MMaster(int id)
        {
            int userid = GetUserID();
            var returnId = new ObjectParameter("Output", typeof(Int32));
            int Result1 = ad.ProcMobileMasterDeleteSP(id, userid, DateTime.Now, returnId);

            if (Result1 > 0)
            {
                return RedirectToAction("Mobile");
            }
            else
            {
                ViewBag.Message = "Mobile Not Deleted";
                return View();
            }
        }
        //Mobile Master End

        //Mobile Details Start
        public ActionResult MobileDetail()
        {
            List<ProcMobileDetailsSelectSP_Result> ar = ad.ProcMobileDetailsSelectSP().ToList();
            return View(ar);
        }
        public ActionResult CreateMDetail_Master()
        {
            ViewBag.MobileID = new SelectList(ad.ProcMobileMasterSelectSP().ToList(), "MobileID", "M_Name");
            return View();
        }
        [HttpPost]
        public ActionResult CreateMDetail_Master(tblMobileDetail am, HttpPostedFileBase file)
        {
            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg" };
            var ext = Path.GetExtension(file.FileName);
            if (allowedExtensions.Contains(ext))
            {
                string myfile = DateTime.Now.ToString("dd/mm/yyy hh:mm:ss tt").Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "") + ext;
                file.SaveAs(Server.MapPath(("../Img/MDetails/") + myfile));

                int userid = GetUserID(); var returnid = new ObjectParameter("output", typeof(Int32));
                int result1 = ad.ProcMobileDetailsInsertSP(am.M_DName, am.MobileID, myfile,am.M_Description, userid, DateTime.Now, returnid);

                if (result1 > 0)
                {
                    return RedirectToAction("MobileDetail");
                }
                else
                {
                    ViewBag.message = "Mobile details not added";
                    return View();
                }
            }
            else
            {
                ViewBag.message = "Please choose only Image file";
                return View();
            }
        }
        public ActionResult EditMDetail_Master(int id)
        {
            ProcMobileDetailsSelectByM_DetailsIDSP_Result ar = ad.ProcMobileDetailsSelectByM_DetailsIDSP(id).FirstOrDefault();
            ViewBag.MobileID = new SelectList(ad.ProcMobileMasterSelectSP().ToList(), "MobileID", "M_Name", ar.MobileID);
            return View(ar);
        }
        [HttpPost]
        public ActionResult EditMDetail_Master(int id, ProcMobileDetailsSelectByM_DetailsIDSP_Result am, HttpPostedFileBase file)
        {
            string myfile = string.Empty;
            if (file != null)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg" };
                var ext = Path.GetExtension(file.FileName);
                if (allowedExtensions.Contains(ext))
                {
                    myfile = DateTime.Now.ToString("dd/mm/yyy hh:mm:ss tt").Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "") + ext;
                    file.SaveAs(Server.MapPath(("../../Img/MDetails/") + myfile));
                }
            }

            int userid = GetUserID();
            var returnId = new ObjectParameter("Output", typeof(Int32));
            int Result1 = ad.ProcMobileDetailsUpdateSP(id, am.M_DName, am.MobileID, (file != null ? myfile : am.M_DImage), am.M_Description, userid, DateTime.Now, returnId);
            if (Result1 > 0)
            {
                return RedirectToAction("MobileDetail");
            }
            else
            {
                ViewBag.Message = "Mobile Details Not Updated";
                return View();
            }
        }
        public ActionResult DeleteDetail_MMaster(int id)
        {
            int userid = GetUserID();
            var returnId = new ObjectParameter("Output", typeof(Int32));
            int Result1 = ad.ProcMobileDetailsDeleteSP(id, userid, DateTime.Now, returnId);

            if (Result1 > 0)
            {
                return RedirectToAction("MobileDetail");
            }
            else
            {
                ViewBag.Message = "Mobile Details Not Deleted";
                return View();
            }
        }
        //Mobile Details End

        //Current Offer Start
        public ActionResult CurrentOffer()
        {
            List<ProcCurrentOffersSelectSP_Result> ar = ad.ProcCurrentOffersSelectSP().ToList();
            return View(ar);
        }
        public ActionResult EditCurrentOffer_Master(int id)
        {
            ProcCurrentOffersSelectByOfferDetailsIDSP_Result ar = ad.ProcCurrentOffersSelectByOfferDetailsIDSP(id).FirstOrDefault();
            return View(ar);
        }
        [HttpPost]
        public ActionResult EditCurrentOffer_Master(int id, ProcCurrentOffersSelectByOfferDetailsIDSP_Result am, HttpPostedFileBase file)
        {
            string myfile = string.Empty;
            if (file != null)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg" };
                var ext = Path.GetExtension(file.FileName);
                if (allowedExtensions.Contains(ext))
                {
                    myfile = DateTime.Now.ToString("dd/mm/yyy hh:mm:ss tt").Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "") + ext;
                    file.SaveAs(Server.MapPath(("../../Img/CurrentOffers/") + myfile));
                }
            }

            int userid = GetUserID();
            var returnId = new ObjectParameter("Output", typeof(Int32));
            int Result1 = ad.ProcCurrentOffersUpdateSP(id, am.OfferName, (file != null ? myfile : am.OfferImage), am.OfferDescription, am.OfferPrice, userid, DateTime.Now, returnId);
            if (Result1 > 0)
            {
                return RedirectToAction("CurrentOffer");
            }
            else
            {
                ViewBag.Message = "Current Offer Not Updated";
                return View();
            }
        }
        //Current Offer End
    }
}