﻿using System;

namespace NHM.Common.Enums
{
    //[Obsolete("UNUSED", true)]
    public enum NhmConectionType
    {
        NONE,
        STRATUM_TCP,
        STRATUM_SSL,
        [Obsolete("UNUSED", true)]
        LOCKED, // inhouse miners that are locked on NH (our eqm)
        [Obsolete("UNUSED", true)]
        SSL
    }
}
