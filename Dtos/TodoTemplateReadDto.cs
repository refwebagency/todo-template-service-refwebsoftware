namespace TodoTemplateService.Dtos
{
    public class TodoTemplateReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Experience { get; set; }

        public string Description { get; set; }

        public int Time { get; set; }

        public int SpecId { get; set; }

        public int ProjectTypeId { get; set;}
    }
}