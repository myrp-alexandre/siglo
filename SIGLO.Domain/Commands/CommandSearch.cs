﻿using SIGLO.Shared.Commands;

namespace SIGLO.Domain.Commands
{
    public class CommandSearch : ICommandSearch
    {
        public string Filter { get; set; }
        public string Order { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
