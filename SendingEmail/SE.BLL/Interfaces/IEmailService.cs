using SE.BLL.DTO;
using SE.BLL.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.BLL.Interfaces
{
    public interface IEmailService
    {
        public Task<RequestResponse<MessageDTO>> SendingMessage(MessageDTO messageDTO);
        public RequestResponse<List<MessageInfoDTO>> GetMessages();
    }
}
