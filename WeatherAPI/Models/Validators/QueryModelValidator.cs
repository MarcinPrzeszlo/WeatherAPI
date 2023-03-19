using FluentValidation;

namespace WeatherAPI.Models.Validators
{
    public class QueryModelValidator: AbstractValidator<QueryModel>
    {
        private int[] allowedPageSizes = new int[] { 5, 10, 15 };
        private string[] allowedSortByColumnNames = new string[] { "Adress", "Type" };
        public QueryModelValidator()
        {
            RuleFor(x => x.PageNumber)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .Custom((value, context) =>
                {
                    if (!allowedPageSizes.Contains(value))
                    {
                        context.AddFailure("PageSize", $"PageSize must contain in [{string.Join(",", allowedPageSizes)}]");
                    }
                });

            RuleFor(x => x.SortBy)
                .Custom((value, context) =>
                {
                    if (!allowedSortByColumnNames.Contains(value))
                    {
                        context.AddFailure("SortBy", $"SortBy must contain in [{string.Join(",",allowedSortByColumnNames)}]");
                    }
                });
        }
    }
}
