using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HLGranite.Nisan;

public partial class MasterPage : System.Web.UI.MasterPage
{
    private User user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["User"] != null)
        {
            pnlUser.Visible = false;
            linLogout.Visible = true;
            linUser.Visible = true;

            user = (User)this.Session["User"];
            linUser.Text = user.Code;
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
        if (success)
        {
            user = user.GetRole();
            this.Session.Add("User", user);
            linUser.Text = user.Code;
        }

        pnlUser.Visible = !success;
        linLogout.Visible = success;
        linUser.Visible = success;
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        this.Session.RemoveAll();
        Response.Redirect("~/Default.aspx", false);
    }
}