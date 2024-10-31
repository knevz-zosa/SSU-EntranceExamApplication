namespace Common.CustomClasses;
public class PagedList<T> where T : class
{
    public int Count { get; set; }
    public ICollection<T> List { get; set; }

    public PagedList(int count, ICollection<T> list)
    {
        Count = count;
        List = list;
    }
}

