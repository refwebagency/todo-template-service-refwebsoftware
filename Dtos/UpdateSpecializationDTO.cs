using System.ComponentModel.DataAnnotations;

namespace TodoTemplateService.Dtos
{
    public class UpdateSpecializationDTO
    {   
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}