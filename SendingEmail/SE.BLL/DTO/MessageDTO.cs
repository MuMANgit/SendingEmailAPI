using SE.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SE.BLL.DTO
{
    /// <summary>
    /// Класс содержащий данные для передачи
    /// и получения данных с уровня представления
    /// </summary>
    public class MessageDTO
    {
        [Required(ErrorMessage ="Поле должно быть заполнено")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string Body { get; set; }
        [Required(ErrorMessage = "Добавьте список адресатов")]
        public List<string> Recipients { get; set; }
    }
}
