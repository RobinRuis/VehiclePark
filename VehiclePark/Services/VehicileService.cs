using VehiclePark.Models;

namespace VehiclePark.Services
{

    public class VehicileService : IVehicleService
    {

        private readonly DataContext _context;

        public VehicileService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Vehicle>> PostDummyVehicles()
        {
            if (_context.Vehicles.Count() == 0)
            {
                try
                {
                    List<Vehicle> vehicles = new List<Vehicle>
                        {
                            new Vehicle
                            {
                                LicensePlate = "HV-860-J",
                                Color = "Grijs",
                                BuildYear = 2016,
                                LoanedTo = "Marco Ruis",
                                Status = StatusOptions.Sold,
                                Comments = "This licence plate works"
                            },

                            new Vehicle
                            {
                                LicensePlate = "RE-432-X",
                                Color = "Zwart",
                                BuildYear = 2010,
                                LoanedTo = "",
                                Status = StatusOptions.Available,
                                Comments = ""
                            }
                        };

                    _context.Vehicles.AddRange(vehicles);
                    await _context.SaveChangesAsync();
                    return vehicles;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            else
            {
                return new List<Vehicle>();
            }
        }

        public async Task<Vehicle> UpdateVehicle(int id, Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public async Task<Vehicle> ValidateLicensePlate(int id, Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
