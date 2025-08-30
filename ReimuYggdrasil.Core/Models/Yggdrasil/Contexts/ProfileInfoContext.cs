using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Yggdrasil.Contexts;

[JsonSerializable(typeof(ProfileInfo))]
public partial class ProfileInfoContext : JsonSerializerContext;
