// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using ActivityPub.Types.Attributes;

namespace ActivityPub.Types.Extended.Actor;

/// <summary>
/// Represents an individual person. 
/// </summary>
public class PersonActor : ASActor
{
    private PersonActorEntity Entity { get; }

    public PersonActor() => Entity = new PersonActorEntity { TypeMap = TypeMap };
    public PersonActor(TypeMap typeMap) : base(typeMap) => Entity = TypeMap.AsEntity<PersonActorEntity>();
}

/// <inheritdoc cref="PersonActor"/>
[ASTypeKey(PersonType)]
[ImpliesOtherEntity(typeof(ASActorEntity))]
public sealed class PersonActorEntity : ASBase<PersonActor>
{
    public const string PersonType = "Person";
    public override string ASTypeName => PersonType;

    public override IReadOnlySet<string> ReplacesASTypes { get; } = new HashSet<string>()
    {
        ASObjectEntity.ObjectType
    };
}