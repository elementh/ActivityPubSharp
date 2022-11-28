namespace ActivityPub.Common.Types.Extended.Activity;

/// <summary>
/// Indicates that the actor has viewed the object. 
/// </summary>
public class ViewActivity : ASActivity
{
    public ViewActivity(string type = "View") : base(type) {}
}