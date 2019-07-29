﻿using System;

namespace SIGLO.Shared.Library
{
    class Utils
    {
        private static long ToUnixEpochDate(DateTime date) => 
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
