object data = sqlComm.ExecuteScalar();<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class Handler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string imgStr = context.Request.QueryString["phDa"];
        var img = Convert.FromBase64String(imgStr);


        context.Response.Write(imgStr);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}