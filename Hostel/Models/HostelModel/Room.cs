using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Hostel.Models
{
    public class Room
    {
        
        public int RoomId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле {0} не может быть пустым")]
        [Display(Name = "Номер комнаты")]
        [Range(0, 10000, ErrorMessage = "{0} должен быть в диапазоне от {1} до {2}")]
        public int Number { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле {0} не может быть пустым")]
        [Display(Name = "Количество мест")]
        [Range(2, 4, ErrorMessage = "{0} в комнате должно быть от {1} до {2}")]       
        public int Capacity { get; set; }
            
        public List<Person> Persons { get; set; }
    }
}