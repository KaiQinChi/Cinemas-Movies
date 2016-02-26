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
    public partial class ViewAEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String eventID = Request.QueryString["EventID"];

            if (!String.IsNullOrWhiteSpace(eventID))
            {
                String conString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand("FindCorporateEvent", conn);
                SqlDataReader read;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EventID", eventID);

                try
                {
                    conn.Open();
                    read = cmd.ExecuteReader();

                    if (read.Read())
                    {
                        Picture.ImageUrl = read["ImageUrl"].ToString();
                        EventTitle.Text = read["Title"].ToString();
                        ShortDescription.Text = read["ShortDescription"].ToString();
                        LongDescription.Text = read["LongDescription"].ToString();

                        read.Close();
                    }
                    else
                    {
                        read.Close();
                        Response.Write("<div class='alert alert-danger' role='alert'> Sorry, this movie does not exist.</div>");
                    }
                }
                catch (SqlException se)
                {
                    Response.Write("<div class='alert alert-danger' role='alert'> " + se.Message + "</div>");
                }
                catch (Exception ex)
                {
                    Response.Write("<div class='alert alert-danger' role='alert'> " + ex.Message + "</div>");
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            else
            {
                Response.Write("<div class='alert alert-danger' role='alert'> Sorry, this movie does not exist.</div>");
            }
        }
    }
}