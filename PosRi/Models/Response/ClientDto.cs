using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Models.Response
{
    public class ClientDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Rfc { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime CreationDate { get; set; }

        public StateDto State { get; set; }
    }
}
