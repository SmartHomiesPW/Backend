using SmartHomeBackend.Database;

namespace SmartHomeBackend.Services
{
    public class BoardService
    {
        public static string GetBoardURL(string systemId, string boardId, SmartHomeDbContext context)
        {
            var calledBoard = context.Boards.First(x => x.System_Id == systemId && x.Board_Id == boardId);
            if (calledBoard == null)
            {
                return null;
            }
            return calledBoard.Board_URL;
        }
    }
}
