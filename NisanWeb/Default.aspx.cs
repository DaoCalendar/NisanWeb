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
        //set user
        if (this.Session["User"] != null) user = (User)this.Session["User"];
        if (user != null)
        {
            txtAgent.Text = user.Code;
            lblAgent.Text = user.Name;
            txtEmail.Text = user.Email;
            txtPhone.Text = user.Phone;
        }

        //set choosen stock
        if (Request.QueryString["Stock"] != null)
            this.stockId = Convert.ToInt32(Request.QueryString["Stock"]);
        if (this.stockId > 0)
            ddlStock.SelectedValue = this.stockId.ToString();

        //set muslim calendar
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
        //define state dropdownlist
        ddlState.DataSource = State.LoadAll();
        ddlState.DataBind();
    }
    private DateTime ToDate(string sender)
    {
        int year = Convert.ToInt32(sender.Substring(6, 4));
        int month = Convert.ToInt32(sender.Substring(3, 2));
        int day = Convert.ToInt32(sender.Substring(0, 2));
        return new DateTime(year, month, day);
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        Address address = new Address();
        address.Street = txtAddress.Text;
        address.Postal = txtPostal.Text;
        address.State = ddlState.Text;

        stockId = Convert.ToInt32(ddlStock.SelectedValue);
        HLGranite.Nisan.Stock stock = new HLGranite.Nisan.Stock(stockId);
        Nisan nisan = new Nisan(stock);
        nisan.Name = txtName.Text.Trim();
        nisan.Death = ToDate(txtDeath.Text);

        Order target = new Order();
        target.Status = TransactionStage.Confirmed;
        target.Agent = user as Agent;
        target.Amount = stock.Price;
        target.Quantity = 1;
        target.Stock = nisan;
        target.ShipTo = address;
        bool success = target.Save();
        if (success)
        {
            //todo: Panel1.Enabled = false;
            //todo: Panel2.Enabled = false;
            btnSubmit.Enabled = false;
        }
    }
    protected void txtDeath_TextChanged(object sender, EventArgs e)
    {
        lblDeathm.Text = ToMuslimDate(CastToLocalDate(txtDeath.Text));
    }
}