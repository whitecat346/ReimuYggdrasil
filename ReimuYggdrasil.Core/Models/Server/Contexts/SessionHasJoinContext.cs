using System.Text.Json.Serialization;
using ReimuYggdrasil.Core.Models.Server.Responses.Sessions;

namespace ReimuYggdrasil.Core.Models.Server.Contexts;

[JsonSerializable(typeof(HasJoinRep))]
public partial class SessionHasJoinContext : JsonSerializerContext;
