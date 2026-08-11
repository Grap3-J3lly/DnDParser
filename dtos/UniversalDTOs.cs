using System.Text.Json.Serialization;
namespace DndParser
{
    // --------------------------------
    //	    GENERALIZED DTOs
    // --------------------------------
    // DTOs typically used to display info. 
    // These tend to be the scaffolding that the other DTOs use, when in need of more complex properties.

    public class NameDTO : IDataTransferObject
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    }

    public class DescriptionDTO : IDataTransferObject
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    // Used In: 
    // Conditions
    // Damage Types
    // Weapon Properties
    // Version of DescriptionDTO that has an array for Desc instead of one string
    public class DescriptionsDTO : IDataTransferObject
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("desc")] public string[] Desc { get; set; }
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    public class FullNameDescriptionDTO : IDataTransferObject
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("full_name")] public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    // Version of Full Name Description DTO that has an array for Desc instead of one string
    public class FullNameDescriptionsDTO : IDataTransferObject
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("full_name")] public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("desc")] public string[] Desc { get; set; }
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    public class AmountDTO : IDataTransferObject
    {
        [JsonPropertyName("quantity")] public float Quantity { get; set; } = 0.0f;
        [JsonPropertyName("unit")] public string Unit { get; set; } = string.Empty;
    }

    public class DistanceDTO : IDataTransferObject
    {
        [JsonPropertyName("normal")] public int Normal { get; set; } = 0;
        [JsonPropertyName("long")] public int Long { get; set; } = 0;
    }

    public class ArmorClassDTO : IDataTransferObject
    {
        [JsonPropertyName("base")] public int Base { get; set; } = 0;
        [JsonPropertyName("dex_bonus")] public bool DexBonus { get; set; } = false;
        [JsonPropertyName("max_bonus")] public int MaxBonus { get; set; } = 0;
    }
}