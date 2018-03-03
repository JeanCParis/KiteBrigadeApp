using System;
using Realms;
using Newtonsoft.Json;

namespace KiteBrigade.Model
{
    // to avoid RealmObject property exposition, this object is serialized with OptIn option
    [JsonObject(MemberSerialization.OptIn)]
    public class KiteSession : RealmObject
    {
        [PrimaryKey, JsonIgnore]
        public String MobileId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("creationDate")]
        public DateTimeOffset CreationDate { get; set; }

        [JsonProperty("windValue")]
        public int WindValue { get; set; }

        [JsonProperty("waterValue")]
        public int WaterValue { get; set; }

        [JsonProperty("funValue")]
        public int FunValue { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("locationLatitude")]
        public double? LocationLatitude { get; set; }

        [JsonProperty("locationLongitude")]
        public double? LocationLongitude { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("pictureUri")]
        public string PictureUri { get; set; }

        public KiteSession()
        {
            CreationDate = DateTimeOffset.Now;
        }

        public KiteSession(KiteSession session)
        {
            CreationDate = new DateTimeOffset(session.CreationDate.DateTime);
            WindValue = session.WindValue;
            WaterValue = session.WaterValue;
            FunValue = session.FunValue;
            Location = session.Location;
            LocationLatitude = session.LocationLatitude;
            LocationLongitude = session.LocationLongitude;
            Duration = session.Duration;
            PictureUri = session.PictureUri;
        }
    }
}
