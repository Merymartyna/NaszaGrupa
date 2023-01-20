using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NaszaGrupa.Models
{
    public class MessagesModel
    {
        [Key]
        public int MessageId { get; set; }
        [Required(ErrorMessage = "Nagłówek jest wymagany! ")]
        [MaxLength(50)]
        [DisplayName("Nagłówek")]
        public string Header { get; set; }
        [DisplayName("Opis")]
        [Required(ErrorMessage = "Opis jest wymagany!")]
        [MaxLength(2000)]
        public string Description { get; set; }
        [DisplayName("Waga")]
        public string Importance { get; set; }
        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        public DateTime Date { get; set; }
    }
}
