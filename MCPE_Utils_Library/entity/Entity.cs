using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.entity
{
    public abstract class Entity
    {
        public virtual int NETWORK_ID { get { return -1; } }

        public abstract int getNetworkId();

        public const int DATA_TYPE_BYTE = 0;
        public const int DATA_TYPE_SHORT = 1;
        public const int DATA_TYPE_INT = 2;
        public const int DATA_TYPE_FLOAT = 3;
        public const int DATA_TYPE_STRING = 4;
        public const int DATA_TYPE_SLOT = 5;
        public const int DATA_TYPE_POS = 6;
        public const int DATA_TYPE_LONG = 7;

        public const int DATA_FLAGS = 0;
        public const int DATA_AIR = 1;
        public const int DATA_NAMETAG = 2;
        public const int DATA_SHOW_NAMETAG = 3;
        public const int DATA_SILENT = 4;
        public const int DATA_POTION_COLOR = 7;
        public const int DATA_POTION_AMBIENT = 8;
        public const int DATA_NO_AI = 15;

        public const int DATA_FLAG_ONFIRE = 0;
        public const int DATA_FLAG_SNEAKING = 1;
        public const int DATA_FLAG_RIDING = 2;
        public const int DATA_FLAG_SPRINTING = 3;
        public const int DATA_FLAG_ACTION = 4;
        public const int DATA_FLAG_INVISIBLE = 5;

        public const int DATA_LEAD_HOLDER = 23;
        public const int DATA_LEAD = 24;
    }
}
