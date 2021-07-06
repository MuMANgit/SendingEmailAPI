using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SE.DAL.Entities
{
    
    public class SubmissionResult
    {
        /// <summary>
        /// Айди записи
        /// </summary>
        [Key]
        public int ResultId { get; set; }
        /// <summary>
        /// Адреса получателей
        /// </summary>
        [Required]
        public string Recipients { get; set; }
        /// <summary>
        /// Результат отправки сообщения
        /// </summary>
        [Required]
        public string Result { get; set; }
        /// <summary>
        /// Сообщения об ошибке
        /// </summary>
        [Required]
        public string FailedMessage { get; set; }
        public int MessageID { get; set; }
        public MessageInfo Message { get; set; }
    }
}
