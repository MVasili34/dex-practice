using Microsoft.AspNetCore.Mvc;
using EntityModels;
using ServicesDb;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BankAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClientsController : ControllerBase
	{
		private readonly IClientService clientService;
		
		public ClientsController(IClientService clientService) 
		{
			this.clientService = clientService;
		}

		//GET: api/clients
		[HttpGet]
		public async Task<IEnumerable<Client>> GetClients() => await clientService.RetrieveAllAsync();

		//GET api/clients/[id]
		[HttpGet("{id:guid}", Name = nameof(GetClient))]
		[ProducesResponseType(200, Type = typeof(Client))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetClient(Guid id)
		{
			Client? c = await clientService.RetrieveClientAsync(id);
			if (c is null)
			{
				return NotFound();
			}
				return Ok(c);
		}

		//POST: api/clients
		//BODY: Client (JSON)
		[HttpPost]
		[ProducesResponseType(201, Type = typeof(Client))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Create([FromBody] Client c)
		{
			if (c is null) 
			{
				return BadRequest();
			}
			Client? addedClient = await clientService.AddClientAsync(c);
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
		public async Task<IActionResult> Update(Guid id, [FromBody] Client c)
		{
			if (c is null || c.ClientId != id) 
			{
				return BadRequest();
			}
			Client? existed = await clientService.RetrieveClientAsync(id);
			if (existed is null)
			{
				return NotFound();
			}
			await clientService.UpdateClientAsync(id, c);
			return new NoContentResult();
		}

		//DELETE: api/clients/[id]
		[HttpDelete("{id:guid}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Delete(Guid id)
		{
			Client? existed = await clientService.RetrieveClientAsync(id);
			if (existed is null) 
			{ 
				return NotFound();
			}
			bool? deleted = await clientService.DeleteClientAsync(id);
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
            bool? deleted = await clientService.DeleteAccountAsync(id);
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
