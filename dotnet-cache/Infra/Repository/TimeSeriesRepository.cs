using Dapper;
using dotnetCache.Models;

namespace dotnet_cache.Infra.Config;

public class TimeSeriesRepository
{
    private readonly DbConnectionProvider _dbProvider;
    public TimeSeriesRepository(DbConnectionProvider dbProvider) =>
                _dbProvider = dbProvider;


    public async Task<IEnumerable<TimeSeriesData>> GetAllTimeSeries()
    {
        using var connection = _dbProvider.GetConnection();
        var query = TimeSeriesQuery.ListAll;
        var result = await connection.QueryAsync<TimeSeriesData>(query);
        return result;
    }
    
}

