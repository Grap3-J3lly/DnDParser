using System.Text.Json.Serialization;
namespace DndParser
{
    // Depending on how this were to grow, SchemaRoot may include other Lists of DTOs
    public class SchemaRootDTO
    {
        [JsonPropertyName("ability-scores")] 
        public List<SchemaAbilityScoreDTO> AbilityScores { get; set; } = new();
    }

    public class SchemaAbilityScoreDTO
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("full_name")] public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public DateTime UpdatedAt { get; set; }
        [JsonPropertyName("skills")] public List<SchemaSkillDTO> Skills { get; set; } = new();

    }

    public class SchemaSkillDTO
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public DateTime UpdatedAt { get; set; }
    }
}