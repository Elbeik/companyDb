﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3MVC.DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }
        
        public int? Age { get; set; }
       
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        [Range(4000,8000)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
       
        //[ForeignKey("Department")]

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public String ImageName { get; set; }
    }
}
