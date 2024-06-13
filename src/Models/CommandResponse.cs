// Decompiled with JetBrains decompiler
// Type: MaticaS3600Simulation.Models.CommandResponse
// Assembly: MaticaS3600Simulation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C6FF613-67D2-4F79-AC1D-EDD371CB1003
// Assembly location: G:\MaticaS3600Simulation\MaticaS3600Simulation.dll

using System.Text.Json.Serialization;

#nullable disable
namespace MaticaS3600Simulation.Models
{
  public class CommandResponse
  {
    [JsonPropertyName("answer")]
    public string Answer { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("error")]
    public ErrorResponse Error { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("data")]
    public string Data { get; set; }

    [JsonPropertyName("Machine_Configuration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MachineConfiguration Machine_Configuration { get; set; }

    [JsonPropertyName("Machine_Status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MachineStatus Machine_Status { get; set; }
  }
}
