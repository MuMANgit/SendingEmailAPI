using Microsoft.AspNetCore.Mvc;
using SE.BLL.DTO;
using SE.BLL.Interfaces;
using System.Threading.Tasks;

namespace SendingEmail.Controllers
{
    /// <summary>
    /// Контроллер для обработки запросов HTTP
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MailsController : Controller
    {
        IEmailService emailService;

        public MailsController(IEmailService service)
        {
            emailService = service;
        }
        /// <summary>
        /// POST метод, формирует и отправлят сообщение адресатам
        /// </summary>
        /// <param name="messageDTO">Содержит параметры сообщения и получателей</param>
        /// <returns>Результат запроса </returns>
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageDTO messageDTO)
        {
            
            if (messageDTO.Recipients.Count == 0)
            {
                ModelState.AddModelError("Recipients", "Пустой список получателей");
            }
            if (ModelState.IsValid)
            {
                return Json(await emailService.SendingMessage(messageDTO));
            }

            return BadRequest(ModelState);
        }
        /// <summary>
        /// Get метод, возвращает список сообщений
        /// </summary>
        /// <returns>Результат запроса</returns>
        [HttpGet]
        public IActionResult GetMessage()
        {
            return Json(emailService.GetMessages());
        }
    }
}
