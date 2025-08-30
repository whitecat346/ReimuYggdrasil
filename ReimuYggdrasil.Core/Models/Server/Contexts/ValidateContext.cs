using System.Text.Json.Serialization;
using ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;

namespace ReimuYggdrasil.Core.Models.Server.Contexts;

[JsonSerializable(typeof(ValidateReq))]
public partial class ValidateContext : JsonSerializerContext;
