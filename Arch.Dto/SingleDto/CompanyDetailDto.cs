using Arch.Core;
using System.Collections.Generic;
namespace Arch.Dto.SingleDto
{
    public class CompanyDetailDto
    {
        public Unit Company { get; set; }
        public CompanyDetail CompanyDetail { get; set; }
        public List<CompanyAuthorities> CompanyAuthorities { get; set; }
        public List<CompanyPasswords> CompanyPasswords { get; set; }
        public List<CompanySocialMedia> CompanySocialMedia { get; set; }
    }
}
