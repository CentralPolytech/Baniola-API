using System;
using System.Collections.Generic;

namespace InterventionAPI.Models
{
    public partial class TblClient
    {
        public int Code { get; set; }
        public string CIN { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string NomPrenom { get; set; }
    }
}
