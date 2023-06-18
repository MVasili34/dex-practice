using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModels;

public interface IPerson
{
	string FirstName { get; set; }
	string LastName { get; set; }
	DateOnly DateOfBirth { get; set; }
	string? Phone { get; set; }
	string Passport { get; set; }
}
