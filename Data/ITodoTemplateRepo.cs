using System.Collections.Generic;
using TodoTemplateService.Models;

namespace TodoTemplateService.Data
{
    public interface ITodoTemplateRepo
    {
        bool SaveChanges();

        IEnumerable<TodoTemplate> GetAllTodoTemplates();

        TodoTemplate GetTodoTemplateById(int id);

        Specialization GetSpecializationById(int id);

        ProjectType GetProjectTypeById(int id);

        TodoTemplate GetTodoTemplateByProjectType(int id);

        void CreateTodoTemplate(TodoTemplate todoTemplate);

        void UpdateTodoTemplateById(int id);

        void DeleteTodoTemplateById(int id);
    }
}