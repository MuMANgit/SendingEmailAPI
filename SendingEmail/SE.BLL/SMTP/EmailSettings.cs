using System;
using System.Collections.Generic;
using System.Text;

namespace SE.BLL.SMTP
{
    /// <summary>
    /// Класс описывающий конфигурацию SMTP
    /// </summary>
    public class EmailSettings
    {
        /// <summary>
        /// Имя секции в конфигурации
        /// </summary>
        public const string SectionName = "EmailSettings";
        
        public string From { get; set; }
        /// <summary>
        /// Почта откуда производится отправка
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Пароль к почте
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Имя хост
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Порт
        /// </summary>
        public int? Port { get; set; }
    }
}
