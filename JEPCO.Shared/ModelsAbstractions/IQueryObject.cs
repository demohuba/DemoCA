namespace JEPCO.Shared.ModelsAbstractions;

public interface IQueryObject
{
    string? SortBy { get; set; }
    bool IsAscending { get; set; }
    int Index { get; set; }
    int Size { get; set; }
}
