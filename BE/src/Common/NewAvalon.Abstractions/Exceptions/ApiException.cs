using System;
using System.Collections.Generic;

namespace NewAvalon.Abstractions.Exceptions
{
    public sealed class ApiException : Exception
    {
        public ApiException(string title, int type, int status, string detail, Dictionary<string, string[]> errors = default)
            : base($"{type}: {detail}")
        {
            Title = title;
            Type = type;
            Status = status;
            Detail = detail;
            Errors = errors ?? new();
        }

        public string Title { get; }

        public int Type { get; }

        public int Status { get; }

        public string Detail { get; }

        public Dictionary<string, string[]> Errors { get; }
    }
}
