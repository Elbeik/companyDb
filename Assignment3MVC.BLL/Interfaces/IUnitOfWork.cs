using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3MVC.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IDepartmentRepository _departmentRepository { get; set; }
        public IEmployeeRepository _employeeRepository { get; set; }

    }
}
