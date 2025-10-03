using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpiWorker.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string Status { get; set; }  // Active/Inactive

        [Column(TypeName = "decimal(18,2)")]   
        public decimal Balance { get; set; }
    }
}
