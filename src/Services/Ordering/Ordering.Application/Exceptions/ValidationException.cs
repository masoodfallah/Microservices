﻿using FluentValidation.Results;

namespace Ordering.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public ValidationException() : base("One or more validation failures have occured.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> errors) : this()
    {
        Errors = errors
            .GroupBy(x => x.PropertyName, x => x.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}
