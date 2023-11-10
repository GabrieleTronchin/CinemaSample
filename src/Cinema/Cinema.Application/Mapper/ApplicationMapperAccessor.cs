using AutoMapper;

namespace Cinema.Application.Mapper;
public class ApplicationMapperAccessor : IApplicationMapperAccessor
{
    public ApplicationMapperAccessor()
    {

        AppMapper = new MapperConfiguration(m =>
        {

        }).CreateMapper();

    }

    public IMapper AppMapper { get; }

}
