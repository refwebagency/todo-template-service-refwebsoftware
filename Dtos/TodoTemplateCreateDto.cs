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

        [Required]
        public int SpecId { get; set; }

        [Required]
        public int ProjectTypeId { get; set;}
    }
}