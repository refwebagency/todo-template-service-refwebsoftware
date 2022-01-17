using System.ComponentModel.DataAnnotations;

namespace TodoTemplateService.Dtos
{
    public class ProjectTypeReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}