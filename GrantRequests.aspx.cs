using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class GrantRequests : System.Web.UI.Page
{
    BookClass bc = new BookClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        //make sure the session is active
        if (Session["userKey"] != null)
        {
            //if good and if not a postback populate
            //drop down list
            if (!IsPostBack)
            {
                DataTable table = bc.GetAuthors();
                DropDownList1.DataSource = table;
                //text displays--value is stored
                DropDownList1.DataTextField = "AuthorName";
                DropDownList1.DataValueField = "AuthorKey";
                DropDownList1.DataBind();
                ListItem item = new ListItem("Choose an Author");
                DropDownList1.Items.Insert(0, item);
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void FillGrid()
    {
        if (!DropDownList1.SelectedItem.Text.Equals("Choose an Author"))
        {
            //get the key for the selected author from the values 
            //in the drop down list
            int key = int.Parse(DropDownList1.SelectedValue.ToString());
            //pass it to the class method and get the datatable
            //of results
            DataTable tbl = bc.Getbooks(key);
            //bind it to the Gridview
            GridView1.DataSource = tbl;
            GridView1.DataBind();
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
}