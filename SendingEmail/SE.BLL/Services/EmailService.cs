using AutoMapper;
using MimeKit;
using SE.BLL.DTO;
using SE.BLL.Interfaces;
using SE.BLL.SMTP;
using SE.DAL.EF;
using SE.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SE.BLL.Responses;

namespace SE.BLL.Services
{
    /// <summary>
    /// Сервис для работы с сообщениями
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly SubmissionResultContext db;
        private readonly IMapper mapper;
        private readonly IOptions<EmailSettings> emailSettings;
        const string cond = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";
        public EmailService(SubmissionResultContext db, IMapper mapper, IOptions<EmailSettings> emailSettings)
        {
            this.db = db;
            this.mapper = mapper;
            this.emailSettings = emailSettings;
        }
        /// <summary>
        /// Метод возвращает список сообщений
        /// </summary>
        /// <returns></returns>
        public RequestResponse<List<MessageInfoDTO>> GetMessages()
        {
            RequestResponse<List<MessageInfoDTO>> requestResponse = new RequestResponse<List<MessageInfoDTO>>();

            try
            {
                var dbLog = mapper.Map<IEnumerable<MessageInfo>, List<MessageInfoDTO>>(
                            from message
                            in db.Messages.Include(m => m.submissionResults)
                            select message
                            );

                requestResponse.SuccessResult(dbLog);
            }
            catch (Exception ex)
            {
                requestResponse.ErrorResult($"Необработанное исключение - {ex.Message}");
            }
            
            return requestResponse;
        }
        /// <summary>
        /// Метод формирует и отправляет сообщение адресатам
        /// </summary>
        /// <param name="messageDTO">Параметры сообщеня и список получателей</param>
        /// <returns></returns>
        public async Task<RequestResponse<MessageDTO>> SendingMessage(MessageDTO messageDTO)
        {
            RequestResponse<MessageDTO> request = new RequestResponse<MessageDTO>();

            try
            {
                var emails = messageDTO.Recipients;
                var validsEmails = GetValidEmails(emails);

                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("name", "no-reply@example.com"));

                foreach (var email in validsEmails)
                {
                    message.To.Add(new MailboxAddress(string.Empty, email));
                }

                message.Subject = messageDTO.Subject;
                message.Body = new BodyBuilder() { HtmlBody = messageDTO.Body }.ToMessageBody();

                if (IncorrectСonfigurations(emailSettings)) 
                {
                    request.ErrorResult("Отсутсвует конфигурация SMTP сервера"); 
                    return request;
                }

                using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(emailSettings.Value.Host, (int)emailSettings.Value.Port, true);
                    await client.AuthenticateAsync(emailSettings.Value.Username, emailSettings.Value.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                await LoggingToDatabase(emails, messageDTO);
                request.SuccessResult(messageDTO);
            }
            catch (Exception ex)
            {
                request.ErrorResult($"Необработанное исключение - {ex.Message}");
            }

            return request;
        }
        /// <summary>
        /// Получение валидных
        /// email адресов
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        private List<string> GetValidEmails(List<string> emails)
        {
            var validsEmails = new List<string>();


            foreach (var email in emails)
            {
                if (Regex.IsMatch(email, cond))
                {
                    validsEmails.Add(email);
                }
            }

            return validsEmails;
        }
        /// <summary>
        /// Получение не валидных
        /// email адресов
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        private List<string> GetInvalidEmails(List<string> emails)
        {
            var invalidEmails = new List<string>();


            foreach (var email in emails)
            {
                if (!Regex.IsMatch(email, cond))
                {
                    invalidEmails.Add(email);
                }
            }

            return invalidEmails;
        }
        /// <summary>
        /// Логирование результата отправленных
        /// сообщений в базу данных
        /// </summary>
        /// <param name="emails"></param>
        /// <param name="messageDTO"></param>
        /// <returns></returns>
        private async Task LoggingToDatabase(List<string> emails, MessageDTO messageDTO)
        {
            var invalidEmails = GetInvalidEmails(emails);
            var validEmails = GetValidEmails(emails);

            MessageInfo message = mapper.Map<MessageInfo>(messageDTO);
            message.Date = DateTime.Now;
            await db.Messages.AddAsync(message);

            foreach (var email in validEmails)
            {
                await db.SubmissionsResult.AddAsync(new SubmissionResult { Result = "OK", Message = message, Recipients = email });
            }
            foreach (var email in invalidEmails)
            {
                await db.SubmissionsResult.AddAsync(new SubmissionResult { Result = "Failed", Message = message, Recipients = email, FailedMessage = $"5.1.3 The recipient address <{email}> is not a valid RFC-5321 address. x3sm1107856ljm.43 - gsmtp" });
            }
            await db.SaveChangesAsync();
        }
        /// <summary>
        /// Проверка на наличие SMTP конфигурации
        /// </summary>
        /// <param name="emailSettings"></param>
        /// <returns></returns>
        private bool IncorrectСonfigurations(IOptions<EmailSettings> emailSettings)
        {
            if (string.IsNullOrEmpty(emailSettings.Value.From))
            {
                return true;
            }
            if (string.IsNullOrEmpty(emailSettings.Value.Username))
            {
                return true;
            }
            if (string.IsNullOrEmpty(emailSettings.Value.Password))
            {
                return true;
            }
            if (string.IsNullOrEmpty(emailSettings.Value.Host))
            {
                return true;
            }
            if (emailSettings.Value.Port == null)
            {
                return true;
            }
            return false;
        }
    }
}
