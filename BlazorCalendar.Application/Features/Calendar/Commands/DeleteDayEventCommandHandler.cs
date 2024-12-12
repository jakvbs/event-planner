using BlazorCalendar.Domain.Calendar;
using MediatR;

namespace BlazorCalendar.Application.Features.Calendar.Commands;

public class DeleteDayEventCommandHandler(IDayInfoWriteRepository dayInfoWriteRepository)
    : IRequestHandler<DeleteDayEventCommand>
{
    public Task Handle(DeleteDayEventCommand request, CancellationToken cancellationToken)
    {
        return dayInfoWriteRepository.DeleteDayEvent(request.EventId);
    }
}