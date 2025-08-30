using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Yggdrasil.Contexts;

[JsonSerializable(typeof(UserInfo))]
public partial class UserInfoContext : JsonSerializerContext;
