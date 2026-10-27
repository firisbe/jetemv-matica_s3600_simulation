using MaticaS3600Simulation.Models;

#nullable disable
namespace MaticaS3600Simulation
{
  public static class StaticValues
  {
    public static MagneticData MagneticData { get; set; }

    public static List<EmbossLine> EmbossLines { get; set; }

    // Son JobSettings ile gelen kontrast değerleri. Print çağrısında loglanır.
    public static string BlackContrast { get; set; }

    public static string ColorContrast { get; set; }

    public static string OverlayContrast { get; set; }

    // Kart makinenin içinde mi? LoadCard -> true, EjectCard/Restore -> false. GetInfoJson card_inside bu değeri döner.
    public static bool CardInside { get; set; }
  }
}
