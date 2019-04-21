using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#region Namespace for database connection
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
#endregion


namespace MvcApplication1.Models
{
    public class BusinessLayer
    {
        #region Variable for database connection
        SqlConnection con;
        SqlDataAdapter da;
        SqlDataReader dr;
        DataSet ds;
        DataTable dt;
        SqlCommand cmd;
        object ab;
        int a;
        #endregion

        #region Function for Openconnection
        public void Openconn()
        {
            if (con == null)
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString());
            if (con.State == ConnectionState.Closed)
                con.Open();
        }
        #endregion

        #region Function for Close Conn
        public void closeconn()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
        #endregion

        #region Function for Dataset
        public DataSet FunDataset(string sqlcmd)
        {
            Openconn();
            da = new SqlDataAdapter(sqlcmd, con);
            ds = new DataSet();
            da.Fill(ds);
            closeconn();
            return ds;
        }
        #endregion

        #region Function for DataTable
        public DataTable FunDataTable(string sqlcmd)
        {
            Openconn();
            da = new SqlDataAdapter(sqlcmd, con);
            dt = new DataTable();
            da.Fill(dt);
            closeconn();
            return dt;
        }
        #endregion

        #region Function for ExecuteNonQuery(Insert,Update,Delete)
        public int FunExecuteNonQuery(string sqlcmd)
        {
            Openconn();
            cmd = new SqlCommand(sqlcmd, con);
            a = cmd.ExecuteNonQuery();
            closeconn();
            return a;
        }
        #endregion

        #region Function for ExecuteReader(fetch Rows)
        public SqlDataReader FunDataReader(string sqlcmd)
        {
            Openconn();
            cmd = new SqlCommand(sqlcmd, con);
            dr = cmd.ExecuteReader();
            return dr;
        }
        #endregion

        #region Function for ExecuteScalar(Fetch single value)
        public object FunExecutescalar(string sqlcmd)
        {
            Openconn();
            cmd = new SqlCommand(sqlcmd, con);
            ab = cmd.ExecuteScalar();
            closeconn();
            return ab;
        }
        #endregion
    }
}