using Assignment3MVC.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3MVC.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDepartmentRepository _departmentRepository { get; set; }
        public IEmployeeRepository _employeeRepository { get; set; }
        
        public UnitOfWork(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }
    }
}
