using BugTrackManagment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackManagment.ViewModels
{
    public class CreateBugViewModel
    {
        
    

        [Required]
        [MaxLength(36, ErrorMessage = "Title can NOT be longer than 36 characters")]
        public string Title { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Summary can NOT be longer than 50 characters")]
        public string Summary { get; set; }

        [Required]
        [MaxLength(256, ErrorMessage = "Description can NOT be longer than 256 characters")]
        public string Description { get; set; }

        [Required]
        public int? Project { get; set; }

        
        public SelectList ProjectList { get; set;}

       














    }



}
