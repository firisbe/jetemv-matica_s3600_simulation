using Microsoft.Extensions.Logging;
using PCSC;
using PCSC.Utils;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace MaticaS3600Simulation.SmartCard
{
  public static class PCSCReader
  {
    private static string ReaderName;
    private static SCardReader scReader;
    private static SCardError scErr;
    private static SCardProtocol proto;
    private static ILogger logger;

    public static void SetPCSCReader(string readerName, ILogger _logger)
    {
      PCSCReader.ReaderName = readerName;
      PCSCReader.logger = _logger;
    }

    public static bool ConnectToCard(out byte[] atr, out string errMsg)
    {
      bool card = true;
      atr = new byte[0];
      SCardContext context = new SCardContext();
      context.Establish(SCardScope.System);
      PCSCReader.scReader = new SCardReader((ISCardContext) context);
      PCSCReader.scErr = PCSCReader.scReader.Connect(PCSCReader.ReaderName, SCardShareMode.Shared, SCardProtocol.Any);
      DefaultInterpolatedStringHandler interpolatedStringHandler;
      if (PCSCReader.scErr == SCardError.Success)
      {
        ILogger logger = PCSCReader.logger;
        interpolatedStringHandler = new DefaultInterpolatedStringHandler(77, 3);
        interpolatedStringHandler.AppendLiteral("[");
        interpolatedStringHandler.AppendFormatted(PCSCReader.ReaderName);
        interpolatedStringHandler.AppendLiteral("] Called 'reader.ConnectToCard' => SCardReader CardHandle: ");
        interpolatedStringHandler.AppendFormatted<IntPtr>(PCSCReader.scReader.CardHandle);
        interpolatedStringHandler.AppendLiteral(" => IsConnected: ");
        interpolatedStringHandler.AppendFormatted<bool>(PCSCReader.scReader.IsConnected);
        string stringAndClear = interpolatedStringHandler.ToStringAndClear();
        object[] objArray = Array.Empty<object>();
        logger.LogInformation(stringAndClear, objArray);
        PCSCReader.scErr = PCSCReader.scReader.Reconnect(SCardShareMode.Shared, SCardProtocol.Any, SCardReaderDisposition.Unpower);
        if (PCSCReader.scErr == SCardError.Success)
        {
          int num1 = (int) PCSCReader.scReader.Status(out string[] _, out SCardState _, out PCSCReader.proto, out atr);
          int num2 = (int) PCSCReader.scReader.BeginTransaction();
        }
      }
      if (PCSCReader.scErr != SCardError.Success)
      {
        context.Dispose();
        PCSCReader.scReader.Dispose();
        atr = new byte[0];
        card = false;
      }
      //ref string local = ref errMsg;
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 3);
      interpolatedStringHandler.AppendLiteral("[");
      interpolatedStringHandler.AppendFormatted(PCSCReader.ReaderName);
      interpolatedStringHandler.AppendLiteral("] ");
      interpolatedStringHandler.AppendFormatted(SCardHelper.StringifyError(PCSCReader.scErr));
      interpolatedStringHandler.AppendLiteral(" Error Code:");
      interpolatedStringHandler.AppendFormatted<SCardError>(PCSCReader.scErr);
      interpolatedStringHandler.AppendLiteral(".");
      string stringAndClear1 = interpolatedStringHandler.ToStringAndClear();
      errMsg = stringAndClear1;
      return card;
    }

    public static bool DisconnectToCard()
    {
      bool card = true;
      ILogger logger = PCSCReader.logger;
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(80, 3);
      interpolatedStringHandler.AppendLiteral("[");
      interpolatedStringHandler.AppendFormatted(PCSCReader.ReaderName);
      interpolatedStringHandler.AppendLiteral("] Called 'reader.DisconnectToCard' => SCardReader CardHandle: ");
      interpolatedStringHandler.AppendFormatted<IntPtr>(PCSCReader.scReader.CardHandle);
      interpolatedStringHandler.AppendLiteral(" => IsConnected: ");
      interpolatedStringHandler.AppendFormatted<bool>(PCSCReader.scReader.IsConnected);
      string stringAndClear = interpolatedStringHandler.ToStringAndClear();
      object[] objArray = Array.Empty<object>();
      logger.LogInformation(stringAndClear, objArray);
      int num1 = (int) PCSCReader.scReader.EndTransaction(SCardReaderDisposition.Unpower);
      int num2 = (int) PCSCReader.scReader.Disconnect(SCardReaderDisposition.Unpower);
      PCSCReader.scReader.Dispose();
      return card;
    }

    public static bool ColdReset(out byte[] atr, out string errMsg)
    {
      bool flag = true;
      atr = new byte[0];
      ILogger logger = PCSCReader.logger;
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(73, 3);
      interpolatedStringHandler.AppendLiteral("[");
      interpolatedStringHandler.AppendFormatted(PCSCReader.ReaderName);
      interpolatedStringHandler.AppendLiteral("] Called 'reader.ColdReset' => SCardReader CardHandle: ");
      interpolatedStringHandler.AppendFormatted<IntPtr>(PCSCReader.scReader.CardHandle);
      interpolatedStringHandler.AppendLiteral(" => IsConnected: ");
      interpolatedStringHandler.AppendFormatted<bool>(PCSCReader.scReader.IsConnected);
      string stringAndClear1 = interpolatedStringHandler.ToStringAndClear();
      object[] objArray = Array.Empty<object>();
      logger.LogInformation(stringAndClear1, objArray);
      PCSCReader.scErr = PCSCReader.scReader.Reconnect(SCardShareMode.Shared, PCSCReader.proto, SCardReaderDisposition.Reset);
      if (PCSCReader.scErr == SCardError.Success)
      {
        PCSCReader.scErr = PCSCReader.scReader.Status(out string[] _, out SCardState _, out PCSCReader.proto, out atr);
        if (PCSCReader.scErr == SCardError.Success && (atr == null || atr.Length < 2))
          flag = false;
      }
      else
        flag = false;
      //ref string local = ref errMsg;
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 3);
      interpolatedStringHandler.AppendLiteral("[");
      interpolatedStringHandler.AppendFormatted(PCSCReader.ReaderName);
      interpolatedStringHandler.AppendLiteral("] ");
      interpolatedStringHandler.AppendFormatted(SCardHelper.StringifyError(PCSCReader.scErr));
      interpolatedStringHandler.AppendLiteral(" Error Code:");
      interpolatedStringHandler.AppendFormatted<SCardError>(PCSCReader.scErr);
      interpolatedStringHandler.AppendLiteral(".");
      string stringAndClear2 = interpolatedStringHandler.ToStringAndClear();
      errMsg = stringAndClear2;
      return flag;
    }

    public static bool WarmReset(out byte[] atr, out string errMsg)
    {
      bool flag = true;
      atr = new byte[0];
      ILogger logger = PCSCReader.logger;
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(73, 3);
      interpolatedStringHandler.AppendLiteral("[");
      interpolatedStringHandler.AppendFormatted(PCSCReader.ReaderName);
      interpolatedStringHandler.AppendLiteral("] Called 'reader.WarmReset' => SCardReader CardHandle: ");
      interpolatedStringHandler.AppendFormatted<IntPtr>(PCSCReader.scReader.CardHandle);
      interpolatedStringHandler.AppendLiteral(" => IsConnected: ");
      interpolatedStringHandler.AppendFormatted<bool>(PCSCReader.scReader.IsConnected);
      string stringAndClear1 = interpolatedStringHandler.ToStringAndClear();
      object[] objArray = Array.Empty<object>();
      logger.LogInformation(stringAndClear1, objArray);
      PCSCReader.scErr = PCSCReader.scReader.Reconnect(SCardShareMode.Shared, PCSCReader.proto, SCardReaderDisposition.Unpower);
      if (PCSCReader.scErr == SCardError.Success)
      {
        PCSCReader.scErr = PCSCReader.scReader.Status(out string[] _, out SCardState _, out PCSCReader.proto, out atr);
        if (PCSCReader.scErr == SCardError.Success && (atr == null || atr.Length < 2))
          flag = false;
      }
      else
        flag = false;
      //ref string local = ref errMsg;
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 3);
      interpolatedStringHandler.AppendLiteral("[");
      interpolatedStringHandler.AppendFormatted(PCSCReader.ReaderName);
      interpolatedStringHandler.AppendLiteral("] ");
      interpolatedStringHandler.AppendFormatted(SCardHelper.StringifyError(PCSCReader.scErr));
      interpolatedStringHandler.AppendLiteral(" Error Code:");
      interpolatedStringHandler.AppendFormatted<SCardError>(PCSCReader.scErr);
      interpolatedStringHandler.AppendLiteral(".");
      string stringAndClear2 = interpolatedStringHandler.ToStringAndClear();
      errMsg = stringAndClear2;
      return flag;
    }

    public static bool Transmit(byte[] SendBuff, ref byte[] RecvBuff, out string errMsg)
    {
      bool flag = true;
      ILogger logger = PCSCReader.logger;
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(72, 3);
      interpolatedStringHandler.AppendLiteral("[");
      interpolatedStringHandler.AppendFormatted(PCSCReader.ReaderName);
      interpolatedStringHandler.AppendLiteral("] Called 'reader.Transmit' => SCardReader CardHandle: ");
      interpolatedStringHandler.AppendFormatted<IntPtr>(PCSCReader.scReader.CardHandle);
      interpolatedStringHandler.AppendLiteral(" => IsConnected: ");
      interpolatedStringHandler.AppendFormatted<bool>(PCSCReader.scReader.IsConnected);
      string stringAndClear1 = interpolatedStringHandler.ToStringAndClear();
      object[] objArray = Array.Empty<object>();
      logger.LogInformation(stringAndClear1, objArray);
      PCSCReader.scErr = PCSCReader.scReader.Transmit(SendBuff, ref RecvBuff);
      if (PCSCReader.scErr != SCardError.Success)
      {
        PCSCReader.logger.LogError("Transmit Error => Received Message: [" + Convert.ToBase64String(RecvBuff) + "]");
        RecvBuff = new byte[2]
        {
          byte.MaxValue,
          byte.MaxValue
        };
        flag = false;
      }
      //ref string local = ref errMsg;
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 3);
      interpolatedStringHandler.AppendLiteral("[");
      interpolatedStringHandler.AppendFormatted(PCSCReader.ReaderName);
      interpolatedStringHandler.AppendLiteral("] ");
      interpolatedStringHandler.AppendFormatted(SCardHelper.StringifyError(PCSCReader.scErr));
      interpolatedStringHandler.AppendLiteral(" Error Code:");
      interpolatedStringHandler.AppendFormatted<SCardError>(PCSCReader.scErr);
      interpolatedStringHandler.AppendLiteral(".");
      string stringAndClear2 = interpolatedStringHandler.ToStringAndClear();
      errMsg = stringAndClear2;
      return flag;
    }

    public static byte[] StringToByteArray(string hex)
    {
      return Enumerable.Range(0, hex.Length).Where<int>((Func<int, bool>) (x => x % 2 == 0)).Select<int, byte>((Func<int, byte>) (x => Convert.ToByte(hex.Substring(x, 2), 16))).ToArray<byte>();
    }
  }
}
