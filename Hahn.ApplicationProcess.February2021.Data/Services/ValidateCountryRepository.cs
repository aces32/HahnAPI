using Hahn.ApplicationProcess.February2021.Data.Interfaces;
using Hahn.ApplicationProcess.February2021.Domain.ConfigurationSettings;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Services
{
    public class ValidateCountryRepository : IValidateCountryRepository
    {
        private readonly ILogger<ValidateCountryRepository> logger;
        private readonly APIUrlConfigurationSettings aPIUrlConfiguration;

        public ValidateCountryRepository(ILogger<ValidateCountryRepository> logger,
            IOptions<APIUrlConfigurationSettings> optionsApi)
        {
            this.logger = logger;
            this.aPIUrlConfiguration = optionsApi.Value;
        }

        public async Task<bool> IsValidCountry(string country)
        {
            try
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.ConnectionClose = false;
                client.BaseAddress = new Uri($"{aPIUrlConfiguration.BaseUrl}{aPIUrlConfiguration.CountryCheckUrl}{country}{"?fullText=true"}");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex}", $"Unable to ValidateCountry{nameof(IsValidCountry)}");
                return false;

            }
        }
    }
}
