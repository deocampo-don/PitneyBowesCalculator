using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;              // ⭐ NEW
using System.Threading.Tasks;       // ⭐ NEW
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

        public event EventHandler ItemsChanged;
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

            // Seed data and bind to views (UI-only for now)
            SeedSampleData();
            RefreshAllViews();


            //refresh this panel
            packedListView2.ItemsChanged += (_, __) =>
            {
                RefreshAllViews();
            };
        }
        // -----------------------------
        // ⭐ rqlite Initialization
        // -----------------------------
        private async Task InitializeRqliteAsync()
        {
            if (RqliteClient.httpClient == null)
            {
                RqliteClient.httpClient = new HttpClient();
                RqliteClient.DefaultEndPoint = "http://127.0.0.1:4001";
            }

            // Safe to call every run
            await RqliteClient.CreatePbSchemaAsync();
        }

        // -----------------------------
        // Form Events
        // -----------------------------
        private async void Form1_Load(object sender, EventArgs e)   // ⭐ MODIFIED
        {
            //// Initialize rqlite FIRST
            //await InitializeRqliteAsync();

            //// Existing logic
            //SyncNavigatorWithCheckedTab();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Intentionally left blank
        }

        // -----------------------------
        // UI Styling / Wiring
        // -----------------------------
        private void ApplyTabStyles()
        {
            CSSDesign.ApplyTabColors(kcbPickedUp);
            CSSDesign.ApplyTabColors(kcbBuildPallets);
            CSSDesign.ApplyTabColors(kcbPackedReady);
        }

        private void WireCheckSetToNavigator()
        {
            kryptonCheckSet1.CheckButtons.Add(kcbBuildPallets);
            kryptonCheckSet1.CheckButtons.Add(kcbPackedReady);
            kryptonCheckSet1.CheckButtons.Add(kcbPickedUp);

            kryptonCheckSet1.CheckedButtonChanged += (_, __) =>
            {
                nvNavigator.SelectedIndex = kryptonCheckSet1.CheckedIndex;
            };

            kryptonCheckSet1.CheckedButton = kcbBuildPallets;
            SyncNavigatorWithCheckedTab();
        }

        private void SyncNavigatorWithCheckedTab()
        {
            var idx = kryptonCheckSet1.CheckedIndex;
            nvNavigator.SelectedIndex = (idx >= 0) ? idx : 0;
        }

        private void InitializeTitleBarButtons()
        {
            CSSDesign.MakeTitleBarButton(btnMaximize);
            CSSDesign.MakeTitleBarButton(btnMinimize);
            CSSDesign.MakeTitleBarButton(btnClose);
            CSSDesign.MakeTitleBarButton(btnSettings);
        }

        // -----------------------------
        // Data: Seeding & Refresh
        // -----------------------------
        private void SeedSampleData()
        {
            var packDate = DateTime.Parse("2025-11-23");

            var pb1 = new PbJobModel
            {
                JobName = "CAPONE",
                JobNumber = 23413,
                PackDate = packDate,

                Pallets = new List<Pallet>
                {
                    new Pallet
                    {
                        Trays = 40,
                        PackedTime = packDate.Date.AddHours(21).AddMinutes(30),
                        WorkOrders = new List<WorkOrder>
                        {
                            new WorkOrder { Code = "WO-001", EnvelopeQty = 1000 },
                            new WorkOrder { Code = "WO-002", EnvelopeQty = 500 },
                             new WorkOrder { Code = "WO-001", EnvelopeQty = 1000 },
                            new WorkOrder { Code = "WO-002", EnvelopeQty = 500 },
                             new WorkOrder { Code = "WO-001", EnvelopeQty = 1000 },
                            new WorkOrder { Code = "WO-002", EnvelopeQty = 500 },
                             new WorkOrder { Code = "WO-001", EnvelopeQty = 1000 },
                            new WorkOrder { Code = "WO-002", EnvelopeQty = 500 },
                             new WorkOrder { Code = "WO-001", EnvelopeQty = 1000 },
                              new WorkOrder { Code = "WO-001", EnvelopeQty = 1000 },
                               new WorkOrder { Code = "WO-001", EnvelopeQty = 1000 },
                                new WorkOrder { Code = "WO-001", EnvelopeQty = 1000 },
                            new WorkOrder { Code = "WO-002", EnvelopeQty = 500 },
                             new WorkOrder { Code = "WO-001", EnvelopeQty = 1000 },
                            new WorkOrder { Code = "WO-002", EnvelopeQty = 500 },
                            new WorkOrder { Code = "WO-002", EnvelopeQty = 500 },
                             new WorkOrder { Code = "WO-001", EnvelopeQty = 1000 },
                            new WorkOrder { Code = "WO-002", EnvelopeQty = 500 },

                        }
                    },
                    new Pallet
                    {
                        Trays = 42,
                        PackedTime = packDate.Date.AddHours(22).AddMinutes(30),
                        WorkOrders = new List<WorkOrder>
                        {
                            new WorkOrder { Code = "WO2-001", EnvelopeQty = 1000 },
                            new WorkOrder { Code = "WO2-002", EnvelopeQty = 500 }
                        }
                    },
                    new Pallet { },
                    new Pallet { }
                }
            };

            var pb2 = new PbJobModel
            {
                JobName = "Test",
                JobNumber = 23414,

                Pallets = new List<Pallet>
                {
                    new Pallet(), new Pallet(), new Pallet(), new Pallet(), new Pallet()
                }
            };

            _pbJobs.Add(pb1);
            _pbJobs.Add(pb2);
        }

        private void RefreshAllViews()
        {
            lvBuild?.SetItems(_pbJobs);
            packedListView2?.SetItems(_pbJobs);
            pickedUpListView?.SetItems(_pbJobs);

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
                }
            }
        }

        // -----------------------------
        // Actions
        // -----------------------------
        private void btnAddPBJob_Click_1(object sender, EventArgs e)
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
                        PackDate = DateTime.Today
                    };

                    _pbJobs.Add(job);
                    RefreshAllViews();
                }
            }
        }
    }
}
