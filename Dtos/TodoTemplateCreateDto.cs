using System.ComponentModel.DataAnnotations;

namespace TodoTemplateService.Dtos
{
    public class TodoTemplateCreateDto
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public int Experience { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Time { get; set; }

        // public Specialization

        // public Template
    }
}