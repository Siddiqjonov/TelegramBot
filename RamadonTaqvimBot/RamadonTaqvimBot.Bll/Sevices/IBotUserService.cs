using RamadonTaqvimBot.Dal.BotUserEntity;

namespace RamadonTaqvimBot.Bll.Sevices;

public interface IBotUserService
{
    Task AddUserAsync(BotUser botUser);
    Task<List<BotUser>> GetAllUsersAsync();
    Task UpdateUserAsync(BotUser botUser);
}