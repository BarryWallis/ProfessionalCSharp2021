// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.Json;

using JsonSample;

Category appetizers = new("Appetizers");
appetizers.Items.Add(new("Dungeness Crab Cocktail", "Classic cocktail sauce", 27M));
appetizers.Items.Add(new("Almond Crusted Scallops", "Almonds, Parmesan, chive beurre blanc", 19M));

Category dinner = new("Dinner");
dinner.Items.Add(new("Grilled King Salmon", "Lemon chive beurre blanc", 49M));

Card card = new("The Restaurant");
card.Categories.Add(appetizers);
card.Categories.Add(dinner);

string json = SerializeJson(card);

DeserializeJson(json);
UseDom(json);
UseReader(json);
UseWriter(json);

static void UseWriter(string json)
{
    using MemoryStream stream = new();
    JsonWriterOptions options = new()
    {
        Indented = true,
    };

    using (Utf8JsonWriter writer = new(stream, options))
    {
        writer.WriteStartArray();
        writer.WriteStartObject();
        writer.WriteStartObject("book");
        writer.WriteString("Title", "Professional C# and .Net");
        writer.WriteString("Subtitle", "2021 Edition");
        writer.WriteEndObject();
        writer.WriteEndObject();
        writer.WriteStartObject();
        writer.WriteStartObject("Book");
        writer.WriteString("Title", "Professional C# and .Net");
        writer.WriteString("Subtitle", "2018 Edition");
        writer.WriteEndObject();
        writer.WriteEndObject();
        writer.WriteEndArray();
    }

    string jsonString = Encoding.UTF8.GetString(stream.ToArray());
    Console.WriteLine(jsonString);
    Console.WriteLine();
}

static void UseReader(string json)
{
    Console.WriteLine(nameof(UseReader));

    bool isNextPrice = false;
    bool isNextTitle = false;
    string? title = default;
    byte[] data = Encoding.UTF8.GetBytes(json);
    Utf8JsonReader reader = new(data);
    while (reader.Read())
    {
        if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "title")
        {
            isNextTitle = true;
        }
        if (reader.TokenType == JsonTokenType.String && isNextTitle)
        {
            title = reader.GetString();
            isNextTitle = false;
        }
        if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "price")
        {
            isNextPrice = true;
        }
        if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "price")
        {
            isNextPrice = true;
        }
        if (reader.TokenType == JsonTokenType.Number && isNextPrice && reader.TryGetDecimal(out decimal price))
        {
            Console.WriteLine($"{title}, price: {price:C}");
            isNextPrice = false;
        }
    }

    Console.WriteLine();
}

static void UseDom(string json)
{
    Console.WriteLine(nameof(UseDom));

    using JsonDocument document = JsonDocument.Parse(json);
    JsonElement titleElement = document.RootElement.GetProperty("title");
    Console.WriteLine(titleElement);
    foreach (JsonElement category in document.RootElement.GetProperty("categories").EnumerateArray())
    {
        foreach (JsonElement item in category.GetProperty("items").EnumerateArray())
        {
            foreach (JsonProperty property in item.EnumerateObject())
            {
                Console.WriteLine($"{property.Name} {property.Value}");
            }
        }
    }

    Console.WriteLine();
}

static void DeserializeJson(string json)
{
    Console.WriteLine(nameof(DeserializeJson));

    JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    Card? card = JsonSerializer.Deserialize<Card>(json, options);
    if (card is null)
    {
        Console.WriteLine("no card serialized");
        return;
    }

    Console.WriteLine($"{card.Title}");
    foreach (Category category in card.Categories)
    {
        Console.WriteLine($"\t{category.Title}");
        foreach (Item item in category.Items)
        {
            Console.WriteLine($"\t\t{item.Text}");
        }
    }

    Console.WriteLine();
}

static string SerializeJson(Card card)
{
    Console.WriteLine(nameof(SerializeJson));

    JsonSerializerOptions options = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        AllowTrailingCommas = true,
        // ReferenceHandler = ReferenceHandler.Preserve,
    };
    string json = JsonSerializer.Serialize(card, options);
    Console.WriteLine(json);
    Console.WriteLine();
    return json;
}
