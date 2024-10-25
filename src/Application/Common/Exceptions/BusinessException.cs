namespace Application.Common.Exceptions;

public sealed class BusinessException(string? message) : Exception(message) { }
