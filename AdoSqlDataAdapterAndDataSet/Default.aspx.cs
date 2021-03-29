using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AdoSqlDataAdapterAndDataSet
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using(SqlConnection con = new SqlConnection(CS))
            {
                SqlDataAdapter da = new SqlDataAdapter("GetDataSetData", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();

                GridView2.DataSource = ds.Tables[1];
                GridView2.DataBind();
            }
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            if (Cache["Data"] == null)
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select * from Student", con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    GridView1.DataSource = ds;                    
                    GridView1.DataBind();
                    Cache["Data"] = ds;
                    Label1.Text = "Data loaded from database";
                }
            }
            else
            {
                GridView1.DataSource = Cache["Data"];
                GridView1.DataBind();
                Label1.Text = "Data loaded from cache";
            }
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Cache["Data"] != null)
            {
                Cache.Remove("Data");
            }
        }

        protected void btnShowTxtBoxData_Click(object sender, EventArgs e)
        {
            GridView1.Visible = false;
            string inputName = TxtName.Text;
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using(SqlConnection con = new SqlConnection(CS))
            {
                SqlDataAdapter da = new SqlDataAdapter("GetDataSetDataWithInputParam", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Name", inputName);
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
        }
    }
}