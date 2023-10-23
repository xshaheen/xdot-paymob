using System.Text.Json;
using System.Text.Json.Serialization;

namespace X.Paymob.CashIn.Internal;

internal sealed class AddNullableAmountConverter : JsonConverter<string> {
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        return reader.TokenType.Equals(JsonTokenType.Number) && reader.TryGetDecimal(out decimal result)
            ? result.ToString()
            : "N/A";
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) {
        writer.WriteStringValue(value);
    }
}
