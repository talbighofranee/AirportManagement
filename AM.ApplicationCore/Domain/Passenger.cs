﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Domain
{
    public class Passenger
    {
        public int Id { get; set; }
        public string PassportNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string EmailAdress { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Flight> Flights { get; set; }
       
        public bool CheckProfile(string prenom, string nom,string email=null)
        {
            if(email!=null)
            return (prenom.Equals(FirstName) && nom.Equals(LastName)&&email.Equals(EmailAdress));
            else
                return (prenom.Equals(FirstName) && nom.Equals(LastName));

        }
        public virtual void PassengerType()
        {
            Console.WriteLine("i am a passenger");
        }
    }
}