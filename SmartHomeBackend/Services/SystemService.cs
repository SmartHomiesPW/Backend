using SmartHomeBackend.Database;

namespace SmartHomeBackend.Services
{
    public class SystemService
    {
        private readonly SmartHomeDbContext _context;

        public SystemService(SmartHomeDbContext context)
        {
            _context = context;
        }

        public bool SystemExists(string systemId)
        {
            return _context.Systems.Any(s => s.System_Id == systemId);
        }
    }
}
