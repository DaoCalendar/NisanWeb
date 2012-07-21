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
    protected bool Validating()
    {
        bool validated = true;
        string message = string.Empty;
        if (txtCode.Text.Trim().Length == 0)
            message += "Agent code cannot be blank!\n";
        if (txtName.Text.Trim().Length == 0)
            message += "Name cannot be blank!\n";
        if (!txtPassword.Text.Equals(txtPassword2.Text))
        {
            message += "Please confirm password again!\n";
            validated &= false;
        }

        lblMessage.Text = message;
        return validated;
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
        if (!Validating()) return;

        Agent agent = new Agent();
        agent.Code = txtCode.Text.Trim();
        agent.Name = txtName.Text.Trim();
        agent.Password = txtPassword.Text;

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
    }
}
