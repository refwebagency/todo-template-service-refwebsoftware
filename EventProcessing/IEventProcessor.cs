namespace TodoTemplateService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}