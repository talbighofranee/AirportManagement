using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Services
{
    public class FlightMethods:IFlightMethods
    {
        public List<Flight> Flights=new List<Flight>();
        public Action<Plane> FlightDetailsDel;
        public Func<string, double> DurationAverageDel;
        public FlightMethods() {
            FlightDetailsDel = ShowFlightDetails;
            DurationAverageDel = DurationAverage;
        }
        public List<DateTime> GetFlightDates(string destination)
        {
            List<DateTime> dates = new List<DateTime>();
           dates=(from f in Flights
                 where f.Destination==destination
                 select f.FlightDate).ToList();
            return dates;
        }

        public void GetFlights(string filterType, string filterValue)
        {
            switch(filterType)
            {
                case "Destination":
                    foreach (Flight f in Flights)
                    {
                        if (f.Destination.Equals(filterValue))
                            Console.WriteLine(f);
                    }
                    break;
                case "Departure":
                    foreach (Flight f in Flights)
                    {
                        if (f.Departure.Equals(filterValue))
                            Console.WriteLine(f);
                    }
                    break;
                case "EstimatedDuration":
                    foreach (Flight f in Flights)
                    {
                        if (f.EstimatedDuration.Equals(int.Parse(filterValue)))
                            Console.WriteLine(f);
                    }
                    break;
                case "FlightDate":
                    foreach (Flight f in Flights)
                    {
                        if (f.FlightDate.Equals(DateTime.Parse(filterValue)))
                            Console.WriteLine(f);
                    }
                    break;
            }
        }

        public void ShowFlightDetails(Plane plane)
        {
            var req = from f in Flights
                                      where f.Plane == plane
                                      select new { f.Departure, f.FlightDate };
            foreach (var f in req)
                Console.WriteLine(f);
        }
        public int ProgrammedFlightNumber(DateTime startTime)
        {
            var req = from f in Flights
                      where (f.FlightDate - startTime).TotalDays <= 7 && DateTime.Compare(f.FlightDate, startTime) > 0
                      select f;
            return req.Count();
        }
        public double DurationAverage(string destination)
        {
            var req = from f in Flights
                      where f.Destination == destination
                      select f.EstimatedDuration;
            return req.Average();
        }
       public IEnumerable<Flight> OrderDurationFlight()
        {
            var req = from f in Flights
                      orderby f.EstimatedDuration descending
                      select f;
            return req;
        }
        public IEnumerable<Traveller>SeniorTravellers(Flight flight)
        {
            var req = from t in flight.Passengers.OfType<Traveller>()

                      orderby t.BirthDate
                      select t;
            return req.Take(3);
            //pour ignorer les 3 premiers skip(3)
        }

        public IEnumerable<IGrouping<string, Flight>> DestinationGroupeFlight()
        {
            var req = from f in Flights
                      group f by f.Destination;
            
            foreach (var g in req)
            {
                Console.WriteLine(g.Key);
                    foreach (var flight in g)
                {
                    Console.WriteLine("decollage:" + flight.FlightDate);
                }
            }
            return req;

        }
    }
}
