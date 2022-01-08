using System.Collections.Generic;
using TodoTemplateService.Models;

namespace TodoTemplateService.Data
{
    public interface ITodoTemplateRepo
    {
         bool SaveChanges();

         IEnumerable<TodoTemplate> GetAllTodoTemplates();

         TodoTemplate GetTodoTemplateById(int id);

         IEnumerable<TodoTemplate> GetTodoTemplateByProjectTypeId(int id);

         IEnumerable<TodoTemplate> GetTodoTemplateBySpecId(int id);

         void CreateTodoTemplate(TodoTemplate todoTemplate);

         void UpdateTodoTemplateById(int id);


         void DeleteTodoTemplateById(int id);
    }
}