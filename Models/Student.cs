//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Student
    {
        public int id { get; set; }
        [Required (ErrorMessage ="Require Name ")]
        public string name { get; set; }
        [Required(ErrorMessage ="Require Password ")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required(ErrorMessage = "Require Enrollment ")]
        public string enrollment { get; set; }
        [Required(ErrorMessage = "Require Email ")]
        public string email { get; set; }
        [Required(ErrorMessage = "Require Password ")]
        public string contact { get; set; }
        [Required(ErrorMessage = "Require Password ")]
        public string address { get; set; }
    }
}
