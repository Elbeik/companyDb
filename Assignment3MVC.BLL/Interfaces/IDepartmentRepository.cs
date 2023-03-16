using Assignment3MVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3MVC.BLL.Interfaces
{
    public interface IDepartmentRepository:IGenaricRepository<Department>
    {

        IQueryable<Department> searchDepartmentByName(string name);

        //IEnumerable<Department> GetAll();
        //Department Get(int id);
        //int Add(Department department);
        //int Update(Department department);
        //int Delete(Department department);
    }
}
