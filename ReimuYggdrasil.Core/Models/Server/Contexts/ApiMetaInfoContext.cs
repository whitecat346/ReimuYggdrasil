using System.Text.Json.Serialization;
using ReimuYggdrasil.Core.Models.Server.Responses;

namespace ReimuYggdrasil.Core.Models.Server.Contexts;

[JsonSerializable(typeof(ApiMetaInfoRep))]
public partial class ApiMetaInfoContext : JsonSerializerContext;
