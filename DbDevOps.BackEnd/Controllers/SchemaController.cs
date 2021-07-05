using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbDevOps.BackEnd.XmlModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DbDevOps.BackEnd.Controllers {
    public class GlobalSchema {
        private static DBSchema schema = DBSchema.LoadDBSchema("/Users/adalbertocajueiro/Projects/DbDevOps.BackEnd/DbDevOps.BackEnd/Resources/DATABASE_STRUCTURE_2.0.xml");
        public static DBSchemaNode GetSchema() {
            return schema.Schema;
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class SchemaController : ControllerBase {

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index() {
            //return View();
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(GlobalSchema.GetSchema());
            return Ok(jsonString);
        }
    }
}
