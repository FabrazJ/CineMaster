using System.Threading.Tasks;
using CineMaster.DTO;

namespace CineMaster.Services
{
    public interface ISalaService
    {
        Task<bool> InhabilitarButacaYCancelarReserva(DTOBooking bookingDto, SeatDto seatDto);
        Task<List<CancellationResultDto>> CancelarCarteleraYReservas(DTOBillboardsCancellation cancellationDto);

    }
}
