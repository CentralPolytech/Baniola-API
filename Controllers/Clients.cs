using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using InterventionAPI.Models;

namespace InterventionAPI.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class Clients : ControllerBase
    {
        private readonly Learn_DBContext context;
        public Clients(Learn_DBContext learn_DB)
        {
            context = learn_DB;
        }
        // GET: api/<ClientController>
        [HttpGet]
        public IEnumerable<TblClient> Get()
        {
            return context.TblClient.ToList();
        }
        [HttpGet("{CIN}")]
        public TblClient Get(int CIN)
        {
            return context.TblClient.FirstOrDefault(o => o.Code == CIN);
        }

        [HttpPost]
        public APIResponse Post([FromBody] TblClient value)
        {
            string result = string.Empty;
            try
            {
                var client = context.TblClient.FirstOrDefault(o => o.Code == value.Code);
                if (client != null)
                {
                    client.NomPrenom = value.NomPrenom;
                    client.Email = value.Email;
                    client.CIN = value.CIN;
                    client.Phone = value.Phone;
                    context.SaveChanges();
                    result= "pass";
                }
                else
                {
                    TblClient tblClient = new TblClient()
                    {
                        CIN = value.CIN,
                        Email = value.Email,
                        Phone = value.Phone,
                        NomPrenom = value.NomPrenom
                    };
                    context.TblClient.Add(tblClient);
                    context.SaveChanges();
                    result= "pass";
                }
                
            }
            catch(Exception)
            {
                result= string.Empty;
            }
            return new APIResponse { keycode = string.Empty, result = result };
        }

        [HttpDelete("{CIN}")]
        public APIResponse Delete(int CIN)
        {
            string result = string.Empty;
            var Client=context.TblClient.FirstOrDefault(o => o.Code == CIN);
            if (Client != null)
            {
                context.TblClient.Remove(Client);
                context.SaveChanges();
                result= "pass";
            }
            return new APIResponse { keycode = string.Empty, result = result };
        }
    }
}
