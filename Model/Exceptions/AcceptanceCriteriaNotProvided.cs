﻿namespace ExceptionManager.Model.Exceptions;

public class AcceptanceCriteriaNotProvided : Exception
{
    public AcceptanceCriteriaNotProvided(Exception? innerException = null) : base(
        "Any acceptance criteria was provided.", innerException)
    {
    }
}