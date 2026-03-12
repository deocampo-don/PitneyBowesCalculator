public class SettingsModel
{
    // Database (CPS)
    public string CPSServer { get; set; }
    public string CPSDatabase { get; set; }
    public string CPSQuery { get; set; }
    public int ConnectionTimeout { get; set; }
    public bool TrustedConnection { get; set; }
    public bool TrustedServerCertificate { get; set; }
    public string rqServer { get; set; }
    public int refreshInterval { get; set; }
    public string lastUpdated { get; set; }

    // Local (INI)
    public string DefaultPrinter { get; set; }
    public string OutputDirectory { get; set; }
    
}
