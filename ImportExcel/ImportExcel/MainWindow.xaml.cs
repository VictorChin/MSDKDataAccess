using ImportExcel.AttendeesTableAdapters;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

namespace ImportExcel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            
            
            OpenFileDialog ofd = new OpenFileDialog();
            if ((bool)ofd.ShowDialog()) {
                textBox.Text = ofd.FileName;
                DataSet ds = ReadExcelFile(ofd.FileName);
                this.dataToUplad = ds;
                dataGrid.ItemsSource = ds.Tables[0].DefaultView;
                button1.IsEnabled = true;
            }

        }
        DataSet dataToUplad;
        private string GetConnectionString(string path)
{
    Dictionary<string, string> props = new Dictionary<string, string>();

    // XLSX - Excel 2007, 2010, 2012, 2013
    props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
    props["Extended Properties"] = "Excel 12.0 XML";
    props["Data Source"] = path;

    // XLS - Excel 2003 and Older
    //props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
    //props["Extended Properties"] = "Excel 8.0";
    //props["Data Source"] = "C:\\MyExcel.xls";

    StringBuilder sb = new StringBuilder();

    foreach (KeyValuePair<string, string> prop in props)
    {
        sb.Append(prop.Key);
        sb.Append('=');
        sb.Append(prop.Value);
        sb.Append(';');
    }

    return sb.ToString();
}
    private DataSet ReadExcelFile(string fileName)
    {
        DataSet ds = new DataSet();

        string connectionString = GetConnectionString(fileName);

        using (OleDbConnection conn = new OleDbConnection(connectionString))
        {

            conn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;

            // Get all Sheets in Excel File
            DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            // Loop through all Sheets to get data
            foreach (DataRow dr in dtSheet.Rows)
            {
                string sheetName = dr["TABLE_NAME"].ToString();

                if (!sheetName.EndsWith("$"))
                    continue;

                // Get all rows from the Sheet
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";

                DataTable dt = new DataTable();
                dt.TableName = sheetName;

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                ds.Tables.Add(dt);
            }

            cmd = null;
            conn.Close();
        }

        return ds;
    }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AttendeeTableAdapter ata = new AttendeeTableAdapter();
            foreach(DataRow datarow in dataToUplad.Tables[0].Rows) {
                ata.Insert(datarow["FirstName"].ToString(), datarow["LastName"].ToString(),
                    datarow["Username"].ToString(), datarow["Email"].ToString()
                    , datarow["Password"].ToString()
                    );
            }
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            AttendeeTableAdapter ata = new AttendeeTableAdapter();
            var cmd = ata.Connection.CreateCommand();
            
            cmd.CommandText = "delete from attendee";
            ata.Connection.Open();
            cmd.ExecuteNonQuery();
            ata.Connection.Close();
        }
    }
  
}
