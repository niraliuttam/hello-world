using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Summary description for AutoCompleteText
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class AutoCompleteText : System.Web.Services.WebService
{

    public AutoCompleteText()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public string[] GetEmpName(string prefixText)
    {
        string sql = "Select DISTINCT EmpName from tblEmployee_3103 Where IsActive = 1 and IsDeleted = 0 and EmpName like @prefixText";

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSSConnectionString"].ConnectionString);

        SqlDataAdapter da = new SqlDataAdapter(sql, con);

        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = "%" + prefixText + "%";

        DataTable dt = new DataTable();

        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];

        int i = 0;

        //foreach (DataRow dr in dt.Rows)
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["EmpName"].ToString(), i);
            i++;
        }
        return items;

    }
    //[WebMethod]
    //public string[] GetCateNameInfo(string prefixText)
    //{
    //    SqlConnection con = new SqlConnection();
    //    con.ConnectionString = ConfigurationManager.ConnectionStrings["Dial24HoursConnectionString"].ConnectionString;
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.CommandText = "Select * from tblCategory Where CategoryName like @prefixText"; 
    //    cmd.Parameters.AddWithValue("@SearchText", prefixText);
    //    cmd.Connection = con;
    //    con.Open();
    //    List<string> CategoryName1 = new List<string>();
    //    SqlDataReader sdr = cmd.ExecuteReader();
    //    while (sdr.Read())
    //    {
    //        string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sdr["CategoryName"].ToString(), sdr["CategoryId"].ToString());
    //        CategoryName1.Add(item);
    //    }
    //    con.Close();
    //    return CategoryName1;
    //}
}