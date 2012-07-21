using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HLGranite.Nisan;

public partial class _Default : System.Web.UI.Page
{
    private int stockId = 0;
    private User user;
    private MuslimCalendar calendar;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["User"] != null) user = (User)this.Session["User"];
        if (Request.QueryString["Stock"] != null)
            this.stockId = Convert.ToInt32(Request.QueryString["Stock"]);
        string file = this.MapPath("App_Data\\muslimcal.xml");
        calendar = new MuslimCalendar(ReadXml(file));

        if (!IsPostBack)
            Initialize();
    }
    private DataTable ReadXml(string fileName)
    {
        DataTable table = new DataTable();
        DataSet dataset = new DataSet();

        try
        {
            if (System.IO.File.Exists(fileName))
                dataset.ReadXml(fileName);
            if (dataset.Tables.Count > 0)
                table = dataset.Tables[0].Copy();

            return table;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            return table;
        }
        finally { dataset.Dispose(); }
    }
    private DateTime CastToLocalDate(string sender)
    {
        DateTime output = DateTime.Now;
        string[] pieces = sender.Split(new char[] { '/' });
        if (pieces.Length != 3) return output;

        output = new DateTime(Convert.ToInt32(pieces[2]), Convert.ToInt32(pieces[1]), Convert.ToInt32(pieces[0]));
        return output;
    }
    private string ToMuslimDate(DateTime sender)
    {
        string output = string.Empty;
        calendar.GetDate(sender);
        output = calendar.Day + " " + MuslimCalendar.GetMuslimMonth(calendar.Month) + " " + calendar.Year;
        return output;
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

        if (user != null)
        {
            txtAgent.Text = user.Code;
            lblAgent.Text = user.Name;
            txtEmail.Text = user.Email;
            txtPhone.Text = user.Phone;
        }

        if (this.stockId > 0)
            ddlStock.SelectedValue = this.stockId.ToString();

        //define state dropdownlist
        ddlState.DataSource = State.LoadAll();
        ddlState.DataBind();
    }
    protected void Submit_Click(object sender, EventArgs e)
    {

    }
    protected void txtDeath_TextChanged(object sender, EventArgs e)
    {
        lblDeathm.Text = ToMuslimDate(CastToLocalDate(txtDeath.Text));
    }
}