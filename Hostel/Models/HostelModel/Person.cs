using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Hostel.Models
{    
    public class Person
    {
        public int PersonId { get; set; }

        public int RoomId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле {0} не может быть пустым")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        public Room Room { get; set; }
    }
}