using System.Text.Json.Serialization;
using ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;

namespace ReimuYggdrasil.Core.Models.Server.Contexts;

[JsonSerializable(typeof(AddUserReq))]
public partial class AddUserContext : JsonSerializerContext;
