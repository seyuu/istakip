namespace Arch.Core
{
    public partial class CompanySocialMedia : BaseEntity
    {
        public int UnitId { get; set; }
        public string Title { get; set; }
        public int? NumberOfFollowers { get; set; }
        public int? NumberOfPosts { get; set; }
    }
}
