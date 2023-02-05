﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.UpdateWorkRequestState
{
    public class UpdateWorkRequestStateCommand : IRequest<Result<NoContent>>
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
    }
}
