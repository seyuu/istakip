using Arch.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Arch.Dto.ParamDto
{
    public class RegisterParamDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen bir kullanıcı adı giriniz")]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Lütfen şifre giriniz")]
        [StringLength(100, ErrorMessage = "{0} en az {2} karakter olmalıdır.", MinimumLength = 4)]
        [DisplayName("Şifre")]
        public string Password { get; set; }
        [Required]
        [DisplayName("Birim")]
        [Range(1, int.MaxValue, ErrorMessage = "İlgili Birim alanı gereklidir.")]
        public int UnitId { get; set; }

        [Required(ErrorMessage = "Lütfen bir ad giriniz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen bir soyad giriniz")]
        public string Surname { get; set; }
        public string Initials { get; set; }

        [Required(ErrorMessage = "Email boş bırakılamaz")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                          @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                          @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                          ErrorMessage = "Email adresi geçersiz")]
        public string Email { get; set; }
        public decimal? Phone { get; set; }
        public bool IsActive { get; set; }
        public int? ImageId { get; set; }
        public int? PersonId { get; set; }
        [Required(ErrorMessage = "Oluşturma tarihi boş bırakılamaz")]
        public DateTime CreatedDate { get; set; }
        public int ResultCount { get; set; }
        public string UnitName { get; set; }
        public string Code { get; set; }
        public Unit Unit { get; set; }
    }
}
