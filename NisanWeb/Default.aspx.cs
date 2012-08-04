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
    private string name = string.Empty;

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
            if (user.Address != null)
            {
                txtAddress.Text = user.Address.Street;
                txtPostal.Text = user.Address.Postal;
                ddlState.SelectedValue = user.Address.State;
            }
        }

        //set choosen stock
        if (Request.QueryString["Stock"] != null)
            this.stockId = Convert.ToInt32(Request.QueryString["Stock"]);
        if (this.stockId > 0)
            ddlStock.SelectedValue = this.stockId.ToString();

        //search by name
        if (Request.QueryString["Name"] != null)
            this.name = Request.QueryString["Name"].ToString().ToLower();

        //set muslim calendar
        string file = this.MapPath("App_Data\\muslimcal.xml");
        calendar = new MuslimCalendar(ReadXml(file));

        if (!IsPostBack)
            Initialize();
        if (this.name.Length > 0)
            LoadOrder(this.name);
    }

    protected void Initialize()
    {
        //define state dropdownlist
        ddlState.DataSource = State.LoadAll();
        ddlState.DataBind();
    }
    private void LoadOrder(string name)
    {
        Order order = new Order();
        order = order.Find(name);
        if (order == null) return;

        imgStatus.ImageUrl = string.Format("Images/{0}.png", order.Status.ToString().ToLower());

        Nisan nisan = order.Stock as Nisan;
        ddlStock.SelectedValue = new Stock(order.Stock.Type).Id.ToString();//todo: what not just order.Stock.Id
        txtName.Text = nisan.Name;
        txtJawi.Text = nisan.Jawi;
        txtDeath.Text = nisan.Death.ToString("dd/MM/yyyy");
        txtRemarks.Text = nisan.Remarks;

        //txtEmail.Text = 
        if (order.Agent != null)
        {
            txtEmail.Text = order.Agent.Email;
            txtPhone.Text = order.Agent.Phone;
        }
        if (order.ShipTo != null)
        {
            txtAddress.Text = order.ShipTo.Street;
            txtPostal.Text = order.ShipTo.Postal;
            ddlState.SelectedValue = order.ShipTo.State;
        }

        EnableForm(false);
        btnSubmit.Visible = false;
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
    private DateTime ToDate(string sender)
    {
        int year = Convert.ToInt32(sender.Substring(6, 4));
        int month = Convert.ToInt32(sender.Substring(3, 2));
        int day = Convert.ToInt32(sender.Substring(0, 2));
        return new DateTime(year, month, day);
    }
    private void EnableForm(bool enabled)
    {
        ddlStock.Enabled = enabled;
        txtName.Enabled = enabled;
        txtJawi.Enabled = enabled;
        txtDeath.Enabled = enabled;
        btnDeath.Enabled = enabled;
        txtRemarks.Enabled = enabled;
        txtAgent.Enabled = enabled;

        txtCustomer.Enabled = enabled;
        txtEmail.Enabled = enabled;
        txtPhone.Enabled = enabled;
        txtAddress.Enabled = enabled;
        txtPostal.Enabled = enabled;
        ddlState.Enabled = enabled;

        btnSubmit.Enabled = enabled;
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
        target.Status = TransactionStage.Submit;
        if (user is Agent)
            target.Agent = user as Agent;
        target.Amount = stock.Price;
        target.Quantity = 1;
        target.Stock = nisan;
        target.ShipTo = address;
        bool success = target.Save();
        if (success)
            EnableForm(false);
    }
    protected void txtDeath_TextChanged(object sender, EventArgs e)
    {
        lblDeathm.Text = ToMuslimDate(CastToLocalDate(txtDeath.Text));
    }
}