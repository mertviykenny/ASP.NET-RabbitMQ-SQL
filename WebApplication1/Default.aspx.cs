using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace WebApplication1
{

    public partial class _Default : Page
    {

        public static string getConnectionString()
        {
            return Properties.Settings.Default.conn;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {

        }
    }
}