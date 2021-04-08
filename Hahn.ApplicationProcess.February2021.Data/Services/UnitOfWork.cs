using Hahn.ApplicationProcess.February2021.Data.DBContext;
using Hahn.ApplicationProcess.February2021.Data.Interfaces;
using Hahn.ApplicationProcess.February2021.Domain.ConfigurationSettings;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AssetDBContext assetDBContext;
        private readonly ILogger<ValidateCountryRepository> logger;
        private readonly IOptions<APIUrlConfigurationSettings> optionsApi;

        public UnitOfWork(AssetDBContext assetDBContext, ILogger<ValidateCountryRepository> logger,
            IOptions<APIUrlConfigurationSettings> optionsApi)
        {
            this.assetDBContext = assetDBContext;
            this.logger = logger;
            this.optionsApi = optionsApi;
        }


        private IRepository<Asset> assetRepository;
        public IRepository<Asset> AssetRepository
        {
            get
            {
                if (assetRepository == null)
                {
                    assetRepository = new AssetRepository(assetDBContext);
                }

                return assetRepository;
            }
        }

        private IValidateCountryRepository validateCountryRepository;
        public IValidateCountryRepository ValidateCountryRepository
        {
            get
            {
                if (validateCountryRepository == null)
                {
                    validateCountryRepository = new ValidateCountryRepository(logger, optionsApi);
                }

                return validateCountryRepository;
            }
        }

        public void SaveChanges()
        {
            assetDBContext.SaveChanges();
        }
    }
}
