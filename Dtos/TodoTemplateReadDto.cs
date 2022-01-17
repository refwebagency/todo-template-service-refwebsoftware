using TodoTemplateService.Models;
namespace TodoTemplateService.Dtos
{
    public class TodoTemplateReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Experience { get; set; }

        public string Description { get; set; }

        public int Time { get; set; }

        public int SpecializationId  { get; set; }

        public Specialization Specialization { get; set; }

        public int ProjectTypeId { get; set; }

        public ProjectType ProjectType { get; set; }
    }
}