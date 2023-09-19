using System;

namespace NewAvalon.Abstractions.Contracts
{
    public sealed record GenerateTokenRequest(string Email, Guid Id);
}
