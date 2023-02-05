using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.DataAccessLayer.Contexts;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.DataAccessLayer.Abstract
{
    public interface IUow
    {
        IRepository<T> GetRepository<T>() where T : class, IEntity, new();
        Task<int> CommitAsync();
    }
}
