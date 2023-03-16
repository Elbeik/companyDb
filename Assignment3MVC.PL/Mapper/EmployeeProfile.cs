using Assignment3MVC.DAL.Entities;
using Assignment3MVC.PL.ViewModels;
using AutoMapper;

namespace Assignment3MVC.PL.Mapper
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
