namespace ActivityPub.Common.Types.Extended.Activity;

/// <summary>
/// Indicates that the actor likes, recommends or endorses the object.
/// The target and origin typically have no defined meaning.
/// </summary>
public class LikeActivity : ASActivity
{
    public LikeActivity(string type = "Like") : base(type) {}
}