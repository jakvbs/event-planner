﻿namespace BlazorCalendar.Application.Features.Identity.Commands;

public class RegisterUserResponse
{
    private RegisterUserResponse()
    {
    }

    public string? UserId { get; private init; }
    public string? Error { get; private init; }

    public static RegisterUserResponse Success(string userId)
    {
        return new RegisterUserResponse { UserId = userId };
    }

    public static RegisterUserResponse Fail(string error)
    {
        return new RegisterUserResponse { Error = error };
    }
}