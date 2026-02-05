using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.Packed_And_Ready;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        // -----------------------------
        // Fields
        // -----------------------------
        private readonly List<PbJobModel> _pbJobs = new List<PbJobModel>();
        private readonly List<PickListModel> _pickLists = new List<PickListModel>();


        // -----------------------------
        // Constructor
        // -----------------------------
        public Main()
        {
            InitializeComponent();

            // UI Setup
            ApplyTabStyles();
            WireCheckSetToNavigator();
            InitializeTitleBarButtons();

            // Seed data and bind to views
            SeedSampleData();
            RefreshAllViews();

            // Add initial rows for custom list controls (if needed)
            //  InitializeListRows();
        }

        // -----------------------------
        // Form Events
        // -----------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            // If you want to ensure nav sync on load:
            SyncNavigatorWithCheckedTab();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Intentionally left blank (remove if unused)
        }

        // -----------------------------
        // UI Styling / Wiring
        // -----------------------------
        private void ApplyTabStyles()
        {
            // Apply styles to tabs
            ApplyTabColors(kcbPickedUp);
            ApplyTabColors(kcbBuildPallets);
            ApplyTabColors(kcbPackedReady);
        }

        private void WireCheckSetToNavigator()
        {
            // Ensure check buttons belong to the same set
            kryptonCheckSet1.CheckButtons.Add(kcbBuildPallets);
            kryptonCheckSet1.CheckButtons.Add(kcbPackedReady);
            kryptonCheckSet1.CheckButtons.Add(kcbPickedUp);

            // Keep Navigator index in sync with selected tab
            kryptonCheckSet1.CheckedButtonChanged += (_, __) =>
            {
                nvNavigator.SelectedIndex = kryptonCheckSet1.CheckedIndex;
            };

            // Set default selected tab and sync immediately
            kryptonCheckSet1.CheckedButton = kcbBuildPallets;
            SyncNavigatorWithCheckedTab();
        }

        private void SyncNavigatorWithCheckedTab()
        {
            // Guard against -1 in rare cases
            var idx = kryptonCheckSet1.CheckedIndex;
            nvNavigator.SelectedIndex = (idx >= 0) ? idx : 0;
        }

        private void ApplyTabColors(KryptonCheckButton b)
        {
            Color purple = Color.FromArgb(110, 74, 191);
            var tabFont = new Font("Segoe UI Semibold", 17f, FontStyle.Regular);
            var pad = new Padding(0, 6, 0, 6);

            b.StateCommon.Content.Padding = pad;
            b.StateCommon.Content.ShortText.Font = tabFont;
            b.AutoSize = true;

            // CHECKED states: background/border visible
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

            // Pressed on checked
            b.StateCheckedPressed.Back.Draw = InheritBool.True;
            b.StateCheckedPressed.Back.Color1 = purple;
            b.StateCheckedPressed.Back.Color2 = purple;
            b.StateCheckedPressed.Border.Draw = InheritBool.True;
            b.StateCheckedPressed.Border.DrawBorders = PaletteDrawBorders.All;
            b.StateCheckedPressed.Border.Color1 = purple;
            b.StateCheckedPressed.Border.Color2 = purple;
            b.StateCheckedPressed.Border.Rounding = 7;
            b.StateCheckedPressed.Content.ShortText.Color1 = Color.White;

            // Normal state
            b.StateNormal.Back.Draw = InheritBool.False;
            b.StateNormal.Border.Draw = InheritBool.False;
            b.StateNormal.Content.ShortText.Color1 = Color.Black;

            // Hover state
            b.StateTracking.Back.Draw = InheritBool.False;
            b.StateTracking.Border.Draw = InheritBool.False;
            b.StateTracking.Content.ShortText.Color1 = Color.Black;

            // Pressed state (when not checked)
            b.StatePressed.Back.Draw = InheritBool.False;
            b.StatePressed.Border.Draw = InheritBool.False;
            b.StatePressed.Content.ShortText.Color1 = Color.Black;
        }

        private void InitializeTitleBarButtons()
        {
            MakeTitleBarButton(btnMaximize);
            MakeTitleBarButton(btnMinimize);
            MakeTitleBarButton(btnClose);
            MakeTitleBarButton(btnSettings);
        }

        private void MakeTitleBarButton(KryptonButton b)
        {
            // Disable background in pressed/disabled states
            b.StatePressed.Back.Draw = InheritBool.False;
            b.StateDisabled.Back.Draw = InheritBool.False;

            // Disable borders in pressed/disabled states
            b.StatePressed.Border.Draw = InheritBool.False;
            b.StateDisabled.Border.Draw = InheritBool.False;

            // Tight padding for icons
            b.StateCommon.Content.Padding = Padding.Empty;

            // No focus rectangle
            b.TabStop = false;
        }

        // -----------------------------
        // Data: Seeding & Refresh
        // -----------------------------
        private void SeedSampleData()
        {
            var packDate = DateTime.Parse("2025-11-23"); // or however you get it
            // Sample PickList
            var pickSample = new PickListModel
            {
                JobName = "CAPONE",
                JobNumber = 23413,
                //EnvelopeQty = 0,
                Trays = 10,
                Pallets = 3,
                ShipDateTime = DateTime.Parse("2025-11-23")
            };

            // Sample Packed & Ready job 1

            var pb1 = new PbJobModel
            {
                JobName = "CAPONE",
                JobNumber = 23413,
                PackDate = packDate,

                Pallets = new List<Pallet>
                {
                    // Pallet 1
                    new Pallet
                    {
                        // ✅ Manual tray input (per pallet)
                        Trays = 40,

                        // ✅ Actual packing time of THIS pallet
                        PackedTime = packDate.Date.AddHours(21).AddMinutes(30), // 9:30 PM

                        WorkOrders = new List<WorkOrder>
                        {
                            // ✅ Envelope quantity comes from DB / scanner
                            new WorkOrder { Code = "WO-001", EnvelopeQty = 1000 },
                            new WorkOrder { Code = "WO-002", EnvelopeQty = 500 }
                        }
                    },

                    // Pallet 2
                    new Pallet
                    {
                        Trays = 42,

                        PackedTime = packDate.Date.AddHours(22).AddMinutes(30), // 10:30 PM (example)

                        WorkOrders = new List<WorkOrder>
                        {
                            new WorkOrder { Code = "WO2-001", EnvelopeQty = 1000 },
                            new WorkOrder { Code = "WO2-002", EnvelopeQty = 500 }
                        }
                    },

                    // Empty pallets (allowed)
                    new Pallet { },
                    new Pallet { }
                }
                        };


            // Sample Packed & Ready job 2
            var pb2 = new PbJobModel
            {
                JobName = "Test",
                JobNumber = 23414,
                //     TrayCount = 40,
                Pallets = new List<Pallet>
                {
                    new Pallet {
                    },
                    new Pallet {
                    },
                    new Pallet {  },
                    new Pallet {  },
                    new Pallet {  },
                },
                PackDate = DateTime.Parse("2025-11-23"),
            };

            _pickLists.Add(pickSample);
            _pbJobs.Add(pb1);
            _pbJobs.Add(pb2);
        }

        private void RefreshAllViews()
        {
            // Refresh / bind all list views from the backing lists
            lvBuild?.SetItems(_pbJobs);
            packedListView2?.SetItems(_pbJobs);
            pickedUpListView?.SetItems(_pickLists);
        }

        private void InitializeListRows()
        {
            // If your custom list views require at least one row control present
            var palletRow = new PalletRowControl();
            var packedRow = new PackedRowControl();
            // var pickedRow = new PickedUpRowControl(); // If needed

            lvBuild?.AddRow(palletRow);
            packedListView2?.AddRow(packedRow);
            // pickedUpListView?.AddRow(pickedRow);
        }

        // -----------------------------
        // Window Buttons
        // -----------------------------
        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnMinimize_Click(object sender, EventArgs e) =>
            WindowState = FormWindowState.Minimized;

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            WindowState = (WindowState == FormWindowState.Maximized)
                ? FormWindowState.Normal
                : FormWindowState.Maximized;
        }

        // -----------------------------
        // Other Control Events
        // -----------------------------
        private void palletListView1_Load(object sender, EventArgs e)
        {
            // If anything needs to happen once the listview is loaded
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var dlg = new SettingsDialog())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    // Apply settings if needed
                }
            }
        }

        // -----------------------------
        // Actions
        // -----------------------------

        private void btnAddPBJob_Click(object sender, EventArgs e)
        {
            using (var dlg = new CreatePBJobDialog())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    var job = new PbJobModel
                    {
                        JobName = dlg.JobName?.ToString() ?? string.Empty,
                        JobNumber = int.TryParse(dlg.JobNumber, out var jobNumber)
                                        ? jobNumber
                                        : 0,

                        // ✅ Job-level date (use dialog value if you have one)
                        PackDate = DateTime.Today
                    };

                    _pbJobs.Add(job);
                    RefreshAllViews();
                }
            }
        }

    }
}