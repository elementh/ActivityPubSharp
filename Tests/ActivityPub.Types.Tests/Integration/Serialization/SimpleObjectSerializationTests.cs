﻿// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using ActivityPub.Types.AS;
using ActivityPub.Types.AS.Collection;
using ActivityPub.Types.AS.Extended.Actor;
using ActivityPub.Types.AS.Extended.Object;
using ActivityPub.Types.Tests.Util.Fixtures;
using ActivityPub.Types.Util;

namespace ActivityPub.Types.Tests.Integration.Serialization;

public abstract class SimpleObjectSerializationTests : SerializationTests
{
    private SimpleObjectSerializationTests(JsonLdSerializerFixture fixture) : base(fixture) {}

    public class EmptyObject : SimpleObjectSerializationTests
    {
        public EmptyObject(JsonLdSerializerFixture fixture) : base(fixture) {}

        [Fact]
        public void ShouldWriteObject()
        {
            ObjectUnderTest = new ASObject();
            JsonUnderTest.Should().BeJsonObject();
        }

        [Fact]
        public void ShouldIncludeContext()
        {
            ObjectUnderTest = new ASObject();
            JsonUnderTest.Should().HaveStringProperty("@context", "https://www.w3.org/ns/activitystreams");
        }

        [Fact]
        public void ShouldIncludeType()
        {
            ObjectUnderTest = new ASObject();
            JsonUnderTest.Should().HaveASType(ASObjectEntity.ObjectType);
        }

        [Fact]
        public void ShouldIncludeOnlyTypeAndContext()
        {
            ObjectUnderTest = new ASObject();

            var props = JsonUnderTest.EnumerateObject().ToList();

            props.Should().HaveCount(2);
            props.Should().Contain(p => p.Name == "type");
            props.Should().Contain(p => p.Name == "@context");
        }
    }

    public class Subclass : SimpleObjectSerializationTests
    {
        public Subclass(JsonLdSerializerFixture fixture) : base(fixture) {}

        [Fact]
        public void ShouldSerializeToCorrectType()
        {
            ObjectUnderTest = new ImageObject();
            JsonUnderTest.Should().HaveASType(ImageObjectEntity.ImageType);
        }

        [Fact]
        public void ShouldIncludePropertiesFromBaseTypes()
        {
            ObjectUnderTest = new PersonActor
            {
                // From APActor
                Inbox = "https://example.com/actor/inbox",
                Outbox = "https://example.com/actor/outbox",

                // From ASObject
                Image = new ImageObject(),

                // From ASType
                Id = "https://example.com/actor/id"
            };

            JsonUnderTest.Should().HaveStringProperty("inbox", "https://example.com/actor/inbox");
            JsonUnderTest.Should().HaveStringProperty("outbox", "https://example.com/actor/outbox");
            JsonUnderTest.GetProperty("image").Should().HaveASType("Image");
            JsonUnderTest.Should().HaveStringProperty("id", "https://example.com/actor/id");
        }
    }

    public class FullObject : SimpleObjectSerializationTests
    {
        public FullObject(JsonLdSerializerFixture fixture) : base(fixture) {}

        [Fact]
        public void ShouldIncludeAllProperties()
        {
            ObjectUnderTest = new ASObject
            {
                // From ASObject
                Attachment = new LinkableList<ASObject> { new ASObject() },
                Audience = new LinkableList<ASObject> { new ASObject() },
                BCC = new LinkableList<ASObject> { new ASObject() },
                BTo = new LinkableList<ASObject> { new ASObject() },
                CC = new LinkableList<ASObject> { new ASObject() },
                Context = "https://example.com/some/context", // this is the worst field name
                Generator = new ASObject(),
                Icon = new ImageObject(),
                Image = new ImageObject(),
                InReplyTo = new ASObject(),
                Location = new ASObject(),
                Replies = new ASCollection
                {
                    Items = new LinkableList<ASObject> { new ASObject() }
                },
                Tag = new LinkableList<ASObject> { new ASObject() },
                To = new LinkableList<ASObject> { new ASObject() },
                Url = new List<ASLink> { "https://example.com" },
                Content = new NaturalLanguageString("content"),
                Duration = "PT5S",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Published = DateTime.Now,
                Summary = new NaturalLanguageString("summary"),
                Updated = DateTime.Now,
                Source = new ASObject(),
                Likes = "https://example.com/likes.collection",
                Shares = "https://example.com/shares.collection",

                // From ASType
                Id = "https://example.com/some.uri",
                AttributedTo = new LinkableList<ASObject> { new ASObject() },
                Preview = new ASObject(),
                Name = new NaturalLanguageString("name"),
                MediaType = "text/html"
            };

            // From ASObject
            JsonUnderTest.Should().HaveProperty("attachment");
            JsonUnderTest.Should().HaveProperty("audience");
            JsonUnderTest.Should().HaveProperty("bcc");
            JsonUnderTest.Should().HaveProperty("bto");
            JsonUnderTest.Should().HaveProperty("cc");
            JsonUnderTest.Should().HaveProperty("context"); // I hate this property NAME IT SOMETHING ELSE >:(
            JsonUnderTest.Should().HaveProperty("generator");
            JsonUnderTest.Should().HaveProperty("icon");
            JsonUnderTest.Should().HaveProperty("image");
            JsonUnderTest.Should().HaveProperty("inReplyTo");
            JsonUnderTest.Should().HaveProperty("location");
            JsonUnderTest.Should().HaveProperty("replies");
            JsonUnderTest.Should().HaveProperty("tag");
            JsonUnderTest.Should().HaveProperty("to");
            JsonUnderTest.Should().HaveProperty("url");
            JsonUnderTest.Should().HaveProperty("content");
            JsonUnderTest.Should().HaveProperty("duration");
            JsonUnderTest.Should().HaveProperty("startTime");
            JsonUnderTest.Should().HaveProperty("endTime");
            JsonUnderTest.Should().HaveProperty("published");
            JsonUnderTest.Should().HaveProperty("summary");
            JsonUnderTest.Should().HaveProperty("updated");
            JsonUnderTest.Should().HaveProperty("source");
            JsonUnderTest.Should().HaveProperty("likes");
            JsonUnderTest.Should().HaveProperty("shares");

            // From ASType
            JsonUnderTest.Should().HaveProperty("id");
            JsonUnderTest.Should().HaveProperty("attributedTo");
            JsonUnderTest.Should().HaveProperty("preview");
            JsonUnderTest.Should().HaveProperty("name");
            JsonUnderTest.Should().HaveProperty("mediaType");
        }
    }
}