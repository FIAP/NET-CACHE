using dotnet_cache.Infra.Config;
using dotnetCache.Models;

namespace dotnetCache.Service
{
    public class TimeSeriesService : ITimeSeriesService
    {
        private readonly TimeSeriesRepository _timeSeriesRepository;
        public TimeSeriesService(TimeSeriesRepository timeSeriesRepository)
        {
            _timeSeriesRepository = timeSeriesRepository;
        }

        public async Task<IEnumerable<TimeSeriesData>> GetTimeSeries() => await _timeSeriesRepository.GetAllTimeSeries();

    }
}
