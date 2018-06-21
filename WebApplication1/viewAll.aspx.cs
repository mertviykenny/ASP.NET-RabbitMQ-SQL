using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
namespace WebApplication1
{
    public partial class viewAll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string conn = _Default.getConnectionString();
            using (SqlConnection connection = new SqlConnection(conn))
            {
                int i = 0;
                SqlCommand command = new SqlCommand("select * from [dbo].[myObjectTable]", connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                int s=reader.FieldCount;
                while (reader.Read())
                {
                    var c1 = new HtmlTableCell
                    {
                        InnerText = reader.GetInt32(0).ToString()
                };
                    var c2 = new HtmlTableCell
                    {
                        InnerText = reader.GetString(1)
                    };
                    var c3 = new HtmlTableCell
                    {
                        InnerText = reader.GetString(2)
                    };

                    HtmlTableRow r = new HtmlTableRow();
                    r.Cells.Add(c1);
                    r.Cells.Add(c2);
                    r.Cells.Add(c3);
                    viewAllTable.Rows.Add(r);
                    i++;
                }
                command.Connection.Close();
            }
        }
    }
}