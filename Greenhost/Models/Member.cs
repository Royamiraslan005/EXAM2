using Greenhost.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Greenhost.Models
{
    public class Member:BaseEntity
    {
        public string? Fullname {  get; set; }
        public string? Designation { get; set; }
        public string? ImgUrl {  get; set; }
        [NotMapped]
      public IFormFile formFile { get; set; }
    }
}
