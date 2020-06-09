namespace Arch.Core
{
    public partial class CompanyDetail:BaseEntity
    {
        public int UnitId { get; set; }
        public string CompanyTitle { get; set; }
        public string BillTitle { get; set; }
        public string TaxIdentityNo { get; set; }
        public string TaxAdministration { get; set; }
        public string Info { get; set; }
        public string Domains { get; set; }
        public string SummaryInformation { get; set; }
        public string AnalysisReport { get; set; }
    }
}
