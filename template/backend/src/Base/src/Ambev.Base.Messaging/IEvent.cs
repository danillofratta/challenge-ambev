﻿namespace Ambev.Base.Messaging;

public interface IEvent
{
    Guid Id { get; }
    DateTime Timestamp { get; }
}