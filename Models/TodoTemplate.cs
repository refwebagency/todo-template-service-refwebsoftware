using System.ComponentModel.DataAnnotations;

namespace TodoTemplateService.Models
{
    public class TodoTemplate
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Experience { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Time { get; set; }

        [Required]
        public int SpecializationId  { get; set; }

        public Specialization Specialization { get; set; }

        [Required]
        public int ProjectTypeId { get; set; }

        public ProjectType ProjectType { get; set; }

        
    }
}