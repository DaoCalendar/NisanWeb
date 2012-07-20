using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HLGranite.Nisan;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            Initialize();
    }
    protected void Initialize()
    {
        //define state dropdownlist
        ddlState.DataSource = State.LoadAll();
        ddlState.DataBind();
    }
}
