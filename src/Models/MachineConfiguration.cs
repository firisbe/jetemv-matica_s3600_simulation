// Decompiled with JetBrains decompiler
// Type: MaticaS3600Simulation.Models.MachineConfiguration
// Assembly: MaticaS3600Simulation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C6FF613-67D2-4F79-AC1D-EDD371CB1003
// Assembly location: G:\MaticaS3600Simulation\MaticaS3600Simulation.dll

#nullable disable
namespace MaticaS3600Simulation.Models
{
  public class MachineConfiguration
  {
    public string machine_model { get; set; } = "S3300e";

    public string machine_name { get; set; } = "S3300E MATICA";

    public string machine_sn { get; set; } = "1234567890";

    public string number_of_feeders { get; set; } = "6";

    public string card_exit { get; set; } = "2";

    public string card_reject { get; set; } = "1";

    public string card_counter { get; set; } = "1234";
  }
}
