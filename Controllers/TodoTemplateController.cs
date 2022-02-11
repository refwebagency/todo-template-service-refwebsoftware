using TodoTemplateService.Data;
using TodoTemplateService.Dtos;
using TodoTemplateService.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

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
        private readonly HttpClient _HttpClient;
        private readonly IConfiguration _configuration;

        public TodoTemplateController(ITodoTemplateRepo repository, IMapper mapper, HttpClient HttpClient, IConfiguration configuration)
        {

            _repository = repository;
            _mapper = mapper;
            _HttpClient = HttpClient;
            _configuration = configuration;
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
        [HttpGet("{id}", Name = "GetTodoTemplateById")]
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

        // Ici on Get une tache par l'ID.
        [HttpGet("projectType/id", Name = "GetTodoTemplateByProjectType")]
        public ActionResult<TodoTemplateReadDto> GetTodoTemplateByProjectType(int id)
        {
            // Initialisation d'une variable qui recupere depuis le repo la methode GetTaskById
            var todoTemplateItem = _repository.GetTodoTemplateByProjectType(id);
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
        public async Task<ActionResult<TodoTemplateReadDto>> CreateTodoTemplate(TodoTemplateCreateDto todoTemplateCreateDto){

            var todoTemplateModel = _mapper.Map<TodoTemplate>(todoTemplateCreateDto);

            var getSpecialization = await _HttpClient.GetAsync($"{_configuration["SpecializationService"]}" + todoTemplateModel.SpecializationId);
            var getProjectType = await _HttpClient.GetAsync($"{_configuration["ProjectTypeService"]}" + todoTemplateModel.ProjectTypeId);

            var deserializeSpecialization = JsonConvert.DeserializeObject<CreateSpecializationDTO>(
                    await getSpecialization.Content.ReadAsStringAsync());

            var deserializeProjectType = JsonConvert.DeserializeObject<ProjectTypeCreateDto>(
                    await getProjectType.Content.ReadAsStringAsync());       

            var SpecializationDTO = _mapper.Map<Specialization>(deserializeSpecialization);
            var ProjectTypetDTO = _mapper.Map<ProjectType>(deserializeProjectType);

            var specialization = _repository.GetSpecializationById(SpecializationDTO.Id);
            var projectType = _repository.GetProjectTypeById(ProjectTypetDTO.Id);

            if (specialization == null) todoTemplateModel.Specialization = SpecializationDTO; else todoTemplateModel.Specialization = specialization;

            if (projectType == null) todoTemplateModel.ProjectType = ProjectTypetDTO; else todoTemplateModel.ProjectType = projectType;

            _repository.CreateTodoTemplate(todoTemplateModel);

            _repository.SaveChanges();

            var TodoTemplateReadDto = _mapper.Map<TodoTemplateReadDto>(todoTemplateModel);

            return CreatedAtRoute(nameof(GetTodoTemplateById), new { id = TodoTemplateReadDto.Id }, TodoTemplateReadDto); 

        }
        // Ici je requete avec la methode Put avec en parametre la route 'update/id'
       [HttpPut("updapte/{id}", Name = "UpdateTodoTemplate")]
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