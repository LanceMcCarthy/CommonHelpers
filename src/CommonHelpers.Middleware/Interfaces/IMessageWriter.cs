namespace CommonHelpers.Middleware.Interfaces;

public interface IMessageWriter
{
    void Write(string message);

    void Write(string message, params object[] extraParameters);
}