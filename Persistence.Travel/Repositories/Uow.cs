using Application.Travel.Services;
using Persistence.Travel.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Travel.Repositories
{
    public class Uow:IUow
    {
        private readonly TravelContext _context;
        public Uow(TravelContext context )
        {
                _context = context;
        }
        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
