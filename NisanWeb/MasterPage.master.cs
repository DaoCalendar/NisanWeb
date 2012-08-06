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

            Menu2.Visible = true;
            if (user is Admin) Menu3.Visible = true;
        }
        if (!IsPostBack)
            Initialize();
    }
    private void Initialize()
    {
        linVersion.Text = "ver " + GetAssemblyVersion("HLGranite.Nisan.DLL", 3);
    }
    /// <summary>
    /// Get speciifed assembly version (ref AssemblyInfo.cs)
    /// </summary>
    /// <param name="assemblyName">Assembly name, eg.GISPM.DLL.</param>
    /// <param name="digit">Digit to show (max 4 only).</param>
    /// <returns></returns>
    public string GetAssemblyVersion(string assemblyName, int digit)
    {
        string lVersion = "";
        assemblyName = assemblyName.Trim().ToUpper();
        if (digit > 4) digit = 4;//maximum

        try
        {
            System.Reflection.Assembly[] lAssemblies = System.Threading.Thread.GetDomain().GetAssemblies();
            foreach (System.Reflection.Assembly ass in lAssemblies)
            {
                if (ass.GetType() == typeof(System.Reflection.Assembly)
                    && ass.CodeBase.ToUpper().LastIndexOf(assemblyName) > -1)
                {
                    string[] lSplits = ass.FullName.Split(' ');
                    foreach (string s in lSplits)
                    {
                        if (s.Trim(',').ToLower().IndexOf("version") > -1)
                        {
                            string[] lVers = s.Trim(',').Split('=');
                            lVersion = lVers[lVers.Length - 1];
                            if (digit < 4) //only less than 4 digit
                            {
                                lVers = lVersion.Split('.');
                                lVersion = "";//reset
                                for (int i = 0; i < digit; i++)
                                    lVersion += lVers[i] + ".";
                            }
                        }
                    }
                }//found specified dll only
            }//end loops

            return lVersion.TrimEnd('.');
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            //logger.Error(ex);
            return lVersion.TrimEnd('.');
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("btnSearch_Click");
        Response.Redirect("~/Default.aspx?Name=" + txtSearch.Text, false);
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

            pnlUser.Visible = !success;
            linLogout.Visible = success;
            linUser.Visible = success;

            Menu2.Visible = true;
            if (user is Admin) Menu3.Visible = true;
            Response.Redirect("~/List.aspx", false);
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        this.Session.RemoveAll();
        Response.Redirect("~/Default.aspx", false);
    }
}