using AutoMapper;
using DAL.Models;

namespace BLL.Infrastructure.Automapper.Automapper_Profiles
{
    public class PagedListConverter<TSource, TDestination>
        : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
    {
        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context) =>
            new PagedList<TDestination>(context.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source), source.TotalCount, source.CurrentPage, source.PageSize);
    }
}
