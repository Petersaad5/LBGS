using Bank.Models;
using Common.Requests;

namespace BAL.IServices
{
    public interface IAtmService
    {
        public Card? AtmCardLogin(AtmLoginRequest request);
    }
}