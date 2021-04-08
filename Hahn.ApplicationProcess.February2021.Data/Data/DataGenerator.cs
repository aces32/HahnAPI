using Hahn.ApplicationProcess.February2021.Data.DBContext;
using Hahn.ApplicationProcess.February2021.Domain.Enums;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AssetDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<AssetDBContext>>()))
            {

                if (context.Assets.Any())
                {
                    return;   // Data was already seeded
                }

                context.Assets.AddRange(
                    new Asset
                    {
                        ID = 1,
                        AssetName = "Oya Try!",
                        EmailAdressOfDepartment = "sbuari@gmail.com",
                        CountryOfDepartment = "Nigeria",
                        Broken = true,
                        Department = DepartmentEnum.Store1,
                        PurchaseDate = DateTime.Now
                    },
                    new Asset
                    {
                        ID = 2,
                        AssetName = "Blue assets!",
                        EmailAdressOfDepartment = "sbuari@gmail.com",
                        CountryOfDepartment = "Nigeria",
                        Broken = true,
                        Department = DepartmentEnum.Store1,
                        PurchaseDate = DateTime.Now
                    },
                    new Asset
                    {
                        ID = 3,
                        AssetName = "Yellow Asset!",
                        EmailAdressOfDepartment = "sbuari@gmail.com",
                        CountryOfDepartment = "Nigeria",
                        Broken = true,
                        Department = DepartmentEnum.Store1,
                        PurchaseDate = DateTime.Now
                    });

                context.SaveChanges();
            }
        }
    }
}
