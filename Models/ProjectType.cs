using System.ComponentModel.DataAnnotations;

namespace TodoTemplateService.Models
{
    public class ProjectType
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}