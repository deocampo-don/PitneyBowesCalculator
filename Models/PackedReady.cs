


using System;

public class PackedReady
    {/*
        public string JobName { get; set; }

        public int JobNumber { get; set; }

         public string StatusReady { get; set; }
        public int EnvelopeQty { get; set; }
        public int Trays { get; set; }
        public int Pallets { get; set; }
        public DateTime ShipDateTime { get; set; }
    */

    public string JobName { get; set; } = string.Empty;
    public int JobNumber { get; set; }

    // Status
    public bool IsReady { get; set; } = false;   // checkbox backing field

    // Quantities
    public int EnvelopeQty { get; set; }
    public int Trays { get; set; } 
    public int Pallets { get; set; } 

    // Dates
    public DateTime ShipDateTime { get; set; } = DateTime.Today;  // or PackDate

    // Optional: convenience constructor
    public PackedReady() { }

    public PackedReady(int jobNumber, string jobName)
    {
        JobNumber = jobNumber;
        JobName = jobName ?? string.Empty;
    }

}

