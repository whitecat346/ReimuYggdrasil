using System.Text.Json.Serialization;
using ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;
using ReimuYggdrasil.Core.Models.Server.Responses.AuthServer;

namespace ReimuYggdrasil.Core.Models.Server.Contexts;

[JsonSerializable(typeof(BindUserReq))]
[JsonSerializable(typeof(BindUserRep))]
public partial class BindUserContext : JsonSerializerContext;
