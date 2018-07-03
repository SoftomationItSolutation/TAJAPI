using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using System.Configuration;

namespace BAL
{
    public class JsonMember
    {
        public class CommonData
        {
            public Int64 Id { get; set; }
            public string Flag { get; set; }
        }

        public class UserDetails
        {
            public Int64 AUserId { get; set; }
            public Int64 UserId { get; set; }
            public Int64 DepartmentId { get; set; }
            public string LoginId { get; set; }
            public string Password { get; set; }
            public string EmailId { get; set; }
            public string ContactNo { get; set; }
            public string Name { get; set; }
            public string NewPassword { get; set; }
            public List<MenuPermission> MenuPermission { get; set; }
        }

        public class MenuPermission
        {
            public Int64 MenuId { get; set; }
            public Int64 AdminType { get; set; }
            public Int64 CounterType { get; set; }
        }

        public class ReportData
        {
            public Int64 id { get; set; }
            public Int64 FromSlot { get; set; }
            public Int64 ToSlot { get; set; }
            public string ReportName { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
        }
        public class SloatManagement
        {
            public Int64 UserId { get; set; }
            public Int64 SloatId { get; set; }
            public Int64 CancelCharges { get; set; }
            public string SloatName { get; set; }
            public string SloatCode { get; set; }
            public string SloatStartTime { get; set; }
            public string SloatEndTime { get; set; }
            public Decimal SloatSeat { get; set; }
            public Decimal SloatAmount_Indian_Adult { get; set; }
            public Decimal SloatAmount_Indian_Child { get; set; }
            public Decimal SloatAmount_Foreigner_Adult { get; set; }
            public Decimal SloatAmount_Foreigner_Child { get; set; }
        }

        public class CountryManagement
        {
            public Int64 UserId { get; set; }
            public Int64 CountryId { get; set; }
            public string CountryName { get; set; }
            public string CountryCode { get; set; }
        }

        public class VisiterManagement
        {
            public Int64 UserId { get; set; }
            public Int64 SloatId { get; set; }
            public Int64 TicketCount { get; set; }
            public Int64 GenderId { get; set; }
            public Int64 CountryId { get; set; }
            public Int64 CurrencyId { get; set; }
            public Int64 VisiterAge { get; set; }
            public Int64 VisiterId { get; set; }
            public Int64 SerialNo { get; set; }
            public string VisiterName { get; set; }
            public string VisitDate { get; set; }
            public string VisiterPassportNo { get; set; }
            public string VisiterAddress { get; set; }
            public string BookingDate { get; set; }
            public string Remark { get; set; }
            public string CancelReason { get; set; }
            public Decimal TicketAmount { get; set; }
            public Decimal TotalAmount { get; set; }
        }
    }
}