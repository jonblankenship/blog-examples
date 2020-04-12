﻿using System;

namespace ErrorHandlingMiddlewareSample.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        { }
    }
}
