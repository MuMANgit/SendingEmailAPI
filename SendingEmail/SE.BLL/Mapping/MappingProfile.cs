using AutoMapper;
using SE.BLL.DTO;
using SE.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SE.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MessageInfo, MessageDTO>();
            CreateMap<MessageDTO, MessageInfo>();

            CreateMap<MessageInfo, MessageInfoDTO>();
            CreateMap<MessageInfoDTO, MessageInfo>();

            CreateMap<SubmissionResult, SubmissionResultDTO>();
            CreateMap<SubmissionResultDTO, SubmissionResult>();

        }
    }
}
