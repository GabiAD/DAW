object data = sqlComm.ExecuteScalar();<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class Handler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string pozaId = context.Request.QueryString["pozaId"].ToString();

        string qry = "SELECT Imagine FROM Poze WHERE Id = @pid";
        
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\FMI 3.1\DAW\Proiect\App_Data\Database.mdf';Integrated Security=True");
        SqlCommand sqlComm = new SqlCommand(qry, conn);
        
        sqlComm.Parameters.AddWithValue("pid", pozaId);

        conn.Open();
        object data = sqlComm.ExecuteScalar();
        conn.Close();

        context.Response.BinaryWrite((byte[])data);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}