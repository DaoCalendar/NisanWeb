using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Catalog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void Order_Click(object sender, EventArgs e)
    {
        int stockId = (sender as Button).TabIndex;
        Response.Redirect("~/Default.aspx?Stock=" + stockId, true);
    }
}