using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Question1
{
    public partial class SearchIncident : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlDisease.Items.Add("H1N1 (Swine Flu)");
                ddlDisease.Items.Add("Cold");
                ddlDisease.Items.Add("Jaundice");
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from cdc_164355.Incident where diseaseName=@name", conn);
            cmd.Parameters.AddWithValue("@name", ddlDisease.SelectedItem.Text);

            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();

            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            if (dr.HasRows)
            {
                dt.Load(dr);
            }

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            gridview1.DataSource = dt;
            gridview1.DataBind();
        }
    }
}