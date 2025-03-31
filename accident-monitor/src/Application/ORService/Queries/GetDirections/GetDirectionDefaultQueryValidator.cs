namespace AccidentMonitoring.Application.ORService.Queries.GetDirections;
public class GetDirectionDefaultQueryValidator : AbstractValidator<GetDirectionDefaultQuery>
{
    public GetDirectionDefaultQueryValidator()
    {
        RuleFor(x => x.Profile)
            .NotEmpty().WithMessage("Origin is required.");
        RuleFor(x => x.RequestDto.DestinationCoordinate)
            .NotEmpty().WithMessage("Destination is required.");
        RuleFor(x => x.RequestDto.StartingCoordinate)
            .NotEmpty().WithMessage("Destination is required.");
    }
}

