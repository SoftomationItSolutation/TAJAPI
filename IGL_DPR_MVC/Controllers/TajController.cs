using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.Http.Cors;
using BAL;
using System.Web;
using Newtonsoft.Json;
using System.Data;
using IGL_DPR_MVC.Models;
using System.Threading.Tasks;
using System.IO;

namespace IGL_DPR_MVC.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TAJController : ApiController
    {

        #region Common declarations
        clsTajMaster objTaj = new clsTajMaster();
        DataTable dt = new DataTable();
        #endregion

        #region Get Test
        [HttpGet]
        public bool Test()
        {
            bool RetType = true;


            return RetType;
        }
        #endregion

        #region Common Data
        [HttpPost]
        public object CommonGetData(JsonMember.CommonData obj)
        {
            string Det = JsonConvert.SerializeObject(objTaj.CommonGetData(obj), Formatting.Indented);
            return Det.Replace("\r", "").Replace("\n", "");
        }
        #endregion

        #region Login Master
        [HttpPost]
        public object ValidateUser(JsonMember.UserDetails obj)
        {
            dt = objTaj.LoginMaster(obj);
            string Det = JsonConvert.SerializeObject(dt, Formatting.Indented);
            return Det.Replace("\r", "").Replace("\n", "");
        }
        [HttpPost]
        public object UserInsertUpdate(JsonMember.UserDetails obj)
        {
            string Det = JsonConvert.SerializeObject(objTaj.UserInsertUpdate(obj), Formatting.Indented);
            return Det.Replace("\r", "").Replace("\n", "");
        }
        [HttpPost]
        public object UpdatePassword(JsonMember.UserDetails obj)
        {
            string Det = JsonConvert.SerializeObject(objTaj.UpdatePassword(obj), Formatting.Indented);
            return Det.Replace("\r", "").Replace("\n", "");
        }
        #endregion

        #region Menu & Header Details
        [HttpPost]
        public object GetMenuDetails(JsonMember.UserDetails obj)
        {
            string Det = JsonConvert.SerializeObject(objTaj.MenuDetails(obj), Formatting.Indented);
            return Det.Replace("\r", "").Replace("\n", "");
        }
        [HttpPost]
        public object GetHeaderDetails(JsonMember.UserDetails obj)
        {
            string Det = JsonConvert.SerializeObject(objTaj.HeaderDetails(obj), Formatting.Indented);
            return Det.Replace("\r", "").Replace("\n", "");
        }

        [HttpPost]
        public object UpdatePermission(JsonMember.UserDetails obj)
        {
            string Det = JsonConvert.SerializeObject(objTaj.MenuPermission(obj), Formatting.Indented);
            return Det.Replace("\r", "").Replace("\n", "");
        }
        #endregion

        #region Sloat Management
        [HttpPost]
        public object SloatInsertUpdate(JsonMember.SloatManagement obj)
        {
            string Det = JsonConvert.SerializeObject(objTaj.SloatInsertUpdate(obj), Formatting.Indented);
            return Det.Replace("\r", "").Replace("\n", "");
        }
        #endregion

        #region Country Management
        [HttpPost]
        public object CountryMasterInsertUpdate(JsonMember.CountryManagement obj)
        {
            string Det = JsonConvert.SerializeObject(objTaj.CountryMasterInserUpdate(obj), Formatting.Indented);
            return Det.Replace("\r", "").Replace("\n", "");
        }
        #endregion

        #region Vegiter Management
        [HttpPost]
        public object GetTicketSerialNo(JsonMember.VisiterManagement obj)
        {
            string Det = JsonConvert.SerializeObject(objTaj.GetTicketSerialNo(obj), Formatting.Indented);
            return Det.Replace("\r", "").Replace("\n", "");
        }
        [HttpPost]
        public object GenerateTicket(JsonMember.VisiterManagement obj)
        {
            string Det = JsonConvert.SerializeObject(objTaj.GenerateTicket(obj), Formatting.Indented);
            return Det.Replace("\r", "").Replace("\n", "");
        }

        [HttpPost]
        public object ModifyTicket(JsonMember.VisiterManagement obj)
        {
            string Det = JsonConvert.SerializeObject(objTaj.ModifyTicket(obj), Formatting.Indented);
            return Det.Replace("\r", "").Replace("\n", "");
        }
        [HttpPost]
        public object CancelTicket(JsonMember.VisiterManagement obj)
        {
            string Det = JsonConvert.SerializeObject(objTaj.CancelTicket(obj), Formatting.Indented);
            return Det.Replace("\r", "").Replace("\n", "");
        }
        [HttpPost]
        public object GetReportData(JsonMember.ReportData obj)
        {
            string Det = JsonConvert.SerializeObject(objTaj.GetReportData(obj), Formatting.Indented);
            return Det.Replace("\r", "").Replace("\n", "");
        }

        [HttpPost]
        public object GetSlotByEntryData(JsonMember.VisiterManagement obj)
        {
            string Det = JsonConvert.SerializeObject(objTaj.GetSlotByEntryData(obj), Formatting.Indented);
            return Det.Replace("\r", "").Replace("\n", "");
        }
        #endregion

    }
}
