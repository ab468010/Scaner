using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;
public partial class Page_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql = "SELECT * FROM dbo.SystemUser";
        NpgsqlConnection con = new NpgsqlConnection("server=localhost;uid=sa;pwd=adc468010;database=ManageSystem");
        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        this.GridView1.DataSource = ds;
        this.GridView1.DataMember = ds.Tables[0].ToString();
        this.GridView1.DataBind();
    }
}