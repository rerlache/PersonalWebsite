using API.Data;

namespace API.Services.GeneralService
{
    public class ApplicationService : IApplicationService
    {
        private readonly GeneralContext _context;

        public ApplicationService(GeneralContext context)
        {
            _context = context;
        }
        public async Task<List<Application>> GetAllApplicationsAsync()
        {
            return await _context.Applications.ToListAsync();
        }

        public async Task<Application> GetApplicationByIdAsync(int id)
        {
            return await _context.Applications.FindAsync(id);
        }

        public async Task<Application> GetApplicationByNameAsync(string name)
        {
            return await _context.Applications.Where(a => a.Name.Contains(name)).FirstAsync();
        }

        public Task<List<Application>> GetApplicationsForUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Application> AddApplicationAsync(Application newApplication)
        {
            await _context.Applications.AddAsync(newApplication);
            await _context.SaveChangesAsync();
            return newApplication;
        }

        public async Task<Application> UpdateApplicationAsync(int id, Application newAppInfo)
        {
            Application application = await _context.Applications.FindAsync(id);
            if(application == null)
            {
                return null;
            }
            application.Name = application.Name == newAppInfo.Name || String.IsNullOrEmpty(newAppInfo.Name) ? application.Name : newAppInfo.Name;
            application.Description = application.Description == newAppInfo.Description || String.IsNullOrEmpty(newAppInfo.Description) ? application.Description : newAppInfo.Description;
            application.Url = application.Url == newAppInfo.Url || String.IsNullOrEmpty(newAppInfo.Url) ? application.Url : newAppInfo.Url;

            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<List<Application>> DeleteApplicationAsync(int id)
        {
            Application application = await _context.Applications.FindAsync(id);
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return await _context.Applications.ToListAsync();
        }
    }
}
