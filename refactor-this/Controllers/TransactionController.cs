using refactor_this.Models;
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
        [HttpGet, Route("api/Accounts/{id}/Transactions")]
        public IHttpActionResult GetTransactions(Guid id)
        {
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command = new SqlCommand($"select Amount, Date from Transactions where AccountId = '{id}'", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                var transactions = new List<Transaction>();
                while (reader.Read())
                {
                    var amount = (float)reader.GetDouble(0);
                    var date = reader.GetDateTime(1);
                    transactions.Add(new Transaction(amount, date));
                }
                return Ok(transactions);
            }
        }

        [HttpPost, Route("api/Accounts/{id}/Transactions")]
        public IHttpActionResult AddTransaction(Guid id, Transaction transaction)
        {
            using (var connection = Helpers.NewConnection())
            {

                SqlCommand command = new SqlCommand($"update Accounts set Amount = Amount + {transaction.Amount} where Id = '{id}'", connection);
                connection.Open();
                if (command.ExecuteNonQuery() != 1)
                    return BadRequest("Could not update account amount");

                command = new SqlCommand($"INSERT INTO Transactions (Id, Amount, Date, AccountId) VALUES ('{Guid.NewGuid()}', {transaction.Amount}, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{id}')", connection);
                if (command.ExecuteNonQuery() != 1)
                    return BadRequest("Could not insert the transaction");

                return Ok();
            }
        }
    }
}