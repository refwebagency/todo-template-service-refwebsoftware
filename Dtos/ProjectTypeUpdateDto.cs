using System.ComponentModel.DataAnnotations;

namespace TodoTemplateService.Dtos
{
    public class ProjectTypeUpdateDto
    {
         [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}