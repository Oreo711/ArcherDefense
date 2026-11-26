using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Infrastructure.ConfigsService;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Economy
{
    public sealed class WalletService
    {
        private readonly Dictionary<CurrencyId, int> _amounts = new();

        public WalletService(ConfigsProviderService configsProviderService)
        {
            GameplayConfig config = configsProviderService.GetConfig<GameplayConfig>();

            foreach (CurrencyId currency in Enum.GetValues(typeof(CurrencyId)))
                _amounts[currency] = 0;

            _amounts[CurrencyId.Gold] = config.StartingGold;
        }

        public int Get(CurrencyId currency) => _amounts[currency];

        public bool CanAfford(CurrencyId currency, int amount)
        {
            ValidateAmount(amount);

            return Get(currency) >= amount;
        }

        public void Add(CurrencyId currency, int amount)
        {
            ValidateAmount(amount);

            _amounts[currency] = Get(currency) + amount;
        }

        public void Spend(CurrencyId currency, int amount)
        {
            if (TrySpend(currency, amount) == false)
                throw new InvalidOperationException($"Not enough {currency}: {Get(currency)} < {amount}");
        }

        public bool TrySpend(CurrencyId currency, int amount)
        {
            if (CanAfford(currency, amount) == false)
                return false;

            _amounts[currency] = Get(currency) - amount;

            return true;
        }

        private static void ValidateAmount(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive");
        }
    }
}
