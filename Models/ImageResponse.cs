using System;
using System.Runtime.Serialization;

namespace picturme_wp7
{
    [DataContract]
    public class ImageResponse
    {
        [DataMember(Name="path")]
        public String Path { get; set; }
        [DataMember(Name="image")]
        public String Image { get; set; }
        [DataMember(Name="thumbnail")]
        public String Thumbnail { get; set; }
        [DataMember(Name="success")]
        public bool Success { get; set; }
    }
}
