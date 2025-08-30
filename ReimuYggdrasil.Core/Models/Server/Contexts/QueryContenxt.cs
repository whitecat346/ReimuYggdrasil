using System.Text.Json.Serialization;
using ReimuYggdrasil.Core.Models.Server.Requests.Sessions.Profile;
using ReimuYggdrasil.Core.Models.Server.Responses.Sessions.Profile;

namespace ReimuYggdrasil.Core.Models.Server.Contexts;

[JsonSerializable(typeof(QueryReq))]
[JsonSerializable(typeof(QueryRep))]
public partial class QueryContenxt : JsonSerializerContext;
