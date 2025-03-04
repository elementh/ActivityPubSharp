﻿// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using ActivityPub.Types.Attributes;
using ActivityPub.Types.Util;
using JetBrains.Annotations;

namespace ActivityPub.Types.AS;

/// <summary>
///     An object that implements the required properties of an ActivityPub Actor.
/// </summary>
/// <remarks>
///     This is a synthetic class included for utility.
///     It does not exist in the ActivityStreams or ActivityPub standards.
/// </remarks>
/// <seealso href="https://www.w3.org/TR/activitypub/#actor-objects" />
public class ASActor : ASObject
{
    public ASActor() => Entity = new ASActorEntity
    {
        TypeMap = TypeMap,
        Inbox = null!,
        Outbox = null!
    };

    public ASActor(TypeMap typeMap) : base(typeMap) => Entity = TypeMap.AsEntity<ASActorEntity>();
    private ASActorEntity Entity { get; }


    /// <summary>
    ///     A reference to an ActivityStreams OrderedCollection comprised of all the messages received by the actor.
    ///     The inbox stream contains all activities received by the actor.
    /// </summary>
    /// <seealso href="https://www.w3.org/TR/activitypub/#inbox" />
    public required ASLink Inbox
    {
        get => Entity.Inbox;
        set => Entity.Inbox = value;
    }

    /// <summary>
    ///     A reference to an ActivityStreams OrderedCollection comprised of all the messages produced by the actor.
    ///     The outbox stream contains activities the user has published, subject to the ability of the requester to retrieve the activity.
    /// </summary>
    /// <seealso href="https://www.w3.org/TR/activitypub/#outbox" />
    public required ASLink Outbox
    {
        get => Entity.Outbox;
        set => Entity.Outbox = value;
    }

    /// <summary>
    ///     A reference to an ActivityStreams collection of the actors that this actor is following.
    ///     This is a list of everybody that the actor has followed, added as a side effect.
    /// </summary>
    /// <seealso href="https://www.w3.org/TR/activitypub/#following" />
    public ASLink? Following
    {
        get => Entity.Following;
        set => Entity.Following = value;
    }

    /// <summary>
    ///     A reference to an ActivityStreams collection of the actors that follow this actor.
    ///     This is a list of everyone who has sent a Follow activity for the actor, added as a side effect.
    /// </summary>
    /// <seealso href="https://www.w3.org/TR/activitypub/#followers" />
    public ASLink? Followers
    {
        get => Entity.Followers;
        set => Entity.Followers = value;
    }

    /// <summary>
    ///     A reference to an ActivityStreams collection of objects this actor has liked.
    ///     This is a list of every object from all of the actor's Like activities, added as a side effect.
    /// </summary>
    /// <seealso href="https://www.w3.org/TR/activitypub/#liked" />
    public ASLink? Liked
    {
        get => Entity.Liked;
        set => Entity.Liked = value;
    }

    /// <summary>
    ///     A list of supplementary Collections which may be of interest.
    /// </summary>
    /// <remarks>
    ///     Not sure what type this is.
    ///     Maybe a link to a collection?
    /// </remarks>
    public ASType? Streams
    {
        get => Entity.Streams;
        set => Entity.Streams = value;
    }

    /// <summary>
    ///     A short username which may be used to refer to the actor, with no uniqueness guarantees.
    /// </summary>
    public NaturalLanguageString? PreferredUsername
    {
        get => Entity.PreferredUsername;
        set => Entity.PreferredUsername = value;
    }

    /// <summary>
    ///     A json object which maps additional (typically server/domain-wide) endpoints which may be useful either for this actor or someone referencing this actor.
    ///     This mapping may be nested inside the actor document as the value or may be a link to a JSON-LD document with these properties.
    /// </summary>
    /// <remarks>
    ///     This should technically be a Linkable{ActorEndpoints}, but ActorEndpoints does not extend ASType
    /// </remarks>
    public Linkable<ActorEndpoints>? Endpoints
    {
        get => Entity.Endpoints;
        set => Entity.Endpoints = value;
    }
}

/// <inheritdoc cref="ASActor" />
[ImpliesOtherEntity(typeof(ASObjectEntity))]
public sealed class ASActorEntity : ASEntity<ASActor>
{
    /// <inheritdoc cref="ASActor.Inbox" />
    [JsonPropertyName("inbox")]
    public required ASLink Inbox { get; set; }

    /// <inheritdoc cref="ASActor.Outbox" />
    [JsonPropertyName("outbox")]
    public required ASLink Outbox { get; set; }

    /// <inheritdoc cref="ASActor.Following" />
    [JsonPropertyName("following")]
    public ASLink? Following { get; set; }

    /// <inheritdoc cref="ASActor.Followers" />
    [JsonPropertyName("followers")]
    public ASLink? Followers { get; set; }

    /// <inheritdoc cref="ASActor.Liked" />
    [JsonPropertyName("liked")]
    public ASLink? Liked { get; set; }

    /// <inheritdoc cref="ASActor.Streams" />
    [JsonPropertyName("streams")]
    public ASType? Streams { get; set; }

    /// <inheritdoc cref="ASActor.PreferredUsername" />
    [JsonPropertyName("preferredUsername")]
    public NaturalLanguageString? PreferredUsername { get; set; }

    /// <inheritdoc cref="ASActor.Endpoints" />
    [JsonPropertyName("endpoints")]
    public Linkable<ActorEndpoints>? Endpoints { get; set; }
}

/// <summary>
///     A json object which maps additional (typically server/domain-wide) endpoints which may be useful for an actor.
/// </summary>
/// <seealso href="https://www.w3.org/TR/activitypub/#actor-objects" />
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
public class ActorEndpoints
{
    /// <summary>
    ///     Endpoint URI so this actor's clients may access remote ActivityStreams objects which require authentication to access.
    ///     To use this endpoint, the client posts an x-www-form-urlencoded id parameter with the value being the id of the requested ActivityStreams object.
    /// </summary>
    [JsonPropertyName("proxyUrl")]
    public ASLink? ProxyUrl { get; set; }

    /// <summary>
    ///     If OAuth 2.0 bearer tokens [RFC6749] [RFC6750] are being used for authenticating client to server interactions, this endpoint specifies a URI at which a browser-authenticated user may obtain a new authorization grant.
    /// </summary>
    [JsonPropertyName("oauthAuthorizationEndpoint")]
    public ASLink? OAuthAuthorizationEndpoint { get; set; }

    /// <summary>
    ///     If OAuth 2.0 bearer tokens [RFC6749] [RFC6750] are being used for authenticating client to server interactions, this endpoint specifies a URI at which a client may acquire an access token.
    /// </summary>
    [JsonPropertyName("oauthTokenEndpoint")]
    public ASLink? OAuthTokenEndpoint { get; set; }

    /// <summary>
    ///     If Linked Data Signatures and HTTP Signatures are being used for authentication and authorization, this endpoint specifies a URI at which browser-authenticated users may authorize a client's public key for client to server interactions.
    /// </summary>
    [JsonPropertyName("provideClientKey")]
    public ASLink? ProvideClientKey { get; set; }

    /// <summary>
    ///     If Linked Data Signatures and HTTP Signatures are being used for authentication and authorization, this endpoint specifies a URI at which a client key may be signed by the actor's key for a time window to act on behalf of the actor in interacting with foreign servers.
    /// </summary>
    [JsonPropertyName("signClientKey")]
    public ASLink? SignClientKey { get; set; }

    /// <summary>
    ///     An optional endpoint used for wide delivery of publicly addressed activities and activities sent to followers.
    ///     SharedInbox endpoints SHOULD also be publicly readable OrderedCollection objects containing objects addressed to the Public special collection.
    ///     Reading from the sharedInbox endpoint MUST NOT present objects which are not addressed to the Public endpoint.
    /// </summary>
    [JsonPropertyName("sharedInbox")]
    public ASLink? SharedInbox { get; set; }
}