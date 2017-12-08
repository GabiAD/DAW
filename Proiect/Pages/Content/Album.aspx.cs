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
            var aId = Request.Params["albumId"];

            string qry = @"SELECT Id AS PozaId
                            FROM Poze
                            WHERE AlbumId = @aid";

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\FMI 3.1\DAW\Proiect\App_Data\Database.mdf';Integrated Security=True");
            SqlCommand sqlComm = new SqlCommand(qry, conn);
            sqlComm.Parameters.AddWithValue("aid", aId);

            var dataTable = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
            adapter.Fill(dataTable);

            AlbumeRepeater.DataSource = dataTable;
            AlbumeRepeater.DataBind();
        }

    }
}