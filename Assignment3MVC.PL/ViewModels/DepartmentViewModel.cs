using Assignment3MVC.DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Assignment3MVC.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is required!!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is required!!")]
        [MaxLength(50, ErrorMessage = "Max Length Name is 50 chars")]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

    
    }
}
