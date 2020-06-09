namespace Arch.Core
{
   public partial class CompanyAuthorities : BaseEntity
    {
        public int UnitId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
    }
}
