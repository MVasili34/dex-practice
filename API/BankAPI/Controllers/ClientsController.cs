using Microsoft.AspNetCore.Mvc;
using EntityModels;
using ServicesDb;

namespace BankAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClientsController : ControllerBase
	{
		private readonly IClientService _clientService;
		
		public ClientsController(IClientService _clientService) 
		{
			this._clientService = _clientService;
		}

		//GET: api/clients
		//QUERY: Page (INT)
		[HttpGet]
		public async Task<IEnumerable<Client>> GetClients([FromQuery] int? page) => await _clientService
			.RetrieveAllAsync(page);

		//GET api/clients/[id]
		[HttpGet("{id:guid}", Name = nameof(GetClient))]
		[ProducesResponseType(200, Type = typeof(Client))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetClient(Guid id)
		{
			Client? client = await _clientService.RetrieveClientAsync(id);
			if (client is null)
			{
				return NotFound();
			}
				return Ok(client);
		}

		//POST: api/clients
		//BODY: Client (JSON)
		[HttpPost]
		[ProducesResponseType(201, Type = typeof(Client))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> CreateClient([FromBody] Client client)
		{
			if (client is null) 
			{
				return BadRequest();
			}
			Client? addedClient = await _clientService.AddClientAsync(client);
			if (addedClient is null)
			{
				return BadRequest("Сервис не смог добавить клиента");
			}
			else
			{
				return CreatedAtRoute(routeName: nameof(GetClient), 
					routeValues: new {id = addedClient.ClientId}, 
					value: addedClient);
			}
		}

        //PUT: api/clients/[id]
        //BODY: Client (JSON)
        [HttpPut("{id:guid}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> UpdateClient(Guid id, [FromBody] Client client)
		{
			if (client is null || client.ClientId != id) 
			{
				return BadRequest();
			}
			Client? existed = await _clientService.RetrieveClientAsync(id);
			if (existed is null)
			{
				return NotFound();
			}
			await _clientService.UpdateClientAsync(id, client);
			return new NoContentResult();
		}

		//DELETE: api/clients/[id]
		[HttpDelete("{id:guid}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> DeleteClient(Guid id)
		{
			Client? existed = await _clientService.RetrieveClientAsync(id);
			if (existed is null) 
			{ 
				return NotFound();
			}
			bool? deleted = await _clientService.DeleteClientAsync(id);
			if (deleted.HasValue && deleted.Value) 
			{
				return new NoContentResult();
			}
			else
			{
				return BadRequest($"При удалении клиента {id} произошла ошибка");
			}
		}

        //DELETE: api/clients/accounts/[id]
        [HttpDelete("accounts/{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            bool? deleted = await _clientService.DeleteAccountAsync(id);
            if (deleted.HasValue && deleted.Value)
            {
                return new NoContentResult();
            }
            else
            {
                return BadRequest($"При удалении аккаунта {id} произошла ошибка");
            }
        }
    }
}
