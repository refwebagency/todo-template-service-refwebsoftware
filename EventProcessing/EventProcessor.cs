using System;
using System.Text.Json;
using AutoMapper;
using TodoTemplateService.Data;
using TodoTemplateService.Dtos;
using TodoTemplateService.Models;
using TodoTemplateService.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace TodoTemplateService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch(eventType)
            {
                // On souscrit à la méthode UpdateClient() si la valeur retournée est bien EventType
                case EventType.SpecializationUpdated:
                    UpdateSpecialization(message);
                    break;
                case EventType.ProjectTypeUpdated:
                    UpdateProjectType(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");

            // On déserialise les données pour retourner un objet (texte vers objet json)
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
            Console.WriteLine($"--> Event Type: {eventType.Event}");

            switch(eventType.Event)
            {
                /* "Client_Updated" est la valeur attribuée dans le controller de ClientService
                lors de l'envoi de données 
                Dans le cas ou la valeur de notre attribue Event est bien "Client_Updated",
                nous retournons notre objet */
                case "Specialization_Updated":
                    Console.WriteLine("--> Platform Updated Event Detected");
                    return EventType.SpecializationUpdated;
                case "ProjectType_Updated":
                    Console.WriteLine("--> Platform Updated Event Detected");
                    return EventType.ProjectTypeUpdated;
                // Sinon nous retournons que l'objet est indeterminé
                default:
                    Console.WriteLine("-> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void UpdateProjectType(string projectTypeDto)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ITodoTemplateRepo>();

                var projectTypeUpdateDto = JsonSerializer.Deserialize<ProjectTypeUpdateDto>(projectTypeDto);
                Console.WriteLine($"--> ProjectType Updated: {projectTypeUpdateDto}");

                try
                {

                    var projectTypeRepo = repo.GetProjectTypeById(projectTypeUpdateDto.Id);
                    _mapper.Map(projectTypeUpdateDto, projectTypeRepo);
                    
                    // SI le client existe bien on l'update sinon rien
                    if(projectTypeRepo != null)
                    {
                        //Console.WriteLine(clientRepo.Name);
                        repo.UpdateProjectTypeById(projectTypeRepo.Id);
                        //Console.WriteLine(clientRepo.Name);
                        repo.SaveChanges();
                        Console.WriteLine("--> ProjectType mis à jour");
                    }
                    else{
                        Console.WriteLine("--> ProjectType non existant");
                    }
                }
                // Si une erreur survient, on affiche un message
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not update ProjectType to DB {ex.Message}");
                }
            }
        }

        private void UpdateSpecialization(string specializationUpdated)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ITodoTemplateRepo>();

                var updateSpecializationAsyncDTO = JsonSerializer.Deserialize<UpdateSpecializationAsyncDTO>(specializationUpdated);
                Console.WriteLine($"--> Client Updated : {updateSpecializationAsyncDTO}");

                try
                {
                    var specializationRepo = repo.GetSpecializationById(updateSpecializationAsyncDTO.Id);
                    _mapper.Map(updateSpecializationAsyncDTO, specializationRepo);

                    if(specializationRepo != null)
                    {
                        repo.UpdateSpecializationById(specializationRepo.Id);
                        repo.SaveChanges();
                        Console.WriteLine("--> Specialization mis à jour");
                    }
                    else
                    {
                        Console.WriteLine("--> Specialization non existant");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not update Specialization to DB {ex.Message}");
                }
            }
        }
    }

    //Type d'event
    enum EventType
    {
        SpecializationUpdated,
        ProjectTypeUpdated,
        Undetermined
    }
}