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
    public partial class EditEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
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
                            EventID.Value = eventID;
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

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                String conString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand("UpdateCorporateEvent", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EventID", EventID.Value);
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