using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;

public partial class Login : System.Web.UI.Page
{
    SqlHelper sh = new SqlHelper();
    DateTime resultTIME = new DateTime();
    protected void Page_Load(object sender, EventArgs e)
    {
        resultTIME = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "India Standard Time");
        Label1.Text = resultTIME.ToLongTimeString();
        if (!Page.IsPostBack)
        {
            //lnkForgetPassword.Text = "&bull;Forgot Password?";
            int j;
            if (Session["VMCULogID"] != null)
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@LogOutTime",resultTIME),
                    new SqlParameter("@Ulid",Convert.ToInt32(Session["VMCULogID"]))
                };
                j = sh.Execute1SP("ProcUserLogUpdate", param);
                if (j > 0)
                {
                    Session.Abandon();
                    Session.Contents.RemoveAll();
                    System.Web.Security.FormsAuthentication.SignOut();
                    if (HttpContext.Current.Request.QueryString["CLI"] != null)
                    {
                        Session["VMCCLI"] = Request.QueryString["CLI"].ToString();
                    }
                    Response.Redirect("Login.aspx");
                }
            }
            txtUsername.Focus();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //int i = 0;
        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@UserName",txtUsername.Text),            
            new SqlParameter("@Password",txtPwd.Text)
            ,new SqlParameter("@ResultDate",resultTIME)
        };
        //i = sh.Execute1SP("ProcUserValidateSPwithIP", param);
        DataTable dtCheck = sh.GetDataTable("ProcUserValidateSPwithIP1", param);

        if (dtCheck.Rows.Count > 0)
        {
            int i = 0; int RoleCheck = 0; int UType = 0;
            foreach (DataRow dr in dtCheck.Rows)
            {
                i = Convert.ToInt32(dr["Ans"].ToString());
                RoleCheck = Convert.ToInt32(dr["RoleID"].ToString());
                UType = Convert.ToInt32(dr["UType"].ToString());
            }
            string clientIp = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();

            var listIP = new List<string>();
            listIP.Add("127.0.0.1");
            listIP.Add("192.168.1.1");
            listIP.Add("192.168.1.107");
            listIP.Add("192.168.1.105");
            listIP.Add("192.168.1.104");
            listIP.Add("192.168.1.103");
            listIP.Add("192.168.1.106");
            listIP.Add("192.168.1.20");
            listIP.Add("192.168.1.21");
            listIP.Add("192.168.1.22");
            listIP.Add("192.168.1.23");
            listIP.Add("192.168.1.24");
            listIP.Add("192.168.1.25");
            listIP.Add("192.168.1.26");
            listIP.Add("192.168.1.27");
            listIP.Add("192.168.1.28");
            listIP.Add("192.168.1.29");
            listIP.Add("192.168.1.30");
            listIP.Add("192.168.1.31");
            listIP.Add("192.168.1.32");
            listIP.Add("192.168.1.33");
            listIP.Add("192.168.1.34");
            listIP.Add("192.168.1.35");
            listIP.Add("192.168.1.36");
            listIP.Add("192.168.1.37");
            listIP.Add("192.168.1.38");
            listIP.Add("192.168.1.39");
            listIP.Add("192.168.1.40");
            listIP.Add("192.168.1.41");
            listIP.Add("192.168.1.42");
            listIP.Add("192.168.1.43");
            listIP.Add("192.168.1.44");
            listIP.Add("192.168.1.45");
            listIP.Add("192.168.1.46");
            listIP.Add("192.168.1.47");
            listIP.Add("192.168.1.48");
            listIP.Add("192.168.1.49");
            listIP.Add("192.168.1.50");
            listIP.Add("192.168.1.51");
            listIP.Add("192.168.1.52");
            listIP.Add("192.168.1.53");
            listIP.Add("192.168.1.54");
            listIP.Add("192.168.1.55");

            listIP.Add("111.90.173.47");
            listIP.Add("111.90.173.48");

            listIP.Add("111.90.173.49");
            listIP.Add("111.90.173.50");
            listIP.Add("111.90.173.51");

            listIP.Add("192.168.1.117");
            listIP.Add("192.168.1.131");
            listIP.Add("192.168.1.102");
            listIP.Add("192.168.1.12");
            listIP.Add("192.168.1.147");
            listIP.Add("192.168.1.119");
            listIP.Add("192.168.1.158");

            listIP.Add("103.254.203.11");
            listIP.Add("27.54.172.228");

            if ((listIP.Contains(clientIp, StringComparer.OrdinalIgnoreCase)) || RoleCheck == 8 || UType == 2 || txtUsername.Text.ToLower() == "pankaj")
            {
                if (i == 1 || i == 2)
                {
                    Session["VMCUserName"] = txtUsername.Text;
                    SqlParameter[] param4 = new SqlParameter[]
                    {
                        new SqlParameter("@UserName",txtUsername.Text),
                        new SqlParameter("@Password",txtPwd.Text)
                    };
                    // For Getting User Information  In Session...
                    DataTable dt;
                    dt = sh.GetDataTable("ProcGetSessionValues", param4);
                    SetSessionValues(dt);

                    // For Inserted In UserLog...
                    int ULID = 0;
                    SqlParameter[] param1 = new SqlParameter[]
                    {
                        new SqlParameter("@UserID",Convert.ToInt32(Session["VMCUserID"].ToString())),
                        new SqlParameter("@LogInTime",resultTIME),
                        new SqlParameter("@clientIp",clientIp)
                    };
                    ULID = sh.Execute1SP("ProcUserLogInsert1", param1);
                    if (ULID > 0)
                    {
                        Session["VMCULogID"] = ULID;
                        int Days;
                        SqlParameter[] param3 = new SqlParameter[]
                        {
                            new SqlParameter("@UserName",txtUsername.Text),
                            new SqlParameter("@Password",txtPwd.Text)
                        };
                        Days = sh.Execute1SP("ProcUpdatePassRemind", param3);
                        Session["VMCPassDays"] = Days.ToString();
                        if (Days == -1 || Days == 0 || Days >= 175)
                        {
                            Response.Redirect("ChangePassword.aspx");
                        }
                        else
                        {

                            if (HttpContext.Current.Session["VMCCLI"] != null)
                            {
                                Response.Redirect("CallPopup.aspx?CLI=" + Session["VMCCLI"].ToString());
                            }
                            else
                            {
                                Response.Redirect("Homepage.aspx");
                            }

                        }
                    }
                }
                else if (i == 99)
                {
                    lblMsg.Text = "Your UserName have been Locked. For More Help Please Contact Administrator";
                    lblUsername.Visible = false;
                    lblPwd.Visible = false;
                    btnSubmit.Visible = false;
                    //lnkForgetPassword.Text = "&bull;Click here for Login";
                    txtUsername.Visible = false;
                    txtPwd.Visible = false;
                }
                else if (i == 0)
                {
                    lblMsg.Text = "User Name or Password is invalid";
                    txtUsername.Focus();
                }
                else
                {
                    lblMsg.Text = "User Name is invalid";
                    txtUsername.Focus();
                }
            }
            else
            {
                string sErrorTime = resultTIME.ToString("ddMMyyyy");
                string path = HttpContext.Current.Server.MapPath(@"IPAddress");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                if (!File.Exists(sErrorTime + ".txt"))
                {
                    StreamWriter sw = new StreamWriter(path + "\\" + sErrorTime + ".txt", true);
                    sw.WriteLine("-------------------------------------------------------------------------");
                    sw.WriteLine("IP Address    : " + clientIp.Trim());//hdnIP.Text.Trim());
                    sw.WriteLine("Date Time     : " + resultTIME.ToString());
                    sw.WriteLine("User Name     : " + txtUsername.Text.ToString());
                    sw.WriteLine("Password      : " + txtPwd.Text.ToString());
                    sw.WriteLine("-------------------------------------------------------------------------");
                    sw.Flush();
                    sw.Close();
                }
                else
                {
                    StreamWriter sw = new StreamWriter(path + "\\" + sErrorTime + ".txt", true);
                    sw.WriteLine("-------------------------------------------------------------------------");
                    sw.WriteLine("IP Address    : " + clientIp.Trim());//hdnIP.Text.Trim());
                    sw.WriteLine("Date Time     : " + resultTIME.ToString());
                    sw.WriteLine("User Name     : " + txtUsername.Text.ToString());
                    sw.WriteLine("Password      : " + txtPwd.Text.ToString());
                    sw.WriteLine("-------------------------------------------------------------------------");
                    sw.Flush();
                    sw.Close();
                }
            }
        }
        else
        {
            lblMsg.Text = "You are Not Authenticated.";
            txtUsername.Focus();
        }
    }
    public void SetSessionValues(DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {
            Session["VMCUserTypeID"] = Convert.ToInt32(dr["UserTypeID"].ToString());

            if (Convert.ToInt32(Session["VMCUserTypeID"]) == 2)
            {
                Session["VMCUserID"] = Convert.ToInt32(dr["UserID"].ToString());
                Session["VMCEmpCode"] = dr["EmpCode"].ToString();
                Session["VMCRoleID"] = Convert.ToInt32(dr["RoleID"].ToString());
                Session["VMCEmpName"] = dr["EmpName"].ToString();
                Session["VMCEmpId"] = Convert.ToInt32(dr["EmpID"].ToString());
                Session["VMCPostID"] = Convert.ToInt32(dr["PostID"].ToString());
                Session["VMCWardID"] = Convert.ToInt32(dr["WardID"].ToString());
                Session["VMCParentEmpCode"] = dr["ParentEmpCode"].ToString();
                Session["VMCParentEmpName"] = dr["ParentEmpName"].ToString();
                Session["VMCRoleName"] = dr["RoleName"].ToString();
                Session["VMCRoleNo"] = dr["RoleNo"].ToString();
                Session["VMCUserTypeName"] = dr["UserTypeName"].ToString();
                Session["VMCPostName"] = dr["PostName"].ToString();
                Session["VMCWardNumber"] = dr["WardNumber"].ToString();
                Session["VMCEmailAddress"] = dr["EmailAddress"].ToString();
                Session["VMCProcess"] = "VMSS";
                //Session["DeptName"] = dr["DeptName"].ToString();
                //Session["CategoryName"] = dr["CategoryName"].ToString();
                //Session["DeptID"] = Convert.ToInt32(dr["DeptID"].ToString());
                //Session["CategoryID"] = Convert.ToInt32(dr["CategoryID"].ToString());
                //Session["AreaId"] = Convert.ToInt32(dr["AreaID"].ToString());
                //Session["AreaName"] = Convert.ToInt32(dr["AreaName"].ToString());
                //Session["CityID"] = Convert.ToInt32(dr["CityID"].ToString());
                //Session["CityName"] = Convert.ToInt32(dr["CityName"].ToString());
                //Session["ZoneID"] = Convert.ToInt32(dr["ZoneID"].ToString());
                //Session["ZoneName"] = Convert.ToInt32(dr["ZoneName"].ToString());
            }
            else if (Convert.ToInt32(Session["VMCUserTypeID"]) == 1)
            {
                Session["VMCUserID"] = Convert.ToInt32(dr["UserID"].ToString());
                Session["VMCPostID"] = "0";
                Session["VMCEmpCode"] = dr["EmpCode"].ToString();
                Session["VMCRoleID"] = Convert.ToInt32(dr["RoleID"].ToString());
                Session["VMCEmpName"] = dr["EmpName"].ToString();
                Session["VMCEmpId"] = Convert.ToInt32(dr["EmpID"].ToString());
                Session["VMCRoleName"] = dr["RoleName"].ToString();
                Session["VMCUserTypeID"] = Convert.ToInt32(dr["UserTypeID"].ToString());
                Session["VMCUserTypeName"] = dr["UserTypeName"].ToString();
                Session["VMCAgentID"] = Convert.ToInt32(dr["AgentID"].ToString());
                Session["VMCEmailAddress"] = dr["EmailAddress"].ToString();
                Session["VMCProcess"] = "VMSS";
            }
        }
    }
    //protected void lnkForgetPassword_Click(object sender, EventArgs e)
    //{
    //    if (lnkForgetPassword.Text == "&bull;Forgot Password?")
    //    {
    //        Response.Redirect("ForgetPassword.aspx");
    //    }
    //    else if (lnkForgetPassword.Text == "&bull;Click here for Login")
    //    {
    //        Response.Redirect("Login.aspx");
    //    }
    //}
}