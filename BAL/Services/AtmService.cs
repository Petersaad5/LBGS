using BAL.IServices;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class AtmService : IAtmService
    {
        private readonly DapperAccess _dapperAccess;
        public AtmService(DapperAccess dapperAccess)
        {
            _dapperAccess = dapperAccess;
        }
    }
}
