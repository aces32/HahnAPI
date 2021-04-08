using FluentValidation;
using Hahn.ApplicationProcess.February2021.Data.Interfaces;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Validators
{

	public class AssetValidator : AbstractValidator<Asset>
	{
		public AssetValidator(IUnitOfWork unitOfWork)
		{
			RuleFor(x => x.ID).NotNull();
			RuleFor(x => x.AssetName).NotNull().MinimumLength(5);
			RuleFor(x => x.Department).NotNull().IsInEnum();
			RuleFor(x => x.EmailAdressOfDepartment).NotNull().EmailAddress();
			RuleFor(x => x.Broken).NotNull();
			RuleFor(m => m.CountryOfDepartment).NotNull().
				MustAsync(async (country, cancellation) => (await unitOfWork.ValidateCountryRepository.IsValidCountry(country))).WithMessage("Country name is not valid.");
			RuleFor(x => x.PurchaseDate).NotNull().GreaterThanOrEqualTo(DateTime.Now.AddYears(-1));
		}
	}
}
