using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace refactor_this.Repository
{
    public class TransactionRepository
    {
        public List<Transaction> GetTransactions(Guid id)
        {
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command = new SqlCommand($"select Amount, Date from Transactions where AccountId = @id", connection);
                command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = id });

                connection.Open();
                var reader = command.ExecuteReader();
                var transactions = new List<Transaction>();

                while (reader.Read())
                {
                    var amount = (float)reader.GetDouble(0);
                    var date = reader.GetDateTime(1);
                    transactions.Add(new Transaction(amount, date));
                }

                return transactions;
            }
        }

        public void AddTransaction(Guid id, Transaction transaction)
        {
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command = new SqlCommand($"update Accounts set Amount = Amount + @amount where Id = @id", connection);
                command.Parameters.Add(new SqlParameter() { ParameterName = "@amount", Value = transaction.Amount });
                command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = id });

                connection.Open();

                if (command.ExecuteNonQuery() != 1)
                    throw new Exception("Could not update account amount");

                command = new SqlCommand($"INSERT INTO Transactions (Id, Amount, Date, AccountId) VALUES (@id, @amount, @date, @accountId)", connection);
                command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = Guid.NewGuid() });
                command.Parameters.Add(new SqlParameter() { ParameterName = "@amount", Value = transaction.Amount });
                command.Parameters.Add(new SqlParameter() { ParameterName = "@date", Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
                command.Parameters.Add(new SqlParameter() { ParameterName = "@accountId", Value = id });

                if (command.ExecuteNonQuery() != 1)
                    throw new Exception("Could not insert the transaction");
            }
        }
    }
}