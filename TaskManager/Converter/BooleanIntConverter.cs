
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class BooleanIntConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
         if (reader.TokenType == JsonTokenType.Number)
        {
            int value = reader.GetInt32();
            return value == 1;
        }
        throw new JsonException("Unexpected token type for boolean conversion.");
    }

       public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
         writer.WriteNumberValue(value ? 1 : 0);
    }


}
