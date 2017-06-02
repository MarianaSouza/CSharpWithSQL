using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpDatabaseSQL_Mari
{
    public partial class Form1 : Form
    {
        System.Data.SqlClient.SqlConnection Con;

        int AdminCurrentRow;
        System.Data.SqlClient.SqlDataAdapter Adminda;
        long AdminRecordCount;
        DataSet Adminds;

        int CustomerCurrentRow;
        System.Data.SqlClient.SqlDataAdapter Customerda;
        long CustomerRecordCount;
        DataSet Customerds;

        int StaffCurrentRow;
        System.Data.SqlClient.SqlDataAdapter Staffda;
        long StaffRecordCount;
        DataSet Staffds;

        int StaffQCurrentRow;
        System.Data.SqlClient.SqlDataAdapter StaffQda;
        long StaffQRecordCount;
        DataSet StaffQds;

        int TicketCurrentRow;
        System.Data.SqlClient.SqlDataAdapter Ticketda;
        long TicketRecordCount;
        DataSet Ticketds;

        int TicketStaffCurrentRow;
        System.Data.SqlClient.SqlDataAdapter TicketStaffda;
        long TicketStaffRecordCount;
        DataSet TicketStaffds;
        DataSet MyTicketStaffds;

        string Role;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'controlTablesDataSet3.TicketStaff' table. You can move, or remove it, as needed.
            this.ticketStaffTableAdapter.Fill(this.controlTablesDataSet3.TicketStaff);
            // TODO: This line of code loads data into the 'controlTablesDataSet2.Ticket' table. You can move, or remove it, as needed.
            this.ticketTableAdapter.Fill(this.controlTablesDataSet2.Ticket);
            // TODO: This line of code loads data into the 'controlTablesDataSet1.StaffQualifications' table. You can move, or remove it, as needed.
            this.staffQualificationsTableAdapter.Fill(this.controlTablesDataSet1.StaffQualifications);
            // TODO: This line of code loads data into the 'controlTablesDataSet.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter.Fill(this.controlTablesDataSet.Staff);
            Con = new System.Data.SqlClient.SqlConnection();
            // A dataset is needed for each table 
            Adminds = new DataSet();
            Customerds = new DataSet();
            Staffds = new DataSet();
            StaffQds = new DataSet();
            Ticketds = new DataSet();
            TicketStaffds = new DataSet();
            MyTicketStaffds = new DataSet();

            Con.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;AttachDbFilename=C:\\Users\\Mariana\\Desktop\\ITD\\Second Term\\CSharp\\CSharpDatabaseSQL_Mari\\CSharpDatabaseSQL_Phase3-4_Mari\\CSharpDatabaseSQL_Mari\\controlTables.mdf;Integrated Security=True";
            Con.Open();
            // Attempting to access Admin Table
            string Adminsqlstr;
            Adminsqlstr = "select * from Admins";
            Adminda = new System.Data.SqlClient.SqlDataAdapter(Adminsqlstr, Con);
            Adminda.Fill(Adminds, "Admins");
            AdminRecordCount = Adminds.Tables["Admins"].Rows.Count;

            // Attempting to access Customers Table
            string Customerssqlstr;
            Customerssqlstr = "select * from Customer";
            Customerda = new System.Data.SqlClient.SqlDataAdapter(Customerssqlstr, Con);
            Customerda.Fill(Customerds, "Customer");
            CustomerRecordCount = Customerds.Tables["Customer"].Rows.Count;

            // Attempting to access Staff Table
            string Staffsqlstr;
            Staffsqlstr = "select * from Staff";
            Staffda = new System.Data.SqlClient.SqlDataAdapter(Staffsqlstr, Con);
            Staffda.Fill(Staffds, "Staff");
            StaffRecordCount = Staffds.Tables["Staff"].Rows.Count;
            dgdStaff.DataSource = Staffds.Tables["Staff"]; //connects the datagrid with the table

            // Attempting to access Staff Qualifications Table
            string StaffQsqlstr;
            StaffQsqlstr = "select * from StaffQualifications";
            StaffQda = new System.Data.SqlClient.SqlDataAdapter(StaffQsqlstr, Con);
            StaffQda.Fill(StaffQds, "StaffQualifications");
            StaffQRecordCount = StaffQds.Tables["StaffQualifications"].Rows.Count;
            dgdStaffQualifications.DataSource = StaffQds.Tables["StaffQualifications"];


            // Attempting to access Ticket Table
            string Ticketsqlstr;
            Ticketsqlstr = "select * from Ticket";
            Ticketda = new System.Data.SqlClient.SqlDataAdapter(Ticketsqlstr, Con);
            Ticketda.Fill(Ticketds, "Ticket");
            TicketRecordCount = Ticketds.Tables["Ticket"].Rows.Count;
            dgdTicket.DataSource = Ticketds.Tables["Ticket"];

            // Attempting to access Ticket Staff Table
            string TicketStaffsqlstr;
            TicketStaffsqlstr = "select * from TicketStaff";
            TicketStaffda = new System.Data.SqlClient.SqlDataAdapter(TicketStaffsqlstr, Con);
            TicketStaffda.Fill(TicketStaffds, "TicketStaff");
            TicketStaffda.Fill(MyTicketStaffds, "TicketStaff");
            TicketStaffRecordCount = TicketStaffds.Tables["TicketStaff"].Rows.Count;
            dgdTicketStaff.DataSource = TicketStaffds.Tables["TicketStaff"];
            


            tabControl.TabPages.Remove(tabStaff);
            tabControl.TabPages.Remove(tabTicket);

            //this.tabPage3.Parent = null;
        }

        private void btnAdminLogin_Click(object sender, EventArgs e)
        {
            // Attempting to Search in a table(Admin)
            // creating a data row array so the result 
            // of search appears in it
            DataRow[] foundRows;
            String Strtofind;
            if (txtAdminID.Text == "")
            {
            MessageBox.Show("Please enter an Admin ID.");
                return;
            }
            Strtofind = "AdminID =" + txtAdminID.Text;
            foundRows = Adminds.Tables["Admins"].Select(Strtofind);

            if (foundRows.Length == 0)
            {
                MessageBox.Show("This Admin ID was not found.");
            }
            else
            {
                // Admin ID is found 
                // need to check the password 
                if (foundRows[0].ItemArray[3].Equals(txtAdminPassword.Text))
                {
                    //Password Correct 
                    //Login OK 
                    Role = "Admin";
                    MessageBox.Show("Welcome back," + " " + foundRows[0].ItemArray[1] + "!" );
                    tabControl.TabPages.Remove(tabLogStaff);
                    tabControl.TabPages.Remove(tabLogCustomer);
                    tabControl.TabPages.Add(tabStaff);
                    tabControl.TabPages.Add(tabTicket);
                    tabControl.SelectedTab = tabTicket;
                    ClearCancelAdmin();
                    //this.tabTicket.Parent = this.tabControl;
                }
                else
                {
                    MessageBox.Show("Wrong password.");
                }
            }
        }

        private void btnAdminCancel_Click(object sender, EventArgs e)
        {
            ClearCancelAdmin();
        }

        private void btnStaffLogin_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            String Strtofind;
            if (txtStaffIDLogin.Text == "")
            {
                MessageBox.Show("Please enter a Staff ID!");
                return;
            }
            Strtofind = "StaffID =" + txtStaffIDLogin.Text;
            foundRows = Staffds.Tables["Staff"].Select(Strtofind);

            if (foundRows.Length == 0)
            {
                MessageBox.Show("This Staff ID was not found.");
            }
            else
            {
                // Staff ID is found 
                // need to check the password 
                if (foundRows[0].ItemArray[3].Equals(txtStaffPasswordLogin.Text))
                {
                    //Password Correct 
                    //Login OK 
                    Role = "Staff";
                    MessageBox.Show("Welcome back," + " " + foundRows[0].ItemArray[1] + "!");
                    tabControl.TabPages.Remove(tabLogAdmin);
                    tabControl.TabPages.Remove(tabLogCustomer);
                    tabControl.TabPages.Add(tabTicket);
                    tabControl.SelectedTab = tabTicket;
                    grpTicket.Enabled = false;
                    //ClearCancelStaff();
                    //this.tabTicket.Parent = this.tabControl;

                    LoadMyTicketsStaff();
                }
                else
                {
                    MessageBox.Show("Wrong password.");
                }
            }
        }

        private void btnStaffCancel_Click(object sender, EventArgs e)
        {
            ClearCancelStaff();
        }

        private void btnCustomerLogin_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            String Strtofind;
            if (txtCustomerIDLogin.Text == "")
            {
                MessageBox.Show("Please enter a Customer ID!");
                return;
            }
            Strtofind = "CustomerID =" + txtCustomerIDLogin.Text;
            foundRows = Customerds.Tables["Customer"].Select(Strtofind);

            if (foundRows.Length == 0)
            {
                MessageBox.Show("This Customer ID was not found.");
            }
            else
            {
                // Staff ID is found 
                // need to check the password 
                if (foundRows[0].ItemArray[3].Equals(txtCustomerPasswordLogin.Text))
                {
                    //Password Correct 
                    //Login OK 
                    Role = "Customer";
                    MessageBox.Show("Welcome back," + " " + foundRows[0].ItemArray[1] + "!");
                    tabControl.TabPages.Remove(tabLogAdmin);
                    tabControl.TabPages.Remove(tabLogStaff);
                    tabControl.TabPages.Add(tabTicket);
                    tabControl.SelectedTab = tabTicket;
                    grpTicketStaff.Visible = false;
                    grpMyTickets.Visible = false;
                    LoadMyTicketsCustomer();
                    txtTicketCustomerID.Text = txtCustomerIDLogin.Text;
                    txtTicketCustomerID.Enabled = false;
                    //this.tabTicket.Parent = this.tabControl;
                }
                else
                {
                    MessageBox.Show("Wrong password.");
                }
            }
        }

        private void btnCustomerCancel_Click(object sender, EventArgs e)
        {
            ClearCancelCustomer();
        }


        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtStaffID.Text == "" && txtStaffName.Text == "" && txtStaffLastName.Text == "" && txtStaffPassword.Text == ""
                && txtStaffEmail.Text == "" && txtStaffPhone.Text == "")
            {
                MessageBox.Show("Please fill up all the fields to add.");
                return;
            }
            FindRecord = "StaffID =" + txtStaffID.Text;
            foundRows = Staffds.Tables["Staff"].Select(FindRecord);
            if (foundRows.Length == 0)
            {
                //its a new record, we should be able to add 
                DataRow NewRow = Staffds.Tables["Staff"].NewRow();
                //Next line is needed so we can update the database 
                System.Data.SqlClient.SqlCommandBuilder Cb = new System.Data.SqlClient.SqlCommandBuilder(Staffda);
                NewRow.SetField<int>("StaffID", Convert.ToInt32(txtStaffID.Text));
                NewRow.SetField<string>("StaffName", txtStaffName.Text);
                NewRow.SetField<string>("StaffLastName", txtStaffLastName.Text);
                NewRow.SetField<string>("StaffPassword", txtStaffPassword.Text);
                NewRow.SetField<string>("StaffEmail", txtStaffEmail.Text);
                NewRow.SetField<string>("StaffPhone", txtStaffPhone.Text);


                Staffds.Tables["Staff"].Rows.Add(NewRow);
                //da.UpdateCommand = Cb.GetUpdateCommand();
                Staffda.Update(Staffds, "Staff");
                //da.AcceptChangesDuringUpdate = true;
                //ds.AcceptChanges(); 
                StaffRecordCount = StaffRecordCount + 1;
                MessageBox.Show("Record added succesfully!");
            }
            else
            {
                MessageBox.Show("Duplicate ID. Choose another one to add the new employee.");

            }
        }

        private void btnSearchStaff_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtStaffID.Text == "")
            {
                MessageBox.Show("Please insert the ID to find the record.");
                return;
            }
            FindRecord = "StaffID =" + txtStaffID.Text;
            foundRows = Staffds.Tables["Staff"].Select(FindRecord);
            if (foundRows.Length == 0)
            {
                MessageBox.Show("Record not found.");
            }
            else
            {
                int Rowindex;
                Rowindex = Staffds.Tables["Staff"].Rows.IndexOf(foundRows[0]);
                StaffCurrentRow = Rowindex;
                ShowRecordStaff(StaffCurrentRow);
            }
         }



        private void btnDeleteStaff_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtStaffID.Text == "")
            {
                MessageBox.Show("Please insert the ID to find the record.");
                return;
            }
            FindRecord = "StaffID =" + txtStaffID.Text;
            foundRows = Staffds.Tables["Staff"].Select(FindRecord);
            if (foundRows.Length == 0)
            {
                MessageBox.Show("Record not found.");
            }
            else
            {
                DialogResult result;
                int Rowindex;
                Rowindex = Staffds.Tables["Staff"].Rows.IndexOf(foundRows[0]);
                result = MessageBox.Show("Are you Sure?", "Deleting Record", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Staffds.Tables["Staff"].Rows[Rowindex].Delete();
                    ClearStaff();
                    MessageBox.Show("Record deleted succesfully!!!");                   

                    var Sql = "DELETE FROM Staff WHERE " + FindRecord;

                    Staffda.DeleteCommand = Con.CreateCommand();
                    Staffda.DeleteCommand.CommandText = Sql;
                    Staffda.DeleteCommand.ExecuteNonQuery();

                    Staffds.AcceptChanges();
                    
                    Staffda.Update(Staffds, "Staff");
                }
            }
        }

        private void btnModifyStaff_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtStaffID.Text == "")
            {
                MessageBox.Show("Please insert the ID to find the record.");
                return;
            }
            FindRecord = "StaffID =" + txtStaffID.Text;
            foundRows = Staffds.Tables["Staff"].Select(FindRecord);
            if (foundRows.Length == 0)
            {
                MessageBox.Show("Record not found.");
            }
            else
            {
                
                int Rowindex;
                Rowindex = Staffds.Tables["Staff"].Rows.IndexOf(foundRows[0]);
                Staffds.Tables["Staff"].Rows[Rowindex].SetField<int>("StaffID", Convert.ToInt32(txtStaffID.Text));
                Staffds.Tables["Staff"].Rows[Rowindex].SetField<string>("StaffName", txtStaffName.Text);
                Staffds.Tables["Staff"].Rows[Rowindex].SetField<string>("StaffLastName", txtStaffLastName.Text);
                Staffds.Tables["Staff"].Rows[Rowindex].SetField<string>("StaffPassword", txtStaffPassword.Text);
                Staffds.Tables["Staff"].Rows[Rowindex].SetField<string>("StaffEmail", txtStaffEmail.Text);
                Staffds.Tables["Staff"].Rows[Rowindex].SetField<string>("StaffPhone", txtStaffPhone.Text);
                MessageBox.Show("Modifications saved successfully!");

                    Staffds.AcceptChanges();
                    Staffda.Update(Staffds, "Staff");
                }
            }
        

        private void btnClearStaff_Click(object sender, EventArgs e)
        {
            ClearStaff();
        }

        private void btnAddQualifications_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtStaffIDQualifications.Text == "" && rtxtStaffQualifications.Text == "")
            {
                MessageBox.Show("Please fill up all the fields to add.");
                return;
            }
            FindRecord = "StaffID =" + txtStaffIDQualifications.Text;
            foundRows = StaffQds.Tables["StaffQualifications"].Select(FindRecord);
            if (foundRows.Length == 0)
            {
                //its a new record, we should be able to add 
                DataRow NewRow = StaffQds.Tables["StaffQualifications"].NewRow();
                //Next line is needed so we can update the database 
                System.Data.SqlClient.SqlCommandBuilder Cb = new System.Data.SqlClient.SqlCommandBuilder(StaffQda);
                NewRow.SetField<int>("StaffID", Convert.ToInt32(txtStaffIDQualifications.Text));
                NewRow.SetField<string>("Qualifications", rtxtStaffQualifications.Text);
                

                StaffQds.Tables["StaffQualifications"].Rows.Add(NewRow);
                //da.UpdateCommand = Cb.GetUpdateCommand();
                StaffQda.Update(StaffQds, "StaffQualifications");
                //da.AcceptChangesDuringUpdate = true;
                //ds.AcceptChanges(); 
                StaffQRecordCount = StaffQRecordCount + 1;
                MessageBox.Show("Record added succesfully!");
            }
            else
            {
                MessageBox.Show("Duplicate ID. Choose another one to add the new employee.");

            }
        }

        private void btnSearchQualifications_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtStaffIDQualifications.Text == "")
            {
                MessageBox.Show("Please insert the ID to find the record.");
                return;
            }
            FindRecord = "StaffID =" + txtStaffIDQualifications.Text;
            foundRows = StaffQds.Tables["StaffQualifications"].Select(FindRecord);
            if (foundRows.Length == 0)
            {
                MessageBox.Show("Record not found.");
            }
            else
            {
                int Rowindex;
                Rowindex = StaffQds.Tables["StaffQualifications"].Rows.IndexOf(foundRows[0]);
                StaffQCurrentRow = Rowindex;
                ShowRecordStaffQualifications(StaffQCurrentRow);
            }
        }

        private void btnModifyQualifications_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtStaffIDQualifications.Text == "")
            {
                MessageBox.Show("Please insert the ID to find the record.");
                return;
            }
            FindRecord = "StaffID =" + txtStaffIDQualifications.Text;
            foundRows = StaffQds.Tables["StaffQualifications"].Select(FindRecord);
            if (foundRows.Length == 0)
            {
                MessageBox.Show("Record not found.");
            }
            else
            {

                int Rowindex;
                Rowindex = StaffQds.Tables["StaffQualifications"].Rows.IndexOf(foundRows[0]);
                StaffQds.Tables["StaffQualifications"].Rows[Rowindex].SetField<int>("StaffID", Convert.ToInt32(txtStaffIDQualifications.Text));
                StaffQds.Tables["StaffQualifications"].Rows[Rowindex].SetField<string>("Qualifications", rtxtStaffQualifications.Text);
                
                MessageBox.Show("Modifications saved successfully!");

                StaffQds.AcceptChanges();
                StaffQda.Update(StaffQds, "StaffQualifications");
            }
        }

        private void btnDeleteQualifications_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtStaffIDQualifications.Text == "")
            {
                MessageBox.Show("Please insert the ID to find the record.");
                return;
            }
            FindRecord = "StaffID =" + txtStaffIDQualifications.Text;
            foundRows = StaffQds.Tables["StaffQualifications"].Select(FindRecord);
            if (foundRows.Length == 0)
            {
                MessageBox.Show("Record not found.");
            }
            else
            {
                DialogResult result;
                int Rowindex;
                Rowindex = StaffQds.Tables["StaffQualifications"].Rows.IndexOf(foundRows[0]);
                result = MessageBox.Show("Are you Sure?", "Deleting Record", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    StaffQds.Tables["StaffQualifications"].Rows[Rowindex].Delete();
                    ClearStaffQualifications();
                    MessageBox.Show("Record deleted succesfully!!!");

                    var Sql = "DELETE FROM StaffQualifications WHERE " + FindRecord;

                    StaffQda.DeleteCommand = Con.CreateCommand();
                    StaffQda.DeleteCommand.CommandText = Sql;
                    StaffQda.DeleteCommand.ExecuteNonQuery();

                    StaffQds.AcceptChanges();

                    StaffQda.Update(StaffQds, "StaffQualifications");
                }
            }
        }


        private void btnClearStaffQualifications_Click(object sender, EventArgs e)
        {
            ClearStaffQualifications();
        }

        private void btnAddTicket_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtTicketID.Text == "" && txtTicketCustomerID.Text == "" && rtxtProblemDescription.Text == "" )
            {
                MessageBox.Show("Please fill up all the fields to add.");
                return;
            }
            FindRecord = "TicketID =" + txtTicketID.Text;
            foundRows = Ticketds.Tables["Ticket"].Select(FindRecord);
            if (foundRows.Length == 0)
            {
                //its a new record, we should be able to add 
                DataRow NewRow = Ticketds.Tables["Ticket"].NewRow();
                //Next line is needed so we can update the database 
                System.Data.SqlClient.SqlCommandBuilder Cb = new System.Data.SqlClient.SqlCommandBuilder(Ticketda);
                NewRow.SetField<int>("TicketID", Convert.ToInt32(txtTicketID.Text));
                NewRow.SetField<int>("CustomerID", Convert.ToInt32(txtTicketCustomerID.Text));
                NewRow.SetField<string>("Description", rtxtProblemDescription.Text);

                Ticketds.Tables["Ticket"].Rows.Add(NewRow);
                //da.UpdateCommand = Cb.GetUpdateCommand();
                Ticketda.Update(Ticketds, "Ticket");
                //da.AcceptChangesDuringUpdate = true;
                //ds.AcceptChanges(); 
                TicketRecordCount = TicketRecordCount + 1;
                MessageBox.Show("Record added succesfully!");
            }
            else
            {
                MessageBox.Show("Duplicated ID. Please choose a new one to add a record.");

            }
        }


        private void btnSearchTicket_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtTicketID.Text == "")
            {
                MessageBox.Show("Please insert the ID to find the record.");
                return;
            }
            FindRecord = "TicketID =" + txtTicketID.Text;
            if (txtTicketCustomerID.Text != "")
            {
                FindRecord = FindRecord + " And CustomerID =" + txtTicketCustomerID.Text;
            }
            foundRows = Ticketds.Tables["Ticket"].Select(FindRecord);
            if (foundRows.Length == 0)
            {
                MessageBox.Show("Record not found.");
            }
            else
            {
                int Rowindex;
                Rowindex = Ticketds.Tables["Ticket"].Rows.IndexOf(foundRows[0]);
                TicketCurrentRow = Rowindex;
                ShowRecordTicket(TicketCurrentRow);
            }
        }

        private void btnModifyTicket_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtTicketID.Text == "")
            {
                MessageBox.Show("Please insert the ID to find the record.");
                return;
            }
            FindRecord = "TicketID =" + txtTicketID.Text;
            if (txtTicketCustomerID.Text != "")
            {
                FindRecord = FindRecord + " And CustomerID =" + txtTicketCustomerID.Text;
            }
            foundRows = Ticketds.Tables["Ticket"].Select(FindRecord);
            if (foundRows.Length == 0)
            {
                MessageBox.Show("Record not found.");
            }
            else
            {

                int Rowindex;
                Rowindex = Ticketds.Tables["Ticket"].Rows.IndexOf(foundRows[0]);
                Ticketds.Tables["Ticket"].Rows[Rowindex].SetField<int>("TicketID", Convert.ToInt32(txtTicketID.Text));
                Ticketds.Tables["Ticket"].Rows[Rowindex].SetField<int>("CustomerID", Convert.ToInt32(txtTicketCustomerID.Text));
                Ticketds.Tables["Ticket"].Rows[Rowindex].SetField<string>("Description", rtxtProblemDescription.Text);

                MessageBox.Show("Modifications saved successfully!");

                Ticketds.AcceptChanges();
                Ticketda.Update(Ticketds, "Ticket");
            }
        }

        private void btnDeleteTicket_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtTicketID.Text == "")
            {
                MessageBox.Show("Please insert the ID to find the record.");
                return;
            }
            FindRecord = "TicketID =" + txtTicketID.Text;
            if (txtTicketCustomerID.Text != "")
            {
                FindRecord = FindRecord + " And CustomerID =" + txtTicketCustomerID.Text;
            }
            foundRows = Ticketds.Tables["Ticket"].Select(FindRecord);
            if (foundRows.Length == 0)
            {
                MessageBox.Show("Record not found.");
            }
            else
            {
                DialogResult result;
                int Rowindex;
                Rowindex = Ticketds.Tables["Ticket"].Rows.IndexOf(foundRows[0]);
                result = MessageBox.Show("Are you Sure?", "Deleting Record", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Ticketds.Tables["Ticket"].Rows[Rowindex].Delete();
                    ClearTicket();
                    MessageBox.Show("Record deleted succesfully!!!");

                    var Sql = "DELETE FROM Ticket WHERE " + FindRecord;

                    Ticketda.DeleteCommand = Con.CreateCommand();
                   Ticketda.DeleteCommand.CommandText = Sql;
                    Ticketda.DeleteCommand.ExecuteNonQuery();

                    Ticketds.AcceptChanges();

                    Ticketda.Update(Ticketds, "Ticket");
                }
            }
        }

        private void btnClearTicket_Click(object sender, EventArgs e)
        {
            ClearTicket();
        }

        private void btnSearchTicketStaff_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtTicketIDTicketStaff.Text == "")
            {
                MessageBox.Show("Please insert the Ticket ID to find the record.");
                return;
            }
            FindRecord = "TicketID =" + txtTicketIDTicketStaff.Text;
            foundRows = TicketStaffds.Tables["TicketStaff"].Select(FindRecord);
            if (foundRows.Length == 0)
            {
                MessageBox.Show("Record not found.");
            }
            else
            {
                int Rowindex;
                Rowindex = TicketStaffds.Tables["TicketStaff"].Rows.IndexOf(foundRows[0]);
                TicketStaffCurrentRow = Rowindex;
                ShowRecordTicketStaff(TicketStaffCurrentRow);
            }
        }

        private void btnAssignTicketStaff_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtTicketIDTicketStaff.Text == "" && txtStaffIDTicketStaff.Text == "")
            {
                MessageBox.Show("Please fill up all the fields to assign.");
                return;
            }
            FindRecord = "TicketID =" + txtTicketIDTicketStaff.Text;
            foundRows = TicketStaffds.Tables["TicketStaff"].Select(FindRecord);
            if (foundRows.Length == 0)
            {
                //its a new record, we should be able to add 
                DataRow NewRow = TicketStaffds.Tables["TicketStaff"].NewRow();
                //Next line is needed so we can update the database 
                System.Data.SqlClient.SqlCommandBuilder Cb = new System.Data.SqlClient.SqlCommandBuilder(TicketStaffda);
                NewRow.SetField<int>("TicketID", Convert.ToInt32(txtTicketIDTicketStaff.Text));
                NewRow.SetField<int>("StaffID", Convert.ToInt32(txtStaffIDTicketStaff.Text));

                TicketStaffds.Tables["TicketStaff"].Rows.Add(NewRow);
                //da.UpdateCommand = Cb.GetUpdateCommand();
                TicketStaffda.Update(TicketStaffds, "TicketStaff");
                //da.AcceptChangesDuringUpdate = true;
                //ds.AcceptChanges(); 
                TicketStaffRecordCount = TicketStaffRecordCount + 1;

                LoadMyTicketsStaff();

                MessageBox.Show("Ticket assigned succesfully!");
            }
            else
            {
                MessageBox.Show("Duplicate ID. Choose another one to assign the new ticket.");

            }
        }

       

        private void btnModifyTicketStaff_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtTicketIDTicketStaff.Text == "")
            {
                MessageBox.Show("Please insert the Ticket ID to find the record.");
                return;
            }
            FindRecord = "TicketID =" + txtTicketIDTicketStaff.Text;
            foundRows = TicketStaffds.Tables["TicketStaff"].Select(FindRecord);
            if (foundRows.Length == 0)
            { 
                MessageBox.Show("Record not found.");
            }
            else
            {

                int Rowindex;
                Rowindex = TicketStaffds.Tables["TicketStaff"].Rows.IndexOf(foundRows[0]);
                TicketStaffds.Tables["TicketStaff"].Rows[Rowindex].SetField<int>("TicketID", Convert.ToInt32(txtTicketIDTicketStaff.Text));
                TicketStaffds.Tables["TicketStaff"].Rows[Rowindex].SetField<int>("StaffID", Convert.ToInt32(txtStaffIDTicketStaff.Text));
              
                MessageBox.Show("Modifications saved successfully!");

                TicketStaffds.AcceptChanges();
                TicketStaffda.Update(TicketStaffds, "TicketStaff");
            }
        }

        private void btnDissociateTicketStaff_Click(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            string FindRecord;

            if (txtTicketIDTicketStaff.Text == "")
            {
                MessageBox.Show("Please insert the ID to find the record.");
                return;
            }
            FindRecord = "TicketID =" + txtTicketIDTicketStaff.Text;
            foundRows = TicketStaffds.Tables["TicketStaff"].Select(FindRecord);
            if (foundRows.Length == 0)
            {
                MessageBox.Show("Record not found.");
            }
            else
            {
                DialogResult result;
                int Rowindex;
                Rowindex = TicketStaffds.Tables["TicketStaff"].Rows.IndexOf(foundRows[0]);
                result = MessageBox.Show("Are you Sure?", "Deleting Record", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    TicketStaffds.Tables["TicketStaff"].Rows[Rowindex].Delete();
                    ClearTicketStaff();
                    MessageBox.Show("Record deleted succesfully!!!");

                    var Sql = "DELETE FROM TicketStaff WHERE " + FindRecord;

                    TicketStaffda.DeleteCommand = Con.CreateCommand();
                    TicketStaffda.DeleteCommand.CommandText = Sql;
                    TicketStaffda.DeleteCommand.ExecuteNonQuery();

                    TicketStaffds.AcceptChanges();

                    TicketStaffda.Update(TicketStaffds, "TicketStaff");

                    LoadMyTicketsStaff();
                }
            }
        }

        private void btnClearStaffTicket_Click(object sender, EventArgs e)
        {
            ClearTicketStaff();
        }

        private void btnStaffLogOut_Click(object sender, EventArgs e)
        {
            
            tabControl.TabPages.Remove(tabStaff);
            tabControl.TabPages.Remove(tabTicket);
            tabControl.SelectedTab = tabLogAdmin;
            tabControl.TabPages.Add(tabLogStaff);
            tabControl.TabPages.Add(tabLogCustomer);
        }

        private void btnTicketLogOut_Click(object sender, EventArgs e)
        {
            tabControl.TabPages.Remove(tabStaff);
            tabControl.TabPages.Remove(tabTicket);
            MyTicketStaffds.Clear();
            dgdMyTickets.DataSource = "";
            if (Role == "Staff")
            {
                tabControl.TabPages.Add(tabLogAdmin);
                tabControl.TabPages.Add(tabLogCustomer);
                grpTicket.Enabled = true;
                ClearCancelStaff();
            }
            if (Role == "Customer")
            {
                tabControl.TabPages.Add(tabLogAdmin);
                tabControl.TabPages.Add(tabLogStaff);
                grpTicketStaff.Visible = true;
                grpMyTickets.Visible = true;
                txtTicketCustomerID.Enabled = true;
                Ticketds.Clear();
                ClearCancelCustomer();
            }
            if (Role == "Admin")
            { 
                tabControl.TabPages.Add(tabLogStaff);
                tabControl.TabPages.Add(tabLogCustomer);
                ClearCancelAdmin();
            }
            Form1_Load(sender, e);
        }

        #region "Functions"
        private void ClearCancelAdmin()
        {
            txtAdminID.Clear();
            txtAdminPassword.Clear();
        }

        private void ClearCancelStaff()
        {
            txtStaffIDLogin.Clear();
            txtStaffPasswordLogin.Clear();
        }

        private void ClearCancelCustomer()
        {
            txtCustomerIDLogin.Clear();
            txtCustomerPasswordLogin.Clear();
        }

        private void ClearStaff()
        {
            txtStaffID.Clear();
            txtStaffName.Clear();
            txtStaffLastName.Clear();
            txtStaffPassword.Clear();
            txtStaffEmail.Clear();
            txtStaffPhone.Clear();
        }

        private void ClearStaffQualifications()
        {
            txtStaffIDQualifications.Clear();
            rtxtStaffQualifications.Clear();
        }


        private void ClearTicket()
        {
            txtTicketID.Clear();
            if(txtTicketCustomerID.Enabled)
            {
                txtTicketCustomerID.Clear();
            }
            rtxtProblemDescription.Clear();
        }

        private void ClearTicketStaff()
        {
            txtStaffIDTicketStaff.Clear();
            txtTicketIDTicketStaff.Clear();
        }


        public void ShowRecordStaff(int ThisRow)
        {

            txtStaffID.Text = Staffds.Tables["Staff"].Rows[ThisRow].Field<int>("StaffID").ToString();
           txtStaffName.Text = Staffds.Tables["Staff"].Rows[ThisRow].Field<string>("StaffName").ToString();
            txtStaffLastName.Text = Staffds.Tables["Staff"].Rows[ThisRow].Field<string>("StaffLastName").ToString();
            txtStaffPassword.Text = Staffds.Tables["Staff"].Rows[ThisRow].Field<string>("StaffPassword").ToString();
            txtStaffEmail.Text = Staffds.Tables["Staff"].Rows[ThisRow].Field<string>("StaffEmail").ToString();
            txtStaffPhone.Text = Staffds.Tables["Staff"].Rows[ThisRow].Field<string>("StaffPhone").ToString();
        }

        public void ShowRecordStaffQualifications(int ThisRow)
        {

            txtStaffIDQualifications.Text = StaffQds.Tables["StaffQualifications"].Rows[ThisRow].Field<int>("StaffID").ToString();
            rtxtStaffQualifications.Text = StaffQds.Tables["StaffQualifications"].Rows[ThisRow].Field<string>("Qualifications").ToString();
        }

        public void ShowRecordTicket(int ThisRow)
        {
            txtTicketID.Text = Ticketds.Tables["Ticket"].Rows[ThisRow].Field<int>("TicketID").ToString();
            txtTicketCustomerID.Text = Ticketds.Tables["Ticket"].Rows[ThisRow].Field<int>("CustomerID").ToString();
            rtxtProblemDescription.Text = Ticketds.Tables["Ticket"].Rows[ThisRow].Field<string>("Description").ToString();
        }

        public void ShowRecordTicketStaff(int ThisRow)
        {
            txtStaffIDTicketStaff.Text = TicketStaffds.Tables["TicketStaff"].Rows[ThisRow].Field<int>("StaffID").ToString();
            txtTicketIDTicketStaff.Text = TicketStaffds.Tables["TicketStaff"].Rows[ThisRow].Field<int>("TicketID").ToString();
        }
        private void LoadMyTicketsStaff()
        {
            MyTicketStaffds.Clear();
            TicketStaffda.Fill(MyTicketStaffds, "TicketStaff");
            var FindRecord = "StaffID =" + txtStaffIDLogin.Text;
            MyTicketStaffds.Tables["TicketStaff"].Select(FindRecord);
            dgdMyTickets.DataSource = MyTicketStaffds.Tables["TicketStaff"];

            (dgdMyTickets.DataSource as DataTable).DefaultView.RowFilter = "StaffID =" + txtStaffIDLogin.Text;
        }

        private void LoadMyTicketsCustomer()
        {
            //MyTicketStaffds.Clear();
            //TicketStaffda.Fill(MyTicketStaffds, "TicketStaff");
            //var FindRecord = "StaffID =" + txtStaffIDLogin.Text;
            //MyTicketStaffds.Tables["TicketStaff"].Select(FindRecord);
            //dgdMyTickets.DataSource = MyTicketStaffds.Tables["TicketStaff"];

            (dgdTicket.DataSource as DataTable).DefaultView.RowFilter = "CustomerID =" + txtCustomerIDLogin.Text;
        }


        #endregion

        
    }
}
