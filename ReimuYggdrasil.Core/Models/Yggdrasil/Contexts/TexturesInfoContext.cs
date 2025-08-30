using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Yggdrasil.Contexts;

[JsonSerializable(typeof(TexturesInfo))]
public partial class TexturesInfoContext : JsonSerializerContext;
