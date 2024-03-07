using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Domain
{
    public class Ticket
    {
        public double Prix {  get; set; }
        public string Siege { get; set; }
        public Boolean VIP {  get; set; }
        public Passenger Passenger { get; set; }

        [ForeignKey(nameof(Passenger))]
        public string PassengerFK {  get; set; }
        public Flight Flight { get; set; }

        [ForeignKey(nameof(Flight))]
        public int FlightFK { get; set; }
    }
}
