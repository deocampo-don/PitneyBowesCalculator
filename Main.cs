using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.Packed_And_Ready;
using WindowsFormsApp1.Picked_Up;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {

        private readonly List<PbJobModel> _pbJobs = new List<PbJobModel>();
        private readonly List<PickListModel> _pickLists = new List<PickListModel>();




        public Main()
        {
            InitializeComponent();



            //Apply styles to tabs
            ApplyTabColors(kcbPickedUp);
            ApplyTabColors(kcbBuildPallets);
            ApplyTabColors(kcbPackedReady);

            kryptonCheckSet1.CheckButtons.Add(kcbBuildPallets);
            kryptonCheckSet1.CheckButtons.Add(kcbPackedReady);
            kryptonCheckSet1.CheckButtons.Add(kcbPickedUp);

            kryptonCheckSet1.CheckedButtonChanged += (_, __) =>
            {
                nvNavigator.SelectedIndex = kryptonCheckSet1.CheckedIndex;
            };

            // Default selected tab
            kryptonCheckSet1.CheckedButton = kcbBuildPallets;

            var list = lvBuild; // the one on your page (select it in designer to see its name)
            var list2 = packedListView2;
            var list3 = pickedUpListView;



            var packedRow = new PackedRowControl();
            var pickedRow = new PickedUpRowControl();
            var row1 = new PalletRowControl();


            var test = new PickListModel
            {
                JobName = "CAPONE",
                JobNumber = 23413,
                EnvelopeQty = 10000,
                Trays = 10,
                Pallets = 3,
                ShipDateTime = DateTime.Parse("2025-11-23")
            };

            var test2 = new PbJobModel
            {
                JobName = "CAPONE",
                JobNumber = 23413,
                EnvelopeQty = 10000,
             

                Pallets = new List<Pallet>
                 {
                    //total traycount 85
                     new Pallet { TrayCount = 30},
                     new Pallet { TrayCount = 40},
                     new Pallet { TrayCount = 15 },

                 },

                PackDate = DateTime.Parse("2025-11-23"),
            };
            var test3 = new PbJobModel
            {
                JobName = "Test",
                JobNumber = 23414,
                EnvelopeQty = 20000,
       

                Pallets = new List<Pallet>
                 {
               
                     new Pallet { TrayCount = 30},
                     new Pallet { TrayCount = 40},
                     new Pallet { TrayCount = 50 },
                     new Pallet { TrayCount = 50 },
                     new Pallet { TrayCount = 55 },

                 },

                PackDate = DateTime.Parse("2025-11-23"),
            };

            // row1.Bind(...) later
            _pickLists.Add(test);                 // ← DATA updated here
            _pbJobs.Add(test2);
            _pbJobs.Add(test3);


            //for Packed and ready View Form
            packedListView2.SetItems(_pbJobs);


            pickedUpListView.SetItems(_pickLists);

            //load the pack & ready using the pickedUpListView data

            //   LoadPackedReady();
            // loadShipAll();
            list.AddRow(row1);
            list2.AddRow(packedRow);
            //list3.AddRow(pickedRow);



            MakeTitleBarButton(btnMaximize);
            MakeTitleBarButton(btnMinimize);
            MakeTitleBarButton(btnClose);
            MakeTitleBarButton(btnSettings);



        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void ApplyTabColors(KryptonCheckButton b)
        {
            Color purple = Color.FromArgb(110, 74, 191);
            var tabFont = new Font("Segoe UI Semibold", 17f, FontStyle.Regular);
            var pad = new Padding(0, 6, 0, 6);

            b.StateCommon.Content.Padding = pad;
            b.StateCommon.Content.ShortText.Font = tabFont;
            b.AutoSize = true;
            // CHECKED states: MUST draw background/border
            b.StateCheckedNormal.Back.Draw = InheritBool.True;
            b.StateCheckedNormal.Back.Color1 = purple;
            b.StateCheckedNormal.Back.Color2 = purple;
            b.StateCheckedNormal.Border.Draw = InheritBool.True;
            b.StateCheckedNormal.Border.DrawBorders = PaletteDrawBorders.All;
            b.StateCheckedNormal.Border.Color1 = purple;
            b.StateCheckedNormal.Border.Color2 = purple;
            b.StateCheckedNormal.Border.Rounding = 7;
            b.StateCheckedNormal.Content.ShortText.Color1 = Color.White;


            // Checked hover (keep purple)
            b.StateCheckedTracking.Back.Draw = InheritBool.True;
            b.StateCheckedTracking.Back.Color1 = purple;
            b.StateCheckedTracking.Back.Color2 = purple;
            b.StateCheckedTracking.Border.Draw = InheritBool.True;
            b.StateCheckedTracking.Border.DrawBorders = PaletteDrawBorders.All;
            b.StateCheckedTracking.Border.Color1 = purple;
            b.StateCheckedTracking.Border.Color2 = purple;
            b.StateCheckedTracking.Border.Rounding = 7;
            b.StateCheckedTracking.Content.ShortText.Color1 = Color.White;


            //Pressed on checked
            b.StateCheckedPressed.Back.Draw = InheritBool.True;
            b.StateCheckedPressed.Back.Color1 = purple;
            b.StateCheckedPressed.Back.Color2 = purple;
            b.StateCheckedPressed.Border.Draw = InheritBool.True;
            b.StateCheckedPressed.Border.DrawBorders = PaletteDrawBorders.All;
            b.StateCheckedPressed.Border.Color1 = purple;
            b.StateCheckedPressed.Border.Color2 = purple;
            b.StateCheckedPressed.Border.Rounding = 7;
            b.StateCheckedPressed.Content.ShortText.Color1 = Color.White;


            //Normal state
            b.StateNormal.Back.Draw = InheritBool.False;
            b.StateNormal.Border.Draw = InheritBool.False;
            b.StateNormal.Content.ShortText.Color1 = Color.Black;


            //Hover state
            b.StateTracking.Back.Draw = InheritBool.False;
            b.StateTracking.Border.Draw = InheritBool.False;
            b.StateTracking.Content.ShortText.Color1 = Color.Black;


            b.StatePressed.Back.Draw = InheritBool.False;
            b.StatePressed.Border.Draw = InheritBool.False;
            b.StatePressed.Content.ShortText.Color1 = Color.Black;

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal; else this.WindowState = FormWindowState.Maximized;
        }

        private void palletListView1_Load(object sender, EventArgs e)
        {

        }

        void MakeTitleBarButton(KryptonButton b)
        {
            // Disable background in ALL states
            //b.StateCommon.Back.Draw = InheritBool.False;
            b.StatePressed.Back.Draw = InheritBool.False;
            b.StateDisabled.Back.Draw = InheritBool.False;

            // Disable borders in ALL states
            //b.StateCommon.Border.Draw = InheritBool.False;
            b.StatePressed.Border.Draw = InheritBool.False;
            b.StateDisabled.Border.Draw = InheritBool.False;

            // Tight padding for icons
            b.StateCommon.Content.Padding = Padding.Empty;

            // No focus rectangle
            b.TabStop = false;

            // Optional: transparent background inheritance

        }




        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var dlg = new SettingsDialog())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {

                }
            }
        }
        private void btnAddPBJob_Click(object sender, EventArgs e)
        {
            using (var dlg = new CreatePBJobDialog())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {

                    var job = new PbJobModel
                    {
                        JobName = dlg.JobName.ToString(),
                        JobNumber = int.TryParse(dlg.JobNumber, out var jobNumer) ? jobNumer : 0,
                        EnvelopeQty = 0,
                        ScannedWorkOrders = 0
                    };

                    _pbJobs.Add(job);                 // ← DATA updated here
                    lvBuild.SetItems(_pbJobs); // ← UI refreshed here
                }
            }
        }
    }
}

