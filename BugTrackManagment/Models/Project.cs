using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackManagment.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(36, ErrorMessage = "Title can NOT be longer than 36 characters")]
        public string Title { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Summary can NOT be longer than 50 characters")]
        public string Summary { get; set; }


    }
}
