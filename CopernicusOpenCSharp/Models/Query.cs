//MIT License

//Copyright(c) 2018 Pedro Henrique de Souza Jesus

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.


using Newtonsoft.Json;
using System;

namespace CopernicusOpenCSharp.Models
{
    public class Query
    {
        [JsonProperty("d")]
        public Respond D { get; set; }
    }

    public class Respond
    {
        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }

    public class Result
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

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
        public DateTime CreationDate { get; set; }

        [JsonProperty("IngestionDate")]
        public DateTime IngestionDate { get; set; }

        [JsonProperty("EvictionDate")]
        public object EvictionDate { get; set; }

        [JsonProperty("ContentDate")]
        public Contentdate ContentDate { get; set; }

        [JsonProperty("Checksum")]
        public Checksum Checksum { get; set; }

        [JsonProperty("ContentGeometry")]
        public string ContentGeometry { get; set; }

        [JsonProperty("Products")]
        public Products Products { get; set; }

        [JsonProperty("Nodes")]
        public Nodes Nodes { get; set; }

        [JsonProperty("Attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("Class")]
        public Class1 Class { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("content_type")]
        public string Content_type { get; set; }

        [JsonProperty("media_src")]
        public string Media_src { get; set; }

        [JsonProperty("edit_media")]
        public string Edit_media { get; set; }
    }

    public class Contentdate
    {
        [JsonProperty("__metadata")]
        public Metadata1 Metadata { get; set; }

        [JsonProperty("Start")]
        public DateTime Start { get; set; }

        [JsonProperty("End")]
        public DateTime End { get; set; }
    }

    public class Metadata1
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Metadata2
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Products
    {
        [JsonProperty("__deferred")]
        public Deferred __Deferred { get; set; }
    }

    public class Deferred
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class Nodes
    {
        [JsonProperty("__deferred")]
        public Deferred1 Deferred { get; set; }
    }

    public class Deferred1
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class Deferred2
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class Class1
    {
        [JsonProperty("__deferred")]
        public Deferred3 Deferred { get; set; }
    }

    public class Deferred3
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
