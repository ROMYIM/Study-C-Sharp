using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using QueryService;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientStore _clientStore;

        public ClientController(ClientIdentityInfoQuery clientStore)
        {
            _clientStore = clientStore;
        }

        [HttpGet]
        public IEnumerable<Client> Clients()
        {
            var clientInfoQuery = _clientStore as ClientIdentityInfoQuery;
            return clientInfoQuery.Clients;
        }
    }
}