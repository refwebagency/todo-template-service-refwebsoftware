using System.ComponentModel.DataAnnotations;

namespace TodoTemplateService.Models
{
    public class Specialization
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}