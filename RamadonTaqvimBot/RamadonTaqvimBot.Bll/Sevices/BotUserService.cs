using Microsoft.EntityFrameworkCore;
using RamadonTaqvimBot.Dal;
using RamadonTaqvimBot.Dal.BotUserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamadonTaqvimBot.Bll.Sevices;

public class BotUserService : IBotUserService
{
    private readonly MainContext mainContext;

    public BotUserService(MainContext mainContext)
    {
        this.mainContext = mainContext;
    }

    public async Task AddUserAsync(BotUser botUser)
    {
        //await mainContext.BotUsers.AddAsync(botUser);
        //await mainContext.SaveChangesAsync();
        var dbUser = await mainContext.BotUsers.FirstOrDefaultAsync(u => u.TelegramUserId == botUser.TelegramUserId);
        if (dbUser != null)
        {
            Console.WriteLine($"User with id : {botUser.TelegramUserId} already exists!");
            return;
        }
        try
        {
            await mainContext.BotUsers.AddAsync(botUser);
            await mainContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public Task<List<BotUser>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(BotUser botUser)
    {
        throw new NotImplementedException();
    }
    
}
