using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Content_AddPhoto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        MembershipUser membershipUser = Membership.GetUser();
        var userId = membershipUser.ProviderUserKey;

        var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\FMI 3.1\DAW\Proiect\App_Data\Database.mdf';Integrated Security=True");
        var sql = "INSERT INTO Albume(Nume, UserId) VALUES (@num, @uid)";

        var comm = new SqlCommand(sql, conn);
        comm.Parameters.AddWithValue("num", albumName.Text);
        comm.Parameters.AddWithValue("uid", userId);

        conn.Open();
        if(comm.ExecuteNonQuery() > 0)
        {
            Response.Redirect("~/Pages/Content/Profile.aspx");
        }
        conn.Close();
    }
}