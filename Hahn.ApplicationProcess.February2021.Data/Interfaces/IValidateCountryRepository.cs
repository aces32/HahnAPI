using Hahn.ApplicationProcess.February2021.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Interfaces
{
    public interface IValidateCountryRepository
    {
        Task<bool> IsValidCountry(string country);
    }
}
