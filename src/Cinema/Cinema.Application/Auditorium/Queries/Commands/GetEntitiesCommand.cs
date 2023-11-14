using Cinema.Application.Auditorium.Queries.Models;
using Cinema.Application.Ticket.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Auditorium.Queries.Commands
{
    public class GetEntitiesCommand : IRequest<IList<AuditoriumReadModel>>
    {
    }
}
