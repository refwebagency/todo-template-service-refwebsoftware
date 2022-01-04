using TodoTemplateService.Data;
using TodoTemplateService.Dtos;
using TodoTemplateService.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TodoTemplateService.Controllers
{
    [ApiController]
    // Nous definissons la route du controller.
    [Route("[controller]")]
    public class TodoTemplateController : ControllerBase
    {
        // Ici on Type avec la propriété readonly afin de recuperer les methode du Repo et du IMapper 
        private readonly ITodoTemplateRepo _repository;
        private readonly IMapper _mapper;

        public TodoTemplateController(ITodoTemplateRepo repository, IMapper mapper)
        {

            _repository = repository;
            _mapper = mapper;

        }

        // Ici nous requettons une liste de taches en passant par le DTO qui nous sert de schema pour lire les taches.
        [HttpGet]
        public ActionResult<IEnumerable<TodoTemplateReadDto>> GetAllTodoTemplates()
        {
            // On initialise une variable qui recupere toute les taches par la methode GetAllTodoes (typage) en passant par le repo.
            var todoTemplateItem = _repository.GetAllTodoTemplates();
            // On retourne un status 200 avec la liste des taches par l'AutoMapper.
            return Ok(_mapper.Map<IEnumerable<TodoTemplateReadDto>>(todoTemplateItem));

        }   


        // Ici on Get une tache par l'ID.
        [HttpGet("id", Name = "GetTodoTemplateById")]
        public ActionResult<TodoTemplateReadDto> GetTodoTemplateById(int id)
        {
            // Initialisation d'une variable qui recupere depuis le repo la methode GetTaskById
            var todoTemplateItem = _repository.GetTodoTemplateById(id);
            // Je lui donne une condition que si la tache par Id n'est pas null alors tu retournes un status 200 avec la tache
            // en question grace a l'autoMapper.
            if(todoTemplateItem != null){
                return Ok(_mapper.Map<TodoTemplateReadDto>(todoTemplateItem));
            }else{
                return NotFound();
            }

        }

        // Ici on requete avec le methode Post ( HttpPost ) pour envoyer les données afin de créé une nouvelle tache
        // En passant par schema du Dto
        [HttpPost]
        public ActionResult<TodoTemplateReadDto> CreateTodoTemplate(TodoTemplateCreateDto todoTemplateCreateDto){

            // On initialise une variable ou l'on stock le model de creation de la tache
            var todoTemplateModel = _mapper.Map<TodoTemplate>(todoTemplateCreateDto);
            // Ici on recupere la méthode du repo CreateTodo.
            _repository.CreateTodoTemplate(todoTemplateModel);
            // On recupere la methode SaveChanges du repo.
            _repository.SaveChanges();
            // On stock dans une variable le schema pour lire la nouvelle tache enregistré précedemment.
            var TodoTemplateReadDto = _mapper.Map<TodoTemplateReadDto>(todoTemplateModel);
            // La CreatedAtRoute méthode est destinée à renvoyer un URI à la ressource nouvellement créée lorsque vous appelez une méthode POST pour stocker un nouvel objet.
            return CreatedAtRoute(nameof(GetTodoTemplateById), new { id = TodoTemplateReadDto.Id }, TodoTemplateReadDto); 

        }
        // Ici je requete avec la methode Put avec en parametre la route 'update/id'
       [HttpPut("updapte/id", Name = "UpdateTodoTemplate")]
        public ActionResult<TodoTemplateReadDto> UpdateTodoTemplateById(int id, TodoTemplateUpdateDto todoTemplateUpdateDto)
        {
            // On initalise une variage qui recupere depuis le repo la methode GetTodoTemplateById
            var todoTemplateModelFromRepo = _repository.GetTodoTemplateById(id);
            // On lui donne la route a suivre pour l'update qui passera par le dto de l'update et passera par la methode GetTodoById du repo
            _mapper.Map(todoTemplateUpdateDto, todoTemplateModelFromRepo);

            // retourne une erreur si null
            if (todoTemplateModelFromRepo == null )
            {
                return NotFound();
            }
            // On recupere la methode UpdateTodo du repo
            _repository.UpdateTodoTemplateById(id);
            // On recupere la methode SaveChanges du repo
            _repository.SaveChanges();
            // La CreatedAtRoute méthode est destinée à renvoyer un URI à la ressource nouvellement créée lorsque vous appelez une méthode POST pour stocker un nouvel objet.
            return CreatedAtRoute(nameof(GetTodoTemplateById), new { id = todoTemplateUpdateDto.Id }, todoTemplateUpdateDto);
            
        }


        // On requete avec le methode Delete avec en paramatre l'Id
        [HttpDelete("{id}")]
        public ActionResult DeleteTodoTemplate(int id)
        {
            // On stock dans taskItem la tache a delete par Id avec la methode du repo GetTodoById.
            var todoTemplateItem = _repository.GetTodoTemplateById(id);
            
            if(todoTemplateItem != null)
            {
                _repository.DeleteTodoTemplateById(todoTemplateItem.Id);
                _repository.SaveChanges();
                return Ok();


            }else{
                return NotFound();
            }

        }


    }
}