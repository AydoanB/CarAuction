﻿namespace CarAuction.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CarAuction.Data.Common.Repositories;
    using CarAuction.Data.Models;
    using CarAuction.Services.Mapping;

    public class SettingsService : ISettingsService
    {
        private readonly IDeletableEntityRepository<Setting> settingsRepository;

        public SettingsService(IDeletableEntityRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public int GetCount()
        {
            return this.settingsRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.settingsRepository.All().To<T>().ToList();
        }
    }
}
