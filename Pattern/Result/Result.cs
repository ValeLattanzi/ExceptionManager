﻿using System.Diagnostics.CodeAnalysis;

namespace ExceptionManager.Pattern.Result;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
            throw new ArgumentException("Success result can't have an error.");

        IsSuccess = isSuccess;
        Error = error;
    }

    public Error Error { get; }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    public static Result<T> Success<T>(T value)
    {
        return new Result<T>(value, true, Error.None);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }

    public static Result<T> Failure<T>(Error error)
    {
        return new Result<T>(default, false, error);
    }
}

public class Result<T> : Result
{
    private readonly T? _value;

    public Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    [NotNull]
    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can't be accessed.");

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, true, Error.None);
    }

    public new static Result<T> Failure(Error error)
    {
        return new Result<T>(default, false, error);
    }

    public static Result<T> ValidationFailure(Error error)
    {
        return new Result<T>(default, false, error);
    }

    public static implicit operator Result<T>(T? value)
    {
        return value is null ? Failure(Error.NullValue) : Success(value);
    }
}