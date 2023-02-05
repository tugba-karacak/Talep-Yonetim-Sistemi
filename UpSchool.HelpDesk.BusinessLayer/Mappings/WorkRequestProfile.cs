using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.CQRS.CreateWorkRequest;
using UpSchool.HelpDesk.DtoLayer;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.Mappings
{
    public class WorkRequestProfile : Profile
    {
        public WorkRequestProfile()
        {
            this.CreateMap<WorkRequest, WorkRequestDto>().ReverseMap();
            this.CreateMap<WorkRequest, CreateWorkRequestCommand>().ReverseMap();
        }
    }
}
