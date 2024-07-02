using Azure.Core;
using BAL.IServices;
using Bank.Models;
using Common.Requests;
using DAL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BAL.Services
{
    public class CardService : ICardService
    {
        private readonly DapperAccess _dapperAccess;

        public CardService(DapperAccess dapperAccess)
        {
            _dapperAccess = dapperAccess;
        }
        public Card? GetCardById(GetCardByIdRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__Id", request.Id);
            return _dapperAccess.Query<Card>("usp_GetCardById", parameters).FirstOrDefault();
        }
        public List<Card> GetCardByAccountId(GetAccountByIdRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__AccountId", request.Id);
            return _dapperAccess.Query<Card>("usp_GetCardByAccountId", parameters).ToList();
        }
        public int AddCard(AddCardRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__AccountId", request.AccountId);
            parameters.Add("P__EmbossedName", request.EmbossedName);
            return _dapperAccess.Execute("usp_AddCard", parameters);
        }
        public int UpdateCard(UpdateCardRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__CardId",request.CardId);
            parameters.Add("P__AccountId",request.AccountId);
            parameters.Add("P__CardNumber",request.CardNumber);
            parameters.Add("P__EmbossedName", request.EmbossedName);
            parameters.Add("P__ExpiryDate",request.ExpiryDate);
            parameters.Add("P__IsActive",request.IsActive);
            parameters.Add("P__CSV",request.CSV);
            return _dapperAccess.Execute("usp_UpdateCard",parameters);

        }
        public int ActivateCard(int cardId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__CardId", cardId);
            return _dapperAccess.Execute("usp_ActivateCard", parameters);
            
        }

        public int DeactivateCard(int cardId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__CardId", cardId);
            return _dapperAccess.Execute("usp_DeactivateCard", parameters);
        }
    }
}
