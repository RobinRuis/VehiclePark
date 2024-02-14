using VehiclePark.Models;

namespace VehiclePark.Services
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetAllVehicles();
        Task<List<Vehicle>> PostDummyVehicles();
        Task<Vehicle> UpdateVehicle(int id, Vehicle vehicle);
        Task<Vehicle> ValidateLicensePlate(int id, Vehicle vehicle);
    }
}
