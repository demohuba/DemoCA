namespace JEPCO.Shared.ModelsAbstractions;

public class QueryResult<T>
{
    public QueryResult()
    {
        Data = new QueryResultData<T>();
    }

    public QueryResult(string title) : this()
    {
        Title = title;
    }

    public QueryResult(QueryResultData<T> data)
    {
        Data = data;
    }

    public QueryResult(QueryResultData<T> data, string title)
    {
        Data = data;
        Title = title;
    }

    public string Title { get; set; } = string.Empty;
    public int Status { get; set; } = (int)SuccessResponseEnum.Succeeded;
    public QueryResultData<T> Data { get; set; }
}



public class QueryResultData<T>
{
    public int Count { get; set; }
    public IEnumerable<T> Rows { get; set; } = new List<T>();
}
