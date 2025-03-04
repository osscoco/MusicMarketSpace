using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Common
{
    public class RevokedToken
    {
        public int Id { get; set; }
        public required string Token { get; set; }
        public DateTime RevokedAt { get; set; }
    }
}
