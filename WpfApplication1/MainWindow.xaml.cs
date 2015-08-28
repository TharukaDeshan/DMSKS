using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data.SqlClient;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class A {
        public static int identy;
        A() {
            identy = 0;
        }
        public static void set(int x) {
            A.identy = 0;
        }
    }

    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
            date.Content = DateTime.Now.ToLongDateString();
            String time = DateTime.Now.ToString("HH:mm tt");
            
            string[] s= time.Split(' ');
            string[] x = s[0].Split(':');
            int y = Int32.Parse(x[0]);
            if (y > 12) {
                y = y - 12;
            }

            if (s[1] == "AM")
            {
                lab.Content = "Good Morning..!";
            }
            else {
                if (y <= 4)
                {
                    lab.Content = "Good Afternoon..!";
                }
                else {
                    lab.Content = "Good Evening..!";
                }
            }
            
            
           
            
        }
       
        private void b1_Click(object sender, RoutedEventArgs e)
        {
            
            Connection c = new Connection();
            c.add();
            String user_name = t1.Text;
            
            String pass_word = p.Password;

            SqlCommand cmd = new SqlCommand("select user_name,password from login where user_name='" + user_name + "' and password='" + pass_word + "' ",c.con);
            SqlDataReader dr;
            dr= cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                count += 1;
            }
            dr.Close();
            if (count == 1)
            {
                //MessageBox.Show("Login sucess ");
                SqlCommand cm = new SqlCommand("select emp_id from login where user_name='" + user_name + "' and password='" + pass_word + "'",c.con);
                int a = (Int32)cm.ExecuteScalar();
                SqlCommand cm1=new SqlCommand("select ws_name from Workstaion where ws_id='"+a+"'",c.con);
                String works = (String)cm1.ExecuteScalar();
                A.identy = a;
                if (a == 3 || a == 4 || a == 5 || a == 6 || a == 7 || a==1 ||a==2) {
                    Window1 a1 = new Window1();
                    a1.Title = "Log in "+works;
                    a1.Show();
                    this.Hide();
                }
                
            }
            else
            {
                MessageBox.Show("User Name or Password is Incorrect!");

            }
            c.exit();
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
