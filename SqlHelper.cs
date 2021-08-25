using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SqlHelper
/// </summary>
public class SqlHelper
{
    public SqlHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public SqlConnection GetConnection()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSSConnectionString"].ToString());
        return con;
    }

    public DataTable GetDataTable(string Sp, SqlParameter[] param)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlConnection con = GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(Sp, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            for (int i = 0; i < param.Length; i++)
            {
                cmd.Parameters.Add(param[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
        }
        catch (Exception ex)
        {
           
        }
        return dt;
    }

    public DataTable GetDataTable(string Sp, SqlParameter param)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlConnection con = GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(Sp, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
                cmd.Parameters.Add(param);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public int Execute1SP(string Sp, SqlParameter[] param)
    {
        int result = 0;
        SqlParameter output = new SqlParameter("@OUTPUT", 0);
        output.Direction = ParameterDirection.Output;
        try
        {
            SqlConnection con = GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(Sp, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            for (int i = 0; i < param.Length; i++)
            {
                cmd.Parameters.Add(param[i]);
            };
            cmd.Parameters.Add(output);
            cmd.ExecuteNonQuery();
            result = Convert.ToInt32(cmd.Parameters["@OUTPUT"].Value);
            con.Close();
        }
        catch (Exception ex)
        {

        }
        return result;
    }
    public string Execute2SP(string Sp, SqlParameter[] param)
    {
        string result = "";
        SqlParameter output = new SqlParameter("@OUTPUT",SqlDbType.VarChar,50);
        output.Direction = ParameterDirection.Output;
        try
        {
            SqlConnection con = GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(Sp, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            for (int i = 0; i < param.Length; i++)
            {
                cmd.Parameters.Add(param[i]);
            };
            cmd.Parameters.Add(output);
            cmd.ExecuteNonQuery();
            result = Convert.ToString(cmd.Parameters["@OUTPUT"].Value);
            con.Close();
        }
        catch (Exception ex)
        {

        }
        return result;
    }
    public Boolean ExecuteSP(string Sp, SqlParameter[] param)
    {
        Boolean result = false;
        SqlParameter output = new SqlParameter("@OUTPUT", 0);
        output.Direction = ParameterDirection.Output;
        try
        {
            SqlConnection con = GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(Sp, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            for (int i = 0; i < param.Length; i++)
            {
                cmd.Parameters.Add(param[i]);
            };
            cmd.Parameters.Add(output);
            cmd.ExecuteNonQuery();
            result = Convert.ToBoolean(cmd.Parameters["@OUTPUT"].Value);
            con.Close();
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public int ExccuteSPGetCode(string Sp, SqlParameter[] param)
    {
        int result = 0;
        SqlParameter output = new SqlParameter("@OUTPUT", DbType.Int32);
        output.Direction = ParameterDirection.Output;
        try
        {
            SqlConnection con = GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(Sp, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            for (int i = 0; i < param.Length; i++)
            {
                cmd.Parameters.Add(param[i]);
            };
            cmd.Parameters.Add(output);
            cmd.ExecuteNonQuery();
            result = Convert.ToInt32(cmd.Parameters["@OUTPUT"].Value);
            con.Close();
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public DataView GetDataView(string Sp, SqlParameter[] param)
    {
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        try
        {
            SqlConnection con = GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(Sp, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(param.ToArray());
            SqlDataAdapter da = new SqlDataAdapter(Sp, con);
            da.Fill(dt);
            dv = dt.DefaultView;
            con.Close();
        }
        catch (Exception ex)
        {

        }
        return dv;
    }
    public int InsertUpdateDeleteMethod(string Query)
    {
        int result = 0;
        SqlConnection con = GetConnection();
        SqlCommand cmd = new SqlCommand(Query, con);
        cmd.CommandTimeout = 0;
        con.Open();
        result = cmd.ExecuteNonQuery();
        con.Close();
        return result;
    }
    public DataSet GetDataset(string Sp, SqlParameter[] param)
    {
        //DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        try
        {
            SqlConnection con = GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(Sp, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            for (int i = 0; i < param.Length; i++)
            {
                cmd.Parameters.Add(param[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
        }
        catch (Exception ex)
        {

        }
        return ds;
    }
}
