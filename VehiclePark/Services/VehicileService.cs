using Microsoft.EntityFrameworkCore;
using RestSharp;
using System.Net;
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
            try {
                return await _context.Vehicles.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
                                LicensePlate = "HV860J",
                                Color = "Grey",
                                BuildYear = 2016,
                                LoanedTo = "",
                                Status = StatusOptions.Sold,
                                Comments = "This licence plate works, and is sold"
                            },

                            new Vehicle
                            {
                                LicensePlate = "DS432X",
                                Color = "Zwart",
                                BuildYear = 2010,
                                LoanedTo = "",
                                Status = StatusOptions.Available,
                                Comments = "This vehicle is available"
                            },

                            new Vehicle
                            {
                                LicensePlate = "PS729W",
                                Color = "Red",
                                BuildYear = 2006,
                                LoanedTo = "",
                                Status = StatusOptions.InRepair,
                                Comments = "This vehicle is in repair"
                            }
                        };

                    _context.Vehicles.AddRange(vehicles);
                    await _context.SaveChangesAsync();
                    return vehicles;
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException($"Error adding the vehicles: {e.Message}");
                }
            }
            else
            {
                return new List<Vehicle>();
            }
        }

        public async Task<Vehicle> UpdateVehicle(int id, Vehicle vehicle)
        {
            var vehicleToUpdate = await _context.Vehicles.FindAsync(id);

            if (vehicleToUpdate == null)
            {
                throw new ArgumentException("Vehicle not found");
            }

            try
            {
                // Update basic vehicle information
                vehicleToUpdate.LicensePlate = vehicle.LicensePlate;
                vehicleToUpdate.Color = vehicle.Color;
                vehicleToUpdate.BuildYear = vehicle.BuildYear;
                vehicleToUpdate.Comments = vehicle.Comments;

                // Handles status updates and validations
                switch (vehicle.Status)
                {
                    case StatusOptions.Sold:
                        if (vehicleToUpdate.Status != StatusOptions.Available)
                        {
                            throw new InvalidOperationException("Vehicle cannot be sold");
                        }
                        break;
                    case StatusOptions.Lentout:
                        if (vehicleToUpdate.Status != StatusOptions.Available)
                        {
                            throw new InvalidOperationException("Vehicle cannot be lent out");
                        }
                        if (vehicleToUpdate.Status == StatusOptions.Lentout && vehicleToUpdate.LoanedTo != vehicle.LoanedTo)
                        {
                            throw new InvalidOperationException("Vehicle is already lent to someone else");
                        }
                        if (string.IsNullOrWhiteSpace(vehicle.LoanedTo))
                        {
                            throw new InvalidOperationException("Loan name is required");
                        }
                        break;
                    case StatusOptions.Available:
                        if (vehicleToUpdate.Status == StatusOptions.Sold)
                        {
                            throw new InvalidOperationException("Vehicle cannot be made available after being sold");
                        }
                        break;
                    case StatusOptions.InRepair:
                        break;
                    default:
                        throw new InvalidOperationException("Something else went wrong");
                }

                vehicleToUpdate.Status = vehicle.Status;
                vehicleToUpdate.LoanedTo = vehicle.LoanedTo;

                await _context.SaveChangesAsync();
                return vehicleToUpdate;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error updating vehicle: {e.Message}");
            }
        }


        public async Task<bool> ValidateLicensePlate(int id)
        {
            var vehicle = _context.Vehicles.Find(id);


            if (vehicle == null)
            {
                throw new ArgumentException("Vehicle not found");
            }else {
                var licensePlate = vehicle.LicensePlate;
                var client = new RestClient("https://opendata.rdw.nl");
                var request = new RestRequest($"/resource/m9d7-ebf2.json?kenteken={licensePlate}", Method.Get);
                var response = await client.ExecuteAsync(request);

                


                if (response.StatusCode == HttpStatusCode.OK)
                    {
                    return true;
                }
                else
                {
                    return false;
                }

            }


        }
    }
}
