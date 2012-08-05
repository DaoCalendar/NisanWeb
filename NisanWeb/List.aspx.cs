using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HLGranite.Nisan;

public partial class List : System.Web.UI.Page
{
    private User user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["User"] != null)
            user = (User)Session["User"];

        if (!IsPostBack)
            Initialize();
    }
    private void Initialize()
    {
        if (user == null) return;
        //todo: handle different user role
        if (user is Agent)
            GridView1.DataSource = (user as Agent).GetSales();
        else if (user is Admin)
            GridView1.DataSource = (user as Admin).GetAllSales();
        GridView1.DataBind();
    }
}