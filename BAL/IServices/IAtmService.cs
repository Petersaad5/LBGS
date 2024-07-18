using Bank.Models;
using Common.Requests;
using Common.Responses;

namespace BAL.IServices
{
    public interface IAtmService
    {
        public Card? AtmCardLogin(AtmLoginRequest request);
        public AtmProfileResponse? GetAtmProfile(GetCardByNumberRequest request);
    }
}