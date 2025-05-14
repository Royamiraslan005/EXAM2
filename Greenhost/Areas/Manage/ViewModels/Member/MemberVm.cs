using System.ComponentModel.DataAnnotations.Schema;

namespace Greenhost.Areas.Manage.ViewModels.Member
{
    public class MemberVm
    {
        public string? Fullname { get; set; }
        public string? Designation { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile formFile { get; set; }
    }
}
