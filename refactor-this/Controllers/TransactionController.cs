using refactor_this.Models;
using refactor_this.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace refactor_this.Controllers
{
    public class TransactionController : ApiController
    {
        private readonly TransactionRepository _transactionRepository = new TransactionRepository();

        [HttpGet, Route("api/Accounts/{id}/Transactions")]
        public IHttpActionResult GetTransactions(Guid id)
        {
            var transactions = _transactionRepository.GetTransactions(id);
            return Ok(transactions);    
        }

        [HttpPost, Route("api/Accounts/{id}/Transactions")]
        public IHttpActionResult AddTransaction(Guid id, Transaction transaction)
        {
            _transactionRepository.AddTransaction(id, transaction);
            return Ok();
        }
    }
}