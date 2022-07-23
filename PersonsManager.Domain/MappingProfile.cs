using AutoMapper;
using PersonsManager.Domain.Models;
using PersonsManager.DTO.Client;
using PersonsManager.DTO.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientDto>();
            CreateMap<Master, MasterDto>();

            CreateMap<ClientForCreationDto, Client>();
            CreateMap<MasterForCreationDto, Master>();

            CreateMap<ClientForUpdateDto, Client>();
            CreateMap<MasterForUpdateDto, Master>();
        }
    }
}
