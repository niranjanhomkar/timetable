using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Demo_1
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand cb;
        SqlDataReader sdr;
        int prac_counter = 0;
        Panel[] paneltt = new Panel[50];
        Control[,] labelarray = new Control[50, 4];
        Control[] lblarr = new Label[4];
        string incoming_string = "";
        string[] selclass_batch, selprof_batch, selsub_batch, seldiv_batch;
        string temp_prof = "", temp_class = "", temp_sub = "", temp_div = "";
        string classRoom = "", division = "", subject = "", professor = "";
        PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        PrintDocument PrintDoc1 = new PrintDocument();
        Form1 f1 = new Form1();

        public Form2(String query_str)
        {
            incoming_string = query_str;
            InitializeComponent();
            assign_panel_listeners();
            //con = new SqlConnection("Data Source=niranjan.no-ip.org;Network Library=DBMSSOCN;Initial Catalog=sample;User ID=sa;Password=computer6268");
            con = new SqlConnection("Data Source=(local)\\SQLEXPRESS;Initial Catalog=sample;Integrated Security=True");
        }

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
        }

        public void Form2_Load(object sender, EventArgs e)
        {
            //f1.display_timetable();
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

        private void button1_Click(object sender, EventArgs e)
        {

            using (Bitmap printImage = new Bitmap(panel1.Width, panel1.Height))
            {
                //Draw the TableLayoutPanel control to the temporary bitmap image
                panel1.DrawToBitmap(printImage, new Rectangle(0, 0, printImage.Width, printImage.Height));
                printImage.Save(@"D:\Foto.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                // will print the temporary image you just created)
                //printDialog1.Document = PrintDoc1;
                //PaperSize ps = new PaperSize();
                //ps.RawKind = (int)PaperKind.A4;
                //PrintDoc1.DefaultPageSettings.PaperSize = ps;
                PrintDoc1.PrintPage += printDocument2_PrintPage;
                //printPreviewDialog1.AllowSomePages = true;
                //printPreviewDialog1.ShowHelp = true;
                PrintDoc1.DefaultPageSettings.Landscape = true;
                printPreviewDialog1.Document = PrintDoc1;
                printPreviewDialog1.Document.DefaultPageSettings.Landscape = true;
                printPreviewDialog1.ShowDialog(); ;
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

        public void display_prof_tt(String prof_temp, String temp_day, int day_count)
        {
            label2.Text = incoming_string;
            temp_sub = ""; temp_class = ""; temp_div = "";
            selsub_batch = new string[4];
            seldiv_batch = new string[4];
            selclass_batch = new string[4];

            cb = new SqlCommand("select * from (select * from time_slots where professor_name = '" + prof_temp + "') as t where t." + temp_day + " = 1", con);
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

                            selclass_batch[0] = sdr.GetString(1);
                            seldiv_batch[0] = sdr.GetString(4);
                            selsub_batch[0] = sdr.GetString(3);


                            if (selclass_batch[0] != temp_class || seldiv_batch[0] != temp_div || selsub_batch[0] != temp_sub)
                            {
                                int lbl_count = 3;
                                foreach (Control p in paneltt[index].Controls)
                                {
                                    lblarr[lbl_count] = p;
                                    lbl_count--;
                                }

                                lblarr[1].Text = seldiv_batch[0] + "     " + selsub_batch[0] + "     " + selclass_batch[0]; // 4 TAB spaces

                            }
                            else if (selclass_batch[0] == temp_class && seldiv_batch[0] == temp_div && selsub_batch[0] == temp_sub)
                            {
                                int lbl_count = 3;
                                foreach (Control p in paneltt[index].Controls)
                                {
                                    lblarr[lbl_count] = p;
                                    lbl_count--;
                                }

                                lblarr[2].Text = "                              DO"; // 8 TAB Spaces
                            }
                            temp_div = seldiv_batch[0]; temp_class = selclass_batch[0]; temp_sub = selsub_batch[0];

                            /*prac_counter++;
                            if (prac_counter > 1 || prac_counter < 3)
                            {
                                lbl_count = 3;
                                foreach (Control p in paneltt[index + 1].Controls)
                                {
                                    lblarr[lbl_count] = p;
                                    lbl_count--;
                                }
                                lblarr[2].Text = "                              DO"; // 8 TAB Spaces
                                prac_counter = 0;
                                //sdr.Read(); // no need for displaying professor tt
                                //sdr.Read();
                            }*/
                        }
                        else if (sdr.GetBoolean(i))
                        {
                            int index = Math.Abs(i - 5) + day_count;
                            classRoom = sdr.GetString(1);
                            division = sdr.GetString(4);
                            subject = sdr.GetString(3);

                            if (classRoom != temp_class || division != temp_div || subject != temp_sub)
                            {
                                int lbl_count = 3;
                                foreach (Control p in paneltt[index].Controls)
                                {
                                    lblarr[lbl_count] = p;
                                    lbl_count--;
                                }

                                lblarr[1].Text = division + "     " + subject + "     " + classRoom;
                            }
                            else if (classRoom == temp_class && division == temp_div && subject == temp_sub)
                            {
                                int lbl_count = 3;
                                foreach (Control p in paneltt[index].Controls)
                                {
                                    lblarr[lbl_count] = p;
                                    lbl_count--;
                                }

                                lblarr[2].Text = "                              DO"; // 8 TAB Spaces
                            }
                            temp_div = division; temp_class = classRoom; temp_sub = subject;
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            con.Close();
        }

        public void display_class_tt(String class_temp, String temp_day, int day_count)
        {
            label2.Text = incoming_string;
            temp_sub = ""; temp_prof = ""; temp_div = "";
            selsub_batch = new string[4];
            seldiv_batch = new string[4];
            selprof_batch = new string[4];

            cb = new SqlCommand("select * from (select * from time_slots where class_room = '" + class_temp.Trim() + "') as t where t." + temp_day + " = 1", con);
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
                            for (int j = 0; j < 2; j++)
                            {
                                selprof_batch[j] = sdr.GetString(2);
                                seldiv_batch[j] = sdr.GetString(4);
                                selsub_batch[j] = sdr.GetString(3);
                                sdr.Read();//for reading separate row entry of pracs
                            }

                            int lbl_count = 3;
                            foreach (Control p in paneltt[index].Controls)
                            {
                                lblarr[lbl_count] = p;
                                lbl_count--;
                            }

                            lblarr[1].Text = seldiv_batch[0] + "      " + selsub_batch[0] + "      " + selprof_batch[0]; // 4 TAB spaces
                            
                            prac_counter++;
                            if (prac_counter > 0 || prac_counter < 2)
                            {
                                lbl_count = 3;
                                foreach (Control p in paneltt[index + 1].Controls)
                                {
                                    lblarr[lbl_count] = p;
                                    lbl_count--;
                                }
                                lblarr[2].Text = "                              DO"; // 8 TAB Spaces
                                //sdr.Read(); // no need for displaying professor tt
                                //sdr.Read();
                            }
                        }
                        else if (sdr.GetBoolean(i))
                        {
                            int index = Math.Abs(i - 5) + day_count;
                            professor = sdr.GetString(2);
                            division = sdr.GetString(4);
                            subject = sdr.GetString(3);

                            if (professor != temp_prof || division != temp_div || subject != temp_sub)
                            {
                                int lbl_count = 3;
                                foreach (Control p in paneltt[index].Controls)
                                {
                                    lblarr[lbl_count] = p;
                                    lbl_count--;
                                }

                                lblarr[1].Text = division + "        " + subject + "        " + professor;
                            }
                            else if (professor == temp_prof && division == temp_div && subject == temp_sub)
                            {
                                int lbl_count = 3;
                                foreach (Control p in paneltt[index].Controls)
                                {
                                    lblarr[lbl_count] = p;
                                    lbl_count--;
                                }

                                lblarr[2].Text = "                              DO"; // 8 TAB Spaces
                            }
                            temp_div = division; temp_prof = professor; temp_sub = subject;
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            con.Close();

            int lbl_count1 = 3;
            foreach (Control p in paneltt[0].Controls)
            {
                lblarr[lbl_count1] = p;
                lbl_count1--;
            }
            lblarr[1].Text = "                       TEST";

            lbl_count1 = 3;
            foreach (Control p in paneltt[1].Controls)
            {
                lblarr[lbl_count1] = p;
                lbl_count1--;
            }
            lblarr[1].Text = "                       TEST";
        }

        public void display_div_tt(String div_temp, String temp_day, int day_count)
        {
            label2.Text = incoming_string;
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

            int lbl_count1 = 3;
            foreach (Control p in paneltt[0].Controls)
            {
                lblarr[lbl_count1] = p;
                lbl_count1--;
            }
            lblarr[1].Text = "                       TEST";

            lbl_count1 = 3;
            foreach (Control p in paneltt[1].Controls)
            {
                lblarr[lbl_count1] = p;
                lbl_count1--;
            }
            lblarr[1].Text = "                       TEST";
        }

        private void gobackview_but_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        
    }
}