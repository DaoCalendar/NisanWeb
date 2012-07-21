using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HLGranite.Nisan;

public partial class Calendar : System.Web.UI.Page
{
    private MuslimCalendar calendar;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime date = DateTime.Now;
            TextBox1.Text = date.Day + "/" + date.Month + "/" + date.Year;
        }

        string file = this.MapPath("App_Data\\muslimcal.xml");
        calendar = new MuslimCalendar(ReadXml(file));
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
    private void ToMuslimDate(DateTime sender)
    {
        try
        {
            calendar.GetDate(sender);
            Label1.Text = calendar.Day + "/" + calendar.Month + "/" + calendar.Year;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            return;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ToMuslimDate(CastToLocalDate(TextBox1.Text));
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        TextBox1.Text = Calendar1.SelectedDate.Day + "/" + Calendar1.SelectedDate.Month + "/" + Calendar1.SelectedDate.Year;
        ToMuslimDate(CastToLocalDate(TextBox1.Text));
    }
}