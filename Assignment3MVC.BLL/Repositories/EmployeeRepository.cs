using Assignment3MVC.BLL.Interfaces;
using Assignment3MVC.DAL.Contexts;
using Assignment3MVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3MVC.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly ProjectMVCDbContext _dbContext;

        public EmployeeRepository(ProjectMVCDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Employee> GetNameOfEmplyees(string name)
                  => _dbContext.Employees.Where(e => e.Name.Contains(name));


        //public IQueryable<Employee> GetEmployeesByDepartmentName(string departmentName)
        //{
        //    throw new NotImplementedException();
        //}
        //private readonly ProjectMVCDbContext _ProjectMVCDbContext;

        //public EmployeeRepository(ProjectMVCDbContext ProjectMVCDbContext) //dependency injection
        //{
        //    //_ProjectMVCDbContext = new ProjectMVCDbContext(); 
        //    _ProjectMVCDbContext = ProjectMVCDbContext; //ProjectMVCDbContext refers to object creation by CLR
        //}
        //public int Add(Employee employee)
        //{

        //    _ProjectMVCDbContext.Employees.Add(employee);
        //    return _ProjectMVCDbContext.SaveChanges();

        //}

        //public int Delete(Employee employee)
        //{
        //    _ProjectMVCDbContext.Employees.Remove(employee);
        //    return _ProjectMVCDbContext.SaveChanges();
        //}

        //public Employee Get(int id)
        //{
        //    return _ProjectMVCDbContext.Employees.Where(D => D.Id == id).FirstOrDefault();
        //}

        //public IEnumerable<Employee> GetAll()
        //    => _ProjectMVCDbContext.Employees.ToList();

        //public int Update(Employee employee)
        //{
        //    _ProjectMVCDbContext.Employees.Update(employee);
        //    return _ProjectMVCDbContext.SaveChanges();
        //}
    }
}
