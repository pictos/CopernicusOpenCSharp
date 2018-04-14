namespace CopernicusOpenCSharp.Models
{
    public enum Select
    {
        format,
        filter,
        orderby,
        select,
        skip,
        top,
        count,
        inlinecount,
        expand
    }

    public enum Format
    {
        atom,
        xml,
        json,
        csv,
    }

    public enum Entites
    {
        Products,
        DeletedProducts,
        Collections
    }
}
