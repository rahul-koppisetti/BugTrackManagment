using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BugTrackManagment.Models
{
    public class Bug
    {
        [Key]
        public int Id { get; set; }

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
        public StatusOptions Status { get; set; }

        
        [DisplayName("Project")]
        public string ProjectTitle { get; set; }

        public Project Project { get; set; }


        //Created User
        public string CreatedUserName { get; set; }
        public AppUser CreatedUser { get; set; }
        public DateTime CreatedTime { get; set; }

        //Assigned
        public string AssiginedUserName { get; set; }
        public AppUser AssigineddUser { get; set; }
        public DateTime AssiginedTime { get; set; }


        //Rejected  not solved
        public string RejectedUserName { get; set; }
        public AppUser RejectedUser { get; set; }
        public DateTime RejectedTime { get; set; }

        //Duplicate
        public string DuplicateUserName { get; set; }
        public AppUser DuplicateUser { get; set; }
        public DateTime DuplicateTime { get; set; }

        //solved   fixed
        public string SolvedUserName { get; set; }
        public AppUser SolvedUser { get; set; }
        public DateTime SolvedTime { get; set; }


        //verfied
        public string VerfiedUserName { get; set; }
        public AppUser VerfiedUser { get; set; }
        public DateTime VerfiedTime { get; set; }


        //closed
        public string ClosedUserName { get; set; }
        public AppUser ClosedUser { get; set; }
        public DateTime ClosedTime { get; set; }



 





    }
}

