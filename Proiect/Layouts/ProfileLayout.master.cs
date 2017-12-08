using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Layouts_ProfileLayout : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated))
        {
            Response.Redirect("~/Pages/Authentication/Login.aspx");
        }
        else
        {
            MembershipUser membershipUser = Membership.GetUser();
            var userId = membershipUser.ProviderUserKey;
            var profileName = LoginView1.FindControl("ProfileName") as Label;

            var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\FMI 3.1\DAW\Proiect\App_Data\Database.mdf';Integrated Security=True");

            var comm = new SqlCommand("SELECT Nume, Prenume FROM Users WHERE UserId = @uid", conn);
            comm.Parameters.AddWithValue("uid", userId);

            conn.Open();
            var reader = comm.ExecuteReader();

            if (reader.Read())
            {
                profileName.Text = reader["Prenume"].ToString() + " " + reader["Nume"].ToString();
            }

            conn.Close();

            var profilePicture = LoginView1.FindControl("ProfilePicture") as Image;
            profilePicture.ImageUrl = "~/ProfilePictureHandler.ashx?userId=" + userId;
            profilePicture.DataBind();
        }
        
    }
}
