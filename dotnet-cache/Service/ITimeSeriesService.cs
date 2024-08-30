using dotnetCache.Models;

namespace dotnetCache.Service;

public interface ITimeSeriesService
{
    Task<IEnumerable<TimeSeriesData>> GetTimeSeries();
}
