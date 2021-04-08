using Hahn.ApplicationProcess.February2021.Data.DBContext;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Services
{
    public class AssetRepository : GenericRepository<Asset>
    {
        public AssetRepository(AssetDBContext assetdbcontext) : base(assetdbcontext)
        {

        }

        public override Asset Update(Asset entity)
        {
            var assets = assetDBContext.Assets.Single(a => a.ID == entity.ID);
            assets.AssetName = entity.AssetName;
            assets.Broken = entity.Broken;
            assets.CountryOfDepartment = entity.CountryOfDepartment;
            assets.Department = entity.Department;
            assets.EmailAdressOfDepartment = entity.EmailAdressOfDepartment;
            assets.PurchaseDate = entity.PurchaseDate;
            return base.Update(entity);
        }

    }
}
