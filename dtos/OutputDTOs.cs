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
    //	    ALIGNMENT DTOs
    // --------------------------------
    #region Alignment DTOs

    public class SchemaRoot_AlignmentDTO
    {
        [JsonPropertyName("alignments")] public List<SchemaAlignmentDTO> Alignments { get; set; } = new();
    }

    public class SchemaAlignmentDTO
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("abbreviation")] public string Abbreviation { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    #endregion

    // --------------------------------
    //	    CONDITIONS DTOs
    // --------------------------------
    #region Condition DTOs

    public class SchemaRoot_ConditionsDTO
    {
        [JsonPropertyName("conditions")] public List<SchemaDescriptionDTO> Conditions { get; set; } = new();
    }

    #endregion

    // --------------------------------
    //	    DAMAGE TYPE DTOs
    // --------------------------------
    #region Damage Type DTOs

    public class SchemaRoot_DamageTypeDTO
    {
        [JsonPropertyName("damage-types")] public List<SchemaDescriptionDTO> DamageTypes { get; set; } = new();
    }

    #endregion

    // --------------------------------
    //	    LANGUAGE DTOs
    // --------------------------------

    public class SchemaRoot_LanguageDTO
    {
        [JsonPropertyName("languages")] public List<SchemaLanguageDTO> Languages { get; set; } = new();
    }

    public class SchemaLanguageDTO
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
        [JsonPropertyName("typical_speakers")] public string TypicalSpeakers { get; set; } = string.Empty;
        [JsonPropertyName("script")] public string Script { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

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