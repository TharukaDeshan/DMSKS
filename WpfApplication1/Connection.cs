using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace WpfApplication1
{
    class Connection
    {
        public SqlConnection con = null;
        
        public void add() {
            con = new SqlConnection("Data Source=.;Initial Catalog=GroupProgect;Integrated Security=True");
            con.Open();
        }

        public void exit() {
            con.Close();
        }
    }
}
