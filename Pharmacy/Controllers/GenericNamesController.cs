using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Razor;
using System.Web.UI.WebControls;
using Pharmacy.Models;

namespace Pharmacy.Controllers
{
    public class GenericNamesController : Controller
    {
        public ActionResult Index()
        {
            var genericNameList = new Dal.GenericName().ListOfGenericNames();

            return View(genericNameList);
        }

        [HttpPost]
        public ActionResult Index(GenericName genericName)
        {
            var query = "INSERT INTO tbl_generic_name (name, create_by) VALUES('" + genericName.Name + "','" + genericName.Create_by + "') ";

            //var query="SELECT * FROM TBL_GENERIC_NAME";
            var isSaved=new Dal.GenericName().QueryExecute(query);

            var genericNameList = new Dal.GenericName().ListOfGenericNames();
            return View(genericNameList);
        }

        public void Update(GenericName genericName)
        {

        }

        


    }
}