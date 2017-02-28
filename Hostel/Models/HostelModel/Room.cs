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

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле Номер комнаты не может быть пустым")]
        [Display(Name = "Номер комнаты")]
        public int Number { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле Количество мест не может быть пустым")]
        [Display(Name = "Количество мест")]
        [Range(2, 4, ErrorMessage = "Количество мест в комнате должно быть от 2 до 4")]
        public int Capacity { get; set; }
            
        public List<Person> Persons { get; set; }
    }
}