using System.Text.Json.Serialization;
namespace DndParser
{

    // --------------------------------
    //	    UNIVERSAL DTOs
    // --------------------------------
    #region Universal DTOs

    public class ResultsDTO
    {
        [JsonPropertyName("results")] public List<UrlDTO> Results { get; set; } = new();
    }

    public class UrlDTO
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name {get; set; } = string.Empty;
        [JsonPropertyName("url")] public string Url { get; set; } = string.Empty;
    }

    public class DescriptionDTO
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("full_name")] public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("desc")] public string[] Desc { get; set; }
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    #endregion

    // --------------------------------
    //	    VERSION-SPECIFIC DTOs
    // --------------------------------
    #region Version-Specific DTOs

    public class CategoryDTO
    {
        [JsonPropertyName("ability-scores")] public string AbilityScores { get; set; } = string.Empty;
        [JsonPropertyName("alignments")] public string Alignments { get; set; } = string.Empty;
        [JsonPropertyName("backgrounds")] public string Backgrounds { get; set; } = string.Empty;
        [JsonPropertyName("classes")] public string Classes { get; set; } = string.Empty;
        [JsonPropertyName("conditions")] public string Conditions { get; set; } = string.Empty;
        [JsonPropertyName("damage-types")] public string DamageTypes { get; set; } = string.Empty;
        [JsonPropertyName("equipment")] public string Equipment { get; set; } = string.Empty;
        [JsonPropertyName("equipment-categories")] public string EquipmentCategories { get; set; } = string.Empty;
        [JsonPropertyName("feats")] public string Feats { get; set; } = string.Empty;
        [JsonPropertyName("features")] public string Features { get; set; } = string.Empty;
        [JsonPropertyName("languages")] public string Languages { get; set; } = string.Empty;
        [JsonPropertyName("magic-items")] public string MagicItems { get; set; } = string.Empty;
        [JsonPropertyName("magic-schools")] public string MagicSchools { get; set; } = string.Empty;
        [JsonPropertyName("monsters")] public string Monsters { get; set; } = string.Empty;
        [JsonPropertyName("proficiencies")] public string Proficiencies { get; set; } = string.Empty;
        [JsonPropertyName("races")] public string Races { get; set; } = string.Empty;
        [JsonPropertyName("rule-sections")] public string RuleSections { get; set; } = string.Empty;
        [JsonPropertyName("rules")] public string Rules { get; set; } = string.Empty;
        [JsonPropertyName("skills")] public string Skills { get; set; } = string.Empty;
        [JsonPropertyName("spells")] public string Spells { get; set; } = string.Empty;
        [JsonPropertyName("subclasses")] public string Subclasses { get; set; } = string.Empty;
        [JsonPropertyName("subraces")] public string Subraces { get; set; } = string.Empty;
        [JsonPropertyName("traits")] public string Traits { get; set; } = string.Empty;
        [JsonPropertyName("weapon-properties")] public string WeaponProperties { get; set; } = string.Empty;
    

    }

    #endregion

    // --------------------------------
    //	    CATEGORY-SPECIFIC DTOs
    // --------------------------------
    #region Category-Specific DTOs

    public class AbilityScoreDTO
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("full_name")] public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("desc")] public string[] Desc { get; set; }
        [JsonPropertyName("skills")] public List<UrlDTO> Skills { get; set; } = new();
        [JsonPropertyName("skillsDetailed")] public List<DescriptionDTO> SkillsDetailed { get; set; } = new();
        [JsonPropertyName("url")] public string Url { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    public class AlignmentDTO
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("abbreviation")] public string Abbreviation { get; set; } = string.Empty;
        [JsonPropertyName("desc")] public string Desc { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    public class LanguageDTO
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
        [JsonPropertyName("typical_speakers")] public string[] TypicalSpeakers { get; set; }
        [JsonPropertyName("script")] public string Script { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    public class SkillDTO
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("desc")] public string[] Desc { get; set; }
        [JsonPropertyName("ability_score")] public UrlDTO AbilityScore { get; set; } = new();
        [JsonPropertyName("abilityScoreDetailed")] public DescriptionDTO AbilityScoreDetailed { get; set; } = new();
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    #endregion
}