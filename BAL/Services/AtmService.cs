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
        public decimal withdrawOrDeposit(WithdrawOrDepositRequest request) // TODO: make it a transaction and add expiry date validation
        {
            GetCardByNumberRequest getCard =new GetCardByNumberRequest { CardNumber = request.cardNumber };
            Card? card = _cardService.GetCardByNumber(getCard);
            if (card == null )
            {
                return -1;
                throw new Exception("Could not conplete the transaction card not found ");
                
            }
            GetAccountByIdRequest getAccountByIdRequest = new GetAccountByIdRequest { Id = card.AccountId };
            Account? account = _accountService.GetAccountById(getAccountByIdRequest);
            decimal newBalance = account.Balance +  request.amount;
            if (newBalance < 0 || !card.IsActive )
            {
                return -1;
            }
            UpdateAccountRequest updateAccountRequest = new UpdateAccountRequest
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                UsertId = account.UserId,
                IsActive = account.IsActive,
                Balance = newBalance,
            };
            _accountService.UpdateAccount(updateAccountRequest);
            return newBalance;

            
        }
        public decimal moneyTransfer(MoneyTransferRequest request)
        {
            GetCardByNumberRequest getCard = new GetCardByNumberRequest { CardNumber = request.CardNumber };
            Card? card = _cardService.GetCardByNumber(getCard);
            if (card == null)
            {
                return -1;
                throw new Exception("Could not conplete the transaction card not found ");
            }
            GetAccountByIdRequest getSenderRequest = new GetAccountByIdRequest { Id = card.AccountId };
            GetAccountByAccountNumberRequest getReceiverRequest = new GetAccountByAccountNumberRequest { accountNumber = request.AccountNumber };
            Account? senderAccount = _accountService.GetAccountById(getSenderRequest);
            Account? receiverAccount = _accountService.GetAccountByNumber(getReceiverRequest);
            if (receiverAccount == null || senderAccount==null)
            {
                return -2;
                throw new Exception("No account found with this number");
            }
            decimal newSenderBalance = senderAccount.Balance - request.ammount;
            UpdateAccountRequest updateSenderAccountRequest = new UpdateAccountRequest
            {
                Id = senderAccount.Id,
                AccountNumber = senderAccount.AccountNumber,
                UsertId = senderAccount.UserId,
                IsActive = senderAccount.IsActive,
                Balance = newSenderBalance,
            };
            _accountService.UpdateAccount(updateSenderAccountRequest);
            decimal newReceiverBalance = receiverAccount.Balance + request.ammount;
            UpdateAccountRequest updateReceiverAccountRequest = new UpdateAccountRequest
            {
                Id = receiverAccount.Id,
                AccountNumber = receiverAccount.AccountNumber,
                UsertId = receiverAccount.UserId,
                IsActive = receiverAccount.IsActive,
                Balance = newReceiverBalance,
            };
            _accountService.UpdateAccount(updateReceiverAccountRequest);


            return newSenderBalance;

        }

    }
}
