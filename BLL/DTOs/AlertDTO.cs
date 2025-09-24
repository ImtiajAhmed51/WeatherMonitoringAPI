using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class AlertDTO
    {
        public int Id { get; set; }
        public string Condition { get; set; }
        public string Message { get; set; }
        public string Severity { get; set; }
        public DateTime TriggeredAt { get; set; }
        public bool IsActive { get; set; }
        public int LocationId { get; set; }
    }
}
