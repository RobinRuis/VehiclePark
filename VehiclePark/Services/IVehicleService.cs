using VehiclePark.Models;

namespace VehiclePark.Services
{
    public interface IVehicleService
    {
        Task<ICollection<Vehicle>> GetAllVehicles();
        Task<Vehicle> UpdateVehicle(int id, Vehicle vehicle);
        Task<Vehicle> ValidateLicensePlate(int id, string licensePlate);
    }
}
