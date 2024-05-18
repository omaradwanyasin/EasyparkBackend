using Easypark_Backend.Data.DataModels;
using Easypark_Backend.Data.Repository;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace signalrtest.Hubs
{
    public class GarageHubs : Hub
    {
        private readonly GarageRepo _garageRepo;
        private readonly ILogger<GarageHubs> _logger;

        public GarageHubs(GarageRepo garageRepo, ILogger<GarageHubs> logger)
        {
            _garageRepo = garageRepo;
            _logger = logger;
        }

        // Method to update the status and notify all clients
        public async Task UpdateStatus(string garageId, int status)
        {
            try
            {
                // Log the incoming request
                _logger.LogInformation($"Updating status of garage {garageId} to {status}");

                // Update the status of the garage in the database
                await _garageRepo.UpdateGarageStatusAsync(garageId, status);

                // Broadcast the garage ID and status update to all clients
                await Clients.All.SendAsync("ReceiveStatusUpdate", garageId, status);
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, $"Error updating garage status for {garageId}");
                throw; // Ensure the exception is thrown so the client can receive the error
            }
        }

        public async Task UpdateSpecificChatRoom(GarageModel garage)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, garage.Id.ToString());
            await Clients.Group(garage.Id.ToString()).SendAsync("updatethegarage", "The specific garage has been updated");
        }
    }
}
