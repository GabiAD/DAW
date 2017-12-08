using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Content_Home : System.Web.UI.Page
{
    private SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\FMI 3.1\DAW\Proiect\App_Data\Database.mdf';Integrated Security=True");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            var searchTerm = Request.Params["searchTerm"];

            PhotosDataSource.SelectCommand = "SELECT Id AS PozaId FROM Poze AS p WHERE (Titlu like '%" + searchTerm + "%') ORDER BY DataPostare DESC";

            SearchText.Text = "Results for \"" + searchTerm + "\":";
        }

    }
}