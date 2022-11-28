namespace ActivityPub.Common.Types.Extended.Activity;

/// <summary>
/// Indicates that the actor has moved object from origin to target.
/// If the origin or target are not specified, either can be determined by context. 
/// </summary>
public class MoveActivity : ASActivity
{
    public MoveActivity(string type = "Move") : base(type) {}
}