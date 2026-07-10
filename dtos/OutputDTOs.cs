using System.Text.Json.Serialization;
namespace DndParser
{
    // --------------------------------
    //	    GENERALIZED DTOs
    // --------------------------------

    public class SchemaDescriptionDTO
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    public class SchemaFullNameDescriptionDTO
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("full_name")] public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    // --------------------------------
    //	    ABILITY-SCORE DTOs
    // --------------------------------
    #region Ability-Score DTOs
    public class SchemaRoot_AbilityScoreDTO
    {
        [JsonPropertyName("ability-scores")] public List<SchemaAbilityScoreDTO> AbilityScores { get; set; } = new();
    }

    public class SchemaAbilityScoreDTO
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("full_name")] public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
        [JsonPropertyName("skills")] public List<SchemaDescriptionDTO> Skills { get; set; } = new();

    }
    #endregion

    // --------------------------------
    //	    SKILL DTOs
    // --------------------------------
    #region Skill DTOs

    public class SchemaRoot_SkillDTO
    {
        [JsonPropertyName("skills")] public List<SchemaSkillDTO> Skills { get; set; } = new();
    }

    public class SchemaSkillDTO
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
        [JsonPropertyName("ability_score")] public SchemaFullNameDescriptionDTO AbilityScore { get; set; } = new();
    }

    #endregion
}