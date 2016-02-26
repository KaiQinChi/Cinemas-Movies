using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace s3357335_a2
{
    public partial class InsertCoporateEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                String conString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand("InsertCorporateEvent", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Title", EventTitle.Text);
                cmd.Parameters.AddWithValue("@SDescription", ShortDescription.Text);
                cmd.Parameters.AddWithValue("@LDescription", LongDescription.Text);

                try
                {
                    String fileName = Request.Files[0].FileName;
                    if (!String.IsNullOrEmpty(fileName))
                    {
                        cmd.Parameters.AddWithValue("@ImageUrl", "/Images/" + fileName);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ImageUrl", Picture.ImageUrl);
                    }
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("ManageEvent.aspx");
                }
                catch (SqlException se)
                {
                    Response.Write("<div class='alert alert-danger' role='alert'> " + se.Message + "</div>");
                }
                catch (Exception ex)
                {
                    Response.Write("<div class='alert alert-danger' role='alert'> " + ex.Message + "</div>");
                }
            }
        }
    }
}