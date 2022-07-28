using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Razor;
using System.Web.UI.WebControls;
using Pharmacy.Common;
using Pharmacy.Models;

namespace Pharmacy.Controllers
{
    public class GenericNamesController : Controller
    {
        private int _currPage = 1;

        public List<GenericName> PaginationList(List<GenericName> dataList, int currentPage)
        {

            if (currentPage < 1)
            {
                currentPage = 1;
            }

            int pageSize = 10;

            _currPage = currentPage;

            int countGeneric = dataList.Count;
            var pager = new Pager(countGeneric, currentPage, pageSize);
            int recSkip = (currentPage + -1) * pageSize;
            var data = dataList.Skip(recSkip).Take(pager.PageSize).ToList();

            ViewBag.Pager = pager;

            int slNumber = ((pageSize * currentPage) - pageSize) + 1;

            ViewBag.slNum = slNumber;

            return data;
        }

        public ActionResult Index(int pg=1)
        {
            var genericNameList = new Dal.GenericName().ListOfGenericNames();

            var dataList = PaginationList(genericNameList, pg);

            return View(dataList);
        }

        [HttpPost]
        public ActionResult Index(GenericName genericName)
        {
            var query = "INSERT INTO tbl_generic_name (name, create_by) VALUES('" + genericName.Name + "','" + genericName.Create_by + "') ";

            if (ModelState.IsValid)
            {
                var isExist = new Dal.GenericName().HasExist(genericName);

                if (!isExist)
                {
                    var isSaved=new Dal.GenericName().QueryExecute(query);
                    if (isSaved)
                    {
                        ViewBag.SMsg = "Inserted";
                    }
                    else
                    {
                        ViewBag.FMsg = "Failed";
                    }
                }
                else
                {
                    ViewBag.FMsg = "Failed. This name already exist.";
                }

            }

            var genericNameList = new Dal.GenericName().ListOfGenericNames();

            var dataList = PaginationList(genericNameList, _currPage);

            return View(dataList);
        }

        [HttpPost]
        public ActionResult Update(GenericName genericName)
        {
            return View("Index");
        }

    }
}