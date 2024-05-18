using Easypark_Backend.Data.DataModels;
using Microsoft.AspNetCore.SignalR;

using System.Threading.Tasks;

namespace signalrtest.Hubs
{
    public class GarageHubs : Hub
    {
        // Method to update the status and notify all clients
        public async Task UpdateStatus(string garageId, string status)
        {
            // Broadcast the garage ID and status update to all clients
            await Clients.All.SendAsync("ReceiveStatusUpdate", garageId, status);
        }

        public async Task UpdateSpecificChatRoom(GarageModel garage)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, garage.Id.ToString());
            await Clients.Group(garage.Id.ToString()).SendAsync("updatethegarage", "The specific garage has been updated");
        }
    }
}
