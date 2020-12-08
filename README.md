# Refactoring Assessment

This repository contains a terribly written Web API project. It's terrible on purpose, so you can show us how we can improve it.

## Getting Started

Fork this repository, and make the changes you would want to see if you had to maintain this api. To set up the project:

 - Open in Visual Studio (2015 or later is preferred)
 - Restore the NuGet packages and rebuild
 - Run the project
 
 Once you are satisied, replace the contents of the readme with a summary of what you have changed, and why. If there are more things that could be improved, list them as well.

The api is composed of the following endpoints:

| Verb     | Path                                   | Description
|----------|----------------------------------------|--------------------------------------------------------
| `GET`    | `/api/Accounts`                        | Gets the list of all accounts
| `GET`    | `/api/Accounts/{id:guid}`              | Gets an account by the specified id
| `POST`   | `/api/Accounts`                        | Creates a new account
| `PUT`    | `/api/Accounts/{id:guid}`              | Updates an account
| `DELETE` | `/api/Accounts/{id:guid}`              | Deletes an account
| `GET`    | `/api/Accounts/{id:guid}/Transactions` | Gets the list of transactions for an account
| `POST`   | `/api/Accounts/{id:guid}/Transactions` | Adds a transaction to an account, and updates the amount of money in the account

Models should conform to the following formats:

**Account**
```
{
    "Id": "01234567-89ab-cdef-0123-456789abcdef",
	"Name": "Savings",
	"Number": "012345678901234",
	"Amount": 123.4
}
```	

**Transaction**
```
{
    "Date": "2018-09-01",
    "Amount": -12.3
}
```


## Details of change
- Move database operations to AccountRepository and TransactionRepository
- Use accountId to determine correct operation and clean up isNew variable from Account.cs
- Remove duplicate database connections on AccountRepository.GetAll operation and clean up Account class for unused members
- Update queries vulnerable to SQL injection and use SQL parameters
- Add exception handling and return correct HttpStatus for AccountController.Get
- Add FluentValidation on AccountController.Add and AccountController.Update

## Future improvements
- Apply exception handling to all API methods
- Add more data validation for Account and Transaction fields and apply to each request automatically using attribute
- Make API calls asyncronous
- Add API documentation (Swagger, eg.)
- Make use of IOC container to remove class dependency and inject dependecies to constructor (between repositories and controller)
- Write automated tests (unit and integration)
