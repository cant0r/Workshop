using System;
using System.Collections.Generic;
using System.Text;

namespace ModelProvider.Models
{
    public class BonusRepair : IModell
    {
        public long RepairID { get; set; }
        public virtual Repair Repair { get; set; }

        public string BonusName { get; set; }

        public virtual Bonus Bonus { get; set; }
    }
}
