using Assignment3MVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3MVC.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenaricRepository<Employee>
    {

        IQueryable<Employee> GetNameOfEmplyees(string name);


        //IEnumerable<Employee> GetAll();
        //Employee Get(int id);
        //int Add(Employee employee);
        //int Update(Employee employee);
        //int Delete(Employee employee);

    }


}
