using System.Web;

namespace Common.CustomClasses;
public class DataGridQuery
{
    public string? Search { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string? SortField { get; set; }
    public DataGridQuerySortDirection SortDir { get; set; }

    private CompactDataGridQuery ToCompact()
    {
        return new CompactDataGridQuery
        {
            s = Search,
            p = Page,
            ps = PageSize,
            sf = SortField,
            sd = SortDir,
        };
    }

    public override string ToString()
    {
        var simple = ToCompact();
        var properties = simple.GetType().GetProperties()
            .Select(x => new
            {
                Name = x.Name.ToLower(),
                Value = x.GetValue(simple)
            })
            .Where(x => x.Value != null)
            .Select(x => $"{x.Name}={HttpUtility.UrlEncode(x.Value?.ToString())}");

        return string.Join("&", properties.ToArray());
    }
}

public enum DataGridQuerySortDirection : byte
{
    Ascending = 0,
    Descending = 1,
}

public class CompactDataGridQuery
{
    public string? s { get; set; }
    public int? p { get; set; }
    public int? ps { get; set; }
    public string? sf { get; set; }
    public DataGridQuerySortDirection sd { get; set; }

    public DataGridQuery ToQuery()
    {
        return new DataGridQuery
        {
            Search = s,
            Page = p,
            PageSize = ps,
            SortField = sf,
            SortDir = sd,
        };
    }
}
