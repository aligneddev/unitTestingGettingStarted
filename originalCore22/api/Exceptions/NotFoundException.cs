﻿using System;

namespace Api.Exceptions
{
    /// <summary>
    /// Custom exception for something that wasn't found
    /// </summary>
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}