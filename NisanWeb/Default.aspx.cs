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
    private Order order;

    protected void Page_Load(object sender, EventArgs e)
    {
        //set user
        if (this.Session["User"] != null) user = (User)this.Session["User"];

        //set choosen stock
        if (Request.QueryString["Stock"] != null)
            this.stockId = Convert.ToInt32(Request.QueryString["Stock"]);

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

        if (user != null)
        {
            if (user is Admin)
            {
                btnPay.Enabled = true;
                btnDeliver.Enabled = true;
                btnWork.Enabled = true;
                btnReceive.Enabled = false;
            }
            else if (user is Designer)
            {
                btnPay.Enabled = false;
                btnDeliver.Enabled = true;
                btnWork.Enabled = true;
                btnReceive.Enabled = false;
            }
            else if (user is Agent)
            {
                btnPay.Enabled = false;
                btnDeliver.Enabled = false;
                btnWork.Enabled = false;
                btnReceive.Enabled = false;
            }
            else
            {
                btnPay.Enabled = false;
                btnDeliver.Enabled = false;
                btnWork.Enabled = false;
                btnReceive.Enabled = true;
            }
        }
        else
        {
            btnPay.Enabled = false;
            btnDeliver.Enabled = false;
            btnWork.Enabled = false;
            btnReceive.Enabled = true;
        }
    }

    protected void Initialize()
    {
        //define muslim day dropdownlist
        for (int i = 1; i <= 31; i++)
            ddlDay.Items.Add(i.ToString());

        //define muslim month dropdownlist
        ddlMonth.DataSource = Enum.GetNames(typeof(MuslimMonth));
        ddlMonth.DataBind();

        //define state dropdownlist
        ddlState.DataSource = State.LoadAll();
        ddlState.DataBind();

        if (user != null)
        {
            txtAgent.Text = user.Code;
            lblAgent.Text = user.Name;
            txtCustomer.Text = user.Name;
            txtEmail.Text = user.Email;
            txtPhone.Text = user.Phone;
            if (user.Address != null)
            {
                txtAddress.Text = user.Address.Street;
                txtPostal.Text = user.Address.Postal;
                ddlState.SelectedValue = user.Address.State;
            }
        }

        if (this.stockId > 0) ddlStock.SelectedValue = this.stockId.ToString();
    }
    private void LoadOrder(string name)
    {
        order = new Order();
        order = order.Find(name);
        if (order == null) return;
        if ((order.Stock as Nisan) == null) return;

        imgStatus.ImageUrl = string.Format("Images/{0}.png", order.Status.ToString().ToLower());
        Nisan nisan = order.Stock as Nisan;
        ddlStock.SelectedValue = new Stock(order.Stock.Type).Id.ToString();//todo: why not just order.Stock.Id
        txtName.Text = nisan.Name;
        txtJawi.Text = nisan.Jawi;
        txtDeath.Text = nisan.Death.ToString("dd/MM/yyyy");
        ddlDay.Text = nisan.Deathm.Day.ToString();
        ddlMonth.SelectedIndex = nisan.Deathm.Month - 1;
        txtYear.Text = nisan.Deathm.Year.ToString();
        txtRemarks.Text = nisan.Remarks;

        if (order.Agent != null)
        {
            txtCustomer.Text = order.Agent.Name;
            txtEmail.Text = order.Agent.Email;
            txtPhone.Text = order.Agent.Phone;
        }
        if (order.Customer != null)
        {
            txtCustomer.Text = order.Customer.Name;
            txtEmail.Text = order.Customer.Email;
            txtPhone.Text = order.Customer.Phone;
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
        txtName.ReadOnly = !enabled;
        txtJawi.ReadOnly = !enabled;
        txtDeath.ReadOnly = !enabled;
        btnDeath.Enabled = enabled;
        ddlDay.Enabled = enabled;
        ddlMonth.Enabled = enabled;
        txtYear.ReadOnly = !enabled;
        txtRemarks.ReadOnly = !enabled;
        txtAgent.ReadOnly = !enabled;

        txtCustomer.ReadOnly = !enabled;
        txtEmail.ReadOnly = !enabled;
        txtPhone.ReadOnly = !enabled;
        txtAddress.ReadOnly = !enabled;
        txtPostal.ReadOnly = !enabled;
        ddlState.Enabled = enabled;

        btnSubmit.Enabled = enabled;
    }
    private void Submit()
    {
        stockId = Convert.ToInt32(ddlStock.SelectedValue);
        HLGranite.Nisan.Stock stock = new HLGranite.Nisan.Stock(stockId);
        Nisan nisan = new Nisan(stock);
        nisan.Name = txtName.Text.Trim();
        nisan.Jawi = txtJawi.Text.Trim();
        nisan.Death = ToDate(txtDeath.Text);
        nisan.Deathm = new DateTime(Convert.ToInt32(txtYear.Text), ddlMonth.SelectedIndex + 1, Convert.ToInt32(ddlDay.Text));

        Order target = new Order();
        target.Status = TransactionStage.Submit;
        if (user == null)
        {
            Customer customer = new Customer();
            customer.Name = txtCustomer.Text;
            customer.Email = txtEmail.Text;
            customer.Phone = txtPhone.Text;

            target.Customer = customer;
            target.Parent.CreatedBy = customer;
        }
        else
        {
            if (txtCustomer.Text.Trim() != user.Name)
            {
                Customer customer = new Customer();
                customer.Name = txtCustomer.Text;
                customer.Email = txtEmail.Text;
                customer.Phone = txtPhone.Text;

                target.Customer = customer;
                target.Parent.CreatedBy = customer;
            }
            else if (user is Agent)
            {
                target.Agent = (user as Agent);
                target.Parent.CreatedBy = (user as Agent);
            }
        }

        target.Amount = stock.Price;
        target.Quantity = 1;
        target.Stock = nisan;

        Address address = new Address();
        address.Street = txtAddress.Text;
        address.Postal = txtPostal.Text;
        address.State = ddlState.Text;
        target.ShipTo = address;

        bool success = target.Save();
        if (success)
        {
            lblMessage.Text = "Your order has been sent to us and we will contact you shortly.";
            EnableForm(false);
        }
    }

    protected void txtName_TextChanged(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("txtName_TextChanged");
        JawiTranslator translator = new JawiTranslator();
        txtJawi.Text = translator.Translate(txtName.Text.Trim());
    }
    protected void txtDeath_TextChanged(object sender, EventArgs e)
    {
        //lblDeathm.Text = ToMuslimDate(CastToLocalDate(txtDeath.Text));        
        calendar.GetDate(CastToLocalDate(txtDeath.Text));
        ddlDay.Text = calendar.Day.ToString();
        ddlMonth.SelectedIndex = calendar.Month - 1;
        txtYear.Text = calendar.Year.ToString();
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        Submit();
    }
    protected void btnPay_Click(object sender, EventArgs e)
    {
        if (order != null && (order.Stock is Nisan))
        {
            System.Diagnostics.Debug.WriteLine("btnPay_Click");
            order.Status = TransactionStage.Pay;
            order.Save();

            LoadOrder(name);
        }
    }
    protected void btnWork_Click(object sender, EventArgs e)
    {
        if (order != null && (order.Stock is Nisan))
        {
            System.Diagnostics.Debug.WriteLine("btnWork_Click");
            order.Status = TransactionStage.Working;
            order.Save();

            LoadOrder(name);
        }
    }
    protected void btnDeliver_Click(object sender, EventArgs e)
    {
        if (order != null && (order.Stock is Nisan))
        {
            System.Diagnostics.Debug.WriteLine("btnDeliver_Click");
            order.Status = TransactionStage.Deliver;
            order.Save();

            LoadOrder(name);
        }
    }
    protected void btnReceive_Click(object sender, EventArgs e)
    {
        if (order != null && (order.Stock is Nisan))
        {
            System.Diagnostics.Debug.WriteLine("btnReceive_Click");
            order.Status = TransactionStage.Receive;
            order.Save();

            LoadOrder(name);
        }
    }
}