using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.CQRS.CreateWorkRequestState;
using UpSchool.HelpDesk.DtoLayer;
using UpSchool.HelpDesk.DtoLayer.WorkRequestState;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.Mappings
{
    public class WorkRequestStateProfile : Profile
    {
        public WorkRequestStateProfile()
        {
            this.CreateMap<WorkRequestState, WorkRequestStateDto>().ReverseMap();
        }
    }
}
