using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HLGranite.Nisan;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["User"] != null)
        {
            pnlUser.Visible = false;
            linLogout.Visible = true;
        }
    }
    //todo: btnSearch_Click
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("btnSearch_Click");
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("login...");
        User user = new User(txtUser.Text);
        bool success = user.Login(txtPasswod.Text);
        if (success) this.Session.Add("User", user);
        pnlUser.Visible = !success;
        linLogout.Visible = success;
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        this.Session.RemoveAll();
        Response.Redirect("~/Default.aspx", false);
    }
}