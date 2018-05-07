using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Reminder : Form
    {
        SqlConnection db_Connection = new SqlConnection();
        SqlCommand db_Command = new SqlCommand();
        SqlDataAdapter db_Adapter = new SqlDataAdapter();
        DateTime Wkdate = System.DateTime.Now.AddDays(7);
        public Reminder()
        {
            InitializeComponent();
            db_Connection.ConnectionString = "Data Source=QIS-ADMIN;Initial Catalog=Information;Integrated Security=True";
        }

        private void Reminder_Load(object sender, EventArgs e)
        {
            dt_startdate.CustomFormat = "mm-dd-yyyy";
            dt_startdate.CustomFormat = "mm-dd-yyyy";
            dt_startdate.Format = DateTimePickerFormat.Time;
            dt_enddate.Format = DateTimePickerFormat.Time;
            //DataTable dt = GetDataTable("SELECT * FROM Save_Info --where CONVERT(date, End_date) >'" + System.DateTime.Now.Date + "'  and CONVERT(date, Start_date) < '" + Wkdate + "' ");
            DataTable dt = GetDataTable("SELECT * FROM Save_Info where CONVERT(date, End_date) >'" + System.DateTime.Now.Date + "'  and CONVERT(date, Start_date) < '" + Wkdate + "' ");
            dataGridView1.DataSource = dt;
            dataGridView1.Show();
            //dataGridView1.Columns["Information_id"].Visible = false;
            //button2.Visible = false;


            try
            {
                for (int x = 0; x < dataGridView1.Rows.Count - 1; x++)
                {
                    string reminder = dataGridView1.Rows[x].Cells[1].Value.ToString();
                    DateTime startdate = Convert.ToDateTime(dataGridView1.Rows[x].Cells[2].Value.ToString());
                    //string reminder = dataGridView1.Rows[x].Cells[2].ToString();

                    if (startdate.Date == System.DateTime.Now.Date)
                    {
                        MessageBox.Show("you have a notification for the event " + reminder);


                    }
                }
            }
            catch (Exception exe)
            { }



        }

        public Int32 ExecuteInsert(String DatabaseQuery)
        {
            Int32 AffectedRows;
            try
            {

                OpenConnection();
                db_Command = new System.Data.SqlClient.SqlCommand();
                db_Command.Connection = getconnection();
                db_Command.CommandType = System.Data.CommandType.Text;
                db_Command.CommandText = DatabaseQuery;
                AffectedRows = db_Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //GlobalAccess.ObjGenerealEL.file_append("DataAccessLayer", ex.Message.ToString(), "ExecuteInsert", "DBFunctionDAL");
                return -1;
            }
            finally
            {

                CloseConnection();
            }
            return AffectedRows;
        }




        public System.Data.SqlClient.SqlConnection getconnection()
        {

            return db_Connection;
        }
        public void OpenConnection()
        {
            try
            {
                if (db_Connection.State != System.Data.ConnectionState.Open)
                {
                    db_Connection.Open();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void CloseConnection()
        {
            if (db_Connection.State != System.Data.ConnectionState.Closed)
            {
                db_Connection.Close();
            }
        }


        public System.Data.DataTable GetDataTable(String SelectQuery)
        {
            System.Data.DataTable dt_Table = new System.Data.DataTable();

            try
            {
                OpenConnection();
                db_Adapter = new System.Data.SqlClient.SqlDataAdapter(SelectQuery, db_Connection);
                //dt_Table.Columns.Add("Select");

                db_Adapter.Fill(dt_Table);

                //DDL.SelectedIndex = 0;
                //DDL.Items.Insert(0, "-Select-");

            }
            catch (Exception ex)
            {

            }
            finally
            {
                db_Adapter.Dispose();

                CloseConnection();
            }
            return dt_Table;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int s = ExecuteInsert("insert into Save_Info values('" + txt_event.Text + "','" + dt_startdate.Value + "','" + dt_enddate.Value + "')");
            DataTable dt = GetDataTable("SELECT * FROM Save_Info --where CONVERT(date, End_date) >'" + System.DateTime.Now.Date + "'  and CONVERT(date, Start_date) < '" + Wkdate + "' ");
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Information_id"].Visible = false;
            dataGridView1.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            button2.Visible = true;
            btnsave.Visible = false;
            DataGridViewCheckBoxColumn chkPresent = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Add(chkPresent);
            chkPresent.HeaderText = "Delete";
            try
            {
                for (int x = 0; x < dataGridView1.Rows.Count - 1; x++)
                {
                    DataGridViewCheckBoxCell chk = dataGridView1.Rows[x].Cells[0] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        string BatchN = dataGridView1.Rows[x].Cells[1].Value.ToString();
                        //int res = objclsMasterUpdate.TypeUP(BatchN, Convert.ToInt32(cmbmedtype1.SelectedValue.ToString()));
                    }
                }
                DataTable dt = GetDataTable("SELECT * FROM Save_Info --where CONVERT(date, End_date) >'" + System.DateTime.Now.Date + "'  and CONVERT(date, Start_date) < '" + Wkdate + "' ");
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Information_id"].Visible = false;
                dataGridView1.Show();
                button1.Visible = false;





            }
            catch (Exception exe) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {


            try
            {
                for (int x = 0; x < dataGridView1.Rows.Count - 1; x++)
                {
                    DataGridViewCheckBoxCell chk = dataGridView1.Rows[x].Cells[4] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        string id = dataGridView1.Rows[x].Cells[0].Value.ToString();

                        int res = ExecuteUpdate("Delete from Save_Info where Information_id=" + id + " "); ;
                    }
                }
                DataTable dt = GetDataTable("SELECT * FROM Save_Info --where CONVERT(date, End_date) >'" + System.DateTime.Now.Date + "'  and CONVERT(date, Start_date) < '" + Wkdate + "' ");
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Information_id"].Visible = false;
                dataGridView1.Show();
                MessageBox.Show("deleted sucessfully");
                button2.Visible = false;
                btnsave.Visible = true;
                button1.Visible = true;


            }
            catch (Exception exe) { }
        }

        public Int32 ExecuteUpdate(String DatabaseQuery)
        {
            Int32 AffectedRows;
            try
            {
                OpenConnection();
                db_Command = new System.Data.SqlClient.SqlCommand();
                db_Command.Connection = getconnection();
                db_Command.CommandType = System.Data.CommandType.Text;
                db_Command.CommandText = DatabaseQuery;
                AffectedRows = db_Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // GlobalAccess.ObjGenerealEL.file_append("DataAccessLayer", ex.Message.ToString(), "ExecuteUpdate", "DBFunctionDAL");
                return -1;
            }
            finally
            {
                CloseConnection();
            }
            return AffectedRows;
        }




    }
}