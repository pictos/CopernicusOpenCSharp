using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace CopernicusOpenCSharp.Models
{
    public class QueryId
    {
        [JsonProperty("d")]
        public D D { get; set; }
    }

    public class D
    {
        [JsonProperty("__metadata")]
        public DMetadata Metadata { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ContentType")]
        public string ContentType { get; set; }

        [JsonProperty("ContentLength")]
        public string ContentLength { get; set; }

        [JsonProperty("ChildrenNumber")]
        public string ChildrenNumber { get; set; }

        [JsonProperty("Value")]
        public object Value { get; set; }

        [JsonProperty("CreationDate")]
        public string CreationDate { get; set; }

        [JsonProperty("IngestionDate")]
        public string IngestionDate { get; set; }

        [JsonProperty("EvictionDate")]
        public object EvictionDate { get; set; }

        [JsonProperty("ContentDate")]
        public ContentDate ContentDate { get; set; }

        [JsonProperty("Checksum")]
        public Checksum Checksum { get; set; }

        [JsonProperty("ContentGeometry")]
        public string ContentGeometry { get; set; }

        [JsonProperty("Products")]
        public Attributes Products { get; set; }

        [JsonProperty("Nodes")]
        public Attributes Nodes { get; set; }

        [JsonProperty("Attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("Class")]
        public Attributes Class { get; set; }
    }

    public class Attributes
    {
        [JsonProperty("__deferred")]
        public Deferred Deferred { get; set; }
    }

    public class Checksum
    {
        [JsonProperty("__metadata")]
        public ChecksumMetadata Metadata { get; set; }

        [JsonProperty("Algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }

    public class ChecksumMetadata
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class ContentDate
    {
        [JsonProperty("__metadata")]
        public ChecksumMetadata Metadata { get; set; }

        [JsonProperty("Start")]
        public string Start { get; set; }

        [JsonProperty("End")]
        public string End { get; set; }
    }

    public class DMetadata
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("media_src")]
        public string MediaSrc { get; set; }

        [JsonProperty("edit_media")]
        public string EditMedia { get; set; }
    }


    internal class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
