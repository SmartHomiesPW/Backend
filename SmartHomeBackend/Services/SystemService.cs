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

        public bool AddSystemToUser(string systemId, Guid userId)
        {
            var system = _context.Systems.FirstOrDefault(x => x.System_Id == systemId);
            var user = _context.Users.FirstOrDefault(x => x.User_Id == userId);
            if (system == null || user == null)
            {
                return false;
            }

            if (!system.Users.Contains(user))
            {
                system.Users.Add(user);
            }
            if (!user.Systems.Contains(system))
            {
                user.Systems.Add(system);
            }

            _context.SaveChanges();

            return true;
        }

        public bool RemoveSystemFromUser(string systemId, Guid userId)
        {
            var system = _context.Systems.FirstOrDefault(x => x.System_Id == systemId);
            var user = _context.Users.FirstOrDefault(x => x.User_Id == userId);
            if (system == null || user == null)
            {
                return false;
            }

            if (system.Users.Contains(user))
            {
                system.Users.Remove(user);
            }
            if (user.Systems.Contains(system))
            {
                user.Systems.Remove(system);
            }

            _context.SaveChanges();

            return true;
        }

        public bool ClearUserSystemList(Guid userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.User_Id == userId);
            if (user == null)
            {
                return false;
            }

            user.Systems.Clear();

            _context.SaveChanges();

            return true;
        }

        public bool AddBoardToSystem(string systemId, string boardId)
        {
            var system = _context.Systems.FirstOrDefault(x => x.System_Id == systemId);
            var board = _context.Boards.FirstOrDefault(x => x.Board_Id == boardId);
            if (system == null || board == null)
            {
                return false;
            }

            board.System_Id = systemId;
            _context.SaveChanges();

            return true;
        }
    }
}
