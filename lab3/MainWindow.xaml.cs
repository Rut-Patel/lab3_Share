/*
 Rut Patel
 Refernce form Week 7 Lab3 video
 */

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
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        ObservableCollection<share> list = new ObservableCollection<share>();
        public MainWindow()
        {
            InitializeComponent();

 

    }

        private void btnCreateEntry_Click(object sender, RoutedEventArgs e)
        {
            //List<share> list = new List<share>(); 
            try
            {
                string name = tbName.Text;
                int numOfShares = int.Parse(tbShares.Text);
                string date = tbDate.SelectedDate.ToString();
                string shareType = "";

                
                

                if (radCommon.IsChecked == true)
                {
                    shareType = "common";
                    CommonShare type = new CommonShare(tbName.Text, tbDate.Text,numOfShares, "Common");
                    list.Add(type);
                    lstViewShares.ItemsSource = list;
                    shareType = "common";
                }
                else if(radPreferred.IsChecked == true)
                {
                     shareType = "preferred";
                    PreferredShare type = new PreferredShare(tbName.Text, tbDate.Text,numOfShares, "Preferred");
                    list.Add(type);
                    lstViewShares.ItemsSource = list;
                   
                }

                //connecting to databse
                string connectString = Properties.Settings.Default.connect_string;
                SqlConnection cn = new SqlConnection(connectString);
                cn.Open();

                String inserQuery = "INSERT INTO Buy(b_name, numOfShares, date, type) VALUES('" + name + "', '" + numOfShares + "','" + date + "', '" + shareType + "')";
                SqlCommand command = new SqlCommand(inserQuery, cn);
                command.ExecuteNonQuery();

                string selectionQuery = "";
                if (shareType == "common")
                {
                    selectionQuery = "SELECT common FROM share_type";
                }
                else
                {
                    selectionQuery = "SELECT preferred FROM share_type";
                }

                SqlCommand secondCommand = new SqlCommand(selectionQuery, cn);
                int availableShares = Convert.ToInt32(secondCommand.ExecuteScalar());
                availableShares = availableShares - numOfShares;

                if (availableShares < 0)
                {
                    MessageBox.Show("Soory! We have only limited shares available.");
                }
                else
                {
                    string updateQuery = "";
                    if (shareType == "common")
                    {
                        updateQuery = "UPDATE share_type SET common = '" + availableShares + "' ";
                        SqlCommand thirdCommand = new SqlCommand(updateQuery, cn);
                        thirdCommand.ExecuteScalar();
                    }
                    else
                    {
                        updateQuery = "UPDATE share_type SET preferred = '" + availableShares + "' ";
                        SqlCommand thirdCommand = new SqlCommand(updateQuery, cn);
                        thirdCommand.ExecuteScalar();
                    }

                    MessageBox.Show("Successfully added the shares of purchase");

                    tbName.Text = string.Empty;
                    tbShares.Text = string.Empty;
                    tbDate.Text = string.Empty;

                    cn.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void TabControl_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            string tabItem = ((sender as TabControl).SelectedItem as TabItem).Header as string;
            switch (tabItem)
            {
                case "View Summary":
                    try
                    {
                        string connectString = Properties.Settings.Default.connect_string;
                        SqlConnection cn = new SqlConnection(connectString);

                        cn.Open();

                        string retriveCommonSharesSoldQuery = "SELECT SUM(numOfShares) FROM Buy WHERE type='common'";
                        SqlCommand fourthCommand = new SqlCommand(retriveCommonSharesSoldQuery, cn);
                        int soldCommonSahres = Convert.ToInt32(fourthCommand.ExecuteScalar());
                        tbCommonShare.Text = soldCommonSahres.ToString();


                        string retrivePreferredSharesSoldQuery = "SELECT SUM(numOfShares) FROM Buy WHERE type='preferred'";
                        SqlCommand fifthCommand = new SqlCommand(retrivePreferredSharesSoldQuery, cn);
                        int soldPreferredSahres = Convert.ToInt32(fifthCommand.ExecuteScalar());
                        tbPreferredShare.Text = soldPreferredSahres.ToString();

                        string retriveCommonSharesAvailableQuery = "SELECT common FROM share_type";
                        SqlCommand sixthCommand = new SqlCommand(retriveCommonSharesAvailableQuery, cn);
                        int AvaiableCommonSahres = Convert.ToInt32(sixthCommand.ExecuteScalar());
                        tbAvaliableCommon.Text = AvaiableCommonSahres.ToString();


                        string retrivePreferredSharesAvailableQuery = "SELECT preferred FROM share_type";
                        SqlCommand seventhCommand = new SqlCommand(retrivePreferredSharesAvailableQuery, cn);
                        int AvailablePreferredSahres = Convert.ToInt32(seventhCommand.ExecuteScalar());
                        tbAvailablePreferred.Text = AvailablePreferredSahres.ToString();


                        int day = new DateTime(2000, 1, 1).Day;
                        Random rnd = new Random(day);
                        int id = rnd.Next(1, 100);

                        int totalRevenueForCommonShare = soldCommonSahres * id;
                        int totalRevenueForPreferredShare = soldPreferredSahres * id;
                        int totalRevenue = totalRevenueForCommonShare + totalRevenueForPreferredShare;
                        tbTotalRevenue.Text = totalRevenue.ToString();



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                case "View Entries":
                    try
                    {
                        string connectString = Properties.Settings.Default.connect_string;
                        SqlConnection cn = new SqlConnection(connectString);

                        cn.Open();

                        string SelectionQuery = "SELECT * FROM Buy";
                        SqlCommand command = new SqlCommand(SelectionQuery, cn);
                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        DataTable dt = new DataTable("Buy");
                        sda.Fill(dt);
                        viewGrid.ItemsSource = dt.DefaultView;


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }


                    break;


            }

        }
    




    }
}

