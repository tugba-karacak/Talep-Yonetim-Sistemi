using MediatR;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DtoLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetWorkRequestByUserId
{
    public class GetWorkRequestByUserIdQuery : IRequest<Result<List<WorkRequestDto>>>
    {

        public int UserId { get; set; }

        public GetWorkRequestByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
