using System.ComponentModel.DataAnnotations;

namespace TodoTemplateService.Dtos
{
    public class ProjectTypeCreateDto
    {
        [Key]
        [Required]
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }
    }
}