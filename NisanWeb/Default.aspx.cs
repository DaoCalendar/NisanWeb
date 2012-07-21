using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HLGranite.Nisan;

public partial class _Default : System.Web.UI.Page 
{
    private int stockId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Stock"] != null)
            this.stockId = Convert.ToInt32(Request.QueryString["Stock"]);
        if (!IsPostBack)
            Initialize();
    }
    protected void Initialize()
    {
        //done at web page
        //define stock dropdownlist
        //Stock stock = new Stock();
        //ddlStock.DataSource = stock.LoadAll();
        //ddlStock.DataValueField = "Id";
        //ddlStock.DataTextField = "Type";
        //ddlStock.DataBind();
        if (this.stockId > 0)
            ddlStock.SelectedValue = this.stockId.ToString();

        //define state dropdownlist
        ddlState.DataSource = State.LoadAll();
        ddlState.DataBind();
    }
}
