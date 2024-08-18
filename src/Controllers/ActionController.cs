using MaticaS3600Simulation.Models;
using MaticaS3600Simulation.SmartCard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace MaticaS3600Simulation.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ActionController : ControllerBase
  {
    private readonly ILogger<ActionController> _logger;
    private readonly SimulatorConfig _config;

    public ActionController(ILogger<ActionController> logger, SimulatorConfig config)
    {
      this._logger = logger;
      this._config = config;
    }

    [HttpPost(Name = "ProcessAction")]
    public ActionResult<CommandRequest> Post([FromBody] CommandRequest body)
    {
      try
      {
        CommandResponse commandResponse1 = new CommandResponse();
        byte[] atr = new byte[0];
        CommandResponse commandResponse2;
        try
        {
          switch (body.Command)
          {
            case "ApduExchange":
              this._logger.LogInformation("Apdu Exchange called!");
              this._logger.LogInformation("SEND: " + body.APDU);
              byte[] byteArray = PCSCReader.StringToByteArray(body.APDU);
              byte[] RecvBuff = new byte[1024];
              string errMsg1;
              if (PCSCReader.Transmit(byteArray, ref RecvBuff, out errMsg1))
              {
                this._logger.LogInformation("RECV: " + Convert.ToHexString(RecvBuff));
                commandResponse2 = new CommandResponse()
                {
                  Answer = "OK",
                  Data = Convert.ToHexString(RecvBuff)
                };
                break;
              }
              this._logger.LogError("Error: " + errMsg1);
              commandResponse2 = new CommandResponse()
              {
                Answer = "NOK",
                Error = new ErrorResponse()
                {
                  Code = "-99",
                  Message = "Error: " + errMsg1
                }
              };
              break;
            case "CoverOpen":
              this._logger.LogInformation("Cover open called!");
              commandResponse2 = new CommandResponse()
              {
                Answer = "OK",
                Data = Convert.ToHexString(atr)
              };
              break;
            case "EjectCard":
              this._logger.LogInformation("Eject Card called!");
              commandResponse2 = new CommandResponse()
              {
                Answer = "OK"
              };
              break;
            case "Emboss":
              this._logger.LogInformation("Emboss called!");
              StaticValues.EmbossLines = body.Emboss_Line;
              commandResponse2 = new CommandResponse()
              {
                Answer = "OK"
              };
              break;
            case "GetInfoJson":
              this._logger.LogInformation("Get Info Json called!");
              commandResponse2 = new CommandResponse()
              {
                Answer = "OK",
                Machine_Configuration = new MachineConfiguration(),
                Machine_Status = new MachineStatus()
              };
              break;
            case "LoadCard":
              this._logger.LogInformation("Load Card called!");
              commandResponse2 = new CommandResponse()
              {
                Answer = "OK"
              };
              break;
            case "MoveToChip":
              this._logger.LogInformation("Move to chip called! => Smartcard reader name is '" + this._config.SmartcardReader + "'");
              PCSCReader.SetPCSCReader(this._config.SmartcardReader, (ILogger) this._logger);
              this._logger.LogInformation("PCSCReader.SetPCSCReader called! => Smartcard reader name is '" + this._config.SmartcardReader + "'");
              string errMsg2;
              if (!PCSCReader.ConnectToCard(out atr, out errMsg2))
              {
                this._logger.LogError("Error: " + errMsg2);
                commandResponse2 = new CommandResponse()
                {
                  Answer = "NOK",
                  Error = new ErrorResponse()
                  {
                    Code = "-99",
                    Message = "Error: " + errMsg2
                  }
                };
                break;
              }
              ILogger<ActionController> logger = this._logger;
              DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(56, 1);
              interpolatedStringHandler.AppendLiteral("PCSCReader.ConnectToCard called! => Legth of the ATR: '");
              interpolatedStringHandler.AppendFormatted<int>(atr.Length);
              interpolatedStringHandler.AppendLiteral("'");
              string stringAndClear = interpolatedStringHandler.ToStringAndClear();
              object[] objArray = Array.Empty<object>();
              logger.LogInformation(stringAndClear, objArray);
              commandResponse2 = new CommandResponse()
              {
                Answer = "OK",
                Data = Convert.ToHexString(atr)
              };
              break;
            case "Print":
              this._logger.LogInformation("Print called!");
              commandResponse2 = new CommandResponse()
              {
                Answer = "OK",
                Data = Convert.ToHexString(atr)
              };
              break;
            case "ReadMAG":
              this._logger.LogInformation("Read MAG called! with TrackId " + body.Track_ID);
              string str1;
              switch (body.Track_ID)
              {
                case "1":
                  str1 = StaticValues.MagneticData.Track1;
                  break;
                case "2":
                  str1 = StaticValues.MagneticData.Track2;
                  break;
                case "3":
                  str1 = StaticValues.MagneticData.Track3;
                  break;
                default:
                  str1 = "";
                  break;
              }
              string str2 = str1;
              commandResponse2 = new CommandResponse()
              {
                Answer = "OK",
                Data = str2
              };
              break;
            case "Restore":
              this._logger.LogInformation("Restore called!");
              commandResponse2 = new CommandResponse()
              {
                Answer = "OK"
              };
              break;
            case "WriteMAG":
              this._logger.LogInformation("Write MAG called!");
              StaticValues.MagneticData = body.Magnetic_Data;
              commandResponse2 = new CommandResponse()
              {
                Answer = "OK"
              };
              break;
            case "ChipReset":
              this._logger.LogInformation("Chip reset called!");
              string errMsg3;
              if (PCSCReader.ColdReset(out atr, out errMsg3))
              {
                commandResponse2 = new CommandResponse()
                {
                  Answer = "OK",
                  Data = Convert.ToHexString(atr)
                };
                break;
              }
              this._logger.LogError("Error: " + errMsg3);
              commandResponse2 = new CommandResponse()
              {
                Answer = "NOK",
                Error = new ErrorResponse()
                {
                  Code = "-99",
                  Message = "Error: " + errMsg3
                }
              };
              break;
            default:
              this._logger.LogError($"Invalid command!!!! {body.Command}");
              commandResponse2 = new CommandResponse()
              {
                Answer = "KO",
                Error = new ErrorResponse()
                {
                  Code = "1001",
                  Message = "Invalid Command!"
                }
              };
              break;
          }
        }
        catch (Exception ex)
        {
          CommandResponse commandResponse3 = new CommandResponse();
          commandResponse3.Answer = "NOK";
          commandResponse3.Error = new ErrorResponse()
          {
            Code = "-99",
            Message = "Error: " + ex.Message
          };
          commandResponse1 = commandResponse3;
          return (ActionResult<CommandRequest>) (ActionResult) this.Ok((object) commandResponse3);
        }
        return (ActionResult<CommandRequest>) (ActionResult) this.Ok((object) commandResponse2);
      }
      catch (Exception ex)
      {
        this._logger.LogError("Action Controller Error: ", (object) ex);
        throw new Exception("Action Controller Error", ex);
      }
    }
  }
}
