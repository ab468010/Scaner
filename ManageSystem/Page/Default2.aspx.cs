using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class Page_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        localhost.WorkService web = new WorkService();
        int[] a = new int[] { 20,18 };
        int b = 27;
        if (web.UpdateShelfId(a, Convert.ToInt32(txtStatus.Text),b))
        {
            Response.Write("<script>alert('aa')</script>");
        }
    }
}