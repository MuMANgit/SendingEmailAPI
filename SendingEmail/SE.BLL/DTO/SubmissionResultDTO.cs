using System;
using System.Collections.Generic;
using System.Text;

namespace SE.BLL.DTO
{
    /// <summary>
    /// Класс содержащий данные для получения
    /// данных с DAL
    /// </summary>
    public class SubmissionResultDTO
    {
        /// <summary>
        /// Адреса получателей
        /// </summary>
        public string Recipients { get; set; }
        /// <summary>
        /// Результат отправки сообщения
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// Сообщения об ошибке
        /// </summary>
        public string FailedMessage { get; set; }
    }
}
