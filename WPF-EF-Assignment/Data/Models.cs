using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_EF_Assignment.Data
{
    public class ReagentLot
    {
        [Key]
        [Required(ErrorMessage = "Serial Number is mandatory")]
        public string SerialNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Name { get; set; }
        public double Volume { get; set; }
        public double ReactionTarget { get; set; }
        public double ReactionRange { get; set; }

        //ForeignKey Property
        public string BatchLotNumber { get; set; }
        public ReagentBatch ReagentBatch { get; set; }
    }
    public class ReagentBatch
    {
        [Key]
        //Batch lot number (Alpha-numeric – first 3 characters are a country code, next 5 are digits representing the source factory code, last 10 digits are the batch serial number)
        [Required(ErrorMessage = "Batch Lot Number is mandatory")]
        public string BatchLotNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ManufacturerDate { get; set; }
        public string ManufacturingSourceCode { get; set; }

        public ICollection<ReagentLot> ReagentLot { get; set; }
    }
}
