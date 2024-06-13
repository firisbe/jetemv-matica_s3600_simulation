// Decompiled with JetBrains decompiler
// Type: MaticaS3600Simulation.Models.MachineStatus
// Assembly: MaticaS3600Simulation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C6FF613-67D2-4F79-AC1D-EDD371CB1003
// Assembly location: G:\MaticaS3600Simulation\MaticaS3600Simulation.dll

#nullable disable
namespace MaticaS3600Simulation.Models
{
  public class MachineStatus
  {
    public string machine_status { get; set; } = "READY";

    public string card_inside { get; set; } = "yes";

    public string cover_open { get; set; } = "no";

    public string error_message { get; set; } = "";

    public string feeder_0_card_presence { get; set; } = "no";

    public string feeder_1_card_presence { get; set; } = "yes";

    public string feeder_2_card_presence { get; set; } = "yes";

    public string feeder_3_card_presence { get; set; } = "yes";

    public string feeder_4_card_presence { get; set; } = "no";

    public string feeder_5_card_presence { get; set; } = "no";

    public string feeder_6_card_presence { get; set; } = "no";

    public string tipper_status { get; set; } = "Ready";

    public string tipper_temperature { get; set; } = "140";

    public string tipper_near_end { get; set; } = "no";

    public string tipper_end_ribbon { get; set; } = "no";

    public string rear_infiller_near_end { get; set; } = "yes";

    public string rear_infiller_end_ribbon { get; set; } = "no";

    public string top_infiller_near_end { get; set; } = "no";

    public string top_infiller_end_ribbon { get; set; } = "no";

    public string ribbon_type { get; set; } = "K";

    public string ribbon_card_left { get; set; } = "1528";
  }
}
