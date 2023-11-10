using AutoMapper;


namespace Cinema.Api.Mapper;
public class ApiMapperAccessor : IApiMapperAccessor
{
    public ApiMapperAccessor()
    {

        ApiMapper = new MapperConfiguration(m =>
        {


        }).CreateMapper();

    }

    public IMapper ApiMapper { get; }

}
