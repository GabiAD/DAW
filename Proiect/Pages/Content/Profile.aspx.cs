using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Content_Profile : System.Web.UI.Page
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

            string qry = @"SELECT p.Id AS PozaId, a.Id AS AlbumId, a.Nume AS NumeAlbum
                            FROM Poze AS p INNER JOIN Albume AS a ON p.AlbumId = a.Id
                            INNER JOIN Users AS u ON a.UserId = u.UserId
                            WHERE (p.DataPostare IN
                            (SELECT MAX(p.DataPostare) AS Expr1
                            FROM Poze AS p INNER JOIN Albume AS a ON a.Id = p.AlbumId
                            GROUP BY p.AlbumId)) 
                            AND u.UserId = @uid";
        

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\FMI 3.1\DAW\Proiect\App_Data\Database.mdf';Integrated Security=True");
            SqlCommand sqlComm = new SqlCommand(qry, conn);
            sqlComm.Parameters.AddWithValue("uid", userId);

            var dataTable = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
            adapter.Fill(dataTable);

            AlbumeRepeater.DataSource = dataTable;
            AlbumeRepeater.DataBind();
        }

    }
}