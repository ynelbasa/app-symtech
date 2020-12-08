using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace refactor_this.Repository
{
    public class AccountRepository
    {
        public Account Get(Guid id)
        {
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command = new SqlCommand($"select * from Accounts where Id = @id", connection);
                command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = id });

                connection.Open();
                var reader = command.ExecuteReader();
                
                if (!reader.Read())
                    throw new ArgumentException();

                var account = new Account()
                {
                    Id = Guid.Parse(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    Number = reader["Number"].ToString(),
                    Amount = float.Parse(reader["Amount"].ToString())
                };

                return account;
            }
        }

        public IEnumerable<Account> GetAll()
        {
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command = new SqlCommand($"select * from Accounts", connection);
                connection.Open();

                var reader = command.ExecuteReader();
                var accounts = new List<Account>();

                while (reader.Read())
                {
                    var account = new Account()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Number = reader["Number"].ToString(),
                        Amount = float.Parse(reader["Amount"].ToString())
                    };

                    accounts.Add(account);
                }

                return accounts;
            }
        }

        public void Save(Account account, Guid? id = null)
        {
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command;
                if (id.HasValue)
                {
                    command = new SqlCommand($"update Accounts set Name = @name where Id = @id", connection);
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@name", Value = account.Name });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = id });
                }
                else 
                {
                    command = new SqlCommand($"insert into Accounts (Id, Name, Number, Amount) values (@id, @name, @number, @amount)", connection);
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = Guid.NewGuid() });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@name", Value = account.Name });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@number", Value = account.Number });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@amount", Value = 0 });
                }

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Guid id)
        {
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command = new SqlCommand($"delete from Accounts where Id = @id", connection);
                command.Parameters.Add(new SqlParameter() { ParameterName = "@id", Value = id });
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}