using EntityModels;
using Microsoft.AspNetCore.Mvc;
using ServicesDb;

namespace BankAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeesController : ControllerBase
	{
		private readonly IEmployeeService employeeService;

		public EmployeesController(IEmployeeService employeeService)
		{
			this.employeeService = employeeService;
		}

		//GET: api/employees
		[HttpGet]
		public async Task<IEnumerable<Employee>> GetEmployee() => await employeeService.RetrieveAllAsync();

		//GET api/employees/[id]
		[HttpGet("{id:guid}", Name = nameof(GetEmployee))]
		[ProducesResponseType(200, Type = typeof(Employee))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetEmployee(Guid id)
		{
			Employee? c = await employeeService.RetrieveEmployeeAsync(id);
			if (c is null)
			{
				return NotFound();
			}
			return Ok(c);
		}

		//POST: api/employees
		//BODY: Employee (JSON)
		[HttpPost]
		[ProducesResponseType(201, Type = typeof(Employee))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Create([FromBody] Employee c)
		{
			if (c is null)
			{
				return BadRequest();
			}
			Employee? addedEmployee = await employeeService.AddEmployeeAsync(c);
			if (addedEmployee is null)
			{
				return BadRequest("Сервис не смог добавить сотрудника");
			}
			else
			{
				return CreatedAtRoute(routeName: nameof(GetEmployee),
					routeValues: new { id = addedEmployee.EmployeeId },
					value: addedEmployee);
			}
		}

		//PUT: api/employees/[id]
		//BODY: Employee (JSON)
		[HttpPut("{id:guid}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Update(Guid id, [FromBody] Employee c)
		{
			if (c is null || c.EmployeeId != id)
			{
				return BadRequest();
			}
			Employee? existed = await employeeService.RetrieveEmployeeAsync(id);
			if (existed is null)
			{
				return NotFound();
			}
			await employeeService.UpdateEmployeeAsync(id, c);
			return new NoContentResult();
		}

		//DELETE: api/employees/[id]
		[HttpDelete("{id:guid}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Delete(Guid id)
		{
			Employee? existed = await employeeService.RetrieveEmployeeAsync(id);
			if (existed is null)
			{
				return NotFound();
			}
			bool? deleted = await employeeService.DeleteEmployeeAsync(id);
			if (deleted.HasValue && deleted.Value)
			{
				return new NoContentResult();
			}
			else
			{
				return BadRequest($"При удалении сотрудника {id} произошла ошибка");
			}
		}
	}
}
