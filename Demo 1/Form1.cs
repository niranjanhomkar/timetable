using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Demo_1
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            FillCombo();
            tbpwd.KeyDown += new KeyEventHandler(tbpwd_KeyDown);
            tbuname.KeyDown += new KeyEventHandler(tbuname_KeyDown);
            selclass_com.KeyDown += new KeyEventHandler(selclass_com_KeyDown);
        }

        Queries query_object = new Queries();
        SqlConnection con;
        SqlCommand cb, cb1, cb2;
        SqlDataAdapter adp;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        SqlDataReader sdr;
        int inum = 0, get_day_time = 0, get_panel = 0, prac_counter = 0;
        String ccode;
        String subcode;
        String selprof = "", selprof_batch1, selprof_batch2, selprof_batch3, selprof_batch4;
        String selclass = "", selclass_batch1, selclass_batch2, selclass_batch3, selclass_batch4;
        String selsub = "", selsub_batch1, selsub_batch2, selsub_batch3, selsub_batch4;
        String selsem;
        String subdel;
        Panel[] paneltt = new Panel[50];
        int[] x = new int[50];
        int[] y = new int[50];
        Control panel_object;
        Control[,] labelarray = new Control[50, 4];
        Control[] lblarr = new Label[4];
        string[] selected_division, selclass_batch, selprof_batch, selsub_batch;
        string sday, div_temp;
        char stime;
        string temp_prof = "", temp_class = "", temp_sub = "";
        PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        PrintDocument PrintDoc1 = new PrintDocument();
        //string temp_profb2 = "", temp_profb2 = "", temp_profb3 = "", temp_profb4 = "";
        //string temp_classb2 = "", temp_classb2 = "", temp_classb3 = "", temp_classb4 = "";
        //string temp_subb1 = "", temp_subb2 = "", temp_subb3 = "", temp_subb4 = "";


        public void assign_panel_listeners()
        {
            paneltt[0] = panel_1_1;
            paneltt[1] = panel_1_2;
            paneltt[2] = panel31;
            paneltt[3] = panel32;
            paneltt[4] = panel33;
            paneltt[5] = panel34;
            paneltt[6] = panel35;
            paneltt[7] = panel36;
            paneltt[8] = panel37;
            paneltt[9] = panel38;
            paneltt[10] = panel40;
            paneltt[11] = panel45;
            paneltt[12] = panel50;
            paneltt[13] = panel55;
            paneltt[14] = panel60;
            paneltt[15] = panel65;
            paneltt[16] = panel70;
            paneltt[17] = panel75;
            paneltt[18] = panel80;
            paneltt[19] = panel85;
            paneltt[20] = panel41;
            paneltt[21] = panel46;
            paneltt[22] = panel51;
            paneltt[23] = panel56;
            paneltt[24] = panel61;
            paneltt[25] = panel66;
            paneltt[26] = panel71;
            paneltt[27] = panel76;
            paneltt[28] = panel81;
            paneltt[29] = panel86;
            paneltt[30] = panel42;
            paneltt[31] = panel47;
            paneltt[32] = panel52;
            paneltt[33] = panel57;
            paneltt[34] = panel62;
            paneltt[35] = panel67;
            paneltt[36] = panel72;
            paneltt[37] = panel77;
            paneltt[38] = panel82;
            paneltt[39] = panel87;
            paneltt[40] = panel43;
            paneltt[41] = panel48;
            paneltt[42] = panel53;
            paneltt[43] = panel58;
            paneltt[44] = panel63;
            paneltt[45] = panel68;
            paneltt[46] = panel73;
            paneltt[47] = panel78;
            paneltt[48] = panel83;
            paneltt[49] = panel88;

            foreach (Panel p in paneltt)
            {
                p.Click += new EventHandler(this.getpos_Click);
            }

            /*paneltt[0].Click += new EventHandler(this.getpos_Click);
            paneltt[1].Click += new EventHandler(this.getpos_Click);
            paneltt[2].Click += new EventHandler(this.getpos_Click);
            paneltt[3].Click += new EventHandler(this.getpos_Click);
            paneltt[4].Click += new EventHandler(this.getpos_Click);
            paneltt[5].Click += new EventHandler(this.getpos_Click);
            paneltt[6].Click += new EventHandler(this.getpos_Click);
            paneltt[7].Click += new EventHandler(this.getpos_Click);
            paneltt[8].Click += new EventHandler(this.getpos_Click);
            paneltt[9].Click += new EventHandler(this.getpos_Click);
            paneltt[10].Click += new EventHandler(this.getpos_Click);
            paneltt[11].Click += new EventHandler(this.getpos_Click);
            paneltt[12].Click += new EventHandler(this.getpos_Click);
            paneltt[13].Click += new EventHandler(this.getpos_Click);
            paneltt[14].Click += new EventHandler(this.getpos_Click);
            paneltt[15].Click += new EventHandler(this.getpos_Click);
            paneltt[16].Click += new EventHandler(this.getpos_Click);
            paneltt[17].Click += new EventHandler(this.getpos_Click);
            paneltt[18].Click += new EventHandler(this.getpos_Click);
            paneltt[19].Click += new EventHandler(this.getpos_Click);
            paneltt[20].Click += new EventHandler(this.getpos_Click);
            paneltt[21].Click += new EventHandler(this.getpos_Click);
            paneltt[22].Click += new EventHandler(this.getpos_Click);
            paneltt[23].Click += new EventHandler(this.getpos_Click);
            paneltt[24].Click += new EventHandler(this.getpos_Click);
            paneltt[25].Click += new EventHandler(this.getpos_Click);
            paneltt[26].Click += new EventHandler(this.getpos_Click);
            paneltt[27].Click += new EventHandler(this.getpos_Click);
            paneltt[28].Click += new EventHandler(this.getpos_Click);
            paneltt[29].Click += new EventHandler(this.getpos_Click);
            paneltt[30].Click += new EventHandler(this.getpos_Click);
            paneltt[31].Click += new EventHandler(this.getpos_Click);
            paneltt[32].Click += new EventHandler(this.getpos_Click);
            paneltt[33].Click += new EventHandler(this.getpos_Click);
            paneltt[34].Click += new EventHandler(this.getpos_Click);
            paneltt[35].Click += new EventHandler(this.getpos_Click);
            paneltt[36].Click += new EventHandler(this.getpos_Click);
            paneltt[37].Click += new EventHandler(this.getpos_Click);
            paneltt[38].Click += new EventHandler(this.getpos_Click);
            paneltt[39].Click += new EventHandler(this.getpos_Click);
            paneltt[40].Click += new EventHandler(this.getpos_Click);
            paneltt[41].Click += new EventHandler(this.getpos_Click);
            paneltt[42].Click += new EventHandler(this.getpos_Click);
            paneltt[43].Click += new EventHandler(this.getpos_Click);
            paneltt[44].Click += new EventHandler(this.getpos_Click);
            paneltt[45].Click += new EventHandler(this.getpos_Click);
            paneltt[46].Click += new EventHandler(this.getpos_Click);
            paneltt[47].Click += new EventHandler(this.getpos_Click);
            paneltt[48].Click += new EventHandler(this.getpos_Click);
            paneltt[49].Click += new EventHandler(this.getpos_Click);*/
        }

        void tbuname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }

        void tbpwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }

        void selclass_com_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                creatett_but_Click(sender, e);
        }

        void FillCombo()
        {
            //con = new SqlConnection("Data Source=niranjan.noip.me;Network Library=DBMSSOCN;Initial Catalog=sample;User ID=sa;Password=computer6268");
            //con = new SqlConnection("Data Source=(local)\\SQLEXPRESS; Initial Catalog=sample; Integrated Security=SSPI; User ID=Lenovo-Y50\\Niranjan; Password=computer6268");
            con = new SqlConnection("Data Source=(local)\\SQLEXPRESS;Initial Catalog=sample;Integrated Security=True");

            //update professor
            cb = new SqlCommand("Select * from professors", con);
            try
            {
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {
                    String sname = (string)sdr.GetString(1);
                    updateprof_com.Items.Add(sname);
                    ttprofsel_com.Items.Add(sname);
                    delp_com.Items.Add(sname);
                    viewproftt_com.Items.Add(sname);
                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            //update classroom
            cb = new SqlCommand("Select * from classrooms", con);
            try
            {
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {
                    String sname = (string)sdr.GetString(0);
                    updateclass_com.Items.Add(sname);
                    delc_com.Items.Add(sname);
                    viewclasstt_com.Items.Add(sname);
                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            //update subject
            List<String> list_subs = new List<String>();

            cb = new SqlCommand("Select * from subjects_odd_sem", con);
            cb1 = new SqlCommand("Select * from subjects_even_sem", con);

            try
            {
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                    list_subs.Add(sdr.GetString(0));

                con.Close();

                con.Open();
                sdr = cb1.ExecuteReader();
                while (sdr.Read())
                    list_subs.Add(sdr.GetString(0));

                con.Close();
                for (int i = 0; i < list_subs.Count; i++)
                {
                    updatesub_com.Items.Add(list_subs[i]);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            //select division
            cb = new SqlCommand("Select * from divisions order by division_name asc, semester asc", con);
            try
            {
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {
                    String sname = (string)sdr.GetString(0);
                    selsem = sdr.GetString(1);
                    selclass_com.Items.Add(sname);
                    viewdivtt_com.Items.Add(sname);
                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        void InsertProfessor()
        {
            con = new SqlConnection("Data Source=(local)\\SQLEXPRESS;Initial Catalog=sample;Integrated Security=True");
            //add professor
            cb = new SqlCommand("INSERT INTO professors (name, designation, department_id, short_name_code, phone_no) Values ('" + this.profname_text.Text + "','" + this.desg_com.Text + "','" + this.profdept_com.Text + "','" + this.profid_text.Text + "', '" + this.profno_text.Text + "')", con);
            try
            {
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {

                }
                con.Close();
                MessageBox.Show("done");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        void InsertClass()
        {
            //add classrom
            cb = new SqlCommand("INSERT INTO classrooms (classroom_code, classroom_description, room_no, block) Values ('" + this.ccode_text.Text + "','" + this.ctype_com.Text + "','" + this.rno_text.Text + "','" + this.block_com.Text + "')", con);
            try
            {
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {

                }
                con.Close();
                MessageBox.Show("done");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        void InsertSubjects()
        {
            //add subjects
            if (subsem_com.SelectedIndex == 0 || subsem_com.SelectedIndex == 2 || subsem_com.SelectedIndex == 4 || subsem_com.SelectedIndex == 6)
            {
                cb = new SqlCommand("INSERT INTO subjects_odd_sem (subject_name, subject_short_code, department_id, semester) Values ('" + this.sub_text.Text + "','" + this.subcode_text.Text + "', '" + this.subdept_com.Text + "', '" + this.subsem_com.Text + "')", con);
                try
                {
                    con.Open();
                    sdr = cb.ExecuteReader();
                    while (sdr.Read())
                    {

                    }
                    con.Close();
                    MessageBox.Show("done");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                cb = new SqlCommand("INSERT INTO subjects_even_sem (subject_name, subject_short_code, department_id, semester) Values ('" + this.sub_text.Text + "','" + this.subcode_text.Text + "', '" + this.subdept_com.Text + "', '" + this.subsem_com.Text + "')", con);
                try
                {
                    con.Open();
                    sdr = cb.ExecuteReader();
                    while (sdr.Read())
                    {

                    }
                    con.Close();
                    MessageBox.Show("done");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            assign_panel_listeners();
            panel2.Visible = false;

            ttprofsel_com.AutoCompleteMode = AutoCompleteMode.Suggest;
            ttprofsel_com.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
            getProfData(combData);

            selclass_com.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            selclass_com.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection combData1 = new AutoCompleteStringCollection();
            getClassData(combData1);

            ttprofsel_com.AutoCompleteCustomSource = combData;
            selclass_com.AutoCompleteCustomSource = combData1;
        }

        public void getClassData(AutoCompleteStringCollection dataCollection)
        {
            cb = new SqlCommand("select division_name from divisions", con);

            con.Open();

            adp = new SqlDataAdapter();
            ds = new DataSet();

            adp.SelectCommand = cb;
            adp.Fill(ds);
            adp.Dispose();
            con.Close();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                dataCollection.Add(row[0].ToString());
            }
        }

        public void getProfData(AutoCompleteStringCollection dataCollection)
        {
            cb = new SqlCommand("select name from professors", con);

            con.Open();

            adp = new SqlDataAdapter();
            ds = new DataSet();

            adp.SelectCommand = cb;
            adp.Fill(ds);
            adp.Dispose();
            con.Close();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                dataCollection.Add(row[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strtbuname = tbuname.Text;
            string strtbpwd = tbpwd.Text;

            if (strtbuname.Equals("admin") && strtbpwd.Equals("admin"))
            {
                //MessageBox.Show("Sucessful login");
                panel1.Visible = false;
                panel2.Visible = true;
            }
            else
                MessageBox.Show("Invalid Login");
        }


        private void Professors_CheckedChanged(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel3.Visible = false;
            panel5.Visible = false;
        }

        private void classradio_CheckedChanged(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel4.Visible = false;
            panel5.Visible = false;
        }

        private void subradio_CheckedChanged(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = true;
        }

        private void prof_reset_Click(object sender, EventArgs e)
        {
            profname_text.Clear();
            profid_text.Clear();
            profno_text.Clear();
            profdept_com.Text = "";
            desg_com.Text = "";

        }

        private void goBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void prof_add_Click(object sender, EventArgs e)
        {
            InsertProfessor();
        }

        private void class_add_Click(object sender, EventArgs e)
        {
            InsertClass();
        }

        private void sub_add_Click(object sender, EventArgs e)
        {
            InsertSubjects();
        }

        private void class_reset_Click(object sender, EventArgs e)
        {
            ccode_text.Clear();
            rno_text.Clear();
        }

        private void sub_reset_Click(object sender, EventArgs e)
        {
            subdept_com.Text = "";
            subsem_com.Text = "";
            sub_text.Clear();
            subcode_text.Clear();
        }

        private void prof_reset_Click_1(object sender, EventArgs e)
        {
            profname_text.Clear();
            profid_text.Clear();
            profno_text.Clear();
            profdept_com.Text = "";
            desg_com.Text = "";

        }

        private void profview_but_Click(object sender, EventArgs e)
        {
            adp = new SqlDataAdapter();
            ds = new DataSet();

            adp.SelectCommand = new SqlCommand("select id,name,designation,department_id,short_name_code,phone_no from professors;", con);
            adp.Fill(ds, "mydata");

            dt = ds.Tables["mydata"];
            dr = dt.Rows[0];
            dgvprof.DataSource = ds.Tables["mydata"];
        }

        private void classview_but_Click(object sender, EventArgs e)
        {
            adp = new SqlDataAdapter();
            ds = new DataSet();

            adp.SelectCommand = new SqlCommand("select classroom_code,classroom_description,block,room_no from classrooms;", con);
            adp.Fill(ds, "mydata");

            dt = ds.Tables["mydata"];
            dr = dt.Rows[0];
            dgvprof.DataSource = ds.Tables["mydata"];

        }

        private void dgvsub_but_Click(object sender, EventArgs e)
        {
            adp = new SqlDataAdapter();
            ds = new DataSet();

            adp.SelectCommand = new SqlCommand("select * from subjects_odd_sem union all select * from subjects_even_sem order by department asc, semester asc, subject_name asc;;", con);
            adp.Fill(ds, "mydata");

            dt = ds.Tables["mydata"];
            dr = dt.Rows[0];
            dgvprof.DataSource = ds.Tables["mydata"];
        }

        public int getprofid()
        {
            panel7.Visible = true;
            panel8.Visible = false;
            panel9.Visible = false;
            cb = new SqlCommand("select * from professors where name='" + this.updateprof_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                inum = sdr.GetInt32(0);
                pnameupdate_text.Text = sdr.GetString(1);
                pcodeupdate_text.Text = sdr.GetString(4);
                desigupdate_combo.Text = sdr.GetString(2);
                //pnoupdate_text.Text = sdr.GetString(5);
                pdeptupdate_combo.Text = sdr.GetString(3);
            }
            con.Close();
            return inum;
        }

        public string getclassid()
        {
            panel7.Visible = false;
            panel8.Visible = true;
            panel9.Visible = false;
            cb = new SqlCommand("select * from classrooms where classroom_code='" + this.updateclass_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                ccode = sdr.GetString(0);
                ccodeupdate_text.Text = sdr.GetString(0);
                ctypeupdate_com.Text = sdr.GetString(1);
                blockupdate_com.Text = sdr.GetString(2);
                rnoupdate_text.Text = sdr.GetString(3);

            }
            con.Close();
            return ccode;
        }

        public string getsuboid()
        {
            panel7.Visible = false;
            panel8.Visible = false;
            panel9.Visible = true;
            cb = new SqlCommand("select * from subjects_odd_sem where subject_name='" + this.updatesub_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                subcode = sdr.GetString(1);
                subcodeupdate_text.Text = sdr.GetString(1);
                subdeptupdate_com.Text = sdr.GetString(2);
                subsemupdate_com.Text = sdr.GetInt32(3).ToString();
                subupdate_text.Text = sdr.GetString(0);
            }
            con.Close();
            return subcode;
        }

        public string getsubeid()
        {
            panel7.Visible = false;
            panel8.Visible = false;
            panel9.Visible = true;
            cb = new SqlCommand("select * from subjects_even_sem where subject_name='" + this.updatesub_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                subcode = sdr.GetString(1);
                subcodeupdate_text.Text = sdr.GetString(1);
                subdeptupdate_com.Text = sdr.GetString(2);
                subsemupdate_com.Text = sdr.GetInt32(3).ToString();
                subupdate_text.Text = sdr.GetString(0);
            }
            con.Close();
            return subcode;
        }
        private void updateprof_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            getprofid();
        }
        private void updateclass_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            getclassid();
        }

        private void updatesub_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            getsuboid();
            getsubeid();
        }

        private void profupdate_but_Click(object sender, EventArgs e)
        {
            cb = new SqlCommand("update professors set name='" + this.pnameupdate_text.Text + "', designation='" + this.desigupdate_combo.Text + "', department_id='" + pdeptupdate_combo.Text + "', short_name_code='" + this.pcodeupdate_text.Text + "', phone_no='" + this.pnoupdate_text.Text + "' where id = '" + inum + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {

            }
            con.Close();
            MessageBox.Show("Done");
        }
        private void classupdate_but_Click(object sender, EventArgs e)
        {
            cb = new SqlCommand("update classrooms set classroom_code='" + this.ccodeupdate_text.Text + "', classroom_description='" + this.ctypeupdate_com.Text + "', block='" + blockupdate_com.Text + "', room_no='" + this.rnoupdate_text.Text + "' where classroom_code = '" + ccode + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {

            }
            con.Close();
            MessageBox.Show("Done");

        }

        private void subupdate_but_Click(object sender, EventArgs e)
        {
            if (subsemupdate_com.SelectedIndex == 0 || subsemupdate_com.SelectedIndex == 2 || subsemupdate_com.SelectedIndex == 4 || subsemupdate_com.SelectedIndex == 6)
            {
                cb = new SqlCommand("update subjects_odd_sem set subject_short_code='" + this.subsemupdate_com.Text + "', semester='" + this.subsemupdate_com.Text + "', department_id='" + subdeptupdate_com.Text + "', subject_name='" + this.subupdate_text.Text + "' where subject_short_code = '" + subcode + "';", con);
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {

                }
                con.Close();
                MessageBox.Show("Done");
            }
            else
            {
                cb = new SqlCommand("update subjects_even_sem set subject_short_code='" + this.subsemupdate_com.Text + "', semester='" + this.subsemupdate_com.Text + "', department_id='" + subdeptupdate_com.Text + "', subject_name='" + this.subupdate_text.Text + "' where subject_short_code = '" + subcode + "';", con);
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {

                }
                con.Close();
                MessageBox.Show("Done");
            }
        }

        private void ttprofsel_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb = new SqlCommand("select * from professors where name='" + this.ttprofsel_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
                selprof = sdr.GetString(4);

            con.Close();
        }

        private void ttclasssel_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            selclass = ttclasssel_com.Text;
        }

        private void ttsubsel_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb = new SqlCommand("Select * from subjects_even_sem where subject_name='" + this.ttsubsel_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                selsub = sdr.GetString(1);
            }
            con.Close();

            cb = new SqlCommand("Select * from subjects_odd_sem where subject_name='" + this.ttsubsel_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                selsub = sdr.GetString(1);
            }
            con.Close();

        }

        public void load_odd_subs()
        {
            ttsubsel_com.Items.Clear();
            cb = new SqlCommand("Select * from subjects_odd_sem where department = '" + this.selected_division[0] + "' and semester = '" + this.selected_division[2] + "' ;", con);

            ttsubsel_com.Items.Clear();
            ttselsubb1_com.Items.Clear();
            ttselsubb2_com.Items.Clear();
            ttselsubb3_com.Items.Clear();
            ttselsubb4_com.Items.Clear();

            con.Open();
            sdr = cb.ExecuteReader();

            while (sdr.Read())
            {
                ttsubsel_com.Items.Add(sdr.GetString(0));
                ttselsubb1_com.Items.Add(sdr.GetString(0));
                ttselsubb2_com.Items.Add(sdr.GetString(0));
                ttselsubb3_com.Items.Add(sdr.GetString(0));
                ttselsubb4_com.Items.Add(sdr.GetString(0));
            }
            con.Close();

        }

        public void load_even_subs()
        {
            ttsubsel_com.Items.Clear();
            cb = new SqlCommand("Select * from subjects_even_sem where department = '" + this.selected_division[0] + "' and semester = '" + this.selected_division[2] + "' ;", con);

            con.Open();

            ttsubsel_com.Items.Clear();
            ttselsubb1_com.Items.Clear();
            ttselsubb2_com.Items.Clear();
            ttselsubb3_com.Items.Clear();
            ttselsubb4_com.Items.Clear();

            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                ttsubsel_com.Items.Add(sdr.GetString(0));
                ttselsubb1_com.Items.Add(sdr.GetString(0));
                ttselsubb2_com.Items.Add(sdr.GetString(0));
                ttselsubb3_com.Items.Add(sdr.GetString(0));
                ttselsubb4_com.Items.Add(sdr.GetString(0));
            }
            con.Close();
        }

        public void creatett_but_Click(object sender, EventArgs e)
        {
            day.Text = "";
            time.Text = "";

            ttlectsel_rad.Checked = false;
            ttpracsel_rad.Checked = false;
            ttlectsel_rad.Visible = false;
            ttpracsel_rad.Visible = false;
            panel28.Visible = false;
            panel44.Visible = false;

            int j = 0;
            while (j < 50)
            {
                foreach (Control p in paneltt[j].Controls)
                {
                    if (p.GetType() == typeof(Label))
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            labelarray[j, i] = p;
                            labelarray[j, i].Text = "";
                        }
                    }
                }
                j++;
            }

            int lbl_count = 3;
            foreach (Control p in paneltt[0].Controls)
            {
                lblarr[lbl_count] = p;
                lbl_count--;
            }
            lblarr[1].Text = "                       TEST";

            lbl_count = 3;
            foreach (Control p in paneltt[1].Controls)
            {
                lblarr[lbl_count] = p;
                lbl_count--;
            }
            lblarr[1].Text = "                       TEST";

            div_temp = selclass_com.Text;
            selected_division = new string[5];
            selected_division = selclass_com.Text.Split(' ');

            if (selclass_com.SelectedIndex == -1)
            {
                MessageBox.Show("Select a class");
            }
            else
            {
                if (selected_division[2] == "1" || selected_division[2] == "3" || selected_division[2] == "5" || selected_division[2] == "7")
                    load_odd_subs();
                else if (selected_division[2] == "2" || selected_division[2] == "4" || selected_division[2] == "6" || selected_division[2] == "8")
                    load_even_subs();

                splitContainer4.Visible = true;
                panel29.Visible = false;
                lbl_sel_class.Text = div_temp;
            }

            display_timetable(div_temp, "monday", 0);
            display_timetable(div_temp, "tuesday", 10);
            display_timetable(div_temp, "wednesday", 20);
            display_timetable(div_temp, "thursday", 30);
            display_timetable(div_temp, "friday", 40);

        }

        public void display_timetable(String div_temp, String temp_day, int day_count)
        {
            string classRoom, professor, subject;
            temp_sub = ""; temp_prof = ""; temp_class = "";
            selsub_batch = new string[4];
            selprof_batch = new string[4];
            selclass_batch = new string[4];

            cb = new SqlCommand("select * from (select * from time_slots where division='" + div_temp + "') as t where t." + temp_day + " = 1", con);
            con.Open();
            sdr = cb.ExecuteReader();

            prac_counter = 0;
            while (sdr.Read())
            {
                for (int i = 5; i <= 14; i++)
                {
                    try
                    {
                        sdr.GetBoolean(21);

                        if (sdr.GetBoolean(21) && sdr.GetBoolean(i))
                        {
                            int index = Math.Abs(i - 5) + day_count;
                            for (int j = 0; j < 4; j++)
                            {
                                selclass_batch[j] = sdr.GetString(1);
                                selprof_batch[j] = sdr.GetString(2);
                                selsub_batch[j] = sdr.GetString(3);
                                sdr.Read();//for reading separate row entry of pracs
                            }

                            int lbl_count = 3;
                            foreach (Control p in paneltt[index].Controls)
                            {
                                lblarr[lbl_count] = p;
                                lbl_count--;
                            }

                            lblarr[1].Text = selsub_batch[0] + "                " + selclass_batch[0] + "               " + selprof_batch[0]; // 4 TAB spaces
                            lblarr[2].Text = selsub_batch[1] + "                " + selclass_batch[1] + "               " + selprof_batch[1];
                            lblarr[3].Text = selsub_batch[2] + "                " + selclass_batch[2] + "               " + selprof_batch[2];
                            lblarr[0].Text = selsub_batch[3] + "                " + selclass_batch[3] + "               " + selprof_batch[3];

                            prac_counter++;
                            if (prac_counter > 3 || prac_counter < 9)
                            {
                                lbl_count = 3;
                                foreach (Control p in paneltt[index + 1].Controls)
                                {
                                    lblarr[lbl_count] = p;
                                    lbl_count--;
                                }
                                lblarr[2].Text = "                              DO"; // 8 TAB Spaces
                                prac_counter = 0;
                                sdr.Read();
                                sdr.Read();
                                sdr.Read();
                                sdr.Read();
                            }
                        }
                        else if (sdr.GetBoolean(i))
                        {
                            int index = Math.Abs(i - 5) + day_count;
                            classRoom = sdr.GetString(1);
                            professor = sdr.GetString(2);
                            subject = sdr.GetString(3);

                            if (classRoom != temp_class || professor != temp_prof || subject != temp_sub) // OR condition works because we are creating a resulting set for a division.
                            {
                                int lbl_count = 3;
                                foreach (Control p in paneltt[index].Controls)
                                {
                                    lblarr[lbl_count] = p;
                                    lbl_count--;
                                }

                                lblarr[1].Text = subject + "                " + classRoom + "                " + professor;
                            }
                            else if (classRoom == temp_class && professor == temp_prof && subject == temp_sub)
                            {
                                int lbl_count = 3;
                                foreach (Control p in paneltt[index].Controls)
                                {
                                    lblarr[lbl_count] = p;
                                    lbl_count--;
                                }

                                lblarr[2].Text = "                              DO"; // 8 TAB Spaces
                            }
                            temp_prof = professor; temp_class = classRoom; temp_sub = subject;
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            con.Close();

        }

        public void getpos_Click(Object sender, EventArgs e)
        {
            panel_object = sender as Panel;

            for (int i = 0; i < 50; i++)
            {
                if (panel_object.Name == paneltt[i].Name)
                {
                    get_day_time = i;
                    get_panel = i;
                }
            }

            if (get_day_time < 10)
            {
                sday = "monday";
                switch (get_day_time)
                {
                    case 0: stime = 'a';
                        break;
                    case 1: stime = 'b';
                        break;
                    case 2: stime = 'c';
                        break;
                    case 3: stime = 'd';
                        break;
                    case 4: stime = 'e';
                        break;
                    case 5: stime = 'f';
                        break;
                    case 6: stime = 'g';
                        break;
                    case 7: stime = 'h';
                        break;
                    case 8: stime = 'i';
                        break;
                    case 9: stime = 'j';
                        break;
                }
            }
            else if (get_day_time >= 10 && get_day_time < 20)
            {
                get_day_time = get_day_time - 10;
                if (get_day_time < 10)
                {
                    sday = "tuesday";
                    switch (get_day_time)
                    {
                        case 0: stime = 'a';
                            break;
                        case 1: stime = 'b';
                            break;
                        case 2: stime = 'c';
                            break;
                        case 3: stime = 'd';
                            break;
                        case 4: stime = 'e';
                            break;
                        case 5: stime = 'f';
                            break;
                        case 6: stime = 'g';
                            break;
                        case 7: stime = 'h';
                            break;
                        case 8: stime = 'i';
                            break;
                        case 9: stime = 'j';
                            break;
                    }
                }
            }
            else if (get_day_time >= 20 && get_day_time < 30)
            {
                get_day_time = get_day_time - 20;
                if (get_day_time < 10)
                {
                    sday = "wednesday";
                    switch (get_day_time)
                    {
                        case 0: stime = 'a';
                            break;
                        case 1: stime = 'b';
                            break;
                        case 2: stime = 'c';
                            break;
                        case 3: stime = 'd';
                            break;
                        case 4: stime = 'e';
                            break;
                        case 5: stime = 'f';
                            break;
                        case 6: stime = 'g';
                            break;
                        case 7: stime = 'h';
                            break;
                        case 8: stime = 'i';
                            break;
                        case 9: stime = 'j';
                            break;
                    }
                }
            }
            else if (get_day_time >= 30 && get_day_time < 40)
            {
                get_day_time = get_day_time - 30;
                if (get_day_time < 10)
                {
                    sday = "thursday";
                    switch (get_day_time)
                    {
                        case 0: stime = 'a';
                            break;
                        case 1: stime = 'b';
                            break;
                        case 2: stime = 'c';
                            break;
                        case 3: stime = 'd';
                            break;
                        case 4: stime = 'e';
                            break;
                        case 5: stime = 'f';
                            break;
                        case 6: stime = 'g';
                            break;
                        case 7: stime = 'h';
                            break;
                        case 8: stime = 'i';
                            break;
                        case 9: stime = 'j';
                            break;
                    }
                }
            }
            else if (get_day_time >= 40 && get_day_time < 50)
            {
                get_day_time = get_day_time - 40;
                if (get_day_time < 10)
                {
                    sday = "friday";
                    switch (get_day_time)
                    {
                        case 0: stime = 'a';
                            break;
                        case 1: stime = 'b';
                            break;
                        case 2: stime = 'c';
                            break;
                        case 3: stime = 'd';
                            break;
                        case 4: stime = 'e';
                            break;
                        case 5: stime = 'f';
                            break;
                        case 6: stime = 'g';
                            break;
                        case 7: stime = 'h';
                            break;
                        case 8: stime = 'i';
                            break;
                        case 9: stime = 'j';
                            break;
                    }
                }
            }

            ttlectsel_rad.Visible = true;
            ttpracsel_rad.Visible = true;
            ttlectsel_rad.Checked = false;
            ttpracsel_rad.Checked = false;

            int count = 3;
            foreach (Control p in panel_object.Controls)
            {
                lblarr[count] = p;
                count--;
            }

            string[] temp = new string[] { };
            string[] temp1 = new string[] { "               " }; // 4 TAB spaces

            if (lblarr[1].Text != "")
            {
                if (lblarr[3].Text == "")
                {
                    if (MessageBox.Show("Do you wish to change the currently assigned slot ?\nThis will also remove the entry from database.\nYou will need to re-enter the entries after deleting.\nProceed ?", " Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        temp = lblarr[1].Text.Split(temp1, StringSplitOptions.RemoveEmptyEntries);
                        temp_sub = temp[0].Trim();
                        temp_class = temp[1].Trim();
                        temp_prof = temp[2].Trim();

                        clear_lect_method(temp_prof, temp_class, sday, stime);

                        count = 3;
                        foreach (Control p in paneltt[get_panel + 1].Controls)
                        {
                            lblarr[count] = p;
                            count--;
                        }
                        if (lblarr[2].Text == "                              DO")
                        {
                            int temp_time = stime + 1;
                            char stime1 = (char)temp_time;

                            clear_lect_method(temp_prof, temp_class, sday, stime1);
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("Do you wish to change the currently assigned slot ?\nThis will also remove the entry from database.\nYou will need to re-enter the entries after deleting.\nProceed ?", " Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        List<String> t = new List<String> { };

                        for (int j = 0; j < 4; j++)
                        {
                            t.AddRange(lblarr[j].Text.Split(temp1, StringSplitOptions.RemoveEmptyEntries));
                        }

                        for (int i = 0; i < t.Count - 2; i = i + 3)
                        {
                            clear_lect_method(t.ElementAt(i + 2).Trim(), t.ElementAt(i + 1).Trim(), sday, stime);
                        }

                        count = 3;
                        foreach (Control p in paneltt[get_panel + 1].Controls)
                        {
                            lblarr[count] = p;
                            count--;
                        }
                        if (lblarr[2].Text == "                              DO")
                        {
                            int temp_time = stime + 1;
                            char stime1 = (char)temp_time;

                            for (int i = 0; i < t.Count - 2; i = i + 3)
                            {
                                clear_lect_method(t.ElementAt(i + 2).Trim(), t.ElementAt(i + 1).Trim(), sday, stime1);
                            }
                        }
                    }
                }
            }
            else
            {
                if (lblarr[2].Text == "                              DO")
                {
                    panel28.Visible = false;
                    panel44.Visible = false;
                    ttlectsel_rad.Visible = false;
                    ttpracsel_rad.Visible = false;
                    ttlectsel_rad.Checked = false;
                    ttpracsel_rad.Checked = false;
                }
            }

            for (int i = 0; i < 50; i++)
            {
                x[i] = tableLayoutPanel1.GetCellPosition(paneltt[i]).Row;
                y[i] = tableLayoutPanel1.GetCellPosition(paneltt[i]).Column;
            }

            //adding prof to combobox based on availibility
            cb = new SqlCommand("select * from professors", con);
            con.Open();

            ttprofsel_com.Items.Clear();
            ttselprofb1_com.Items.Clear();
            ttselprofb2_com.Items.Clear();
            ttselprofb3_com.Items.Clear();
            ttselprofb4_com.Items.Clear();

            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                ttprofsel_com.Items.Add(sdr.GetString(1));
                ttselprofb1_com.Items.Add(sdr.GetString(1));
                ttselprofb2_com.Items.Add(sdr.GetString(1));
                ttselprofb3_com.Items.Add(sdr.GetString(1));
                ttselprofb4_com.Items.Add(sdr.GetString(1));
            }
            con.Close();

            cb = new SqlCommand("select * from professor_tt where " + this.sday + " = 1 and " + this.stime + " = 1 ", con);
            con.Open();
            String[] str_remprof = new String[500];
            count = 0;
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                str_remprof[count] = sdr.GetString(1);
                count++;
            }
            con.Close();

            foreach (string p in str_remprof)
            {
                ttprofsel_com.Items.Remove(p);
                ttselprofb1_com.Items.Remove(p);
                ttselprofb2_com.Items.Remove(p);
                ttselprofb3_com.Items.Remove(p);
                ttselprofb4_com.Items.Remove(p);
            }

            //adding classrooms based on availibility
            cb = new SqlCommand("select * from classrooms where classroom_description = 'Classroom'", con);
            con.Open();

            ttclasssel_com.Items.Clear();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                ttclasssel_com.Items.Add(sdr.GetString(0));
            }
            con.Close();

            cb = new SqlCommand("select * from classroom_tt where " + this.sday + " = 1 and " + this.stime + " = 1 ", con);
            con.Open();
            String[] str_remclass = new String[500];
            count = 0;
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                str_remclass[count] = sdr.GetString(1);
                count++;
            }
            con.Close();

            foreach (string p in str_remclass)
            {
                ttclasssel_com.Items.Remove(p);
            }

            //adding labs based on availibility
            cb = new SqlCommand("select * from classrooms where classroom_description = 'Lab'", con);
            con.Open();

            ttselclassb1_com.Items.Clear();
            ttselclassb2_com.Items.Clear();
            ttselclassb3_com.Items.Clear();
            ttselclassb4_com.Items.Clear();

            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                ttselclassb1_com.Items.Add(sdr.GetString(0));
                ttselclassb2_com.Items.Add(sdr.GetString(0));
                ttselclassb3_com.Items.Add(sdr.GetString(0));
                ttselclassb4_com.Items.Add(sdr.GetString(0));
            }
            con.Close();

            cb = new SqlCommand("select * from classroom_tt where " + this.sday + " = 1 and " + this.stime + " = 1 ", con);
            con.Open();
            String[] str_remlab = new String[500];
            count = 0;
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                str_remlab[count] = sdr.GetString(1);
                count++;
            }
            con.Close();

            foreach (string p in str_remlab)
            {
                ttselclassb1_com.Items.Remove(p);
                ttselclassb2_com.Items.Remove(p);
                ttselclassb3_com.Items.Remove(p);
                ttselclassb4_com.Items.Remove(p);
            }

            if (panel_object != null)
            {
                for (int i = 0; i < 50; i++)
                {
                    if (panel_object.Name.Equals(paneltt[i].Name))
                    {
                        switch (y[i])
                        {
                            case 1: day.Text = "Monday";
                                break;
                            case 2: day.Text = "Tuesday";
                                break;
                            case 3: day.Text = "Wednesday";
                                break;
                            case 4: day.Text = "Thursday";
                                break;
                            case 5: day.Text = "Friday";
                                break;
                        }

                        switch (x[i])
                        {

                            case 1: time.Text = label37.Text;
                                break;
                            case 2: time.Text = label35.Text;
                                break;
                            case 3: time.Text = label44.Text;
                                break;
                            case 4: time.Text = label45.Text;
                                break;
                            case 5: time.Text = label46.Text;
                                break;
                            case 6: time.Text = label47.Text;
                                break;
                            case 7: time.Text = label48.Text;
                                break;
                            case 8: time.Text = label49.Text;
                                break;
                            case 9: time.Text = label50.Text;
                                break;
                            case 10: time.Text = label51.Text;
                                break;
                        }
                    }
                }
            }
        }

        private void cellsubmit_but_Click(object sender, EventArgs e)
        {
            int count = 3;
            foreach (Control p in panel_object.Controls)
            {
                lblarr[count] = p;
                count--;
            }

            if (ttlectsel_rad.Checked == true)
            {
                if (selclass.Equals("") || selprof.Equals("") || selsub.Equals(""))
                    MessageBox.Show("Invalid Entries");
                else if ((selclass != "" || selclass != "Classroom") && (selprof != "" || selprof != "Professor") && (selsub != "" || selsub != "Subject"))
                {
                    temp_prof = selprof;
                    temp_class = selclass;
                    temp_sub = selsub;

                    lblarr[1].Text = selsub + "             " + selclass + "             " + selprof; // 4 TAB spaces
                    lblarr[2].Text = "";
                    lblarr[3].Text = "";
                    lblarr[0].Text = "";

                    cb = new SqlCommand("insert into professor_tt(name,short_name," + stime + "," + sday + ") values('" + ttprofsel_com.Text + "','" + selprof + "',1,1);insert into classroom_tt(class_code,class_desc," + stime + "," + sday + ") values('" + ttclasssel_com.Text + "','Classroom',1,1);insert into time_slots(class_room,professor_name,subject,division," + stime + "," + sday + ") values('" + selclass + "','" + selprof + "','" + selsub + "','" + div_temp + "',1,1)", con);
                    con.Open();
                    sdr = cb.ExecuteReader();
                    while (sdr.Read())
                    {

                    }
                    con.Close();

                    if (chkbox_lect_do.Checked == true)
                    {
                        count = 3;
                        foreach (Control c in paneltt[get_panel + 1].Controls)
                        {
                            lblarr[count] = c;
                            count--;
                        }
                        int next_slot = (int)stime + 1;
                        char stime1 = (char)next_slot;

                        cb = new SqlCommand("insert into professor_tt(name,short_name," + stime1 + "," + sday + ") values('" + ttprofsel_com.Text + "','" + selprof + "',1,1);insert into classroom_tt(class_code,class_desc," + stime1 + "," + sday + ") values('" + ttclasssel_com.Text + "','Classroom',1,1);insert into time_slots(class_room,professor_name,subject,division," + stime1 + "," + sday + ") values('" + selclass + "','" + selprof + "','" + selsub + "','" + div_temp + "',1,1)", con);
                        con.Open();
                        sdr = cb.ExecuteReader();
                        while (sdr.Read())
                        {

                        }
                        con.Close();
                        lblarr[2].Text = "                              DO"; // 8 TAB Spaces
                    }
                    chkbox_lect_do.Checked = false;

                    selsub = "";
                    selprof = "";
                    selclass = "";
                }
            }
            //ttlectsel_rad.Visible = false;
            //ttpracsel_rad.Visible = false;
            ttlectsel_rad.Checked = false;
            ttpracsel_rad.Checked = false;
            //panel28.Visible = false;

            ttprofsel_com.Text = "Professor";
            ttclasssel_com.Text = "Classroom";
            ttsubsel_com.Text = "Subject";

            selsub = "";
            selprof = "";
            selclass = "";
        }

        private void ttlectsel_rad_CheckedChanged(object sender, EventArgs e)
        {
            panel28.Visible = true;
            panel44.Visible = false;
        }

        private void delp_rad_CheckedChanged(object sender, EventArgs e)
        {
            panel18.Visible = true;
            panel30.Visible = false;
            panel39.Visible = false;
            delete.Visible = true;
        }

        private void delc_rad_CheckedChanged(object sender, EventArgs e)
        {
            panel30.Visible = true;
            panel18.Visible = false;
            panel39.Visible = false;
            delete.Visible = true;
        }

        private void dels_rad_CheckedChanged(object sender, EventArgs e)
        {
            panel39.Visible = true;
            panel30.Visible = false;
            panel18.Visible = false;
            delete.Visible = true;
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (delp_rad.Checked == true)
            {
                cb = new SqlCommand("delete from professors where name='" + this.delp_com.Text + "';", con);
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {

                }
                con.Close();
            }

            if (delc_rad.Checked == true)
            {
                cb = new SqlCommand("delete  from classrooms where classroom_code='" + this.delc_com.Text + "';", con);
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {

                }
                con.Close();
            }

            if (dels_rad.Checked == true)
            {
                if (del_sub_even_rad.Checked == true)
                {
                    cb = new SqlCommand("Delete from subjects_even_sem where subject_name = '" + this.dels_com.Text + "';", con);
                    con.Open();
                    sdr = cb.ExecuteReader();
                    while (sdr.Read())
                    {
                    }
                    con.Close();
                }
                else if (del_sub_odd_rad.Checked == true)
                {
                    cb = new SqlCommand("Delete from subjects_odd_sem where subject_name = '" + this.dels_com.Text + "';", con);
                    con.Open();
                    sdr = cb.ExecuteReader();
                    while (sdr.Read())
                    {
                    }
                    con.Close();
                }
            }
        }

        private void del_sub_even_rad_CheckedChanged(object sender, EventArgs e)
        {
            dels_com.Items.Clear();
            if (del_sub_even_rad.Checked == true)
            {
                cb = new SqlCommand("Select * from subjects_even_sem", con);
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {
                    subdel = sdr.GetString(0);
                    dels_com.Items.Add(subdel);
                }
                con.Close();
            }
        }

        private void del_sub_odd_rad_CheckedChanged(object sender, EventArgs e)
        {
            dels_com.Items.Clear();
            if (del_sub_odd_rad.Checked == true)
            {
                cb = new SqlCommand("Select * from subjects_odd_sem", con);
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {
                    subdel = sdr.GetString(0);
                    dels_com.Items.Add(subdel);
                }
                con.Close();
            }
        }

        private void ttpracsel_rad_CheckedChanged(object sender, EventArgs e)
        {
            panel28.Visible = false;
            panel44.Visible = true;
        }

        private void tt_back_btn_Click(object sender, EventArgs e)
        {
            splitContainer4.Visible = false;
            panel29.Visible = true;

            ttprofsel_com.Text = "Professor";
            ttclasssel_com.Text = "Classroom";
            ttsubsel_com.Text = "Subject";
        }

        private void ttselsubb1_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb = new SqlCommand("Select * from subjects_even_sem where subject_name='" + this.ttselsubb1_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                selsub_batch1 = sdr.GetString(1);
            }
            con.Close();

            cb = new SqlCommand("Select * from subjects_odd_sem where subject_name='" + this.ttselsubb1_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                selsub_batch1 = sdr.GetString(1);
            }
            con.Close();
        }

        private void ttselsubb2_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb = new SqlCommand("Select * from subjects_even_sem where subject_name='" + this.ttselsubb2_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                selsub_batch2 = sdr.GetString(1);
            }
            con.Close();

            cb = new SqlCommand("Select * from subjects_odd_sem where subject_name='" + this.ttselsubb2_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                selsub_batch2 = sdr.GetString(1);
            }
            con.Close();
        }

        private void ttselsubb3_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb = new SqlCommand("Select * from subjects_even_sem where subject_name='" + this.ttselsubb3_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                selsub_batch3 = sdr.GetString(1);
            }
            con.Close();

            cb = new SqlCommand("Select * from subjects_odd_sem where subject_name='" + this.ttselsubb3_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                selsub_batch3 = sdr.GetString(1);
            }
            con.Close();
        }

        private void ttselsubb4_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb = new SqlCommand("Select * from subjects_even_sem where subject_name='" + this.ttselsubb4_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                selsub_batch4 = sdr.GetString(1);
            }
            con.Close();

            cb = new SqlCommand("Select * from subjects_odd_sem where subject_name='" + this.ttselsubb4_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {
                selsub_batch4 = sdr.GetString(1);
            }
            con.Close();
        }

        private void ttselprofb1_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb = new SqlCommand("select * from professors where name='" + this.ttselprofb1_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
                selprof_batch1 = sdr.GetString(4);

            con.Close();
        }

        private void ttselprofb2_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb = new SqlCommand("select * from professors where name='" + this.ttselprofb2_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
                selprof_batch2 = sdr.GetString(4);

            con.Close();
        }

        private void ttselprofb3_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb = new SqlCommand("select * from professors where name='" + this.ttselprofb3_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
                selprof_batch3 = sdr.GetString(4);

            con.Close();
        }

        private void ttselprofb4_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb = new SqlCommand("select * from professors where name='" + this.ttselprofb4_com.Text + "';", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
                selprof_batch4 = sdr.GetString(4);

            con.Close();
        }

        private void ttselclassb1_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            selclass_batch1 = ttselclassb1_com.Text;
        }

        private void ttselclassb2_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            selclass_batch2 = ttselclassb2_com.Text;
        }

        private void ttselclassb3_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            selclass_batch3 = ttselclassb3_com.Text;
        }

        private void ttselclassb4_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            selclass_batch4 = ttselclassb4_com.Text;
        }

        private void subprac_but_Click(object sender, EventArgs e)
        {
            int count = 3;
            foreach (Control p in panel_object.Controls)
            {
                lblarr[count] = p;
                count--;
            }

            selsub_batch = new string[4];
            selsub_batch[0] = selsub_batch1;
            selsub_batch[1] = selsub_batch2;
            selsub_batch[2] = selsub_batch3;
            selsub_batch[3] = selsub_batch4;

            selprof_batch = new string[4];
            selprof_batch[0] = selprof_batch1;
            selprof_batch[1] = selprof_batch2;
            selprof_batch[2] = selprof_batch3;
            selprof_batch[3] = selprof_batch4;

            selclass_batch = new string[4];
            selclass_batch[0] = selclass_batch1;
            selclass_batch[1] = selclass_batch2;
            selclass_batch[2] = selclass_batch3;
            selclass_batch[3] = selclass_batch4;

            bool prof_flag = false, class_flag = false, sub_flag = false;

            for (int i = 0; i < 3; i++)
            {
                if (selclass_batch[i] != selprof_batch[i + 1])
                    class_flag = true;
            }

            for (int i = 0; i < 3; i++)
            {
                if (selprof_batch[i] != selprof_batch[i + 1])
                    prof_flag = true;
            }

            for (int i = 0; i < 3; i++)
            {
                if (selsub_batch[i] != selsub_batch[i + 1])
                    sub_flag = true;
            }

            if (class_flag == true && prof_flag == true && sub_flag == true)
            {
                lblarr[1].Text = selsub_batch1 + "              " + selclass_batch1 + "              " + selprof_batch1; // 4 TAB spaces
                lblarr[2].Text = selsub_batch2 + "              " + selclass_batch2 + "              " + selprof_batch2;
                lblarr[3].Text = selsub_batch3 + "              " + selclass_batch3 + "              " + selprof_batch3;
                lblarr[0].Text = selsub_batch4 + "              " + selclass_batch4 + "              " + selprof_batch4;

                string prof1 = ttselprofb1_com.Text, prof2 = ttselprofb2_com.Text, prof3 = ttselprofb3_com.Text, prof4 = ttselprofb4_com.Text;

                insert_pracs1(selclass_batch1, prof1, selprof_batch1, selsub_batch1);
                insert_pracs1(selclass_batch2, prof2, selprof_batch2, selsub_batch2);
                insert_pracs1(selclass_batch3, prof3, selprof_batch3, selsub_batch3);
                insert_pracs1(selclass_batch4, prof4, selprof_batch4, selsub_batch4);

                insert_pracs2(selclass_batch1, prof1, selprof_batch1, selsub_batch1);
                insert_pracs2(selclass_batch2, prof2, selprof_batch2, selsub_batch2);
                insert_pracs2(selclass_batch3, prof3, selprof_batch3, selsub_batch3);
                insert_pracs2(selclass_batch4, prof4, selprof_batch4, selsub_batch4);

                count = 3;
                foreach (Control c in paneltt[get_panel + 1].Controls)
                {
                    lblarr[count] = c;
                    count--;
                }

                lblarr[2].Text = "                              DO"; // 8 TAB spaces

                selprof_batch1 = selprof_batch2 = selprof_batch3 = selprof_batch4 = "";
                selsub_batch1 = selsub_batch2 = selsub_batch3 = selsub_batch4 = "";
                selclass_batch1 = selclass_batch2 = selclass_batch3 = selclass_batch4 = "";

            }
            else if (class_flag == false || prof_flag == false || sub_flag == false)
                MessageBox.Show("Invalid Entries");

            ttselprofb1_com.Text = ""; ttselprofb2_com.Text = ""; ttselprofb3_com.Text = ""; ttselprofb4_com.Text = "";
            ttselsubb1_com.Text = ""; ttselsubb2_com.Text = ""; ttselsubb3_com.Text = ""; ttselsubb4_com.Text = "";
            ttselclassb1_com.Text = ""; ttselclassb2_com.Text = ""; ttselclassb3_com.Text = ""; ttselclassb4_com.Text = "";

            ttlectsel_rad.Checked = false;
            ttpracsel_rad.Checked = false;
            panel28.Visible = false;

        }

        private void insert_pracs1(string sel_lab, string sel_prof_name, string sel_prof_short_name, string sel_sub)
        {
            cb = new SqlCommand("insert into professor_tt(name,short_name," + stime + "," + sday + ",practicals) values('" + sel_prof_name + "','" + sel_prof_short_name + "',1,1,1);insert into classroom_tt(class_code,class_desc," + stime + "," + sday + ",practicals) values('" + sel_lab + "','Lab',1,1,1);insert into time_slots(class_room,professor_name,subject,division," + stime + "," + sday + ",practicals) values('" + sel_lab + "','" + sel_prof_short_name + "','" + sel_sub + "','" + div_temp + "',1,1,1)", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {

            }
            con.Close();
        }

        private void insert_pracs2(string sel_lab, string sel_prof_name, string sel_prof_short_name, string sel_sub)
        {
            int temp = (int)stime + 1;
            char stime1 = (char)temp;

            cb = new SqlCommand("insert into professor_tt(name,short_name," + stime1 + "," + sday + ",practicals) values('" + sel_prof_name + "','" + sel_prof_short_name + "',1,1,1);insert into classroom_tt(class_code,class_desc," + stime1 + "," + sday + ",practicals) values('" + sel_lab + "','Lab',1,1,1); insert into time_slots(class_room,professor_name,subject,division," + stime1 + "," + sday + ",practicals) values('" + sel_lab + "','" + sel_prof_short_name + "','" + sel_sub + "','" + div_temp + "',1,1,1);", con);
            //insert into time_slots(class_room,professor_name,subject,division," + stime1 + "," + sday + ",practicals) values('" + sel_lab + "','" + sel_prof_short_name + "','" + sel_sub + "','" + div_temp + "',1,1,1)

            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            {

            }
            con.Close();
        }

        public void clear_lect_method(string prof_short_name, string class_code, string sday_param, char stime_param)
        {
            cb = new SqlCommand("delete from time_slots where professor_name = '" + prof_short_name + "' and " + sday_param + " = 1 and " + stime_param + " = 1 ; ", con);
            cb1 = new SqlCommand("delete from professor_tt where short_name = '" + prof_short_name + "' and " + sday_param + " = 1 and " + stime_param + " = 1; ", con);
            cb2 = new SqlCommand("delete from classroom_tt where class_code = '" + class_code + "' and " + sday_param + " = 1 and " + stime_param + " = 1;", con);
            con.Open();
            sdr = cb.ExecuteReader();
            while (sdr.Read())
            { }
            con.Close();

            con.Open();
            sdr = cb1.ExecuteReader();
            while (sdr.Read())
            { }
            con.Close();

            con.Open();
            sdr = cb2.ExecuteReader();
            while (sdr.Read())
            { }
            con.Close();

            lblarr[1].Text = "";
            lblarr[2].Text = "";
            lblarr[3].Text = "";
            lblarr[0].Text = "";
        }

        private void btn_print_Click(object sender, EventArgs e)
        {

            using (Bitmap printImage = new Bitmap(tableLayoutPanel1.Width, tableLayoutPanel1.Height))
            {
                //Draw the TableLayoutPanel control to the temporary bitmap image
                tableLayoutPanel1.DrawToBitmap(printImage, new Rectangle(0, 0, printImage.Width, printImage.Height));
                printImage.Save(@"D:\Foto.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                // will print the temporary image you just created)
                //printDialog1.Document = PrintDoc1;
                //PaperSize ps = new PaperSize();
                //ps.RawKind = (int)PaperKind.A4;
                //PrintDoc1.DefaultPageSettings.PaperSize = ps;
                PrintDoc1.PrintPage += printDocument2_PrintPage;
                printDialog1.AllowSomePages = true;
                printDialog1.ShowHelp = true;
                PrintDoc1.DefaultPageSettings.Landscape = true;
                printPreviewDialog1.Document = PrintDoc1;
                printPreviewDialog1.Document.DefaultPageSettings.Landscape = true;
                printDialog1.ShowDialog(); ;
                DialogResult result = printPreviewDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    PrintDoc1.Print();//this will trigger the Print Event handeler PrintPage
                }
            }
        }

        public void printDocument2_PrintPage(Object o, PrintPageEventArgs e)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile("D:\\Foto.jpg");
                Point loc = new Point(0, 0);
                //e.Graphics.DrawImage(img, loc);
                if (File.Exists("D:\\Foto.jpg"))
                {
                    //Adjust the size of the image to the page to print the full image without loosing any part of it
                    Rectangle m = e.MarginBounds;

                    if ((double)img.Width / (double)img.Height > (double)m.Width / (double)m.Height) // image is wider
                    {
                        m.Height = (int)((double)img.Height / (double)img.Width * (double)m.Width);
                    }
                    else
                    {
                        m.Width = (int)((double)img.Width / (double)img.Height * (double)m.Height);
                    }
                    e.Graphics.DrawImage(img, m);
                }
            }
            catch (Exception)
            {

            }
        }

        private void viewclasstt_rad_CheckedChanged(object sender, EventArgs e)
        {
            viewproftt_com.Visible = false;
            viewdivtt_com.Visible = false;
            viewclasstt_com.Visible = true;
            viewtt_btn.Visible = true;
        }

        private void viewdivtt_rad_CheckedChanged(object sender, EventArgs e)
        {
            viewproftt_com.Visible = false;
            viewdivtt_com.Visible = true;
            viewclasstt_com.Visible = false;
            viewtt_btn.Visible = true;
        }

        private void viewproftt_rad_CheckedChanged(object sender, EventArgs e)
        {
            viewproftt_com.Visible = true;
            viewdivtt_com.Visible = false;
            viewclasstt_com.Visible = false;
            viewtt_btn.Visible = true;
        }

        private void viewtt_btn_Click(object sender, EventArgs e)
        {
            if (viewproftt_rad.Checked == true)
            {
                string prof_name = "";
                cb = new SqlCommand("select short_name_code from professors where name = '" + viewproftt_com.Text + "' ;", con);
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {
                    prof_name = sdr.GetString(0);
                }
                con.Close();
                Form2 f2 = new Form2(prof_name);
                f2.Show();

                f2.display_prof_tt(prof_name, "monday", 0);
                f2.display_prof_tt(prof_name, "tuesday", 10);
                f2.display_prof_tt(prof_name, "wednesday", 20);
                f2.display_prof_tt(prof_name, "thursday", 30);
                f2.display_prof_tt(prof_name, "friday", 40);
            }
            else if (viewclasstt_rad.Checked == true)
            {
                Form2 f2 = new Form2(viewclasstt_com.Text);
                f2.Show();

                f2.display_class_tt(viewclasstt_com.Text, "monday", 0);
                f2.display_class_tt(viewclasstt_com.Text, "tuesday", 10);
                f2.display_class_tt(viewclasstt_com.Text, "wednesday", 20);
                f2.display_class_tt(viewclasstt_com.Text, "thursday", 30);
                f2.display_class_tt(viewclasstt_com.Text, "friday", 40);
            }
            else if (viewdivtt_rad.Checked == true)
            {
                Form2 f2 = new Form2(viewdivtt_com.Text);
                f2.Show();

                f2.display_div_tt(viewdivtt_com.Text, "monday", 0);
                f2.display_div_tt(viewdivtt_com.Text, "tuesday", 10);
                f2.display_div_tt(viewdivtt_com.Text, "wednesday", 20);
                f2.display_div_tt(viewdivtt_com.Text, "thursday", 30);
                f2.display_div_tt(viewdivtt_com.Text, "friday", 40);
            }
        }

        private void search_but_Click(object sender, EventArgs e)
        {
            dgvsearch.Rows.Clear();
            dgvsearch.Columns.Clear();

            sday = searchday_com.Text.ToLower();

            switch (searchtimeslot_com.SelectedIndex + 1)
            {
                case 1: stime = 'a';
                    break;
                case 2: stime = 'b';
                    break;
                case 3: stime = 'c';
                    break;
                case 4: stime = 'd';
                    break;
                case 5: stime = 'e';
                    break;
                case 6: stime = 'f';
                    break;
                case 7: stime = 'g';
                    break;
                case 8: stime = 'h';
                    break;
                case 9: stime = 'i';
                    break;
                case 10: stime = 'j';
                    break;
            }


            if (searchprof_rad.Checked == true)
            {
                String[] prof_load = new String[200];
                String[] prof_rem = new String[200];
                cb = new SqlCommand("Select name from professors", con);
                int count = 0;
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {
                    prof_load[count] = sdr.GetString(0);
                    count++;
                }
                con.Close();

                cb = new SqlCommand("select name from professor_tt where " + sday + " = 1 and " + stime + " = 1;", con);
                count = 0;
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {
                    prof_rem[count] = sdr.GetString(0);
                    count++;
                }
                con.Close();

                var listCommon = prof_load.Except(prof_rem);

                dgvsearch.Columns.Add("Name", "Name");
                foreach (String s in listCommon)
                {
                    dgvsearch.Rows.Add(s);
                }
            }
            else if (searchclass_rad.Checked == true)
            {
                String[] class_load = new String[200];
                String[] class_rem = new String[200];
                cb = new SqlCommand("Select classroom_code from classrooms where classroom_description = 'Classroom' ;", con);
                int count = 0;
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {
                    class_load[count] = sdr.GetString(0);
                    count++;
                }
                con.Close();

                cb = new SqlCommand("select class_code from classroom_tt where " + sday + " = 1 and " + stime + " = 1 and class_desc = 'Classroom' ;", con);
                count = 0;
                con.Open();
                sdr = cb.ExecuteReader();
                while (sdr.Read())
                {
                    class_rem[count] = sdr.GetString(0);
                    count++;
                }
                con.Close();

                var listCommon = class_load.Except(class_rem);

                dgvsearch.Columns.Add("Name", "Name");
                foreach (String s in listCommon)
                {
                    dgvsearch.Rows.Add(s);
                }
            }
        }
    }
}