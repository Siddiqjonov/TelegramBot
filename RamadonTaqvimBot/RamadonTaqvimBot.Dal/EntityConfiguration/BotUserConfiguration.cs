using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RamadonTaqvimBot.Dal.BotUserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamadonTaqvimBot.Dal.EntityConfiguration;

public class BotUserConfiguration : IEntityTypeConfiguration<BotUser>
{
    public void Configure(EntityTypeBuilder<BotUser> builder)
    {
        builder.ToTable("BotUser");
        builder.HasKey(bU => bU.BotUserId);
        builder.HasIndex(bU => bU.TelegramUserId).IsUnique(true);
        builder.Property(bU => bU.TelegramUserId).IsRequired(true);
        builder.Property(bU => bU.FirstName).IsRequired(false).HasMaxLength(70);
        builder.Property(bU => bU.LastName).IsRequired(false).HasMaxLength(70);
        builder.Property(bU => bU.Username).IsRequired(false).HasMaxLength(70);
        builder.Property(bU => bU.PhoneNumber).IsRequired(false).HasMaxLength(20);
        builder.Property(bU => bU.UpdatedAt).IsRequired(false);
    }
}
