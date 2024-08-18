// Decompiled with JetBrains decompiler
// Type: MaticaS3600Simulation.Models.CommandRequest
// Assembly: MaticaS3600Simulation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C6FF613-67D2-4F79-AC1D-EDD371CB1003
// Assembly location: G:\MaticaS3600Simulation\MaticaS3600Simulation.dll

using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable
namespace MaticaS3600Simulation.Models
{
    public class CommandRequest
    {
        public string Command { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string APDU { get; set; }

        [JsonPropertyName("MagData")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MagneticData Magnetic_Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Track_ID { get; set; }

        [JsonPropertyName("Emboss_Line")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<EmbossLine> Emboss_Line { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Tipper_ON { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Chip_Station { get; set; }
        public string Front_Color { get; set; }
        public string Front_Black { get; set; }
        public string Front_Overlay { get; set; }
        public string Rear_Color { get; set; }
        public string Rear_Black { get; set; }
        public string Rear_Overlay { get; set; }
    }
}
