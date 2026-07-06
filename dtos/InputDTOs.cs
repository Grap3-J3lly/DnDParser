using System.Text.Json.Serialization;
namespace DndParser
{
    public class ResultsDTO
    {
        [JsonPropertyName("results")] public List<GeneralInfoDTO> Results { get; set; } = new();
    }

    public class GeneralInfoDTO
    {
        [JsonPropertyName("name")] public string Name {get; set; } = string.Empty;
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("url")] public string Url { get; set; } = string.Empty;
    }

    public class AbilityScoreDTO
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("full_name")] public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("desc")] public string[] Desc { get; set; }
        [JsonPropertyName("skills")] public List<GeneralInfoDTO> Skills { get; set; } = new();
        [JsonPropertyName("skillsDetailed")] public List<SkillDTO> SkillsDetailed { get; set; } = new();
        [JsonPropertyName("url")] public string Url { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public DateTime UpdatedAt { get; set; }
    }

    public class SkillDTO
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("desc")] public string[] Desc { get; set; }
        [JsonPropertyName("updated_at")] public DateTime UpdatedAt { get; set; }
    }
}