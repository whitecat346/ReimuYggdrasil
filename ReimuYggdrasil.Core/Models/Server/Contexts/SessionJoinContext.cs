using System.Text.Json.Serialization;
using ReimuYggdrasil.Core.Models.Server.Requests.Sessions;

namespace ReimuYggdrasil.Core.Models.Server.Contexts;

[JsonSerializable(typeof(JoinReq))]
public partial class SessionJoinContext : JsonSerializerContext;
