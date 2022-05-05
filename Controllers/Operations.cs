using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using InterventionAPI.Models;

namespace InterventionAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]

    public class Operations : ControllerBase
    {
        private readonly Learn_DBContext context;
        public Operations(Learn_DBContext learn_DB)
        {
            context = learn_DB;
        }
        // GET: api/<OperationsController>
        [HttpGet]
        public IEnumerable<TblOperations> Get()
        {
            return (IEnumerable<TblOperations>)context.TblOperations.ToList();
        }

        [HttpPost]
        public APIResponse Post([FromBody] TblOperations value)
        {
            string result = string.Empty;
            try
            {
                var Operation = context.TblOperations.FirstOrDefault(o => o.Code == value.Code);
                if (Operation != null)
                {
                    Operation.Name = value.Name;
                    context.SaveChanges();
                    result = "pass";
                }
                else
                {
                    TblOperations tblOperations = new TblOperations()
                    {
                        Name = value.Name,
                        Code = value.Code
                    };
                    TblOperations Operations = tblOperations;
                    context.TblOperations.Add(Operation);
                    context.SaveChanges();
                    result = "pass";
                }

            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return new APIResponse { keycode = string.Empty, result = result };
        }
    }
}
