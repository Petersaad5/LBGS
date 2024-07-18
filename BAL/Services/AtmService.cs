using BAL.IServices;
using Bank.Models;
using Common.Requests;
using Common.Responses;
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
        public readonly IAccountService _accountService;
        public readonly IUserService _userService;

        public AtmService(DapperAccess dapperAccess, ICardService cardService, IUserService userService, IAccountService accountService)
        {
            _dapperAccess = dapperAccess;
            _cardService = cardService;
            _accountService = accountService;
            _userService = userService;
        }



        public Card? AtmCardLogin(AtmLoginRequest request)
        {
            GetCardByNumberRequest getCardRequest = new GetCardByNumberRequest { CardNumber = request.CardNumber };
            Card? card = _cardService.GetCardByNumber(getCardRequest);
            if (card == null)
            {
                return null;
            }
            if (card.CSV == request.CSV && card.IsActive)
            {
                return card;
            }
            return null;
        }
        public AtmProfileResponse? GetAtmProfile(GetCardByNumberRequest request)
        {
            GetCardByNumberRequest getCardRequest = new GetCardByNumberRequest { CardNumber = request.CardNumber };
            Card? card = _cardService.GetCardByNumber(getCardRequest);
            GetAccountByIdRequest getAccountRequest = new GetAccountByIdRequest { Id = card.AccountId };
            Account? account = _accountService.GetAccountById(getAccountRequest);
            GetUserByIdRequest getUserRequest = new GetUserByIdRequest { UserId = account.UserId };
            User? user = _userService.GetUser(getUserRequest);
            if (user == null || account == null || card == null)
            {
                return null;
            }
            AtmProfileResponse profile = new AtmProfileResponse
            {
                userId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                AccountId = account.Id,
                AccountNumber = account.AccountNumber,
                balance = account.Balance,
                CardNumber = card.CardNumber

            };
            return profile;

        }

    }
}
