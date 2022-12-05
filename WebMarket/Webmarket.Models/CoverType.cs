using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
