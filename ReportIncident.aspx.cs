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
    public partial class ReportIncident : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        SqlCommand cmd = null;
        SqlDataReader dr = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            if (!Page.IsPostBack)
            {
                ddlDisease.Items.Add("H1N1 (Swine Flu)");
                ddlDisease.Items.Add("Cold");
                ddlDisease.Items.Add("Jaundice");
            }
            if (!Page.IsPostBack)
            {
                ddlState.Items.Add("Maharashtra");
                ddlState.Items.Add("MP");
                ddlState.Items.Add("UP");
            }
            if (!Page.IsPostBack)
            {
                ddlCity.Items.Add("Pune");
                ddlCity.Items.Add("Mumbai");
                ddlCity.Items.Add("Delhi");
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("insert into cdc_164355.Incident values(@diseaseName,@dateOfFirstIncident,@totalCasesTillDate,@diseaseState,@city,@hospitalName)", conn);
            
            cmd.Parameters.AddWithValue("@diseaseName", ddlDisease.Text);
            cmd.Parameters.AddWithValue("@dateOfFirstIncident", Convert.ToDateTime(txtFirstIncident.Text));
            cmd.Parameters.AddWithValue("@totalCasesTillDate",Convert.ToInt32(txtTotalCases.Text));
            cmd.Parameters.AddWithValue("@diseaseState", ddlState.Text);
            cmd.Parameters.AddWithValue("@city", ddlCity.Text);
            cmd.Parameters.AddWithValue("@hospitalName", txtHospitalName.Text);


            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                Response.Write("<script type='text/javascript'> alert('Record added successfully')</script>");
                txtTotalCases.Text = "";
                txtHospitalName.Text = "";
                txtFirstIncident.Text = "";
            }
            else
                Response.Write("<script type='text/javascript'> alert('Couldn't Add Data')</script>");

            //cmd = new SqlCommand("select max(id) from cdc_164355.Incident", conn);

            //DataTable dt = new DataTable();
            //dr= cmd.ExecuteReader();
            //dt.Load(dr);
            
            //lblId.Text = dt.ToString();

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }
    }
}