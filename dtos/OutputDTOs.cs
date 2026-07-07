using System.Text.Json.Serialization;
namespace DndParser
{
    // --------------------------------
    //	    ABILITY-SCORE DTOs
    // --------------------------------
    #region Ability-Score DTOs
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
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
        [JsonPropertyName("skills")] public List<SchemaSkillDTO> Skills { get; set; } = new();

    }

    public class SchemaSkillDTO
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }
    #endregion
}