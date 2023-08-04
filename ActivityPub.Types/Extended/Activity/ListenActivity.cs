// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using ActivityPub.Types.Attributes;

namespace ActivityPub.Types.Extended.Activity;

/// <summary>
/// Indicates that the actor has listened to the object. 
/// </summary>
public class ListenActivity : ASTransitiveActivity
{
    private ListenActivityEntity Entity { get; }

    public ListenActivity() => Entity = new ListenActivityEntity { TypeMap = TypeMap };
    public ListenActivity(TypeMap typeMap) : base(typeMap) => Entity = TypeMap.AsEntity<ListenActivityEntity>();
}

/// <inheritdoc cref="ListenActivity"/>
[ASTypeKey(ListenType)]
[ImpliesOtherEntity(typeof(ASTransitiveActivityEntity))]
public sealed class ListenActivityEntity : ASBase<ListenActivity>
{
    public const string ListenType = "Listen";
    public override string ASTypeName => ListenType;

    public override IReadOnlySet<string> ReplacesASTypes { get; } = new HashSet<string>()
    {
        ASActivityEntity.ActivityType
    };
}