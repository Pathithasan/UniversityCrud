using System;
using System.ComponentModel.DataAnnotations;

namespace UniversityCrud.Shared.Model
{
    public class Course : BaseModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, ErrorMessage = "Title must be between 2 and 50 characters.", MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(70, ErrorMessage = "Title must be between 2 and 50 characters.", MinimumLength = 2)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        public bool IsActive { get; set; }
    }
}

