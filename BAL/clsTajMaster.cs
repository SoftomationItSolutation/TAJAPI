using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class clsTajMaster
    {
        #region Common Variable
        DBManager Sqldbmanager = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["TajApi"].ConnectionString);
        DataSet DS = new DataSet();
        DataTable dt = new DataTable();
        #endregion

        #region Common Method
        public DataSet LogError(string ModuleName, string ErrorSource, string Description)
        {
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(3);
                Sqldbmanager.AddParameters(0, "@ModuleName", ModuleName);
                Sqldbmanager.AddParameters(1, "@ErroSource", ErrorSource);
                Sqldbmanager.AddParameters(2, "@Description", Description);
                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "usp_LogError");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return DS;
        }

        public void ActivityLog(string UserId, string Action, string Menu, string Category, string Item, string AssociatedItem)
        {
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(6);
                Sqldbmanager.AddParameters(0, "@UserId", UserId);
                Sqldbmanager.AddParameters(1, "@Action", Action);
                Sqldbmanager.AddParameters(2, "@Menu", Menu);
                Sqldbmanager.AddParameters(3, "@Category", Category);
                Sqldbmanager.AddParameters(4, "@Item", Item);
                Sqldbmanager.AddParameters(5, "@AssociatedItem ", AssociatedItem);
                Sqldbmanager.ExecuteNonQuery(CommandType.StoredProcedure, "USP_ActivityLog");
            }
            catch (Exception Ex)
            {
                DS = LogError("ActivityLog", Ex.Message.ToString(), "SP Name: USP_ActivityLog");
            }
            finally
            {
                Sqldbmanager.Close();
            }
        }

        public DataSet CommonGetData(JsonMember.CommonData obj)
        {
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(2);
                Sqldbmanager.AddParameters(0, "@Flag", obj.Flag);
                Sqldbmanager.AddParameters(1, "@Id", obj.Id);
                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_CommonData");
            }
            catch (Exception Ex)
            {
                DS = LogError("CommonGetData", Ex.Message.ToString(), "SP Name: CommonGetData and Flag is : " + obj.Flag);
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return DS;
        }
        #endregion

        #region Login Master
        public DataTable LoginMaster(JsonMember.UserDetails obj)
        {
            DataTable Logindt = new DataTable();
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(1);
                Sqldbmanager.AddParameters(0, "@LoginId", obj.LoginId);
                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_LoginMaster");
                if (DS.Tables[1].Rows.Count > 0)
                {
                    if (DS.Tables[0].Rows[0]["UserPassword"].ToString() == obj.Password)
                    {

                        Logindt = DS.Tables[1];
                    }
                    else
                    {
                        Logindt = LogError("LoginMaster", "Invaid Password", obj.LoginId).Tables[0];
                    }
                }
                else
                {
                    Logindt = LogError("LoginMaster", "Invaid Login Id", obj.LoginId).Tables[0];
                }

            }
            catch (Exception Ex)
            {
                Logindt = LogError("LoginMaster", Ex.Message.ToString(), "").Tables[0];
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return Logindt;
        }
        public DataSet UserInsertUpdate(JsonMember.UserDetails obj)
        {
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(8);
                Sqldbmanager.AddParameters(0, "@AUserId", obj.AUserId);
                Sqldbmanager.AddParameters(1, "@UserId", obj.UserId);
                Sqldbmanager.AddParameters(2, "@LoginId", obj.LoginId);
                Sqldbmanager.AddParameters(3, "@Name", obj.Name);
                Sqldbmanager.AddParameters(4, "@Password", obj.Password);
                Sqldbmanager.AddParameters(5, "@EmailId", obj.EmailId);
                Sqldbmanager.AddParameters(6, "@ContactNo", obj.ContactNo);
                Sqldbmanager.AddParameters(7, "@DepartmentId", obj.DepartmentId);
                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_UserInserUpdate");
            }
            catch (Exception Ex)
            {
                DS = LogError("User Insert Update", Ex.Message.ToString(), "SP Name: USP_UserInserUpdate");
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return DS;
        }
        public DataTable UpdatePassword(JsonMember.UserDetails obj)
        {
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(2);
                Sqldbmanager.AddParameters(0, "@Flag", "getPassword");
                Sqldbmanager.AddParameters(1, "@Id", obj.UserId);
                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_CommonData");
                if (DS.Tables[0].Rows[0]["UserPassword"].ToString() == obj.Password)
                {
                    Sqldbmanager.CreateParameters(8);
                    Sqldbmanager.AddParameters(0, "@UserId", obj.UserId);
                    Sqldbmanager.AddParameters(1, "@NewPassword", obj.NewPassword);
                    DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_UpdatePassword");
                    dt = DS.Tables[0];
                }
                else
                    dt = DS.Tables[1];
            }
            catch (Exception Ex)
            {
                DS = LogError("Update Password", Ex.Message.ToString(), "SP Name: USP_UpdatePassword");
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return dt;
        }
        #endregion

        #region Menu and Header Details
        public DataSet MenuDetails(JsonMember.UserDetails obj)
        {
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(1);
                Sqldbmanager.AddParameters(0, "@UserId", obj.UserId);
                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_MeunDeatils");
            }
            catch (Exception Ex)
            {
                DS = LogError("Menu Details", Ex.Message.ToString(), "SP Name: USP_MeunDeatils");
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return DS;
        }
        public DataSet HeaderDetails(JsonMember.UserDetails obj)
        {
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(1);
                Sqldbmanager.AddParameters(0, "@UserId", obj.UserId);
                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_HeaderDetails");
            }
            catch (Exception Ex)
            {
                DS = LogError("Header Details", Ex.Message.ToString(), "SP Name: USP_HeaderDetails");
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return DS;
        }
        public DataSet MenuPermission(JsonMember.UserDetails obj)
        {
            try
            {
                StringBuilder InMenuPermission = new StringBuilder();
                for (int i = 0; i < obj.MenuPermission.Count; i++)
                {
                    InMenuPermission.AppendLine("<nodes>");
                    InMenuPermission.AppendLine("    <node>");
                    InMenuPermission.AppendLine("      <MenuId>" + obj.MenuPermission[i].MenuId + "</MenuId>");
                    InMenuPermission.AppendLine("      <AdminType>" + obj.MenuPermission[i].AdminType + "</AdminType>");
                    InMenuPermission.AppendLine("      <CounterType>" + obj.MenuPermission[i].CounterType + "</CounterType>");
                    InMenuPermission.AppendLine("    </node>");
                    InMenuPermission.AppendLine("</nodes>");
                }
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(2);
                Sqldbmanager.AddParameters(0, "@UserId", obj.UserId);
                Sqldbmanager.AddParameters(1, "@InMenuPermission", InMenuPermission.ToString());
                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_MeunPermission");
            }
            catch (Exception Ex)
            {
                DS = LogError("MenuPermission", Ex.Message.ToString(), "SP Name: USP_MeunPermission");
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return DS;
        }

        #endregion

        #region Sloat Management
        public DataSet SloatInsertUpdate(JsonMember.SloatManagement obj)
        {
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(10);
                Sqldbmanager.AddParameters(0, "@SloatId", obj.SloatId);
                Sqldbmanager.AddParameters(1, "@UserId", obj.UserId);
                Sqldbmanager.AddParameters(2, "@SloatSeat", obj.SloatSeat);
                Sqldbmanager.AddParameters(3, "@SloatStartTime", obj.SloatStartTime);
                Sqldbmanager.AddParameters(4, "@SloatEndTime", obj.SloatEndTime);
                Sqldbmanager.AddParameters(5, "@SloatAmount_Indian_Adult", obj.SloatAmount_Indian_Adult);
                Sqldbmanager.AddParameters(6, "@SloatAmount_Indian_Child", obj.SloatAmount_Indian_Child);
                Sqldbmanager.AddParameters(7, "@SloatAmount_Foreigner_Adult", obj.SloatAmount_Foreigner_Adult);
                Sqldbmanager.AddParameters(8, "@SloatAmount_Foreigner_Child", obj.SloatAmount_Foreigner_Child);
                Sqldbmanager.AddParameters(9, "@CancelCharges", obj.CancelCharges);

                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_SloatInserUpdate");
            }
            catch (Exception Ex)
            {
                DS = LogError("Sloat Insert Update", Ex.Message.ToString(), "SP Name: USP_SloatInserUpdate");
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return DS;
        }

        #endregion

        #region Country Management
        public DataSet CountryMasterInserUpdate(JsonMember.CountryManagement obj)
        {
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(4);
                Sqldbmanager.AddParameters(0, "@CountryId", obj.CountryId);
                Sqldbmanager.AddParameters(1, "@UserId", obj.UserId);
                Sqldbmanager.AddParameters(2, "@CountryName", obj.CountryName);
                Sqldbmanager.AddParameters(3, "@CountryCode", obj.CountryCode);
                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_CountryMasterInserUpdate");
            }
            catch (Exception Ex)
            {
                DS = LogError("Country Master InserUpdate", Ex.Message.ToString(), "SP Name: USP_CountryMasterInserUpdate");
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return DS;
        }

        #endregion

        #region Vegiter Management
        public DataSet GetTicketSerialNo(JsonMember.VisiterManagement obj)
        {
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(2);
                Sqldbmanager.AddParameters(0, "@VisitDate", obj.VisitDate);
                Sqldbmanager.AddParameters(1, "@SloatId", obj.SloatId);
                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_GetTicketSerialNo");
            }
            catch (Exception Ex)
            {
                DS = LogError("GetTicketSerialNo", Ex.Message.ToString(), "SP Name: USP_GetTicketSerialNo");
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return DS;
        }

        public DataSet GenerateTicket(JsonMember.VisiterManagement obj)
        {
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(12);
                Sqldbmanager.AddParameters(0, "@VisiterName", obj.VisiterName);
                Sqldbmanager.AddParameters(1, "@GenderId", obj.GenderId);
                Sqldbmanager.AddParameters(2, "@VisiterAge", obj.VisiterAge);
                Sqldbmanager.AddParameters(3, "@VisitDate", obj.VisitDate);
                Sqldbmanager.AddParameters(4, "@VisiterPassportNo", obj.VisiterPassportNo);
                Sqldbmanager.AddParameters(5, "@VisiterAddress", obj.VisiterAddress);
                Sqldbmanager.AddParameters(6, "@CountryId", obj.CountryId);
                Sqldbmanager.AddParameters(7, "@CurrencyId", obj.CurrencyId);
                Sqldbmanager.AddParameters(8, "@TicketAmount", obj.TicketAmount);
                Sqldbmanager.AddParameters(9, "@TicketCount", obj.TicketCount);
                Sqldbmanager.AddParameters(10, "@SloatId", obj.SloatId);
                Sqldbmanager.AddParameters(11, "@UserId", obj.UserId);
                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_GenerateTicket");
            }
            catch (Exception Ex)
            {
                DS = LogError("GenerateTicket", Ex.Message.ToString(), "SP Name: USP_GetTicketSerialNo");
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return DS;
        }


        public DataSet ModifyTicket(JsonMember.VisiterManagement obj)
        {
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(13);
                Sqldbmanager.AddParameters(0, "@VisiterId", obj.VisiterId);
                Sqldbmanager.AddParameters(1, "@VisiterName", obj.VisiterName);
                Sqldbmanager.AddParameters(2, "@GenderId", obj.GenderId);
                Sqldbmanager.AddParameters(3, "@VisiterAge", obj.VisiterAge);
                Sqldbmanager.AddParameters(4, "@VisiterPassportNo", obj.VisiterPassportNo);
                Sqldbmanager.AddParameters(5, "@VisiterAddress", obj.VisiterAddress);
                Sqldbmanager.AddParameters(6, "@CountryId", obj.CountryId);
                Sqldbmanager.AddParameters(7, "@CurrencyId", obj.CurrencyId);
                Sqldbmanager.AddParameters(8, "@TicketAmount", obj.TicketAmount);
                Sqldbmanager.AddParameters(9, "@TicketCount", obj.TicketCount);
                Sqldbmanager.AddParameters(10, "@SloatId", obj.SloatId);
                Sqldbmanager.AddParameters(11, "@UserId", obj.UserId);
                Sqldbmanager.AddParameters(12, "@Remark", obj.Remark);
                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_ModifyTicket");
            }
            catch (Exception Ex)
            {
                DS = LogError("Modify Ticket", Ex.Message.ToString(), "SP Name: USP_ModifyTicket");
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return DS;
        }

        public DataSet CancelTicket(JsonMember.VisiterManagement obj)
        {
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(5);
                Sqldbmanager.AddParameters(0, "@VisiterId", obj.VisiterId);
                Sqldbmanager.AddParameters(1, "@CancelCharge", obj.TicketAmount);
                Sqldbmanager.AddParameters(2, "@ReturnCount", obj.TicketCount);
                Sqldbmanager.AddParameters(3, "@UserId", obj.UserId);
                Sqldbmanager.AddParameters(4, "@CancelReason", obj.Remark);
                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_CancelTicket");


            }
            catch (Exception Ex)
            {
                DS = LogError("Cancel Ticket", Ex.Message.ToString(), "SP Name: USP_CancelTicket");
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return DS;
        }

        public DataSet GetReportData(JsonMember.ReportData obj)
        {
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(5);
                Sqldbmanager.AddParameters(0, "@ReportTypeId", obj.id);
                Sqldbmanager.AddParameters(1, "@FromDate", obj.FromDate);
                Sqldbmanager.AddParameters(2, "@ToDate", obj.ToDate);
                Sqldbmanager.AddParameters(3, "@FromSlot", obj.FromSlot);
                Sqldbmanager.AddParameters(4, "@ToSlot", obj.ToSlot);
                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_DailyReport");
            }
            catch (Exception Ex)
            {
                DS = LogError("Get Report Data", Ex.Message.ToString(), "SP Name: USP_DailyReport");
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return DS;
        }

        public DataSet GetSlotByEntryData(JsonMember.VisiterManagement obj)
        {
            try
            {
                Sqldbmanager.Open();
                Sqldbmanager.CreateParameters(1);
                Sqldbmanager.AddParameters(0, "@VisitDate", obj.VisitDate);
                DS = Sqldbmanager.ExecuteDataSet(CommandType.StoredProcedure, "USP_ManageSlotByEntryDate");
            }
            catch (Exception Ex)
            {
                DS = LogError("Get SlotBy Entry Data", Ex.Message.ToString(), "SP Name: USP_ManageSlotByEntryDate");
            }
            finally
            {
                Sqldbmanager.Close();
            }
            return DS;
        }

        #endregion
    }
}
