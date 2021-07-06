using SE.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SE.BLL.DTO
{
    /// <summary>
    /// Класс содержащий данные для получения
    /// данных с DAL
    /// </summary>
    public class MessageInfoDTO
    {
        /// <summary>
        /// Тема сообщеня
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        ///Тело сообщения
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Дата создания сообщения
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Результат отправки сообщения
        /// </summary>
        public List<SubmissionResultDTO> submissionResults { get; set; }

    }
}
