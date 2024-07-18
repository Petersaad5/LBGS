using BAL.IServices;
using Bank.Models;
using Common.Requests;
using DAL;
using Microsoft.AspNetCore.Cors.Infrastructure;
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
        private readonly ICardService _cardService;

        public AtmService(DapperAccess dapperAccess, ICardService cardService)
        {
            _dapperAccess = dapperAccess;
            _cardService = cardService;
        }
        
        
        
        public Card? AtmCardLogin(AtmLoginRequest request)
        {
            GetCardByNumberRequest getCardRequest= new GetCardByNumberRequest { CardNumber=request.CardNumber};
            Card? card = _cardService.GetCardByNumber(getCardRequest);
            if (card == null)
            {
                return null;
            }
            if (card.CSV == request.CSV)
            {
                return card;
            }
            return null;
        }

    }
}
