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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly ProjectMVCDbContext _dbContext;

        public DepartmentRepository(ProjectMVCDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Department> searchDepartmentByName(string name)
         => _dbContext.Departments.Where(D => D.Name == name);




        //private readonly ProjectMVCDbContext _ProjectMVCDbContext;

        //public DepartmentRepository(ProjectMVCDbContext ProjectMVCDbContext) //dependency injection
        //{
        //    //_ProjectMVCDbContext = new ProjectMVCDbContext(); 
        //    _ProjectMVCDbContext = ProjectMVCDbContext; //ProjectMVCDbContext refers to object creation by CLR
        //}
        //public int Add(Department department)
        //{

        //    _ProjectMVCDbContext.Departments.Add(department);
        //    return _ProjectMVCDbContext.SaveChanges();

        //}

        //public int Delete(Department department)
        //{
        //    _ProjectMVCDbContext.Departments.Remove(department);
        //    return _ProjectMVCDbContext.SaveChanges();
        //}

        //public Department Get(int id)
        //{
        //   return _ProjectMVCDbContext.Departments.Where(D => D.Id==id).FirstOrDefault();
        //}

        //public IEnumerable<Department> GetAll()
        //    => _ProjectMVCDbContext.Departments.ToList();

        //public int Update(Department department)
        //{
        //    _ProjectMVCDbContext.Departments.Update(department);
        //    return _ProjectMVCDbContext.SaveChanges();
        //}
    }
}
