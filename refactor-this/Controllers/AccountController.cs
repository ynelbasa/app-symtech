using refactor_this.Models;
using refactor_this.Repository;
using System;
using System.Web.Http;

namespace refactor_this.Controllers
{
    public class AccountController : ApiController
    {
        private readonly AccountRepository _accountRepository = new AccountRepository();

        [HttpGet, Route("api/Accounts/{id}")]
        public IHttpActionResult Get(Guid id)
        {
            var account = _accountRepository.Get(id);
            return Ok(account);
        }

        [HttpGet, Route("api/Accounts")]
        public IHttpActionResult GetAll()
        {
            var accounts = _accountRepository.GetAll();
            return Ok(accounts);
        } 

        [HttpPost, Route("api/Accounts")]
        public IHttpActionResult Add(Account account)
        {
            _accountRepository.Save(account);
            return Ok();
        }

        [HttpPut, Route("api/Accounts/{id}")]
        public IHttpActionResult Update(Guid id, Account account)
        {
            _accountRepository.Save(account, id);
            return Ok();
        }

        [HttpDelete, Route("api/Accounts/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            _accountRepository.Delete(id);
            return Ok();
        }
    }
}