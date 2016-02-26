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
    public partial class DeleteComingMovie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String comingMovieSoonID = Request.QueryString["MovieComingSoonID"];

            if (!String.IsNullOrWhiteSpace(comingMovieSoonID))
            {
                String conString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand("FindComingMovie", conn);
                SqlDataReader read;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ComingMovieID", comingMovieSoonID);

                try
                {
                    conn.Open();
                    read = cmd.ExecuteReader();

                    if (read.Read())
                    {
                        ComingMovieID.Value = comingMovieSoonID;
                        Picture.ImageUrl = read["ImageUrl"].ToString();
                        ComingMovieTitle.Text = read["Title"].ToString();
                        MovieDescription.Text = read["LongDescription"].ToString();
                        ReleaseTime.Text = read["RealeseTime"].ToString();
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

        protected void deleteBtn_Click(object sender, EventArgs e)
        {
            String conString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("DeleteComingMovie", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ComingMovieID", ComingMovieID.Value);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Response.Redirect("ManageComingMovie.aspx");
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageComingMovie.aspx");
        }
    }
}