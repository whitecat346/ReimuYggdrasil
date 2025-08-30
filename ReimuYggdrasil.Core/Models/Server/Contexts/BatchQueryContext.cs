using System.Text.Json.Serialization;
using ReimuYggdrasil.Core.Models.Server.Requests.Profiles;
using ReimuYggdrasil.Core.Models.Server.Responses.Sessions.Profile;

namespace ReimuYggdrasil.Core.Models.Server.Contexts;

[JsonSerializable(typeof(BatchQueryReq))]
[JsonSerializable(typeof(BatchQueryRep))]
public partial class BatchQueryContext : JsonSerializerContext;
