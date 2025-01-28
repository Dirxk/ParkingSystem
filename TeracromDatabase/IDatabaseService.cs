namespace TeracromDatabase
{
    public interface IDatabaseService
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null);
        Task<IEnumerable<T>> QuerySPAsync<T>(string sql, object? parameters = null);
        Task<T>QueryFirstOrDefaultAsync<T>(string sql, object? parameter = null);
        Task<int> ExecuteAsync(string sql, object? parameter = null);




    }
}
