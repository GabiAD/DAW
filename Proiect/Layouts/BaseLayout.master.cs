using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Layouts_BaseLayout : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            var userDisplayName = LoginView1.FindControl("UserDisplayName") as HyperLink;
            if (userDisplayName != null)
            {
                userDisplayName.Text = HttpContext.Current.User.Identity.Name;
            }
        }
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        var searchTerm = SearchBox.Text;
        Response.Redirect("~/Pages/Content/SearchResult.aspx?searchTerm=" + searchTerm);
        SearchBox.Text = "";
    }
}
