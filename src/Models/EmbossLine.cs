// Decompiled with JetBrains decompiler
// Type: MaticaS3600Simulation.Models.EmbossLine
// Assembly: MaticaS3600Simulation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C6FF613-67D2-4F79-AC1D-EDD371CB1003
// Assembly location: G:\MaticaS3600Simulation\MaticaS3600Simulation.dll

using System.Text.Json.Serialization;

#nullable disable
namespace MaticaS3600Simulation.Models
{
  public class EmbossLine
  {
    [JsonPropertyName("font")]
    public string Font { get; set; }

    [JsonPropertyName("cpi")]
    public string Cpi { get; set; }

    [JsonPropertyName("x")]
    public string X { get; set; }

    [JsonPropertyName("y")]
    public string Y { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }
  }
}
