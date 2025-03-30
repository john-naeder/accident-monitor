using AccidentMonitoring.Application.ORService.Queries.GetDirections;

namespace AccidentMonitoring.Application.DTOs
{
    public class DirectionDefaultCutResponseDto
    {
        public List<double> Bbox { get; set; } = [];
        public List<SegmentDto> Segments { get; set; } = [];
    }

    public class SegmentDto
    {
        public double Distance { get; set; }
        public double Duration { get; set; }
        public List<StepDto> Steps { get; set; } = new List<StepDto>();
    }

    public class StepDto
    {
        public double Distance { get; set; }
        public double Duration { get; set; }
        public string Instruction { get; set; } = string.Empty;
    }

    public static class DirectionMapper
    {
        public static DirectionDefaultCutResponseDto Map(GetDirectionDefaultResponseDto source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var response = new DirectionDefaultCutResponseDto();

            response.Bbox = source.Bbox.Select(x => Convert.ToDouble(x)).ToList();

            if (source.Features != null && source.Features.Any())
            {
                var segments = source.Features
                                     .Where(f => f?.Properties?.Segments != null)
                                     .SelectMany(f => f.Properties.Segments)
                                     .ToList();

                response.Segments = segments.Select(segment => new SegmentDto
                {
                    Distance = segment.Distance,
                    Duration = segment.Duration,
                    Steps = segment.Steps?.Select(step => new StepDto
                    {
                        Distance = step.Distance,
                        Duration = step.Duration,
                        Instruction = step.Instruction
                    }).ToList() ?? new List<StepDto>()
                }).ToList();
            }

            return response;
        }
    }
}
