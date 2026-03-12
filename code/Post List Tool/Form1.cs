using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Drawing.Printing;

namespace Post_List_Tool

{
    public partial class FrmRoyalMail : Form
    {
        private DataTable dataTable;
        PrintDocument pdoc = null;
        string trigram = "";
        string duedate = "";
        string Copy = "";
        string printer = "";
        string DespatchTypes = "";
        string DespatchList = "";
        int location;
        int startpage = 0;
        int itemsperpage = 15;
        int pagenumber = 1;
        int Apos;
        int Bpos;
        bool split_wo = false;
        bool from_Preview = false;
        int TotalQTY = 0;
        int TotalPulls = 0;
        int TotalProdSupp = 0;
        int TotalDespatch = 0;
        int TotalTrays = 0;
        int scannedItems;
        bool exists = false;
        public FrmRoyalMail()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
            dataTable = new DataTable();

            // Add columns to the DataTable
            dataTable.Columns.Add("Work Order", typeof(string));
            dataTable.Columns.Add("Despatch", typeof(string));
            dataTable.Columns.Add("Qty Carriers", typeof(string));

            dataTable.Columns.Add("Pulls", typeof(string));
            dataTable.Columns.Add("With Prod Supp", typeof(string));
            dataTable.Columns.Add("Despatch Qty", typeof(string));
            dataTable.Columns.Add("Trays", typeof(string));





            // Set the DataGridView's data source to the DataTable
            dgvItems.DataSource = dataTable;
            dgvItems.Columns["Work Order"].Width = dgvItems.Columns["Work Order"].Width + 50;
            dgvItems.Columns["Work Order"].ReadOnly = true;
            dgvItems.Columns["Despatch"].ReadOnly = true;
            dgvItems.Columns["Qty Carriers"].ReadOnly = true;





        }

        private Boolean valid_barcode(string barcode)
        {
            int spacepos;
            Apos = txtBarcode.Text.ToUpper().IndexOf('A', 0);
            Bpos = txtBarcode.Text.ToUpper().IndexOf('B', 0);
            spacepos = txtBarcode.Text.ToUpper().IndexOf(' ', 0);
            if ((Apos != -1) && (Bpos != -1) && (spacepos == -1))
                return true;
            else
                return false;
        }
        private void btnAdd_workOrder(object sender, EventArgs e)
        {
            String ID = "";
            String NSID = "";
            String strCPSCon = "";
            String SelectSQL;
            String strCPSSQL;
            string priority = "ERROR";


            SqlDataReader oneWOeReader = null;

            if (valid_barcode(txtBarcode.Text.ToUpper()))
            {

                ID = txtBarcode.Text.Substring(Apos + 1, Bpos - Apos - 1).ToUpper();
                NSID = txtBarcode.Text.Substring(0, Apos).ToUpper();
                strCPSCon = Properties.Settings.Default.CPSConnectionString;

                SqlConnection myConnection = new SqlConnection(strCPSCon);

                //Below line if Unique Flow
                SelectSQL = "Select ONE.NSID, ONE.ID, One.Name as Name, Substring(ONE.Name,2,3) as Trigram,One.Numberofitems, One.FIRSTRANKINDELIVERY ,CONVERT(VARCHAR(20),ONE.SCHEDULEDDATE,3) as dueDate, Priority From AOCSbankoneforallstepsworkorder ONE";
                //Perso Work Orders
                strCPSSQL = SelectSQL + " where ONE.ID =  '" + ID + "' and one.NSID =  '" + NSID + "' and one.h_iscurvers = 1";
                try
                {
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand(strCPSSQL, myConnection);
                    oneWOeReader = myCommand.ExecuteReader();

                    if (oneWOeReader.HasRows)
                    {
                        oneWOeReader.Read();
                        if (((oneWOeReader["Trigram"].ToString() == trigram) || (trigram == "")) && ((oneWOeReader["dueDate"].ToString() == duedate) || (duedate == "")))
                        {
                            if (listBox1.Items.Contains(oneWOeReader["Name"].ToString()))
                            {
                                MessageBox.Show("This Work Order is already in the list.");
                            }
                            else
                            {
                                listBox1.Items.Add(oneWOeReader["Name"].ToString());
                                trigram = oneWOeReader["Trigram"].ToString();
                                txtTrigram.Text = trigram;
                                duedate = oneWOeReader["dueDate"].ToString();
                                txtDueOut.Text = duedate;

                                switch (oneWOeReader["Priority"].ToString())
                                {
                                    case "0":

                                        priority = "SDP";
                                        break;

                                    case "1":

                                        priority = "Daily";
                                        break;

                                    case "2":

                                        priority = "Renewal";
                                        break;

                                    case "3":

                                        priority = "Reissue";
                                        break;

                                    case "4":

                                        priority = "Remake";
                                        break;
                                }
                                txtRunType.Text = priority;
                                scannedItems++;
                                txtscanneditems.Text = scannedItems.ToString();
                                DataRow newRow = dataTable.NewRow();
                                newRow[0] = oneWOeReader["Name"].ToString();
                                newRow[1] = "";
                                newRow[2] = "";
                                newRow[3] = "";
                                // Add the new row to the DataTable
                                dataTable.Rows.Add(newRow);

                            }

                        }
                        else
                        {
                            MessageBox.Show("Posts list can only be made up for items with the same Trigram & Due Out Dates. Please check the work orders you are scanning.");
                        }


                    }




                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show(ex.Message);
                }



            }
            else
            {
                MessageBox.Show("Barcode is in incorrect Format");
                txtBarcode.Text = "";

            }
            txtBarcode.Focus();
            txtBarcode.Text = "";
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string selectSQL;
            String fromSQL;
            string whereSQL;
            string orderSQL;
            string groupBYSQL;
            string strCPSSQL;
            string Workorder_list;
            String strCPSCon = "";

            System.Data.DataTable dtTable = new System.Data.DataTable("dtTable");
            if (listBox1.Items.Count > 0)
            {
                Workorder_list = "";
                for (int i = 0; i <= listBox1.Items.Count - 1; i++)
                {
                    Workorder_list = Workorder_list + "'" + listBox1.Items[i].ToString() + "'";
                    if (i != listBox1.Items.Count - 1)
                        Workorder_list = Workorder_list + ",";
                }
                selectSQL = "Select ONE.Name,   SHIP.NAME, COUNT(BCI.RANKINPRODUCTIONDELIVERY_1) as items, CO.Name,ONE.SCHEDULEDDATE as dueDate,      BCI.[SHIPPINGMODELABEL_1_ID], pull.PREVIOUSSHIPPINGMODELABEL_ID,  SHIPOLD.NAME as OLDship, Case when SHIPOLD.NAME IS NULL Then ship.name else SHIPOLD.name END as ORIGDESPATCH  ";
                fromSQL = " from AOCSBANKONEFORALLSTEPSWORKORDER ONE, AOCSBANKCARDPERSOWORKORDER PWO, AOCSPERSOBATCHINWORKORDER PBWO,AOCSBANKSHIPPINGMODELABEL SHIP, AOCSBANKCARDPERSOCUSTOMEROFFER CO,AOCSBANKCARDCUSTOMERPRODUCTIONPROFILE PP, AOCSBANKCARDIMAGE BCI";
                fromSQL = fromSQL + " left outer join AOCSBANKCARDPULLOUT pull on  pull.CARDIMAGE_ID = BCI.ID and pull.H_iscurvers = 1 LEFT join AOCSBANKSHIPPINGMODELABEL as SHIPOLD on Shipold.ID = PREVIOUSSHIPPINGMODELABEL_ID and Shipold.NSID = PREVIOUSSHIPPINGMODELABEL_NSID and shipold.H_ISCURVERS =1";
                whereSQL = " where ONE.name in (" + Workorder_list + ")";
                whereSQL = whereSQL + " and (ship.ID in (" + Properties.Settings.Default.RM_Shipment_methods + ") ";
                whereSQL = whereSQL + " or shipold.ID in (" + Properties.Settings.Default.RM_Shipment_methods + ") )";
                whereSQL = whereSQL + " and PWO.ID = ONE.SECONDPASSPERSOWO_ID";
                whereSQL = whereSQL + " and PWO.NSID = PBWO.INWORKORDER_NSID";
                whereSQL = whereSQL + " and PWO.ID = PBWO.INWORKORDER_ID";
                whereSQL = whereSQL + " and PBWO.FORPERSOBATCH_NSID = BCI.INBATCH_NSID";
                whereSQL = whereSQL + " and PBWO.FORPERSOBATCH_ID = BCI.INBATCH_ID";
                whereSQL = whereSQL + " and SHIP.ID = BCI.SHIPPINGMODELABEL_1_ID";
                whereSQL = whereSQL + " and SHIP.NSID = BCI.SHIPPINGMODELABEL_1_NSID";
                whereSQL = whereSQL + " and CO.ID = PP.MYOWNER_ID";
                whereSQL = whereSQL + " and CO.NSID = PP.MYOWNER_NSID";
                whereSQL = whereSQL + " and BCI.FROMPRODUCTIONPROFILE_ID = PP.ID";
                whereSQL = whereSQL + " and BCI.FROMPRODUCTIONPROFILE_NSID = PP.NSID";

                whereSQL = whereSQL + " and ONE.h_iscurvers = 1";
                whereSQL = whereSQL + " and CO.h_iscurvers = 1";
                whereSQL = whereSQL + " and PWO.h_iscurvers = 1";
                whereSQL = whereSQL + " and pbwo.H_ISCURVERS = 1";
                whereSQL = whereSQL + " and SHIP.H_ISCURVERS = 1";
                whereSQL = whereSQL + " and CO.H_ISCURVERS = 1";
                whereSQL = whereSQL + " and PP.H_ISCURVERS = 1";
                whereSQL = whereSQL + " and BCI.INSERTSET_1_NSID != 0";

                whereSQL = whereSQL + " and BCI.RANKINPRODUCTIONDELIVERY_1 != 0";
                whereSQL = whereSQL + " and BCI.RANKINPRODUCTIONDELIVERY_1 > pwo.FIRSTRANKINDELIVERY - 1";
                whereSQL = whereSQL + " and BCI.RANKINPRODUCTIONDELIVERY_1 < PWO.FIRSTRANKINDELIVERY + PWO.NumberofItems";
                groupBYSQL = " group by Co.Name, One.Name,SHIPOLD.NAME,ONE.SCHEDULEDDATE,ship.ID,Ship.Name,BCI.[SHIPPINGMODELABEL_1_ID],pull.PREVIOUSSHIPPINGMODELABEL_ID,Shipold.ID, SHIPOLD.NAME, Case when SHIPOLD.NAME IS NULL Then ship.name else SHIPOLD.name END";
                orderSQL = " Order by OrigDespatch,";
                orderSQL = orderSQL + "case ";
                for (int i = 0; i <= listBox1.Items.Count - 1; i++)
                {
                    orderSQL = orderSQL + "WHEN ONE.NAME = '" + listBox1.Items[i].ToString() + "' THEN '" + i.ToString() + "' ";
                    //Workorder_list = Workorder_list + "'" + listBox1.Items[i].ToString() + "'";
                }
                orderSQL = orderSQL + " END, ";
                orderSQL = orderSQL + "One.Name,Ship.Name,Shipold.ID";
                strCPSSQL = selectSQL + fromSQL + whereSQL + groupBYSQL + orderSQL;

                strCPSCon = Properties.Settings.Default.CPSConnectionString;
                SqlConnection myConnection = new SqlConnection(strCPSCon);
                myConnection.Open();

                SqlDataAdapter sqlAdpt = new SqlDataAdapter(strCPSSQL, myConnection);
                sqlAdpt.Fill(dtTable);
                dataGridView1.DataSource = dtTable;


                txtDespatchMethod.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                txtCustomerOffer.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                var result = MessageBox.Show("Is " + dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[0].Value.ToString() + " Split Across Yorkies?", "Split Work Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    split_wo = false;
                }
                else
                {
                    split_wo = true;
                }
                pagenumber = 1;
                Copy = "Yorkie";
                print();
                pagenumber = 1;
                //  Copy = "Despatch";
                //  print();
                var clear = MessageBox.Show("Do you want to clear the Work order list? If the last work order has been split it will be left in the list. ", "Clear Work Order List", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (clear == DialogResult.Yes)
                {

                    while (listBox1.Items.Count > 1)
                    {
                        listBox1.Items.RemoveAt(0);
                    }

                    if (split_wo == false)
                    {
                        listBox1.Items.RemoveAt(0);
                    }


                }
                dataGridView1.DataSource = "";
            }
            else
                MessageBox.Show("Please select some work Orders");
        }

        //private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtBarcode.Focus();
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            txtVer.Text = "Ver: " + " " + version.Major + "." + version.Minor + " (build " + version.Build + ")"; //change form title
            txtBarcode.Focus();
        }


        public void print()
        {
            try
            {




                pdoc = new PrintDocument();
                // pdoc.PrinterSettings.PrinterName = Properties.Settings.Default.Printername;

                pdoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("PaperA4", 830, 1170);

                pdoc.DefaultPageSettings.Landscape = false;

                pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

                PrintDialog pd = new PrintDialog();


                // PrinterSettings ps = new PrinterSettings();
                startpage = 0;
                if (Copy == "Yorkie")
                {

                    pd.ShowDialog();
                    printer = pd.PrinterSettings.PrinterName;
                    pdoc.PrinterSettings.PrinterName = pd.PrinterSettings.PrinterName;
                    PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
                    printPrvDlg.Document = pdoc;
                    pagenumber = 1;
                    from_Preview = true;
                    printPrvDlg.ShowDialog();
                    from_Preview = false;
                    pagenumber = 1;

                    Copy = "Despatch";
                    print();
                    //  pdoc.Print();
                }
                else
                {
                    pdoc.PrinterSettings.PrinterName = printer;
                    pdoc.Print();
                }






            }
            catch (Exception e1)
            {
                MessageBox.Show("Error Printing on : " + Properties.Settings.Default.Printername);
                MessageBox.Show(e1.Message);
            }
        }


        public void print_PostList()
        {
            try
            {




                pdoc = new PrintDocument();
                // pdoc.PrinterSettings.PrinterName = Properties.Settings.Default.Printername;

                pdoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("PaperA4", 830, 1170);

                pdoc.DefaultPageSettings.Landscape = false;

                pdoc.PrintPage += new PrintPageEventHandler(pdoc_postlist_PrintPage);

                PrintDialog pd = new PrintDialog();


                // PrinterSettings ps = new PrinterSettings();
                startpage = 0;
                if (Copy == "Yorkie")
                {

                    pd.ShowDialog();
                    printer = pd.PrinterSettings.PrinterName;
                    pdoc.PrinterSettings.PrinterName = pd.PrinterSettings.PrinterName;
                    PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
                    printPrvDlg.Document = pdoc;
                    pagenumber = 1;
                    from_Preview = true;
                    printPrvDlg.ShowDialog();
                    from_Preview = false;

                    DespatchList = DespatchTypes;
                    if (DespatchList.Contains(","))
                    {
                        txtDespatchMethod.Text = DespatchList.Substring(0, DespatchList.IndexOf(","));
                    }
                    else
                    {
                        txtDespatchMethod.Text = DespatchList;
                    }
                    startpage = 0;
                    pagenumber = 1;
                    pdoc.Print();
                    Copy = "Despatch";
                    print_PostList();

                }
                else
                {
                    pdoc.PrinterSettings.PrinterName = printer;
                    DespatchList = DespatchTypes;
                    if (DespatchList.Contains(","))
                    {
                        txtDespatchMethod.Text = DespatchList.Substring(0, DespatchList.IndexOf(","));
                    }
                    else
                    {
                        txtDespatchMethod.Text = DespatchList;
                    }
                    pagenumber = 1;
                    pdoc.Print();
                    //Save the Archive Copy
                    //Save the file
                    pagenumber = 1;
                    DespatchList = DespatchTypes;
                    if (DespatchList.Contains(","))
                    {
                        txtDespatchMethod.Text = DespatchList.Substring(0, DespatchList.IndexOf(","));
                    }
                    else
                    {
                        txtDespatchMethod.Text = DespatchList;
                    }
                    Copy = "Archive";
                    startpage = 0;
                    pdoc.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                    pdoc.PrinterSettings.PrintToFile = true;
                    pdoc.PrinterSettings.PrintFileName = Properties.Settings.Default.BackupPath + txtTrigram.Text + "-" + DateTime.Now.ToString("ddMMyy HHmm") + ".PDF"; ;
                    pdoc.Print();

                }






            }
            catch (Exception e1)
            {
                MessageBox.Show("Error Printing on : " + pdoc.PrinterSettings.PrinterName);
                MessageBox.Show(e1.Message);
            }
        }


        void pdoc_PrintPage(object sender, PrintPageEventArgs e)

        {



        }

        void Print_Header_section(Graphics graphics, Pen blackPen, Font customerfont, ref int startX, ref int startY, ref int Offset)
        {
            string outputstring;


            //  startX = 300;
            //Customer offer name
            Font headingfont = new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize + 2, FontStyle.Bold);
            outputstring = txtCustomerOffer.Text;
            graphics.DrawString(outputstring, customerfont,
                                           new SolidBrush(Color.Black), startX, startY + Offset);


            //Headings
            startX = 50;
            Offset = Offset + 30;
            graphics.DrawString("Despatch Type", headingfont,
                                            new SolidBrush(Color.Black), startX, startY + Offset);

            startX = 200;
            graphics.DrawString("Trigram", headingfont,
                                            new SolidBrush(Color.Black), startX, startY + Offset);

            startX = 350;
            graphics.DrawString("Due Out", headingfont,
                                            new SolidBrush(Color.Black), startX, startY + Offset);

            startX = 500;
            graphics.DrawString("Run Type", headingfont,
                                            new SolidBrush(Color.Black), startX, startY + Offset);



            //Shipment Type
            startX = 50;
            Offset = Offset + 20;

            outputstring = txtDespatchMethod.Text;
            //dataGridView1.Rows[startpage].Cells[1].Value.ToString();
            graphics.DrawString(outputstring, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize),
                                            new SolidBrush(Color.Black), startX, startY + Offset);

            //Trigram


            startX = 200;
            outputstring = dgvItems.Rows[startpage].Cells[0].Value.ToString().Substring(1, 3);
            graphics.DrawString(outputstring, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize),
                                            new SolidBrush(Color.Black), startX, startY + Offset);

            //Due Out
            startX = 350;
            graphics.DrawString(duedate, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize),
                                            new SolidBrush(Color.Black), startX, startY + Offset);

            startX = 500;
            graphics.DrawString(txtRunType.Text, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize),
                                            new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 50;
            startX = 50;
            graphics.DrawString("Yorkie         of      ", headingfont,
                                            new SolidBrush(Color.Black), startX, startY + Offset);


            Offset = Offset + 50;


            startX = 50;
            outputstring = "JOB";

            graphics.DrawString(outputstring, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize, FontStyle.Bold),
                                            new SolidBrush(Color.Black), startX, startY + Offset);

            startX = 200;
            outputstring = "Work Order";

            graphics.DrawString(outputstring, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize, FontStyle.Bold),
                                            new SolidBrush(Color.Black), startX, startY + Offset);

            startX = 300;

            outputstring = "Carrier\nQTY";

            graphics.DrawString(outputstring, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize, FontStyle.Bold),
                                            new SolidBrush(Color.Black), startX, startY + Offset);

            startX = 380;
            outputstring = "Pull Qty";

            graphics.DrawString(outputstring, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize, FontStyle.Bold),
                                            new SolidBrush(Color.Black), startX, startY + Offset);


            startX = 460;
            outputstring = "With\nProd Sup";

            graphics.DrawString(outputstring, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize, FontStyle.Bold),
                                            new SolidBrush(Color.Black), startX, startY + Offset);


            startX = 540;
            outputstring = "Despatch\nQty";

            graphics.DrawString(outputstring, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize, FontStyle.Bold),
                                            new SolidBrush(Color.Black), startX, startY + Offset);

            startX = 620;
            outputstring = "Trays";

            graphics.DrawString(outputstring, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize, FontStyle.Bold),
                                            new SolidBrush(Color.Black), startX, startY + Offset);

            startX = 700;
            outputstring = "Bags";

            graphics.DrawString(outputstring, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize, FontStyle.Bold),
                                            new SolidBrush(Color.Black), startX, startY + Offset);

            //Top Line
            graphics.DrawLine(blackPen, 49, startY + Offset, 761, startY + Offset);

            //Vertical lines
            graphics.DrawLine(blackPen, 50, startY + Offset, 50, startY + Offset + 38);

            graphics.DrawLine(blackPen, 190, startY + Offset, 190, startY + Offset + 38);
            graphics.DrawLine(blackPen, 290, startY + Offset, 290, startY + Offset + 38);
            graphics.DrawLine(blackPen, 370, startY + Offset, 370, startY + Offset + 38);
            graphics.DrawLine(blackPen, 450, startY + Offset, 450, startY + Offset + 38);
            graphics.DrawLine(blackPen, 530, startY + Offset, 530, startY + Offset + 38);
            graphics.DrawLine(blackPen, 610, startY + Offset, 610, startY + Offset + 38);
            graphics.DrawLine(blackPen, 700, startY + Offset, 700, startY + Offset + 38);
            graphics.DrawLine(blackPen, 760, startY + Offset, 760, startY + Offset + 38);



            //Bottom Line
            graphics.DrawLine(blackPen, 50, startY + Offset + 38, 760, startY + Offset + 38);

        }

        void print_detail_line(Graphics graphics, Pen blackPen, Font customerfont, ref int startX, ref int startY, ref int Offset, int i)
        {
            int QTY = 0;
            int QtyPulls = 0;
            int QtyProdSupp = 0;
            int QtyDespatch = 0;
            int QtyTrays = 0;
            string outputstring;
            outputstring = dgvItems.Rows[i].Cells[0].Value.ToString().Substring(0, 13);



            graphics.DrawString(outputstring, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize),
                                           new SolidBrush(Color.Black), startX, startY + Offset + 10);



            //Work Order
            startX = 200;
            outputstring = dgvItems.Rows[i].Cells[0].Value.ToString().Substring(13);

            graphics.DrawString(outputstring, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize),
                                           new SolidBrush(Color.Black), startX, startY + Offset + 10);

            // Quantity
            startX = 300;


            if ((i != dgvItems.Rows.Count - 1) || (i == dgvItems.Rows.Count - 2) && !(split_wo))
            {
                QTY = int.Parse(dgvItems.Rows[i].Cells[2].Value.ToString());
                QtyPulls = int.Parse(dgvItems.Rows[i].Cells[3].Value.ToString());
                QtyProdSupp = int.Parse(dgvItems.Rows[i].Cells[4].Value.ToString());
                TotalPulls = TotalPulls + QtyPulls;
                TotalProdSupp = TotalProdSupp + QtyProdSupp;

                TotalQTY = QTY + TotalQTY;
                outputstring = QTY.ToString();

                graphics.DrawString(outputstring, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize),
                                               new SolidBrush(Color.Black), startX, startY + Offset + 10);
                //Pulls
                startX = 380;
                //If the pulls are 0 we want to leave the box blank so Production can write in thier own 
                if (QtyPulls > 0)
                {
                    graphics.DrawString(QtyPulls.ToString(), new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize),
                                                   new SolidBrush(Color.Black), startX, startY + Offset + 10);
                }
                QtyPulls = 0;

                //Pulls
                startX = 460;
                //If the pulls are 0 we want to leave the box blank so Production can write in thier own 
                if (QtyProdSupp > 0)
                {
                    graphics.DrawString(QtyProdSupp.ToString(), new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize),
                                                   new SolidBrush(Color.Black), startX, startY + Offset + 10);
                }
                QtyProdSupp = 0;

                //Despatch quantity
                startX = 540;
                if (dgvItems.Rows[i].Cells[5].Value.ToString() == "")
                    QtyDespatch = 0;
                else
                    QtyDespatch = int.Parse(dgvItems.Rows[i].Cells[5].Value.ToString());
                //If the pulls are 0 we want to leave the box blank so Production can write in thier own 
                TotalDespatch = TotalDespatch + QtyDespatch;
                if (QtyDespatch > 0)
                {
                    graphics.DrawString(QtyDespatch.ToString(), new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize),
                                                   new SolidBrush(Color.Black), startX, startY + Offset + 10);
                }
                //Trays
                startX = 620;
                if (dgvItems.Rows[i].Cells[6].Value.ToString() == "")
                    QtyTrays = 0;
                else
                    QtyTrays = int.Parse(dgvItems.Rows[i].Cells[6].Value.ToString());
                //If the pulls are 0 we want to leave the box blank so Production can write in thier own 
                TotalTrays = TotalTrays + QtyTrays;
                if (QtyTrays > 0)
                {
                    graphics.DrawString(QtyTrays.ToString(), new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize),
                                                   new SolidBrush(Color.Black), startX, startY + Offset + 10);
                }
            }

            graphics.DrawLine(blackPen, 50, startY + Offset - 5, 50, startY + Offset + 38);
            graphics.DrawLine(blackPen, 190, startY + Offset - 5, 190, startY + Offset + 38);
            graphics.DrawLine(blackPen, 290, startY + Offset - 5, 290, startY + Offset + 38);
            graphics.DrawLine(blackPen, 370, startY + Offset - 5, 370, startY + Offset + 38);
            graphics.DrawLine(blackPen, 450, startY + Offset - 5, 450, startY + Offset + 38);
            graphics.DrawLine(blackPen, 530, startY + Offset - 5, 530, startY + Offset + 38);
            graphics.DrawLine(blackPen, 610, startY + Offset - 5, 610, startY + Offset + 38);
            graphics.DrawLine(blackPen, 700, startY + Offset - 5, 700, startY + Offset + 38);
            graphics.DrawLine(blackPen, 760, startY + Offset - 5, 760, startY + Offset + 38);

            //bottom Line
            graphics.DrawLine(blackPen, 50, startY + Offset + 38, 760, startY + Offset + 38);
            Offset = Offset + 40;
        }

        void Print_totals(Graphics graphics, Pen blackPen, Font customerfont, ref int startX, ref int startY, ref int Offset)
        {


            startX = 50;
            graphics.DrawString("Total:", new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize, FontStyle.Bold),
                                                       new SolidBrush(Color.Black), startX, startY + Offset);

            //Total Quantity
            startX = 300;

            graphics.DrawString(TotalQTY.ToString(), new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize, FontStyle.Bold),
                                           new SolidBrush(Color.Black), startX, startY + Offset);

            //Total Pulls
            startX = 380;

            graphics.DrawString(TotalPulls.ToString(), new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize, FontStyle.Bold),
                                           new SolidBrush(Color.Black), startX, startY + Offset);

            //Total with Prod support
            startX = 460;


            graphics.DrawString(TotalProdSupp.ToString(), new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize, FontStyle.Bold),
                                           new SolidBrush(Color.Black), startX, startY + Offset);

            //Total Despatch
            startX = 540;

            graphics.DrawString(TotalDespatch.ToString(), new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize, FontStyle.Bold),
                                           new SolidBrush(Color.Black), startX, startY + Offset);

            //Total Trays
            startX = 620;

            graphics.DrawString(TotalTrays.ToString(), new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize, FontStyle.Bold),
                                           new SolidBrush(Color.Black), startX, startY + Offset);



            TotalQTY = 0;
            TotalPulls = 0;
            TotalProdSupp = 0;
            TotalDespatch = 0;
            TotalTrays = 0;


            graphics.DrawLine(blackPen, 50, startY + Offset - 5, 50, startY + Offset + 38);
            graphics.DrawLine(blackPen, 190, startY + Offset - 5, 190, startY + Offset + 38);
            graphics.DrawLine(blackPen, 290, startY + Offset - 5, 290, startY + Offset + 38);
            graphics.DrawLine(blackPen, 370, startY + Offset - 5, 370, startY + Offset + 38);
            graphics.DrawLine(blackPen, 450, startY + Offset - 5, 450, startY + Offset + 38);
            graphics.DrawLine(blackPen, 530, startY + Offset - 5, 530, startY + Offset + 38);
            graphics.DrawLine(blackPen, 610, startY + Offset - 5, 610, startY + Offset + 38);
            graphics.DrawLine(blackPen, 700, startY + Offset - 5, 700, startY + Offset + 38);
            graphics.DrawLine(blackPen, 760, startY + Offset - 5, 760, startY + Offset + 38);
            //bottom Line
            graphics.DrawLine(blackPen, 50, startY + Offset + 38, 760, startY + Offset + 38);
            Offset = Offset + 40;

        }
        void pdoc_postlist_PrintPage(object sender, PrintPageEventArgs e)

        {


            try
            {


                Graphics graphics = e.Graphics;
                Font standardfont = new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize);

                Font customerfont = new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize + 6, FontStyle.Bold);
                //  float fontHeight = standardfont.GetHeight();
                string outputstring = "";
                int startX = 300;
                int startY = 10;
                int Offset = 0;
                int Items_on_Page = 0;
                int count;
                int totalpages = 0;
                int row = 0;
                int Items_for_Despatch = 0;
                bool Total_Printed = false;
                Boolean new_despatch = false;
                Boolean last_item = false;
                Pen blackPen = new Pen(Color.Black, 2);
                Print_Header_section(graphics, blackPen, customerfont, ref startX, ref startY, ref Offset);

                Offset = Offset + 40;
                //     if (from_Preview)


                count = 0;

                if (DespatchList.Contains(","))
                {

                    txtDespatchMethod.Text = DespatchList.Substring(0, DespatchList.IndexOf(","));
                }
                else
                {
                    txtDespatchMethod.Text = DespatchList;
                }


                //Loop through while theres still a despatch to do.
                //    while (DespatchList.Length > 0)
                //   {
                for (int i = startpage; i <= dgvItems.Rows.Count - 2; i++)
                {
                    if (i == dgvItems.Rows.Count - 2)
                    {
                        last_item = true;
                    }

                    Items_for_Despatch = 0;
                    for (int itemrow = 1; itemrow <= dgvItems.Rows.Count - 2; itemrow++)
                    {
                        if (txtDespatchMethod.Text == dgvItems.Rows[itemrow].Cells[1].Value.ToString())
                        {
                            Items_for_Despatch++;
                        }
                    }
                    //Despatch Method Match  
                    if (txtDespatchMethod.Text == dgvItems.Rows[i].Cells[1].Value.ToString())
                    {
                        startX = 50;
                        print_detail_line(graphics, blackPen, customerfont, ref startX, ref startY, ref Offset, i);
                        Items_on_Page++;
                        //Have we filled a Page;
                        if ((Items_on_Page == itemsperpage) && i != dgvItems.Rows.Count - 2)
                        {
                            //Check if any more of this despatch after this record
                            for (int i2 = i + 1; i2 <= dgvItems.Rows.Count - 2; i2++)
                            {
                                if (e.HasMorePages == false)
                                {
                                    if (txtDespatchMethod.Text == dgvItems.Rows[i2].Cells[1].Value.ToString())
                                    {

                                        e.HasMorePages = true;

                                        count = 0;
                                        startpage = i2;
                                        i = dgvItems.Rows.Count;
                                    }
                                    else
                                    {

                                        if (i2 == dgvItems.Rows.Count - 2)
                                        {
                                            last_item = true;
                                        }
                                    }
                                }
                            }

                        }

                    }
                    else
                    {

                    }

                }

                //We have completed add the Total Line.
                //Only want to do this if the last item and no more pages.
                if ((Items_on_Page != itemsperpage) || (last_item == true))
                {
                    Print_totals(graphics, blackPen, customerfont, ref startX, ref startY, ref Offset);

                    if (DespatchList.Contains(","))
                    {
                        DespatchList = DespatchList.Remove(0, txtDespatchMethod.Text.Length + 1);
                    }
                    else
                    {
                        DespatchList = DespatchList.Remove(0, txtDespatchMethod.Text.Length);
                    }


                    if (DespatchList.Contains(","))
                    {

                        txtDespatchMethod.Text = DespatchList.Substring(0, DespatchList.IndexOf(","));
                    }
                    else
                    {
                        txtDespatchMethod.Text = DespatchList;
                    }

                    if (txtDespatchMethod.Text != "")
                    {
                        //Another Despatch_method to do.
                        e.HasMorePages = true;
                        count = 0;
                        startpage = 0;
                    }
                }

                totalpages = (Items_for_Despatch / itemsperpage) + 1;

                print_stamp_Sections(graphics, ref Offset);


                print_footer(Offset, graphics, startY, totalpages);
                if ((Items_on_Page != itemsperpage) || (last_item == true))
                    pagenumber = 1;

            }



            catch
            {
                MessageBox.Show("Problems printing Post List");
            }

        }

        private void print_footer(int Offset, Graphics graphics, int startY, int totalpages)
        {
            Offset = 1100;

            graphics.DrawString("Printed: " + DateTime.Now, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize + 2, FontStyle.Bold),
                                            new SolidBrush(Color.Black), 50, startY + Offset);
            graphics.DrawString("Sheet " + pagenumber.ToString() + " of " + totalpages.ToString(), new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize + 2, FontStyle.Bold),
                                            new SolidBrush(Color.Black), 350, startY + Offset);

            graphics.DrawString("Copy For " + Copy, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize + 2, FontStyle.Bold),
                                            new SolidBrush(Color.Black), 600, startY + Offset);
            pagenumber++;
        }
        private void print_stamp_Sections(Graphics graphics, ref int Offset)
        {
            //Stamp Area line 1
            Offset = Offset + 40;
            insert_stamp_section(Offset, "Seal No", "Sealed By", "Checked By", graphics);
            Offset = Offset + 80;
            insert_stamp_section(Offset, "2nd Seal No", "Sealed By", "Checked By", graphics);
            Offset = Offset + 80;
            insert_stamp_section(Offset, "Despatch Date", "1st Despatcher", "2nd Dispatcher", graphics);
        }
        private void insert_stamp_section(int ypos, string text1, string text2, string text3, Graphics graphics)

        {
            int X_pos;
            Pen blackPen = new Pen(Color.Black, 2);
            X_pos = 50;


            graphics.DrawString(text1, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize),
                                               new SolidBrush(Color.Black), X_pos, ypos);




            X_pos = 200;

            graphics.DrawString(text2, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize),
                                               new SolidBrush(Color.Black), X_pos, ypos);




            X_pos = 350;

            graphics.DrawString(text3, new Font(Properties.Settings.Default.Fontname, Properties.Settings.Default.FontSize),
                                               new SolidBrush(Color.Black), X_pos, ypos);

            graphics.DrawRectangle(blackPen, 49, ypos, 450, 80);

            graphics.DrawLine(blackPen, 190, ypos, 190, ypos + 80);
            graphics.DrawLine(blackPen, 340, ypos, 340, ypos + 80);


        }






        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {

            location = listBox1.IndexFromPoint(e.Location);
            listBox1.SelectedIndex = location;

        }


        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (location != -1)

            {
                listBox1.Items.RemoveAt(location);
                scannedItems--;
                txtscanneditems.Text = scannedItems.ToString();
            }
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            if (txtBarcode.Text.EndsWith("\n"))
            {
                txtBarcode.Text.Replace("\n", "");
                button1_Click(this, null);
                txtBarcode.Text = "";
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            panel1.Top = this.Size.Height - 150;
            listBox1.Height = this.Size.Height - 220;
            dgvItems.Height = this.Size.Height - 245;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int item_count;
            int pull_count;
            String ID = "";
            String NSID = "";
            String strCPSCon = "";
            String SelectSQL;
            String strCPSSQL;
            string selectSQL;
            String fromSQL;
            string whereSQL;
            string orderSQL;
            string groupBYSQL;

            string priority = "ERROR";

            SqlDataReader oneWOeReader = null;

            if (valid_barcode(txtBarcode.Text.ToUpper()))
            {

                ID = txtBarcode.Text.Substring(Apos + 1, Bpos - Apos - 1).ToUpper();
                NSID = txtBarcode.Text.Substring(0, Apos).ToUpper();
                strCPSCon = Properties.Settings.Default.CPSConnectionString;

                SqlConnection myConnection = new SqlConnection(strCPSCon);

                selectSQL = "Select ONE.Name as WONAME, ONE.Priority,   SHIP.NAME, COUNT(BCI.RANKINPRODUCTIONDELIVERY_1) as items, CO.Name as CONAME,Convert(NVARCHAR,ONE.SCHEDULEDDATE , 103)  as dueDate,   BCI.[SHIPPINGMODELABEL_1_ID], pull.PREVIOUSSHIPPINGMODELABEL_ID,  SHIPOLD.NAME as OLDship, Case when SHIPOLD.NAME IS NULL Then ship.name else SHIPOLD.name END as ORIGDESPATCH,  Case when SHIPOLD.NAME IS NULL Then 'false' else 'true' END as PULL    ";
                fromSQL = " from AOCSBANKONEFORALLSTEPSWORKORDER ONE, AOCSBANKCARDPERSOWORKORDER PWO, AOCSPERSOBATCHINWORKORDER PBWO,AOCSBANKSHIPPINGMODELABEL SHIP, AOCSBANKCARDPERSOCUSTOMEROFFER CO,AOCSBANKCARDCUSTOMERPRODUCTIONPROFILE PP, AOCSBANKCARDIMAGE BCI";
                fromSQL = fromSQL + " left outer join AOCSBANKCARDPULLOUT pull on  pull.CARDIMAGE_ID = BCI.ID and pull.H_iscurvers = 1 LEFT join AOCSBANKSHIPPINGMODELABEL as SHIPOLD on Shipold.ID = PREVIOUSSHIPPINGMODELABEL_ID and Shipold.NSID = PREVIOUSSHIPPINGMODELABEL_NSID and shipold.H_ISCURVERS =1";
                whereSQL = " where ONE.NSID = " + NSID;
                whereSQL = whereSQL + " And ONE.ID = " + ID;
                MessageBox.Show("Despatch Method filter turned off this should not be LIVE");
                //whereSQL = whereSQL + " and (ship.ID in (" + Properties.Settings.Default.RM_Shipment_methods + ") ";
                //whereSQL = whereSQL + " or shipold.ID in (" + Properties.Settings.Default.RM_Shipment_methods + ") )";
                whereSQL = whereSQL + " and PWO.ID = ONE.SECONDPASSPERSOWO_ID";
                whereSQL = whereSQL + " and PWO.NSID = PBWO.INWORKORDER_NSID";
                whereSQL = whereSQL + " and PWO.ID = PBWO.INWORKORDER_ID";
                whereSQL = whereSQL + " and PBWO.FORPERSOBATCH_NSID = BCI.INBATCH_NSID";
                whereSQL = whereSQL + " and PBWO.FORPERSOBATCH_ID = BCI.INBATCH_ID";
                whereSQL = whereSQL + " and SHIP.ID = BCI.SHIPPINGMODELABEL_1_ID";
                whereSQL = whereSQL + " and SHIP.NSID = BCI.SHIPPINGMODELABEL_1_NSID";
                whereSQL = whereSQL + " and CO.ID = PP.MYOWNER_ID";
                whereSQL = whereSQL + " and CO.NSID = PP.MYOWNER_NSID";
                whereSQL = whereSQL + " and BCI.FROMPRODUCTIONPROFILE_ID = PP.ID";
                whereSQL = whereSQL + " and BCI.FROMPRODUCTIONPROFILE_NSID = PP.NSID";

                whereSQL = whereSQL + " and ONE.h_iscurvers = 1";
                whereSQL = whereSQL + " and CO.h_iscurvers = 1";
                whereSQL = whereSQL + " and PWO.h_iscurvers = 1";
                whereSQL = whereSQL + " and pbwo.H_ISCURVERS = 1";
                whereSQL = whereSQL + " and SHIP.H_ISCURVERS = 1";
                whereSQL = whereSQL + " and CO.H_ISCURVERS = 1";
                whereSQL = whereSQL + " and PP.H_ISCURVERS = 1";
                whereSQL = whereSQL + " and BCI.INSERTSET_1_NSID != 0";

                whereSQL = whereSQL + " and BCI.RANKINPRODUCTIONDELIVERY_1 != 0";
                whereSQL = whereSQL + " and BCI.RANKINPRODUCTIONDELIVERY_1 > pwo.FIRSTRANKINDELIVERY - 1";
                whereSQL = whereSQL + " and BCI.RANKINPRODUCTIONDELIVERY_1 < PWO.FIRSTRANKINDELIVERY + PWO.NumberofItems";
                groupBYSQL = " group by Co.Name, ONE.Priority, One.Name,SHIPOLD.NAME,ONE.SCHEDULEDDATE,ship.ID,Ship.Name,BCI.[SHIPPINGMODELABEL_1_ID],pull.PREVIOUSSHIPPINGMODELABEL_ID,Shipold.ID, SHIPOLD.NAME, Case when SHIPOLD.NAME IS NULL Then ship.name else SHIPOLD.name END";
                orderSQL = " Order by ";
                /*   orderSQL = orderSQL + "case ";
                   for (int i = 0; i <= listBox1.Items.Count - 1; i++)
                   {
                       orderSQL = orderSQL + "WHEN ONE.NAME = '" + listBox1.Items[i].ToString() + "' THEN '" + i.ToString() + "' ";
                       //Workorder_list = Workorder_list + "'" + listBox1.Items[i].ToString() + "'";
                   }
                   orderSQL = orderSQL + " END, ";*/
                //  orderSQL = orderSQL + "One.Name,PREVIOUSSHIPPINGMODELABEL_ID,Ship.Name,Shipold.ID";
                orderSQL = orderSQL + "One.Name,ORIGDESPATCH,Ship.Name,PULL";
                strCPSSQL = selectSQL + fromSQL + whereSQL + groupBYSQL + orderSQL;

                try
                {
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand(strCPSSQL, myConnection);
                    oneWOeReader = myCommand.ExecuteReader();

                    if (oneWOeReader.HasRows)
                    {

                        oneWOeReader.Read();
                        if (txtCustomerOffer.Text == "")
                        {
                            txtCustomerOffer.Text = oneWOeReader["CONAME"].ToString();
                        }

                        if (txtDueOut.Text == "")
                        {
                            txtDueOut.Text = oneWOeReader["duedate"].ToString();
                        }
                        if ((txtCustomerOffer.Text == oneWOeReader["CONAME"].ToString()))   //&& (oneWOeReader["duedate"].ToString() == txtDueOut.Text))
                        {
                            exists = false;
                            // MessageBox.Show(oneWOeReader["WONAME"].ToString());
                            if (((oneWOeReader["WONAME"].ToString().Substring(1, 3) == trigram) || (trigram == ""))) //&& ((oneWOeReader["dueDate"].ToString() == duedate) || (duedate == "")))
                            {
                                foreach (DataGridViewRow row in dgvItems.Rows)
                                {
                                    object val1 = row.Cells[0].Value;
                                    if (val1 != null)
                                    {


                                        //    if (val1 != null && oneWOeReader["pull"].ToString() == "true")
                                        if (val1.ToString() == oneWOeReader["WONAME"].ToString())
                                        {
                                            // MessageBox.Show("Entry already exist");
                                            exists = true;
                                            break;
                                        }
                                    }
                                    //if (dgvItems.Rows.
                                    // Find(oneWOeReader["WONAME"].ToString())!=null)
                                }
                                if (exists)
                                {
                                    MessageBox.Show("This Work Order is already in the list.");
                                }
                                else
                                {
                                    listBox1.Items.Add(oneWOeReader["WONAME"].ToString());
                                    trigram = oneWOeReader["WONAME"].ToString().Substring(1, 3);
                                    txtTrigram.Text = trigram;
                                    duedate = oneWOeReader["dueDate"].ToString();
                                    txtDueOut.Text = duedate;

                                    switch (oneWOeReader["Priority"].ToString())
                                    {
                                        case "0":

                                            priority = "SDP";
                                            break;

                                        case "1":

                                            priority = "Daily";
                                            break;

                                        case "2":

                                            priority = "Renewal";
                                            break;

                                        case "3":

                                            priority = "Reissue";
                                            break;

                                        case "4":

                                            priority = "Remake";
                                            break;
                                    }
                                    txtRunType.Text = priority;
                                    scannedItems++;
                                    txtscanneditems.Text = scannedItems.ToString();
                                    TotalQTY = TotalQTY + int.Parse(oneWOeReader["Items"].ToString());
                                    txtCarriers.Text = TotalQTY.ToString();

                                    DataRow newRow = dataTable.NewRow();
                                    //Work order
                                    newRow[0] = oneWOeReader["WONAME"].ToString();
                                    //Despatch
                                    newRow[1] = oneWOeReader["ORIGDESPATCH"].ToString();
                                    //MessageBox.Show(oneWOeReader["ORIGDESPATCH"].ToString());
                                    //if (oneWOeReader["pull"].ToString() == "true")
                                    // {

                                    
                                     if (DespatchTypes == "")
                                      {
                                          DespatchTypes = oneWOeReader["ORIGDESPATCH"].ToString();
                                          clbdespatches.Items.Add(oneWOeReader["ORIGDESPATCH"].ToString(),true);
                                        
                                      }
                                      else
                                      {
                                          if (DespatchTypes.Contains(oneWOeReader["ORIGDESPATCH"].ToString()))
                                          {
                                              // clbdespatches.Items.Add(oneWOeReader["ORIGDESPATCH"].ToString());
                                          }
                                          else
                                          {
                                              DespatchTypes = DespatchTypes + ',' + oneWOeReader["ORIGDESPATCH"].ToString();
                                              clbdespatches.Items.Add(oneWOeReader["ORIGDESPATCH"].ToString());
                                          }

                                      }
                                    //}
                                    //Original Quantity
                                    newRow[2] = oneWOeReader["items"].ToString();
                                    //Pull Quantity
                                    newRow[3] = "0";
                                    if (oneWOeReader["pull"].ToString() == "true")
                                    {
                                        pull_count = Int32.Parse(newRow[3].ToString()) + Int32.Parse(oneWOeReader["items"].ToString());
                                        newRow[3] = pull_count.ToString();
                                    }
                                    //WITH PS
                                    newRow[4] = "0";
                                    //Despatch Qty
                                    newRow[5] = "";
                                    //Trays
                                    newRow[6] = "";
                                    while (oneWOeReader.Read())
                                    {
                                        if (oneWOeReader["pull"].ToString() == "true")
                                        {
                                            if (newRow[1].ToString() == oneWOeReader["ORIGDESPATCH"].ToString())
                                            {
                                                item_count = Int32.Parse(newRow[2].ToString()) + Int32.Parse(oneWOeReader["items"].ToString());
                                                newRow[2] = item_count.ToString();
                                                // newRow[3] = "0";
                                                pull_count = Int32.Parse(newRow[3].ToString()) + Int32.Parse(oneWOeReader["items"].ToString());
                                                newRow[3] = pull_count.ToString();
                                            }
                                            else
                                            {
                                                if (DespatchTypes.Contains(oneWOeReader["ORIGDESPATCH"].ToString()) == false)
                                                {
                                                    if (DespatchTypes == "")
                                                    {
                                                        DespatchTypes = oneWOeReader["ORIGDESPATCH"].ToString();
                                                        clbdespatches.Items.Add(oneWOeReader["ORIGDESPATCH"].ToString());
                                                    }
                                                    else
                                                    {
                                                        DespatchTypes = DespatchTypes + ',' + oneWOeReader["ORIGDESPATCH"].ToString();
                                                        clbdespatches.Items.Add(oneWOeReader["ORIGDESPATCH"].ToString());
                                                    }
                                                }
                                            }
                                        }

                                        if ((oneWOeReader["pull"].ToString() != "true") || (newRow[1].ToString() != oneWOeReader["ORIGDESPATCH"].ToString()))
                                        {

                                            if (DespatchTypes.Contains(oneWOeReader["ORIGDESPATCH"].ToString()) == false)
                                            {
                                                if (DespatchTypes == "")
                                                {
                                                    DespatchTypes = oneWOeReader["ORIGDESPATCH"].ToString();
                                                    clbdespatches.Items.Add(oneWOeReader["ORIGDESPATCH"].ToString());
                                                }
                                                else
                                                {
                                                    DespatchTypes = DespatchTypes + ',' + oneWOeReader["ORIGDESPATCH"].ToString();
                                                    clbdespatches.Items.Add(oneWOeReader["ORIGDESPATCH"].ToString());
                                                }
                                            }
                                            dataTable.Rows.Add(newRow);
                                            newRow = dataTable.NewRow();
                                            //Work order
                                            newRow[0] = oneWOeReader["WONAME"].ToString();
                                            //Despatch
                                            newRow[1] = oneWOeReader["ORIGDESPATCH"].ToString();
                                            // MessageBox.Show(oneWOeReader["ORIGDESPATCH"].ToString()+ " " + oneWOeReader["pull"].ToString());
                                            //Original Quantity
                                            newRow[2] = oneWOeReader["items"].ToString();
                                            //Pull Quantity
                                            newRow[3] = "0";
                                            if (oneWOeReader["pull"].ToString() == "true")
                                            {
                                                newRow[3] = Int32.Parse(newRow[3].ToString()) + Int32.Parse(oneWOeReader["items"].ToString());
                                            }
                                            //WITH PS
                                            newRow[4] = "0";

                                            //Despatch Qty
                                            newRow[5] = "";
                                            //Trays
                                            newRow[6] = "";
                                            TotalQTY = TotalQTY + int.Parse(oneWOeReader["Items"].ToString());
                                            txtCarriers.Text = TotalQTY.ToString();
                                        }
                                    }
                                    // Add the new row to the DataTable

                                    dataTable.Rows.Add(newRow);

                                    txtBarcode.Text = "";
                                    txtBarcode.Focus();

                                }
                            }
                            else
                                MessageBox.Show("All Work Orders on a post list Must belong to the Same Customer offer and have the same Due Out Date");
                            //   dataTable.Rows.Add(newRow);

                        }
                    }
                    else
                        MessageBox.Show("This Work Order does not contain any records with the despatch methods set up for the post list Tool");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            txtBarcode.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string DespatchToRemove;
            //Despatch lists to print
            if (clbdespatches.CheckedItems.Count > 0)
            {
                DespatchList = "";
                foreach (var item in clbdespatches.CheckedItems)
                {
                    if (DespatchList == "")
                    {
                        DespatchList = item.ToString();
                        txtDespatchMethod.Text = item.ToString();
                    }
                    else

                        DespatchList = DespatchList + "," + item.ToString();
                }
                if (dgvItems.RowCount > 1)
                {

                    TotalQTY = 0;
                    DespatchTypes = DespatchList;
                    //DespatchList = DespatchTypes;
                    //  txtDespatchMethod.Text = dgvItems.Rows[0].Cells[1].Value.ToString();
                    //  txtCustomerOffer.Text = dgvItems.Rows[0].Cells[3].Value.ToString();
                    var result = MessageBox.Show("Is " + dgvItems.Rows[dgvItems.Rows.Count - 2].Cells[0].Value.ToString() + " Split Across Yorkies?", "Split Work Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        split_wo = false;
                    }
                    else
                    {
                        split_wo = true;
                    }
                    pagenumber = 1;
                    Copy = "Yorkie";
                    print_PostList();

                    //print();
                    pagenumber = 1;
                    //  Copy = "Despatch";
                    //  print();
                    int rowsRemoved = 0;
                    int itemsRemoved = 0;
                    var clear = MessageBox.Show("Do you want to clear the Work order list? If the last work order has been split it will be left in the list. ", "Clear Work Order List", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (clear == DialogResult.Yes)
                    {

                        foreach (var item in clbdespatches.CheckedItems)
                        {
                            DespatchToRemove = item.ToString();

                         

                            for (int i = dgvItems.Rows.Count - 1; i >= 0; i--)
                            {
                                DataGridViewRow row = dgvItems.Rows[i];
                                if (row.Cells["Despatch"].Value?.ToString() == DespatchToRemove)
                                {
                                    rowsRemoved++;
                                  //  MessageBox.Show(row.Cells["Qty Carriers"].ToString());
                                    itemsRemoved = itemsRemoved + Int32.Parse(row.Cells["Qty Carriers"].Value.ToString());
                                    dgvItems.Rows.RemoveAt(i);
                                    
                                }
                            }
                            txtscanneditems.Text = (int.Parse(txtscanneditems.Text) - rowsRemoved).ToString();
                            txtCarriers.Text= (int.Parse(txtCarriers.Text) - itemsRemoved).ToString();


                            /*while (dgvItems.Rows.Count > 2)
                            {

                                if (dgvItems.NewRowIndex != 0)
                                {

                                    dgvItems.Rows.RemoveAt(0);
                                }
                                // listBox1.Items.RemoveAt(0);
                                txtscanneditems.Text = "1";
                                txtCarriers.Text = dgvItems.Rows[dgvItems.Rows.Count - 2].Cells[2].Value.ToString();
                            }

                            if (split_wo == false)
                            {
                                if (dgvItems.NewRowIndex != 0)
                                {
                                    dgvItems.Rows.RemoveAt(0);
                                }
                                txtscanneditems.Text = "0";
                                txtCarriers.Text = "0";
                            }

                            while (listBox1.Items.Count > 1)
                            {
                                listBox1.Items.RemoveAt(0);
                            }

                            if (split_wo == false)
                            {
                                listBox1.Items.RemoveAt(0);
                            }*/
                        }
                        for (int i = clbdespatches.CheckedItems.Count - 1; i >= 0; i--)
                        {
                            clbdespatches.Items.Remove(clbdespatches.CheckedItems[i]);
                        }

                        if (clbdespatches.Items.Count == 0)
                        {
                            txtCustomerOffer.Text = "";
                            txtDespatchMethod.Text = "";
                            txtRunType.Text = "";
                            txtTrigram.Text = "";
                            trigram = "";
                            txtDueOut.Text = "";
                        }
                        DespatchList = "";
                        DespatchTypes = "";
                    }


                }
                else
                    MessageBox.Show("No Items in list Please scan some Work orders before Trying to generate a Post List");
            }
            else
                MessageBox.Show("Please select Some items to Print");
            txtBarcode.Focus();
        }

        private void dgvItems_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void dgvItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //        int number;
            //    if (int.TryParse(e.FormattedValue.ToString(), out number) == false)
            //    {
            //    MessageBox.Show("The Value in this Cell Needs to be a Number");
            //      }
        }

        private void dgvItems_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

        }

        private void dgvItems_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            //  MessageBox.Show(e.Row.Cells[0].Value.ToString());
        /*    string Removed_Shipping_Method;
            Boolean KeepinList = false;
            //scannedItems--;
            //txtscanneditems.Text = scannedItems.ToString();
            Removed_Shipping_Method = e.Row.Cells["Despatch"].Value.ToString();
            //TotalQTY = TotalQTY - int.Parse(e.Row.Cells["Qty Carriers"].Value.ToString());

            //txtCarriers.Text = TotalQTY.ToString();
            foreach (DataGridViewRow Row in dgvItems.Rows)
            {
                if (Row.Cells["Despatch"].Value == Removed_Shipping_Method)

                { KeepinList = true; }
            }
            if (!(KeepinList))
            {
                foreach (var item in clbdespatches.Items)
                {
                    if (Removed_Shipping_Method == item.ToString())
                        clbdespatches.Items.Remove(Removed_Shipping_Method);
                }
            }*/
        }

        private void dgvItems_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string Removed_Shipping_Method;
            Boolean KeepinList = false;
            scannedItems--;
            txtscanneditems.Text = scannedItems.ToString();
            Removed_Shipping_Method = e.Row.Cells["Despatch"].Value.ToString();
            TotalQTY = TotalQTY - int.Parse(e.Row.Cells["Qty Carriers"].Value.ToString());
            
            txtCarriers.Text = TotalQTY.ToString();
            foreach (DataGridViewRow Row in dgvItems.Rows)
            {
                if (Row == e.Row || Row.IsNewRow) continue;
                if (Row.Cells["Despatch"].Value.ToString() == Removed_Shipping_Method)

                { KeepinList = true; }
            }
           
            if (!(KeepinList))
                {
                for (int i = clbdespatches.Items.Count - 1; i >= 0; i--)
                {
                    if (clbdespatches.Items[i]?.ToString() == Removed_Shipping_Method)
                    {
                        clbdespatches.Items.RemoveAt(i);
                       DespatchTypes =  DespatchTypes.Replace(Removed_Shipping_Method, "");
                    }
                }

            }
        } 

        private void txtVer_Click(object sender, EventArgs e)
        {

        }

        private void txtDueOut_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmRoyalMail_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void clbdespatches_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
