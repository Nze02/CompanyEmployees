using CompanyEmployees.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees.Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);

        Employee GetEmployee(Guid companyId, Guid employeeId, bool trackChanges);
    }
}
