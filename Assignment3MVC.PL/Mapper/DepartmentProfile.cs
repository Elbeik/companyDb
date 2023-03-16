using Assignment3MVC.DAL.Entities;
using Assignment3MVC.PL.ViewModels;
using AutoMapper;

namespace Assignment3MVC.PL.Mapper
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
