using Bank.Models;
using Common.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface ICardService
    {
        public Card? GetCardById(GetCardByIdRequest request);
        public List<Card> GetCardByAccountId(GetAccountByIdRequest request);
        public int UpdateCard(UpdateCardRequest request);
        public int AddCard(AddCardRequest request);
        public int DeactivateCard(int cardId);
        public int ActivateCard(int cardId);
    }
}
