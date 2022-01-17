using System.Linq;
using System;
using System.Collections.Generic;
using TodoTemplateService.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoTemplateService.Data
{
    public class TodoTemplateRepo : ITodoTemplateRepo
    {
        private readonly AppDbContext _context;

        public TodoTemplateRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateTodoTemplate(TodoTemplate todoTemplate)
        {
            /* Si la tache n'est pas null alors ajout d'une tache dans le contexte
               de la bdd*/
            if(todoTemplate != null)
            {

                _context.todoTemplate.Add(todoTemplate);
                _context.SaveChanges();

            }else{
                throw new ArgumentException(nameof(todoTemplate));
            }
        }

        public IEnumerable<TodoTemplate> GetAllTodoTemplates()
        {
            _context.Specialization.ToList();
            _context.ProjectType.ToList();
            return _context.todoTemplate.ToList();

        }

        public TodoTemplate GetTodoTemplateById(int id)
        {
            _context.Specialization.ToList();
            _context.ProjectType.ToList();
            return _context.todoTemplate.FirstOrDefault(t => t.Id == id);
        }

        public TodoTemplate GetTodoTemplateByProjectType(int id)
        {
            _context.Specialization.ToList();
            _context.ProjectType.ToList();
            return _context.todoTemplate.FirstOrDefault(t => t.ProjectTypeId == id);
        }

        public Specialization GetSpecializationById(int id)
        {
            return _context.Specialization.FirstOrDefault(s => s.Id == id);
        }

        public ProjectType GetProjectTypeById(int id)
        {
            return _context.ProjectType.FirstOrDefault(p => p.Id == id);
        }

        public void UpdateTodoTemplateById(int id)
        {

                var todoTemplateItem = _context.todoTemplate.Find(id);

                // On indique au contexte d’attacher l’entité et de définir son état sur modifié ( et une fois cela fait on va pouvoir invoqué la methode SaveChanges)
                _context.Entry(todoTemplateItem).State = EntityState.Modified;

        }

        public void DeleteTodoTemplateById(int id)
        {
            // La méthode Find() recherche l'élément correspondant au paramètre spécifié.
            // Et on le retourne.
            var todoTemplateItem = _context.todoTemplate.Find(id);

            // On vérifie que l'élément ne soit pas nul.
            if (todoTemplateItem != null)
            {
                // On supprime avec la méthode Remove().
                _context.todoTemplate.Remove(todoTemplateItem);       
            }

        }

        /**
        * Pour sauvegarder les changements si dans le context
        * les changements sont >= à 0
        */
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >=0 );
        }
    }
}