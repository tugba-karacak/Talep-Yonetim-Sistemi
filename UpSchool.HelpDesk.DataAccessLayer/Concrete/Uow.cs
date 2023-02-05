using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.DataAccessLayer.Contexts;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.DataAccessLayer.Concrete
{
    public class Uow : IUow
    {
        private readonly HelpDeskContext _context;

        public Uow(HelpDeskContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class ,IEntity, new()
        {
            return new Repository<T>(_context);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
