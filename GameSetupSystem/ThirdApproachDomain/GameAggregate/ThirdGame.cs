using System;
using System.Collections.Generic;
using System.Linq;

namespace ThirdApproachDomain.GameAggregate
{
    public static class DateTimeProvider
    {
        public static DateTimeOffset Now => GetNowFunc();

        public static Func<DateTimeOffset> GetNowFunc { get; set; } = () => DateTimeOffset.Now;
    }

    public abstract class ThirdGame
    {
        public Guid Guid { get; }
        public abstract ThirdGameDiscriminator Discriminator { get; }
        public DateTimeOffset GameDate { get; }
        public string Description { get; }

        public DateTimeOffset RegistrationEndDate { get; }
        public ZeroPositiveInt MaxPlayersCount { get; }
        public ZeroPositiveInt MinimalRequiredPlayersCount { get; }
        public IEnumerable<PlayerRegisteredForGame> PlayersRegistered => _playerRegistered;
        private readonly IList<PlayerRegisteredForGame> _playerRegistered;

        protected ThirdGame(
            ZeroPositiveInt maxPlayersCount,
            ZeroPositiveInt minimalRequiredPlayersCount,
            DateTimeOffset gameDate,
            string description, 
            DateTimeOffset registrationEndDate)
        {
            if (minimalRequiredPlayersCount.Value > maxPlayersCount.Value)
            {
                throw new BusinessLogicException($"Minimal required players count [{minimalRequiredPlayersCount.Value}] cannot be higher than max players count [{maxPlayersCount.Value}].");
            }

            Guid = Guid.NewGuid();
            MaxPlayersCount = maxPlayersCount;
            MinimalRequiredPlayersCount = minimalRequiredPlayersCount;
            GameDate = gameDate;
            Description = description;
            RegistrationEndDate = registrationEndDate;

            _playerRegistered = new List<PlayerRegisteredForGame>();
        }

        public void RegisterPlayer(Player player)
        {
            if (_playerRegistered.SingleOrDefault(x => x.PlayerGuid == player.Guid) != null)
            {
                return;
            }

            if (RegistrationEndDate < DateTimeProvider.Now)
            {
                throw new BusinessLogicException("Time for registering to this game has elapsed.");
            }

            if (MaxPlayersCount.Value <= PlayersRegistered.Count())
            {
                throw new BusinessLogicException("There is already enough players in this game.");
            }

            _playerRegistered.Add(new PlayerRegisteredForGame(player, this));
        }

        public void UnregisterPlayer(Player player)
        {
            var registeredPlayer = _playerRegistered.SingleOrDefault(x => x.PlayerGuid == player.Guid);
            if (registeredPlayer == null)
            {
                return;
            }

            if (RegistrationEndDate < DateTimeProvider.Now)
            {
                throw new BusinessLogicException("Time for registering to this game has elapsed.");
            }

            _playerRegistered.Remove(registeredPlayer);
        }
    }
}
