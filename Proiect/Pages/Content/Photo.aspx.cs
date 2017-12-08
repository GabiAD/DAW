using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Content_Photo : System.Web.UI.Page
{
    private SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\FMI 3.1\DAW\Proiect\App_Data\Database.mdf';Integrated Security=True");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            var pId = Request.Params["photoId"];
            ImageFull.ImageUrl = "~/AlbumHandler.ashx?pozaId=" + pId;

            var comm = new SqlCommand("SELECT u.Prenume as Prenume, u.Nume AS Nume, a.UserId AS userId, p.Descriere AS Descriere FROM Poze p JOIN Albume a ON p.AlbumId = a.Id JOIN Users u ON u.UserId = a.UserId WHERE p.Id = @pid", conn);

            comm.Parameters.AddWithValue("pid", pId);

            conn.Open();
            var reader = comm.ExecuteReader();
            reader.Read();

            var uId = reader["UserId"].ToString();
            ImageUser.ImageUrl = "~/ProfilePictureHandler.ashx?userId=" + uId;
            Caption.Text = reader["Descriere"].ToString();
            UserPhoto.Text = reader["Prenume"].ToString() + " " + reader["Nume"].ToString();
            conn.Close();

            if ((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                MembershipUser membershipUser = Membership.GetUser();
                var userId = membershipUser.ProviderUserKey;

                if(uId == userId.ToString() || User.IsInRole("Administrator"))
                {
                    DeletePhoto.Visible = true;
                }
            }

        }
        
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        LoadComments();
    }

    protected void PostComment_Click(object sender, EventArgs e)
    {
        if(LoginView1.FindControl("CommentBox") != null && (LoginView1.FindControl("CommentBox") as TextBox).Text != "")
        {
            MembershipUser membershipUser = Membership.GetUser();
            var userId = membershipUser.ProviderUserKey;

            var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\FMI 3.1\DAW\Proiect\App_Data\Database.mdf';Integrated Security=True");
            var comm = new SqlCommand("INSERT INTO Comentarii(Text, DataPostare, UserComentariuId, PozaId) VALUES (@text, @data, @uid, @pid)", conn);

            comm.Parameters.AddWithValue("uid", userId);
            comm.Parameters.AddWithValue("pid", Request.Params["photoId"]);
            comm.Parameters.AddWithValue("data", DateTime.Now);
            comm.Parameters.AddWithValue("text", (LoginView1.FindControl("CommentBox") as TextBox).Text);

            conn.Open();
            try
            {
                comm.ExecuteNonQuery();
                (LoginView1.FindControl("CommentBox") as TextBox).Text = "";
            }
            catch (Exception exc)
            {

            }
            conn.Close();
        }
    }

    private void LoadComments()
    {
        var pId = Request.Params["photoId"];

        var sqlComm = "SELECT c.Id AS ComentariuId, u.Prenume, u.Nume, c.Text As TextComentariu FROM Comentarii c JOIN Users u ON c.UserComentariuId = u.UserId WHERE c.PozaId = @pid";
        var comm = new SqlCommand(sqlComm, conn);
        comm.Parameters.AddWithValue("pid", pId);

        sqlComm = "SELECT c.Id AS ComentariuId, u.UserId AS UserId, u.Prenume, u.Nume, c.Text As TextComentariu FROM Comentarii c JOIN Users u ON c.UserComentariuId = u.UserId WHERE c.PozaId = " + pId;
        CommentsDataSource.SelectCommand = sqlComm;
        CommentsDataSource.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\FMI 3.1\DAW\Proiect\App_Data\Database.mdf';Integrated Security=True";
        CommentsDataSource.DataBind();

        //var dataTable = new DataTable();

        //SqlDataAdapter adapter = new SqlDataAdapter(comm);
        //adapter.Fill(dataTable);

        //ComentariiRepeater.DataSource = dataTable;
        //ComentariiRepeater.DataBind();


    }

    protected void DeletePhoto_Click(object sender, EventArgs e)
    {
        var pId = Request.Params["photoId"];

        var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\FMI 3.1\DAW\Proiect\App_Data\Database.mdf';Integrated Security=True");
        var comm = new SqlCommand("DELETE FROM Poze WHERE Id = @pid", conn);

        comm.Parameters.AddWithValue("pid", pId);

        conn.Open();
        try
        {
            comm.ExecuteNonQuery();
            Response.Redirect("~/Pages/Content/Home.aspx");
        }
        catch (Exception exc)
        {
        }
        conn.Close();

    }
    
    protected void DeleteComment_Click(object sender, EventArgs e)
    {
        LinkButton clickedButton = (LinkButton)sender;
        var cId = (clickedButton.Parent.FindControl("comentariuHidVal") as HiddenField).Value;

        var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\FMI 3.1\DAW\Proiect\App_Data\Database.mdf';Integrated Security=True");
        var comm = new SqlCommand("DELETE FROM Comentarii WHERE Id = @cid", conn);

        comm.Parameters.AddWithValue("cid", cId);

        conn.Open();
        try
        {
            comm.ExecuteNonQuery();
        }
        catch (Exception exc)
        {

        }
        conn.Close();
    }
}