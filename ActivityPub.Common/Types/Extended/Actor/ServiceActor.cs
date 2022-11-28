namespace ActivityPub.Common.Types.Extended.Actor;

/// <summary>
/// Represents a service of any kind.
/// </summary>
public class ServiceActor : ASObject
{
    public ServiceActor(string type = "Service") : base(type) {}
}