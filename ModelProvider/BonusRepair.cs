using System;
using System.Collections.Generic;
using System.Text;

namespace ModelProvider
{
    public class BonusRepair
    {
        public long RepairID { get; set; }
        public virtual Repair Repair { get; set; }

        public long BonusName { get; set; }

        public virtual Bonus Bonus { get; set; }
    }
}
