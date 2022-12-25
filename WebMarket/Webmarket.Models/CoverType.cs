using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Webmarket.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "کاور دسته اجباریست")]
        [DisplayName("کاور دسته")]
        public string Name { get; set; }
    }
}
