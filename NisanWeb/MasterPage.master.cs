using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //todo: btnSearch_Click
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("btnSearch_Click");
    }
}
