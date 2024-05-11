using Easypark_Backend.Data.DataModels;
using Microsoft.AspNetCore.SignalR;

namespace Easypark_Backend.Services.Hubs
{
    public class ResearvationHub:Hub
    {
        public async Task SendRev(ReservationModel model)
        {
            await Clients.All.SendAsync("ReceiveRequest", "Admin", $"{model.Name} sent an request");
        }
        public async Task SendRevToGarage(ReservationModel model)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, model.GarageId.ToString());
            await Clients.Group(model.GarageId.ToString()).SendAsync("ReceiveRequest", "admin", $"{model.Name} sent an request");
        }
    }
}
