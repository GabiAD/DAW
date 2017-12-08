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
            var tId = Request.Params["tagId"];

            PhotosDataSource.SelectCommand = "SELECT Id AS PozaId FROM Poze AS p WHERE (TagId =" + tId + ") ORDER BY DataPostare DESC";

            var comm = new SqlCommand("SELECT Tag FROM Tags WHERE Id = @tId", conn);
            comm.Parameters.AddWithValue("tId", tId);

            conn.Open();
            var reader = comm.ExecuteReader();
            reader.Read();

            CategoryTitle.Text = reader["Tag"].ToString();
            conn.Close();

        }

        
    }
}