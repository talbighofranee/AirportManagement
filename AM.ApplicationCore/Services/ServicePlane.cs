using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Services
{
    public class ServicePlane : Service<Plane>, IServicePlane
    {
        public ServicePlane(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Flight> GetFlights(int n)
        {
            return GetMany().OrderByDescending(p => p.PlaneId)
                 .Take(n)
                 .SelectMany(p => p.Flights)
                 .OrderBy(f => f.FlightDate);
        }

        public IEnumerable<Traveller> GetPassengers(Plane p)
        {
            return p.Flights.SelectMany(f => f.Tickets)
                .Select(t => t.Passenger)
                .OfType<Traveller>();
        }
    }
}
