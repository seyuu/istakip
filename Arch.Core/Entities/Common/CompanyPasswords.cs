namespace Arch.Core
{
    public partial class CompanyPasswords : BaseEntity
    {
        public int UnitId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Password { get; set; }
    }
}
