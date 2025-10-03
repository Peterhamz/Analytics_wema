using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpiWorker.Models
{
    public class KpiSnapshot
    {
        public int Id { get; set; }
        public DateTime SnapshotDate { get; set; }
        public int TotalAccounts { get; set; }
        public int ActiveAccounts { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalBalance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AverageBalance { get; set; }
    }
}
