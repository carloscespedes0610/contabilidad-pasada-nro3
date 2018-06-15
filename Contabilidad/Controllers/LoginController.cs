using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml;
using Contabilidad.Models.DAC;
using Contabilidad.Models.VM;
using Contabilidad.Models.Modules;

namespace Contabilidad.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            try
            {
                clsLoginVM oLoginVM = new clsLoginVM();

                oLoginVM.UsuarioCod = "jmercado";
                oLoginVM.UsuarioPass = "123";

                ViewBag.EmpresaId = new SelectList(EmpresaList(), "EmpresaId", "EmpresaDes");
                return View(oLoginVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("ErrorMsg", "Error", new { KeyMessageErr = exp.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(clsLoginVM oLoginVM)
        {
            this.GetDefaultData();

            if (ModelState.IsValid)
            {
                clsEmpresaVM oEmpresaVM = EmpresaList().Find(x => x.EmpresaId == oLoginVM.EmpresaId);

                try
                {
                    clsAppInfo.Init(oEmpresaVM.DataSource, oEmpresaVM.InitialCatalog, SysData.ToStr(oLoginVM.UsuarioCod), oLoginVM.UsuarioPass);

                    if (clsAppInfo.OpenConection())
                    {
                        if (AppCode.AplicacionFind(clsAppInfo.AplicacionId))
                        {
                            if (AppCode.UsuarioCodFind(SysData.ToStr(oLoginVM.UsuarioCod)))
                            {
                                if (AppCode.AutorizaFind(clsAppInfo.TipoUsuarioId, clsAppInfo.AutorizaItemId, clsAppInfo.AplicacionId))
                                {
                                    Session["User"] = clsAppInfo.UsuarioCod;
                                    clsAppInfo.AppPath = Request.UrlReferrer.OriginalString;
                                    return RedirectToAction("Index", "Home");
                                }
                                else
                                {
                                    ViewBag.MessageErr = "Aplicación no Autorizada para el Usuario";
                                    ViewBag.EmpresaId = new SelectList(EmpresaList(), "EmpresaId", "EmpresaDes");
                                    return View(oLoginVM);
                                }
                            }
                            else
                            {
                                ViewBag.MessageErr = "Usuario no Registrado";
                                ViewBag.EmpresaId = new SelectList(EmpresaList(), "EmpresaId", "EmpresaDes");
                                return View(oLoginVM);
                            }
                        }
                        else
                        {
                            ViewBag.MessageErr = "Aplicación no Autorizada";
                            ViewBag.EmpresaId = new SelectList(EmpresaList(), "EmpresaId", "EmpresaDes");
                            return View(oLoginVM);
                        }
                    }
                }

                catch (Exception exp)
                {
                    ViewBag.MessageErr = Convert.ToString(exp.Message);
                    ViewBag.EmpresaId = new SelectList(EmpresaList(), "EmpresaId", "EmpresaDes");
                    return View(oLoginVM);
                }
            }

            ViewBag.EmpresaId = new SelectList(EmpresaList(), "EmpresaId", "EmpresaDes");
            return View(oLoginVM);
        }

        public ActionResult LogOut()
        {
            Session.Clear();

            return RedirectToAction("Index", "Login");
        }




        public List<clsEmpresaVM> EmpresaList()
        {
            List<clsEmpresaVM> oEmpresaVM = new List<clsEmpresaVM>();
            XmlDocument Xml = default(XmlDocument);
            XmlNodeList NodeList = default(XmlNodeList);
            XmlNode Node = default(XmlNode);

            try
            {
                Xml = new XmlDocument();
                Xml.Load(Server.MapPath("~/Models/Empresa.xml"));
                NodeList = Xml.SelectNodes("/EmpresaList/Empresa");

                foreach (XmlNode tempLoopVar_Node in NodeList)
                {
                    Node = tempLoopVar_Node;
                    oEmpresaVM.Add(new clsEmpresaVM()
                    {
                        EmpresaId = SysData.ToLong(Node.Attributes.GetNamedItem("EmpresaId").Value),
                        EmpresaDes = SysData.ToStr(Node.Attributes.GetNamedItem("EmpresaDes").Value),
                        DataSource = SysData.ToStr(Node.Attributes.GetNamedItem("DataSource").Value),
                        InitialCatalog = SysData.ToStr(Node.Attributes.GetNamedItem("InitialCatalog").Value)
                    });
                }
            }

            catch (Exception exp)
            {
                throw (exp);
            }

            return oEmpresaVM;
        }
    }
}