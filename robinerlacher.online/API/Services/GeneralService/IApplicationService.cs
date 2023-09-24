namespace API.Services.GeneralService
{
    public interface IApplicationService
    {
        Task<Application> GetApplicationByIdAsync(int id);
        Task<Application> GetApplicationByNameAsync(string name);
        Task<List<Application>> GetAllApplicationsAsync();
        Task<List<Application>> GetApplicationsForUserAsync(int userId);
        Task<Application> AddApplicationAsync(Application application);
        Task<Application> UpdateApplicationAsync(int id, Application newAppInfo);
        Task<List<Application>> DeleteApplicationAsync(int id);
    }
}
