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
    public partial class ManageEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dtComingMovieList = new DataTable("ManageEvent");
            String conString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("GetAllCorporateEvent", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                sda.Fill(dtComingMovieList);
            }
            EventsListView.DataSource = dtComingMovieList;
            EventsListView.DataBind();
        }
    }
}