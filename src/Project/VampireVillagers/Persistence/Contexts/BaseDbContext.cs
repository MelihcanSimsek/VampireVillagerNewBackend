using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }

        public DbSet<Player> Players { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<GameSetting> GameSettings { get; set; }
        public DbSet<GameState> GameStates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Player>(a =>
            {
                a.ToTable("Players").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.LobbyId).HasColumnName("LobbyId");
                a.Property(p => p.IsOwner).HasColumnName("IsOwner");
                a.HasOne(p => p.User);
                a.HasOne(p => p.Lobby);
                a.HasMany(p => p.Votes).WithOne(u=>u.Player).HasForeignKey(v=>v.PlayerId).OnDelete(DeleteBehavior.Restrict);
                a.HasMany(p => p.VotesAsTarget).WithOne(u=>u.Target).HasForeignKey(v=>v.TargetId).OnDelete(DeleteBehavior.Restrict);
                a.HasMany(p => p.GameStates);
            });

            modelBuilder.Entity<Lobby>(a =>
            {
                a.ToTable("Lobbies").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.CreationDate).HasColumnName("CreationDate");
                a.Property(p => p.HasPassword).HasColumnName("HasPassword");
                a.Property(p => p.Password).HasColumnName("Password");
                a.HasMany(p => p.Chats);
                a.HasMany(p => p.Players);
                a.HasMany(p => p.GameSettings);
            });

            modelBuilder.Entity<Chat>(a =>
            {
                a.ToTable("Chats").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.LobbyId).HasColumnName("LobbyId");
                a.Property(p => p.PlayerId).HasColumnName("PlayerId");
                a.Property(p => p.Message).HasColumnName("Message");
                a.Property(p => p.MessageDate).HasColumnName("MessageDate");
                a.HasOne(p => p.Player);
                a.HasOne(p => p.Lobby);
            });

            modelBuilder.Entity<GameSetting>(a =>
            {
                a.ToTable("GameSettings").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.LobbyId).HasColumnName("LobbyId");
                a.Property(p => p.CreationDate).HasColumnName("CreationDate");
                a.Property(p => p.NightTime).HasColumnName("NightTime");
                a.Property(p => p.DayTime).HasColumnName("DayTime");
                a.Property(p => p.VampireNumber).HasColumnName("VampireNumber");
                a.Property(p => p.PriestNumber).HasColumnName("PriestNumber");
                a.Property(p => p.WitchNumber).HasColumnName("WitchNumber");
                a.Property(p => p.VampireHunterNumber).HasColumnName("VampireHunterNumber");
                a.Property(p => p.ShapeshifterNumber).HasColumnName("ShapeshifterNumber");
                a.Property(p => p.TransformingVampireNumber).HasColumnName("TransformingVampireNumber");
                a.HasMany(p => p.GameStates);
                a.HasMany(p => p.Votes);
                a.HasOne(p => p.Lobby);
            });


            modelBuilder.Entity<GameState>(a =>
            {
                a.ToTable("GameStates").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.PlayerId).HasColumnName("PlayerId");
                a.Property(p => p.GameSettingId).HasColumnName("GameSettingId");
                a.Property(p => p.LiveState).HasColumnName("LiveState");
                a.Property(p => p.Role).HasColumnName("Role");
                a.Property(p => p.Skill).HasColumnName("Skill");
                a.HasOne(p => p.Player);
                a.HasOne(p => p.GameSetting);
            });


            modelBuilder.Entity<Vote>(a =>
            {
                a.ToTable("Votes").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.PlayerId).HasColumnName("PlayerId");
                a.Property(p => p.GameSettingId).HasColumnName("GameSettingId");
                a.Property(p => p.TargetId).HasColumnName("TargetId");
                a.Property(p => p.Day).HasColumnName("Day");
                a.Property(p => p.DayType).HasColumnName("DayType");
                a.HasOne(p => p.Player).WithMany(p=>p.Votes).HasForeignKey(p=>p.PlayerId).OnDelete(DeleteBehavior.Restrict);
                a.HasOne(p => p.Target).WithMany(p=>p.VotesAsTarget).HasForeignKey(p=>p.TargetId).OnDelete(DeleteBehavior.Restrict);
                a.HasOne(p => p.GameSetting);
            });

            modelBuilder.Entity<User>(a =>
            {
                a.ToTable("Users").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.FirstName).HasColumnName("FirstName");
                a.Property(p => p.LastName).HasColumnName("LastName");
                a.Property(p => p.Email).HasColumnName("Email");
                a.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
                a.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
                a.Property(p => p.Status).HasColumnName("Status");
                a.HasMany(p => p.UserOperationClaims);
                a.HasMany(p => p.RefreshTokens);
            });

            modelBuilder.Entity<OperationClaim>(a =>
            {
                a.ToTable("OperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<UserOperationClaim>(a =>
            {
                a.ToTable("UserOperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");
                a.HasOne(p => p.User);
                a.HasOne(p => p.OperationClaim);
            });

            modelBuilder.Entity<RefreshToken>(a =>
            {
                a.ToTable("RefreshTokens").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.Token).HasColumnName("Token");
                a.Property(p => p.Expires).HasColumnName("Expires");
                a.Property(p => p.Created).HasColumnName("Created");
                a.Property(p => p.CreatedByIp).HasColumnName("CreatedByIp");
                a.Property(p => p.Revoked).HasColumnName("Revoked");
                a.Property(p => p.RevokedByIp).HasColumnName("RevokedByIp");
                a.Property(p => p.ReplacedByToken).HasColumnName("ReplacedByToken");
                a.Property(p => p.ReasonRevoked).HasColumnName("ReasonRevoked");
                a.HasOne(p => p.User);

            });

        }
    }
}
