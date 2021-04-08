using Hahn.ApplicationProcess.February2021.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Asset> AssetRepository { get; }
        IValidateCountryRepository ValidateCountryRepository { get; }

        void SaveChanges();

    }
}
