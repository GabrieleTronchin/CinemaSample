using Cinema.Application.Commands;
using MediatR;

namespace Cinema.Application.Handlers
{
    public class AssignShowtimeCommandHandler : IRequestHandler<AssignShowtimeCommand, AssignShowtimeCommandComplete>
    {


        public AssignShowtimeCommandHandler()
        {

        }

        public async Task<AssignShowtimeCommandComplete> Handle(AssignShowtimeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }


}