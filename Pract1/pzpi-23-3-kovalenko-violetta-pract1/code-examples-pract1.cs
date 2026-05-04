using System;

public interface IHandler
{
    void Handle(Request request);
}

public class Request
{
    public string Type { get; set; }
}

public abstract class BaseHandler : IHandler
{
    protected IHandler _next;

    protected BaseHandler(IHandler next)
    {
        _next = next;
    }

    public virtual void Handle(Request request)
    {
        if (_next != null)
        {
            _next.Handle(request);
        }
    }
}

public class AuthHandler : BaseHandler
{
    public AuthHandler(IHandler next) : base(next) { }

    public override void Handle(Request request)
    {
        if (request.Type == "Auth")
        {
            Console.WriteLine("Авторизація виконана");
        }
        else
        {
            base.Handle(request);
        }
    }
}

public class ValidationHandler : BaseHandler
{
    public ValidationHandler(IHandler next) : base(next) { }

    public override void Handle(Request request)
    {
        if (request.Type == "Validation")
        {
            Console.WriteLine("Дані перевірені");
        }
        else
        {
            base.Handle(request);
        }
    }
}

class Program
{
    static void Main()
    {
        IHandler handler = new AuthHandler(
            new ValidationHandler(null));

        var request = new Request { Type = "Validation" };

        handler.Handle(request);
    }
}
