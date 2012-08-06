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

        lblMessage.Text = string.Empty;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Agent agent = new Agent();
        agent.Code = txtCode.Text.Trim();
        agent.Name = txtName.Text.Trim();
        agent.Password = txtPassword.Text;
        agent.Email = txtEmail.Text.Trim();
        agent.Phone = txtPhone.Text.Trim();
        agent.Remarks = txtRemarks.Text.Trim();

        Address address = new Address();
        address.Street = txtAddress.Text;
        address.Postal = txtPostal.Text.Trim();
        address.State = ddlState.Text;
        agent.Address = address;

        if (agent.Save())
        {
            Panel1.Enabled = false;
            btnSubmit.Enabled = false;
            lblMessage.Text = "Register successfully. Thank you.";
        }
        else
            lblMessage.Text = agent.Message;
    }
}