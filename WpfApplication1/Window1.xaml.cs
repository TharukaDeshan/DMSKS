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
using System.Windows.Shapes;

using System.Data.SqlClient;
using System.Data;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {

            InitializeComponent();

            if (A.identy == 1) {   // when Log in Main Store change Tab item and also button name //
                ((TabItem)TAB.Items[0]).Header = "Add Stock";
                B.Content = "Create New Stock";
                b2.Content = "Add Drugs to Stock";
                
            }
            
            Show_Accepted_Details();
            Show_Requested_Details();
            View_Near_To_Finish_Pharmaceuticals();

            add_Location_Combo();
            add_PhID_Comb();
            add_PhName_Comb();
            add_PhType_combo();
            
        }
        
        Int32 count;
        int S;
       
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        void Show_Accepted_Details() {
            Connection co = new Connection();
            co.add();

            SqlCommand cms = new SqlCommand("select stock_id from RequestNotification where ws_id='" + A.identy + "' and Status='" + 1 + "' or Status='" + 2 + "'", co.con); // used to select accepted request from IPD according to Log in Workstation //
            SqlDataReader dr = cms.ExecuteReader();
            int c = 0;
            while (dr.Read())
            {
                c += 1;
            }
            dr.Close();
            co.exit();
            if (c != 0)
            {
                if (A.identy != 2 && A.identy != 3 && A.identy!=1)   //   words/Lab/Theaters   //
                {
                    Accepted.Visibility = Visibility.Visible;
                    ReqNotify.Visibility = Visibility.Hidden;
                    View_Accepted_Details();
                }
                else if (A.identy == 3)      // IPD //
                {
                    Accepted.Visibility = Visibility.Visible;
                    ReqNotify.Visibility = Visibility.Visible;
                    View_Accepted_Details();
                }
                else if (A.identy == 2)   // OPD //
                {
                    Accepted.Visibility = Visibility.Visible;
                    ReqNotify.Visibility = Visibility.Hidden;
                    View_Accepted_Details();
                }

            }

            else
            {
                if (A.identy != 2 && A.identy != 3 && A.identy != 1)   //   words/Lab/Theaters   //
                {
                    Accepted.Visibility = Visibility.Visible;
                    ListBox3.Visibility = Visibility.Hidden;
                    ReqNotify.Visibility = Visibility.Hidden;
                    // View_Accepted_Details();
                }
                else if (A.identy == 3)      // IPD //
                {
                    Accepted.Visibility = Visibility.Visible;
                    ListBox3.Visibility = Visibility.Hidden;
                    ReqNotify.Visibility = Visibility.Visible;
                    //View_Accepted_Details();
                }
                else if (A.identy == 2)   // OPD //
                {
                    Accepted.Visibility = Visibility.Visible;
                    ListBox3.Visibility = Visibility.Hidden;
                    ReqNotify.Visibility = Visibility.Hidden;
                    //View_Accepted_Details();
                }
                

            }
        
        }


        void Show_Requested_Details() {
            Connection co = new Connection();
            co.add();

            SqlCommand cms = new SqlCommand("select stock_id from RequestNotification where Destination='" + A.identy + "' and Status='" + 0 + "' ", co.con); // used to select  request from IPD according to Log in Workstation //
            SqlDataReader dr = cms.ExecuteReader();
            int c = 0;
            while (dr.Read())
            {
                c += 1;
            }
            dr.Close();
            co.exit();
            if (c != 0)
            {
                if (A.identy != 2 && A.identy != 3 && A.identy != 1)   //   words/Lab/Theaters   //
                {
                    Accepted.Visibility = Visibility.Visible;
                    ReqNotify.Visibility = Visibility.Hidden;
                    
                }
                else if (A.identy == 3)      // IPD //
                {
                    Accepted.Visibility = Visibility.Visible;
                    ReqNotify.Visibility = Visibility.Visible;
                    ListBox2.Visibility = Visibility.Visible;
                    View_Request_Details();
                }
                else if (A.identy == 2)   // OPD //
                {
                    Accepted.Visibility = Visibility.Visible;
                    ReqNotify.Visibility = Visibility.Hidden;
                    
                }
                else if (A.identy == 1)
                {
                    ReqNotify.Visibility = Visibility.Hidden;
                    MSRequest.Visibility = Visibility.Visible;
                    View_Request_Details();
                }
            }

            else
            {
                if (A.identy != 2 && A.identy != 3 && A.identy != 1)   //   words/Lab/Theaters   //
                {
                    Accepted.Visibility = Visibility.Visible;
                    ReqNotify.Visibility = Visibility.Hidden;

                }
                else if (A.identy == 3)      // IPD //
                {
                    Accepted.Visibility = Visibility.Visible;
                    ReqNotify.Visibility = Visibility.Visible;
                    ListBox2.Visibility = Visibility.Hidden;
                   
                }
                else if (A.identy == 2)   // OPD //
                {
                    Accepted.Visibility = Visibility.Visible;
                    ReqNotify.Visibility = Visibility.Hidden;

                }
                else if (A.identy == 1)
                {
                    ReqNotify.Visibility = Visibility.Hidden;
                    MSRequest.Visibility = Visibility.Visible;
                    ListBox4.Visibility = Visibility.Hidden;
                    
                }
                

            }
        
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        void View_Request_Details() {
            Connection co = new Connection();
            try
            {
                co.add();
                
                SqlCommand command = new SqlCommand("select * from RequestNotification where Status='"+0+"'", co.con); // used to select didn't see Requests //
                SqlDataReader DR = command.ExecuteReader();
                while (DR.Read()) {
                    DateTime date = DR.GetDateTime(0);
                    int word = DR.GetInt32(3); // used to store Requested workstaion ID //
                    int id = DR.GetInt32(5);  // used to store Request ID //
                    
                    if(word!=1 && word!=2 && word!=3) 
                        ListBox2.Items.Add("Request_ID: "+id+" From Workstation_ID: "+word);       //  used to store Words/Lab/Theaters Requests  //
                    else if (word == 2 || word == 3) {
                        ListBox4.Items.Add("Request_ID: " + id + " From Workstation_ID: " + word); //  used to store IPD/OPD Requests  //
                    }
                    
                }
                DR.Close();
                co.exit();
            }
            catch (Exception e) {
                MessageBox.Show("Incorrect");
            }
        }

        void View_Accepted_Details()
        {
            Connection co = new Connection();
            try
            {
                co.add();

                SqlCommand command = new SqlCommand("select * from RequestNotification where Status='" + 1 + "'", co.con); // used to select accepted request details //
                SqlDataReader DR = command.ExecuteReader();
                String v;
                while (DR.Read())
                {
                    //DateTime date = DR.GetDateTime(0);
                    int word = DR.GetInt32(3);
                    int id = DR.GetInt32(5);
                    if (word != 2 && word != 3)
                        v = "IPD";
                    else
                        v = "Main Store";
                    
                    if(A.identy==word)
                        ListBox3.Items.Add("Request_ID: "+ id +" From Workstation_ID:"+ word+" Accepted by "+v); // used to show requests that are acepted by IPD //

                }
                DR.Close();

                SqlCommand command1 = new SqlCommand("select * from RequestNotification where Status='" + 2 + "'", co.con); // used to select Rejected request details //
                SqlDataReader DR1 = command1.ExecuteReader();
                while (DR1.Read())
                {
                    //DateTime date = DR.GetDateTime(0);
                    int word1 = DR1.GetInt32(3);
                    int id1 = DR1.GetInt32(5);
                    
                    if (A.identy == word1)
                        ListBox3.Items.Add("Request_ID: " + id1 + " From Workstation_ID:" + word1 + " Rejected by IPD"); //used to view Rejected Request details //

                }
                DR1.Close();

                co.exit();
            }
            catch (Exception e)
            {
                MessageBox.Show("Incorrect");
            }
        }


        void View_Near_To_Finish_Pharmaceuticals() {    //////////////////////used to view near to finish drugs ///////////////////
            try
            {
                Connection c = new Connection();
                c.add();

                SqlCommand cmd = new SqlCommand("select ph_id,ph_name,ExistQuentity from PharmaceuticalWorkstation where ws_id='" + A.identy + "' and ExistQuentity<=StaderdQuentity;", c.con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("PharmaceuticalWorkstation");
                da.Fill(dt);
                NearToFinish.ItemsSource = dt.DefaultView;
            }
            catch (Exception w)
            {
                MessageBox.Show("Cant view Near to finish Details");
            }
            
        }


        /// <summary>
        /// ///////////////////////////////////////Add combo boxes/////////////////////////////////////
        /// </summary>
        void add_Location_Combo() {
            try
            {
                Connection myConnection = new Connection();
                myConnection.add();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select ws_name from Workstaion", myConnection.con);
                myReader = myCommand.ExecuteReader();
                //string userText = MainMDI.globalstring;
                while (myReader.Read())
                {
                    Location.Items.Add(myReader.GetString(0));
                }
            }
            catch (Exception b)
            {
                MessageBox.Show(b.ToString());
            }
        }

        void add_PhID_Comb()
        {
            try
            {
                Connection myConnection = new Connection();
                myConnection.add();

                if (Location.SelectedIndex != -1) {
                    Object selectedItem = Location.SelectedItem;
                    SqlCommand cmd = new SqlCommand("select ws_id from Workstaion where ws_name='" + selectedItem.ToString() + "'", myConnection.con);
                    int a = (Int32)cmd.ExecuteScalar();

                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand("select ph_ID from PharmaceuticalWorkstation where ws_id='" + a + "' ", myConnection.con);
                    myReader = myCommand.ExecuteReader();
                    //string userText = MainMDI.globalstring;
                    while (myReader.Read())
                    {
                        DrugID.Items.Add(myReader.GetInt32(0));
                    }
                    myReader.Close();
                }
                
            }
            catch (Exception b)
            {
                MessageBox.Show(b.ToString());
            }
        }

        void add_PhName_Comb()
        {
            try
            {
                Connection myConnection = new Connection();
                myConnection.add();

                if (Location.SelectedIndex != -1)
                {
                    Object selectedItem = Location.SelectedItem;
                    SqlCommand cmd = new SqlCommand("select ws_id from Workstaion where ws_name='" + selectedItem.ToString() + "'", myConnection.con);
                    int a = (Int32)cmd.ExecuteScalar();

                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand("select ph_name from PharmaceuticalWorkstation where ws_id='" + a + "' ", myConnection.con);
                    myReader = myCommand.ExecuteReader();
                    //string userText = MainMDI.globalstring;
                    while (myReader.Read())
                    {
                        DrugName.Items.Add(myReader.GetString(0));
                    }
                    myReader.Close();
                }

            }
            catch (Exception b)
            {
                MessageBox.Show(b.ToString());
            }
        }


        void add_PhType_combo() {
            if (Location.SelectedIndex != -1) {
                DrugType.Items.Add("Consumable");
                DrugType.Items.Add(" NonConcumable");
            }
           
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void B_Click(object sender, RoutedEventArgs e) // When clicked "Create new Request" Button //
        {
            if (A.identy == 1)
            {
                Dposition.Visibility = Visibility.Visible;
                pos.Visibility = Visibility.Visible;
                ExpireDate.Visibility = Visibility.Visible;
                ExDate.Visibility = Visibility.Visible;
                //t1.Text != null && t2.Text != null && ExDate.Text != null && pos.Text != null

                
            }
            
            l1.Visibility = Visibility.Visible;
            l2.Visibility = Visibility.Visible;
            t1.Visibility = Visibility.Visible;
            t2.Visibility = Visibility.Visible;
            b1.Visibility = Visibility.Visible;
            b2.Visibility = Visibility.Visible;
            B.Visibility = Visibility.Hidden;
            grid.Visibility = Visibility.Visible;
            grid.ItemsSource = null;
            ChangeDetails.Visibility = Visibility.Visible;
            DeleteDetails.Visibility = Visibility.Visible;
            
            
            Connection conn = new Connection();
            conn.add();
            SqlCommand sq = new SqlCommand("select count(stock_id) from Stock", conn.con);
            int cou = (Int32)sq.ExecuteScalar();
            if (cou == 0)
            {
                count = 0;
            }
            else {
                SqlCommand sq1 = new SqlCommand("select top 1 * from Stock order by stock_id desc", conn.con);
                count = (Int32)sq1.ExecuteScalar();
            }
            
            count++;
            conn.exit();
        }

        private void b1_Click(object sender, RoutedEventArgs e)// used to add pharmaceuticals to created stock //
        {
            try
            {
                Connection c = new Connection();
                c.add();
                if (A.identy == 1)    /// if user log in using Main Store ///
                {
                    if (t1.Text != "" && t2.Text != "" && ExDate.Text != "" && pos.Text != "")
                    {
                        SqlCommand cmmand = new SqlCommand("select ph_id from Pharmaceuticals  where ph_id='" + t1.Text + "' ", c.con);
                        SqlDataReader d = cmmand.ExecuteReader();


                        int count1 = 0;

                        while (d.Read())
                        {
                            count1 += 1;
                        }
                        d.Close();
                        if (count1 == 1)
                        {
                            SqlCommand sqcom = new SqlCommand("select ph_name from Pharmaceuticals where ph_id='" + t1.Text + "' ", c.con);
                            String s1 = sqcom.ExecuteScalar().ToString();

                            DateTime today = DateTime.Today;
                            if (ExDate.Text == null) {
                                MessageBox.Show("invalid");
                                
                            }

                            SqlCommand cmd = new SqlCommand("insert into Stock(stock_id,ph_id,ph_quantity,quentity,ph_name,ph_expire_date,ph_position) values('" + count + "','" + t1.Text + "','" + t2.Text + "','" + t2.Text + "','" + s1 + "','" + ExDate.Text + "','" + pos.Text + "');", c.con);

                            SqlDataReader dr = cmd.ExecuteReader();
                            dr.Close();

                            SqlCommand sq1 = new SqlCommand("select ph_id,ph_name,ph_quantity,ph_expire_date,ph_position from Stock where stock_id='" + count + "' ", c.con);
                            SqlDataAdapter da = new SqlDataAdapter(sq1);

                            DataTable dt = new DataTable("Stock");
                            da.Fill(dt);
                            grid.ItemsSource = dt.DefaultView;

                        }
                        else
                            MessageBox.Show("Parmecutical ID is in valid!");
                        t1.Text = "";
                        t2.Text = "";
                        ExDate.Text = "";
                        pos.Text = "";
                        c.exit();
                    }
                    else
                    {
                        MessageBox.Show("All data must input!");

                    }
                }
                else {
                    if (t1.Text != "" && t2.Text != "")
                    {
                        SqlCommand cm = new SqlCommand("select ph_id from Pharmaceuticals  where ph_id='" + t1.Text + "' ", c.con);
                        SqlDataReader d = cm.ExecuteReader();


                        int count1 = 0;

                        while (d.Read())
                        {
                            count1 += 1;
                        }
                        d.Close();
                        if (count1 == 1)
                        {
                            SqlCommand sq = new SqlCommand("select ph_name from Pharmaceuticals where ph_id='" + t1.Text + "' ", c.con);
                            String s1 = sq.ExecuteScalar().ToString();

                            SqlCommand cmd = new SqlCommand("insert into Stock(stock_id,ph_id,ph_quantity,quentity,ph_name) values('" + count + "','" + t1.Text + "','" + t2.Text + "','" + t2.Text + "','" + s1 + "');", c.con);

                            SqlDataReader dr = cmd.ExecuteReader();
                            dr.Close();

                            SqlCommand sq1 = new SqlCommand("select ph_id,ph_name,ph_quantity from Stock where stock_id='" + count + "' ", c.con);
                            SqlDataAdapter da = new SqlDataAdapter(sq1);

                            DataTable dt = new DataTable("Stock");
                            da.Fill(dt);
                            grid.ItemsSource = dt.DefaultView;

                        }
                        else
                            MessageBox.Show("Parmecutical ID is in valid!");
                        t1.Text = "";
                        t2.Text = "";
                        c.exit();
                    }
                    else {
                        MessageBox.Show("All data must input!!!");
                    }
                
                }
                
            }

            catch (Exception) {
                MessageBox.Show("check Input!!");
            }
            
        }
        private void ChangeDetails_Click(object sender, RoutedEventArgs e) ///used to change Drugs details///
        {
            if (t1.Text == "")
                MessageBox.Show("Please enter drug_id for save changes");
            else
            {
                Connection c = new Connection();
                c.add();
                try
                {
                    
                    if (t2.Text != "") {
                        SqlCommand cmd1 = new SqlCommand("update Stock set ph_quantity='"+t2.Text+"'  where stock_id='"+count+"' and ph_id='"+t1.Text+"' ", c.con);
                        SqlDataReader dr1 = cmd1.ExecuteReader();
                        dr1.Close();

                        SqlCommand cmd2 = new SqlCommand("update Stock set quentity='" + t2.Text + "'  where stock_id='" + count + "' and ph_id='"+t1.Text+"' ", c.con);
                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        dr2.Close();
                    }
                    else if (pos.Text != "") {
                        SqlCommand cmd3 = new SqlCommand("update Stock set ph_position='" + pos.Text + "' where stock_id='"+count+"' and ph_id='"+t1.Text+"'", c.con);
                        SqlDataReader dr3 = cmd3.ExecuteReader();
                        dr3.Close();
                    }
                    else if (ExDate.Text != "")
                    {
                        SqlCommand cmd4 = new SqlCommand("update Stock set ph_expire_date='" + ExDate.Text + "' where stock_id='" + count + "' and ph_id='"+t1.Text+"' ", c.con);
                        SqlDataReader dr4 = cmd4.ExecuteReader();
                        dr4.Close();
                    }

                    set_details();

                    t2.Text = "";
                    pos.Text = "";
                    ExDate.Text = "";
                    t1.Text = "";
                    t2.Text = "";
                    
                }
                    

                catch (Exception w2) 
                {
                    MessageBox.Show("Invalid Drug ID!!!");
                
                }
            }
        }
        void set_details() {
            Connection c = new Connection();
            c.add();
            if (A.identy == 1)
            {
                SqlCommand sq1 = new SqlCommand("select ph_id,ph_name,ph_quantity,ph_expire_date,ph_position from Stock where stock_id='" + count + "' ", c.con);
                SqlDataAdapter da = new SqlDataAdapter(sq1);

                DataTable dt = new DataTable("Stock");
                da.Fill(dt);
                grid.ItemsSource = dt.DefaultView;

            }
            else
            {
                SqlCommand sq1 = new SqlCommand("select ph_id,ph_name,ph_quantity from Stock where stock_id='" + count + "' ", c.con);
                SqlDataAdapter da = new SqlDataAdapter(sq1);

                DataTable dt = new DataTable("Stock");
                da.Fill(dt);
                grid.ItemsSource = dt.DefaultView;
            }
        }
        private void DeleteDetails_Click(object sender, RoutedEventArgs e)  //delete drugs details //
        {
            try
            {
                Connection c=new Connection();
                c.add();
                if (t1.Text != "")
                {
                    SqlCommand com = new SqlCommand("delete Stock where stock_id='" + count + "' and ph_id='" + t1.Text + "'", c.con);
                    SqlDataReader dr = com.ExecuteReader();
                    dr.Close();
                }
                else {
                    MessageBox.Show("Enter Drug_id");
                }

                if (A.identy == 1)
                {
                    SqlCommand sq1 = new SqlCommand("select ph_id,ph_name,ph_quantity,ph_expire_date,ph_position from Stock where stock_id='" + count + "' ", c.con);
                    SqlDataAdapter da = new SqlDataAdapter(sq1);

                    DataTable dt = new DataTable("Stock");
                    da.Fill(dt);
                    grid.ItemsSource = dt.DefaultView;

                }
                else
                {
                    SqlCommand sq1 = new SqlCommand("select ph_id,ph_name,ph_quantity from Stock where stock_id='" + count + "' ", c.con);
                    SqlDataAdapter da = new SqlDataAdapter(sq1);

                    DataTable dt = new DataTable("Stock");
                    da.Fill(dt);
                    grid.ItemsSource = dt.DefaultView;
                }
                t2.Text = "";
                pos.Text = "";
                ExDate.Text = "";
                t1.Text = "";
                t2.Text = "";
            }
            catch (Exception v)
            {
                MessageBox.Show("Invalid Drug ID!!!");
            }
        }

        private void b2_Click(object sender, RoutedEventArgs e) // used to send request //
        {
            B.Visibility = Visibility.Visible;

            DateTime dt = DateTime.Now;
            TimeSpan t = DateTime.Now.TimeOfDay;
            int state = 0;
            int dest=0;
            if (A.identy != 1 && A.identy != 2 && A.identy != 3)
                dest = 3;
            else if (A.identy == 2 || A.identy == 3)
                dest = 1;

            Connection conn=new Connection();
            conn.add();
            if (A.identy != 1) {
                SqlCommand cmd11 = new SqlCommand("insert into RequestNotification(RequestDate,stock_id,RequestTime,Status,ws_id,Destination) values('" + dt + "','" + count + "','" + t + "','" + state + "','" + A.identy + "','" + dest + "');", conn.con);
                SqlDataReader d11 = cmd11.ExecuteReader();
                d11.Close();
            }
            if (A.identy == 1) {
                labal.Content = "Do You sure you need to store such details";
                SqlCommand cm = new SqlCommand("update Stock set ws_id='" + 1 + "' where stock_id='" + count + "'", conn.con);
                SqlDataReader dr = cm.ExecuteReader();
                dr.Close();
            }
           
            //MessageBox.Show("Success");
            b2.Visibility = Visibility.Hidden;
            l1.Visibility = Visibility.Hidden;
            l2.Visibility = Visibility.Hidden;
            t1.Visibility = Visibility.Hidden;
            t2.Visibility = Visibility.Hidden;
            b1.Visibility = Visibility.Hidden;

            Dposition.Visibility = Visibility.Hidden;
            pos.Visibility = Visibility.Hidden;
            ExpireDate.Visibility = Visibility.Hidden;
            ExDate.Visibility = Visibility.Hidden;
            ChangeDetails.Visibility = Visibility.Hidden;
            DeleteDetails.Visibility = Visibility.Hidden;
            //////////////////////////////////////////
            //grid.Visibility = Visibility.Hidden;

            labal.Visibility = Visibility.Visible;
            Yes.Visibility = Visibility.Visible;
            No.Visibility = Visibility.Visible;

            conn.exit();

        }
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            labal.Visibility = Visibility.Hidden;
            Yes.Visibility = Visibility.Hidden;
            No.Visibility = Visibility.Hidden;
            grid.Visibility = Visibility.Hidden;

            if (A.identy == 1) {
                Connection conn = new Connection();
                conn.add();
                try
                {
                    
                    List<int> phid = new List<int>();
                    List<int> qenty = new List<int>();


                    SqlCommand sql = new SqlCommand("select * from Stock where stock_id='" + count + "'", conn.con);
                    SqlDataReader d = sql.ExecuteReader();
                    while (d.Read())
                    {
                        int a = d.GetInt32(1);
                        phid.Add(a);

                        int b = d.GetInt32(7);
                        qenty.Add(b);



                    }
                    d.Close();

                    for (int i = 0; i < phid.Count; i++)
                    {
                        SqlCommand cm = new SqlCommand("update PharmaceuticalWorkstation set ExistQuentity=ExistQuentity+'" + qenty[i] + "' where ws_id='" + A.identy + "' and ph_id='" + phid[i] + "'", conn.con);
                        SqlDataReader dr = cm.ExecuteReader();
                        dr.Close();
                    }
                    phid.Clear();
                    qenty.Clear();

                    View_Near_To_Finish_Pharmaceuticals();


                    //SqlCommand cm = new SqlCommand("update PharmaceuticalWorkstation set ExistQuentity= where stock_id='" + AcceptStockID + "'", conn.con);


                }
                catch (Exception n)
                {
                    MessageBox.Show(n.ToString());
                }
            }

        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            labal.Visibility = Visibility.Hidden;
            Yes.Visibility = Visibility.Hidden;
            No.Visibility = Visibility.Hidden;

            grid.Visibility = Visibility.Visible;
            b2.Visibility = Visibility.Visible;
            l1.Visibility = Visibility.Visible;
            l2.Visibility = Visibility.Visible;
            t1.Visibility = Visibility.Visible;
            t2.Visibility = Visibility.Visible;
            b1.Visibility = Visibility.Visible;

            Dposition.Visibility = Visibility.Visible;
            pos.Visibility = Visibility.Visible;
            ExpireDate.Visibility = Visibility.Visible;
            ExDate.Visibility = Visibility.Visible;
            ChangeDetails.Visibility = Visibility.Visible;
            DeleteDetails.Visibility = Visibility.Visible;

        }

        private void ListBox2_SelectionChanged(object sender, SelectionChangedEventArgs e) // used to view extended Request details of wards/lab/Theaters //
        {
            Phar_notify.Visibility = Visibility.Hidden;
            ReqNotify.Visibility = Visibility.Hidden;
            Accepted.Visibility = Visibility.Hidden;
            grid123.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Hidden;
            g11.Visibility = Visibility.Visible;

            try
            {
                String selItem = ListBox2.SelectedItem.ToString();
                String[] B = selItem.Split(' ');
                int x = Int32.Parse(B[1]);
                int y = Int32.Parse(B[4]);
                Connection C1 = new Connection();
                C1.add();
                SqlCommand cmds = new SqlCommand("select stock_id from RequestNotification where Request_id='" + x + "'", C1.con);
                S = (Int32)cmds.ExecuteScalar();

                SqlCommand cmds1 = new SqlCommand("select ph_id,ph_name,ph_quantity from Stock where stock_id='" + S + "' ", C1.con);
                SqlDataAdapter da = new SqlDataAdapter(cmds1);

                DataTable dt = new DataTable("Stock");
                da.Fill(dt);
                grid123.ItemsSource = dt.DefaultView;
                

                SqlCommand sq = new SqlCommand("select ws_name from Workstaion where ws_id='" + y + "'", C1.con);
                String wor = (String)sq.ExecuteScalar();

                C1.exit();
                label1.Content = "Requested from "+wor;
                label1.Visibility = Visibility.Visible;

                approve.Visibility = Visibility.Visible;
                Reject.Visibility = Visibility.Visible;

            }
            catch (Exception ex124) {
                //MessageBox.Show("Good");
            }
           
        }
        

        private void back_Click(object sender, RoutedEventArgs e) // when did Accept/Reject use to back to main menue //
        {
            if (ListBox2.SelectedIndex != -1)
                ListBox2.Items.RemoveAt(ListBox2.SelectedIndex);

            label1.Visibility = Visibility.Hidden;
            back.Visibility = Visibility.Hidden;
            ABack.Visibility = Visibility.Hidden;
            grid123.Visibility = Visibility.Hidden;
            g11.Visibility = Visibility.Hidden;
            approve.Visibility = Visibility.Hidden;
            Reject.Visibility = Visibility.Hidden;

            

            if (A.identy == 1) {
                Phar_notify.Visibility = Visibility.Visible;
                ReqNotify.Visibility = Visibility.Visible;
            }
            else if (A.identy == 3)
            {
                Phar_notify.Visibility = Visibility.Visible;
                ReqNotify.Visibility = Visibility.Visible;
                Accepted.Visibility = Visibility.Visible;
            }
            
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e) // used to change Request or Recived Pharmaceuticals quantity //
        {
            Connection c=new Connection();
            try
            {
                c.add();
                SqlCommand comm = new SqlCommand("update Stock set ph_quantity='" + t22.Text + "' where stock_id='" + S + "' and ph_id='" + t11.Text + "'", c.con);
                SqlDataReader dr = comm.ExecuteReader();
                dr.Close();
            }
            catch (Exception ex) {
                MessageBox.Show("Not working");
            }
            t11.Text = "";
            t22.Text = "";

            SqlCommand cmds1 = new SqlCommand("select ph_id,ph_name,ph_quantity from Stock where stock_id='" + S + "' ", c.con);
            SqlDataAdapter da = new SqlDataAdapter(cmds1);

            DataTable dt = new DataTable("Stock");
            da.Fill(dt);
            grid123.ItemsSource = dt.DefaultView;
            c.exit();


            
        }

        private void approve_Click(object sender, RoutedEventArgs e) ///when approve request by IPD or Main Store///
        {
            
            Connection cn = new Connection();
            cn.add();

            if (A.identy == 3 || A.identy==1)
            {
                try
                {
                    
                    SqlCommand cmd11 = new SqlCommand("update RequestNotification set Status='" + 1 + "' where stock_id='" + S + "'", cn.con);
                    SqlDataReader d11 = cmd11.ExecuteReader();
                    d11.Close();
                    
                    approve.Visibility = Visibility.Hidden;
                    Reject.Visibility = Visibility.Hidden;
                    g11.Visibility = Visibility.Hidden;
                    back.Visibility = Visibility.Visible;

                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    List<int> phid = new List<int>();
                    List<int> qenty = new List<int>();
                    List<int> Existqenty = new List<int>();

                    SqlCommand sql = new SqlCommand("select * from Stock where stock_id='" + S + "'", cn.con);
                    SqlDataReader d = sql.ExecuteReader();
                    while (d.Read())
                    {
                        int a = d.GetInt32(1);
                        phid.Add(a);

                        int b = d.GetInt32(7);
                        qenty.Add(b);
                    }
                    d.Close();

                    for (int j = 0; j < phid.Count; j++) {
                        SqlCommand scmd = new SqlCommand("select ExistQuentity from PharmaceuticalWorkstation where ws_id='" + A.identy + "' and ph_id='" + phid[j] + "'", cn.con);
                        Existqenty.Add((Int32)scmd.ExecuteScalar());
                    }

                    for (int i = 0; i < phid.Count; i++)
                    {
                        if (qenty[i] <= Existqenty[i])
                        {
                            SqlCommand cm = new SqlCommand("update PharmaceuticalWorkstation set ExistQuentity=ExistQuentity-'" + qenty[i] + "' where ws_id='" + A.identy + "' and ph_id='" + phid[i] + "'", cn.con);
                            SqlDataReader dr = cm.ExecuteReader();
                            dr.Close();
                        }
                        else {
                            SqlCommand cm123 = new SqlCommand("update Stock set ph_quantity='" + 0 + "' where stock_id='" + S + "' and ph_id='" + phid[i] + "'", cn.con);
                            SqlDataReader dr1 = cm123.ExecuteReader();
                            dr1.Close();
                        }
                           
                    }
                    phid.Clear();
                    qenty.Clear();
                    Existqenty.Clear();

                    View_Near_To_Finish_Pharmaceuticals();
                    //SqlCommand cm = new SqlCommand("update PharmaceuticalWorkstation set ExistQuentity= where stock_id='" + AcceptStockID + "'", conn.con);
                }
                catch (Exception ex1)
                {
                    MessageBox.Show("No exis");
                }
            }
             cn.exit();
         }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            Connection cn = new Connection();
            try
            {
                cn.add();
                SqlCommand cmd11 = new SqlCommand("update RequestNotification set Status='" + 2 + "' where stock_id='" + S + "'", cn.con);
                SqlDataReader d11 = cmd11.ExecuteReader();
                d11.Close();
                //Add_ListBox();
                
                approve.Visibility = Visibility.Hidden;
                Reject.Visibility = Visibility.Hidden;
                g11.Visibility = Visibility.Hidden;
                back.Visibility = Visibility.Visible;
                
                
                cn.exit();
            }
            catch (Exception exe)
            {
                MessageBox.Show("No exis");
            }
            
        }
        int AcceptStockID = 0;

        private void ListBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            Phar_notify.Visibility = Visibility.Hidden;
            ReqNotify.Visibility = Visibility.Hidden;
            Accepted.Visibility = Visibility.Hidden;
            grid123.Visibility = Visibility.Visible;
            ABack.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Hidden;

           

            try
            {
                String selItem = ListBox3.SelectedItem.ToString();
                String[] B = selItem.Split(' ');
                int x = Int32.Parse(B[1]);
                String state = B[4];
                String k;
                if (A.identy == 2 || A.identy == 3)
                    k = "Main Store";
                else
                    k = "IPD";
                if (state == "Accepted")
                {
                    g11.Visibility = Visibility.Visible;
                    label1.Content = "Request accepted by "+k;
                    Accept.Visibility = Visibility.Visible;
                }
                else {
                    g11.Visibility = Visibility.Hidden;
                    label1.Content = "Request rejected by "+k;
                    Cancel.Visibility = Visibility.Visible;
                }
                Connection C1 = new Connection();
                C1.add();
                SqlCommand cmds = new SqlCommand("select stock_id from RequestNotification where Request_id='" + x + "'", C1.con);
                S = (Int32)cmds.ExecuteScalar();
                if (state == "Accepted")
                    AcceptStockID = S;

                SqlCommand cmds1 = new SqlCommand("select ph_id,ph_name,ph_quantity from Stock where stock_id='" + S + "' ", C1.con);
                SqlDataAdapter da = new SqlDataAdapter(cmds1);

                DataTable dt = new DataTable("Stock");
                da.Fill(dt);
                grid123.ItemsSource = dt.DefaultView;
                C1.exit();

                
                
                label1.Visibility = Visibility.Visible;
                

            }
            catch (Exception ex124)
            {
                //MessageBox.Show("Good");
            }
        }

        private void ListBox4_SelectionChanged(object sender, SelectionChangedEventArgs e) // used to view extended request details from IPD,OPD
        {
            Phar_notify.Visibility = Visibility.Hidden;
            ReqNotify.Visibility = Visibility.Hidden;
            
            MSRequest.Visibility = Visibility.Hidden;
            grid123.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Hidden;
            g11.Visibility = Visibility.Visible;

            try
            {
                String selItem = ListBox4.SelectedItem.ToString();
                String[] B = selItem.Split(' ');
                int x = Int32.Parse(B[1]);
                int y = Int32.Parse(B[4]);
                Connection C1 = new Connection();
                C1.add();
                SqlCommand cmds = new SqlCommand("select stock_id from RequestNotification where Request_id='" + x + "'", C1.con);
                S = (Int32)cmds.ExecuteScalar();

                SqlCommand cmds1 = new SqlCommand("select ph_id,ph_name,ph_quantity from Stock where stock_id='" + S + "' ", C1.con);
                SqlDataAdapter da = new SqlDataAdapter(cmds1);

                DataTable dt = new DataTable("Stock");
                da.Fill(dt);
                grid123.ItemsSource = dt.DefaultView;


                SqlCommand sq = new SqlCommand("select ws_name from Workstaion where ws_id='" + y + "'", C1.con);
                String wor = (String)sq.ExecuteScalar();

                C1.exit();
                label1.Content = "Requested from " + wor;
                label1.Visibility = Visibility.Visible;

                approve.Visibility = Visibility.Visible;
                Reject.Visibility = Visibility.Visible;

            }
            catch (Exception ex124)
            {
                //MessageBox.Show("Good");
            }
        }

       
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            Connection cn = new Connection();
            cn.add();
            try
            {
                if (ListBox3.SelectedIndex != -1)
                    ListBox3.Items.RemoveAt(ListBox3.SelectedIndex);
                SqlCommand cmd11 = new SqlCommand("update RequestNotification set Status='" + 3 + "' where stock_id='" + S + "'", cn.con);
                SqlDataReader d11 = cmd11.ExecuteReader();
                d11.Close();

                Accept.Visibility = Visibility.Hidden;
                approve.Visibility = Visibility.Hidden;
                Reject.Visibility = Visibility.Hidden;
                g11.Visibility = Visibility.Hidden;
                Cancel.Visibility = Visibility.Hidden;
                ABack.Visibility = Visibility.Visible;
                grid123.Visibility = Visibility.Hidden;
                

                Connection c = new Connection();
                c.add();


                SqlCommand cmds1 = new SqlCommand("select ph_id,ph_name,ph_quantity,ph_expire_date,ph_position from Stock where stock_id='" + S + "' ", c.con);
                SqlDataAdapter da = new SqlDataAdapter(cmds1);

                DataTable dt = new DataTable("Stock");
                da.Fill(dt);
                AcceptDrugsDetails.ItemsSource = dt.DefaultView;
                
                c.exit();

                AcceptDrugsDetails.Visibility = Visibility.Visible;
                PositionAdd.Visibility = Visibility.Visible;
                ADD.Visibility = Visibility.Visible;

            }
            catch (Exception ex1)
            {
                MessageBox.Show("No exis");
            }
            cn.exit();
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Connection cn = new Connection();
            cn.add();
            try
            {
                if (ListBox3.SelectedIndex != -1)
                    ListBox3.Items.RemoveAt(ListBox3.SelectedIndex);
                SqlCommand cmd11 = new SqlCommand("update RequestNotification set Status='" + 4 + "' where stock_id='" + S + "'", cn.con);
                SqlDataReader d11 = cmd11.ExecuteReader();
                d11.Close();

                Accept.Visibility = Visibility.Hidden;
                approve.Visibility = Visibility.Hidden;
                Reject.Visibility = Visibility.Hidden;
                g11.Visibility = Visibility.Hidden;
                Cancel.Visibility = Visibility.Hidden;
                ABack.Visibility = Visibility.Visible;
                ADD.Visibility = Visibility.Hidden;
                //AcceptDrugsDetails.Visibility = Visibility.Hidden;
                PositionAdd.Visibility = Visibility.Hidden;
            }
            catch (Exception ex1)
            {
                MessageBox.Show("No exis");
            }
            cn.exit();
            
        }

        private void ABack_Click(object sender, RoutedEventArgs e)
        {
            ListBox3.SelectedIndex = -1;
            ABack.Visibility = Visibility.Hidden;
            back.Visibility = Visibility.Hidden;
            Accept.Visibility = Visibility.Hidden;
            Cancel.Visibility = Visibility.Hidden;
            grid123.Visibility = Visibility.Hidden;
            g11.Visibility = Visibility.Hidden;
            AcceptDrugsDetails.Visibility = Visibility.Hidden;
            ADD.Visibility = Visibility.Hidden;
            PositionAdd.Visibility = Visibility.Hidden;

            if (A.identy == 1) {
                Phar_notify.Visibility = Visibility.Visible;
                ReqNotify.Visibility = Visibility.Visible;
            }
            else if (A.identy == 3)
            {
                Phar_notify.Visibility = Visibility.Visible;
                ReqNotify.Visibility = Visibility.Visible;
                Accepted.Visibility = Visibility.Visible;
            }
            else {
                Phar_notify.Visibility = Visibility.Visible;
                Accepted.Visibility = Visibility.Visible;
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void adds_Click(object sender, RoutedEventArgs e)
        {
            Connection c = new Connection();
            c.add();
            try
            {
                SqlCommand com = new SqlCommand("update Stock set ph_position='" +T2.Text + "' where stock_id='" + AcceptStockID + "' and ph_id='" + T1.Text + "'", c.con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Close();

                SqlCommand cmds1 = new SqlCommand("select ph_id,ph_name,ph_quantity,ph_expire_date,ph_position from Stock where stock_id='" + AcceptStockID + "' ", c.con);
                SqlDataAdapter da = new SqlDataAdapter(cmds1);

                DataTable dt = new DataTable("Stock");
                da.Fill(dt);
                AcceptDrugsDetails.ItemsSource = dt.DefaultView;
                T1.Text = null;
                T2.Text = null;
            }
            catch (Exception Ep) {
                MessageBox.Show("not");
            }
           
            c.exit();
            //SqlCommand com = new SqlCommand("insert into Stock(ph_position) values('" + T2 + "') where ph_id='"+T1+"';",c.con);

        }

        private void ADD_Click(object sender, RoutedEventArgs e)
        {
            ADD.Visibility = Visibility.Hidden;
            PositionAdd.Visibility = Visibility.Hidden;
            Connection conn = new Connection();
            conn.add();
            try
            {
                SqlCommand cmd11 = new SqlCommand("update Stock set ws_id='" + A.identy + "' where stock_id='" + AcceptStockID + "'", conn.con);
                SqlDataReader d11 = cmd11.ExecuteReader();
                d11.Close();
                List<int> phid=new List<int>();
                List<int> qenty = new List<int>();
                

                SqlCommand sql=new SqlCommand("select * from Stock where stock_id='"+AcceptStockID+"'",conn.con);
                SqlDataReader d=sql.ExecuteReader();
                while (d.Read()) {
                    int a = d.GetInt32(1);
                    phid.Add(a);
                    
                    int b = d.GetInt32(7);
                    qenty.Add(b);

                    
                    
                }
                d.Close();
                
                for (int i = 0; i < phid.Count; i++) {
                    SqlCommand cm = new SqlCommand("update PharmaceuticalWorkstation set ExistQuentity=ExistQuentity+'" + qenty[i] + "' where ws_id='" + A.identy + "' and ph_id='" + phid[i] + "'", conn.con);
                    SqlDataReader dr = cm.ExecuteReader();
                    dr.Close();
                }
                phid.Clear();
                qenty.Clear();

                View_Near_To_Finish_Pharmaceuticals();
                //SqlCommand cm = new SqlCommand("update PharmaceuticalWorkstation set ExistQuentity= where stock_id='" + AcceptStockID + "'", conn.con);
                
                
            }
            catch (Exception n)
            {
                MessageBox.Show(n.ToString());
            }
            
        }

        private void Location_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Connection c = new Connection();
                c.add();
                Object selectedItem = Location.SelectedItem;

                header.Content = selectedItem.ToString()+" Drug Warehouse";
                header.Visibility = Visibility.Visible;

                SqlCommand cmd = new SqlCommand("select ws_id from Workstaion where ws_name='" + selectedItem.ToString() + "'", c.con);
                int a = (Int32)cmd.ExecuteScalar();

                SqlCommand cmds1 = new SqlCommand("select ph_id,ph_name,ExistQuentity,ph_type from PharmaceuticalWorkstation where ws_id='" + a + "' ", c.con);
                SqlDataAdapter da = new SqlDataAdapter(cmds1);

                DataTable dt = new DataTable("PharmaceuticalWorkstation");
                da.Fill(dt);
                database.ItemsSource = dt.DefaultView;

                //DrugID.SelectedIndex = -1;
                DrugID.Items.Clear();
                add_PhID_Comb();

                DrugName.Items.Clear();
                add_PhName_Comb();

                DrugType.Items.Clear();
                add_PhType_combo();
            }
            catch (Exception e111) 
            {
                MessageBox.Show(e111.ToString());
            }
            
           

        }

        private void DrugID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Connection c = new Connection();
                c.add();
                Object selectedItem1 = Location.SelectedItem;
                SqlCommand cmd = new SqlCommand("select ws_id from Workstaion where ws_name='" + selectedItem1.ToString() + "'", c.con);
                int a = (Int32)cmd.ExecuteScalar();

                Object selectedItem2 = DrugID.SelectedItem;
                int b = (Int32)selectedItem2;

                //Location.SelectedIndex = -1;

                SqlCommand cmds1 = new SqlCommand("select ph_id,ph_name,ExistQuentity,ph_type from PharmaceuticalWorkstation where ws_id='" + a + "' and ph_id='" + b + "'", c.con);
                SqlDataAdapter da = new SqlDataAdapter(cmds1);

                DataTable dt = new DataTable("PharmaceuticalWorkstation");
                da.Fill(dt);
                database.ItemsSource = dt.DefaultView;
            }
            catch (Exception e123)
            {
                //MessageBox.Show(e.ToString());
            }
            
            
            
            
            
        }

        private void DrugName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Connection c = new Connection();
                c.add();
                Object selectedItem1 = Location.SelectedItem;
                SqlCommand cmd = new SqlCommand("select ws_id from Workstaion where ws_name='" + selectedItem1.ToString() + "'", c.con);
                int a = (Int32)cmd.ExecuteScalar();

                Object selectedItem2 = DrugName.SelectedItem;
                String b = selectedItem2.ToString();

                //Location.SelectedIndex = -1;

                SqlCommand cmds1 = new SqlCommand("select ph_id,ph_name,ExistQuentity,ph_type from PharmaceuticalWorkstation where ws_id='" + a + "' and ph_name='" + b + "'", c.con);
                SqlDataAdapter da = new SqlDataAdapter(cmds1);

                DataTable dt = new DataTable("PharmaceuticalWorkstation");
                da.Fill(dt);
                database.ItemsSource = dt.DefaultView;
            }
            catch (Exception e123)
            {
                //MessageBox.Show(e.ToString());
            }
        }

        private void DrugType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Connection c = new Connection();
                c.add();
                Object selectedItem1 = Location.SelectedItem;
                SqlCommand cmd = new SqlCommand("select ws_id from Workstaion where ws_name='" + selectedItem1.ToString() + "'", c.con);
                int a = (Int32)cmd.ExecuteScalar();

                Object selectedItem2 = DrugType.SelectedItem;
                String b = selectedItem2.ToString();

                //Location.SelectedIndex = -1;

                SqlCommand cmds1 = new SqlCommand("select ph_id,ph_name,ExistQuentity,ph_type from PharmaceuticalWorkstation where ws_id='" + a + "' and ph_type='" + b + "'", c.con);
                SqlDataAdapter da = new SqlDataAdapter(cmds1);

                DataTable dt = new DataTable("PharmaceuticalWorkstation");
                da.Fill(dt);
                database.ItemsSource = dt.DefaultView;
            }
            catch (Exception e123)
            {
                //MessageBox.Show(e.ToString());
            }
        }

        

        

       
        

        

        

        

       
    }
}
