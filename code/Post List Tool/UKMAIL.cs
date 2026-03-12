using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Post_List_Tool
{
    public partial class FrmUKmail : Form
    {

        private DataTable dataTable;
        int Apos;
        int Bpos;
        bool exists = false;
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
       
        bool split_wo = false;
        bool from_Preview = false;
        int TotalQTY = 0;
        int TotalPulls = 0;
        int TotalProdSupp = 0;
        int TotalDespatch = 0;
        int TotalTrays = 0;
        int scannedItems;
        

        public FrmUKmail()
        {
            InitializeComponent();
        }

        private void FrmUKmail_Load(object sender, EventArgs e)
        {

        }

        private void FrmUKmail_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            if (txtBarcode.Text.EndsWith("\n"))
            {
                txtBarcode.Text.Replace("\n", "");
                btn_add_to_PostList_Click(this, null);
                txtBarcode.Text = "";
            }
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

        private void btn_add_to_PostList_Click(object sender, EventArgs e)
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
                //MessageBox.Show("Despatch Method filter turned off this should not be LIVE");
                whereSQL = whereSQL + " and (ship.ID in (" + Properties.Settings.Default.UKM_Shipment_Methods + ") ";
                whereSQL = whereSQL + " or shipold.ID in (" + Properties.Settings.Default.UKM_Shipment_Methods + ") )";
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
                                        clbdespatches.Items.Add(oneWOeReader["ORIGDESPATCH"].ToString());
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
    }
}
