using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace s3357335_a2
{

    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("<div class='alert alert-danger' role='alert'> " + Encrypt("kai1") + "</div>");
        }

        protected void SignIn_Click(object sender, EventArgs e)
        {
            String conString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("FindAdministor", conn);
            SqlDataReader read;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", Page.Request.Form["inputUserName"].ToString());
            cmd.Parameters.AddWithValue("@Password", Encrypt(Page.Request.Form["inputPassword"].ToString()));

            try
            {
                conn.Open();
                read = cmd.ExecuteReader();

                if (read.Read())
                {
                    read.Close();
                    Session["UserName"] = Page.Request.Form["inputUserName"].ToString();
                    Response.Redirect("ManageMovie.aspx");
                }
                else
                {
                    read.Close();
                    Response.Write("<div class='alert alert-danger' role='alert'> Sorry, Wrong UserName and Password.</div>");
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

        //The text will be encrypted using AES Symmetric key (Same key) algorithm and then will be stored in the database.  
        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}