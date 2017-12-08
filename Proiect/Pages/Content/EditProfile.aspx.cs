using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Content_EditProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated))
        {
            return;
        }

        if (!Page.IsPostBack)
        {
            MembershipUser membershipUser = Membership.GetUser();
            var userId = membershipUser.ProviderUserKey;

            ContentPlaceHolder cp = this.Master.Master.FindControl("ContentPlaceHolderBase") as ContentPlaceHolder;
            var profilePicture = (cp.FindControl("LoginView1") as LoginView).FindControl("ProfilePicture") as Image;

            var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\FMI 3.1\DAW\Proiect\App_Data\Database.mdf';Integrated Security=True");
            var comm = new SqlCommand("SELECT Id, Nume FROM Orase", conn);

            var dataTable = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            adapter.Fill(dataTable);

            Oras.DataSource = dataTable;
            Oras.DataTextField = "Nume";
            Oras.DataValueField = "Id";
            Oras.DataBind();

            comm = new SqlCommand("SELECT Nume, Prenume, DataNasterii, PozaProfil, OrasId FROM Users WHERE UserId = @uid", conn);
            comm.Parameters.AddWithValue("uid", userId);

            conn.Open();
            var reader = comm.ExecuteReader();

            if (reader.Read())
            {
                Prenume.Text = reader["Prenume"].ToString();
                Nume.Text = reader["Nume"].ToString();
                DataNasterii.Text = reader["DataNasterii"].ToString().Substring(0, Math.Min(10, reader["DataNasterii"].ToString().Length));
                Oras.SelectedValue = reader["OrasId"].ToString();
            }
            

            conn.Close();
        }
        
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        MembershipUser membershipUser = Membership.GetUser();
        var userId = membershipUser.ProviderUserKey;

        string qry = "UPDATE Users SET Prenume = @prenume, Nume = @nume, DataNasterii = @dataNasterii, OrasId = @orasId WHERE UserId = @uid";

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\FMI 3.1\DAW\Proiect\App_Data\Database.mdf';Integrated Security=True");
        SqlCommand sqlComm = new SqlCommand(qry, conn);

        sqlComm.Parameters.AddWithValue("uid", userId);
        sqlComm.Parameters.AddWithValue("prenume", Prenume.Text);
        sqlComm.Parameters.AddWithValue("nume", Nume.Text);
        sqlComm.Parameters.AddWithValue("dataNasterii", DataNasterii.Text);
        sqlComm.Parameters.AddWithValue("orasId", Oras.SelectedValue);

        conn.Open();
        try
        {
            sqlComm.ExecuteNonQuery();
            message.Visible = true;
            message.Text = "<b>Great!</b>  Your profile is up to date.";
        }
        catch (Exception exc)
        {
            
        }
        
        if (PhotoUpload.HasFile)
        {
            Stream photoStream = PhotoUpload.PostedFile.InputStream;
            int photoLength = PhotoUpload.PostedFile.ContentLength;
            byte[] photoData = new byte[photoLength];
            photoStream.Read(photoData, 0, photoLength);

            qry = "UPDATE Users SET PozaProfil = @img WHERE UserId = @uid";
            sqlComm = new SqlCommand(qry, conn);

            sqlComm.Parameters.AddWithValue("uid", userId);
            sqlComm.Parameters.AddWithValue("img", photoData);

            try
            {
                sqlComm.ExecuteNonQuery();
                message.Visible = true;
                message.Text += "<br /> Profile picture successfully updated.";
            }
            catch (Exception exc)
            {

            }
        }

        conn.Close();
    }
}