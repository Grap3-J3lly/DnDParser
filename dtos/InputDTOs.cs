using System.Text.Json.Serialization;
namespace DndParser
{

    // --------------------------------
    //	    UNIVERSAL DTOs
    // --------------------------------
    #region Universal DTOs

    public class ResultsDTO : IDataTransferObject
    {
        [JsonPropertyName("results")] public List<UrlDTO> Results { get; set; } = new();
    }

    public class UrlDTO : IDataTransferObject
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name {get; set; } = string.Empty;
        [JsonPropertyName("url")] public string Url { get; set; } = string.Empty;
    }

    public class DescriptionDTO : IDataTransferObject
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("desc")] public string Desc { get; set; }
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    public class DescriptionsDTO : IDataTransferObject
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
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

    #endregion

    // --------------------------------
    //	    VERSION-SPECIFIC DTOs
    // --------------------------------
    #region Version-Specific DTOs

    public class CategoryDTO : IDataTransferObject
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

    public class AbilityScoreDTO : IDataTransferObject
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("full_name")] public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("desc")] public string[] Desc { get; set; }
        [JsonPropertyName("skills")] public List<UrlDTO> Skills { get; set; } = new();
        [JsonPropertyName("skillsDetailed")] public List<DescriptionsDTO> SkillsDetailed { get; set; } = new();
        [JsonPropertyName("url")] public string Url { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    public class AlignmentDTO : IDataTransferObject
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("abbreviation")] public string Abbreviation { get; set; } = string.Empty;
        [JsonPropertyName("desc")] public string Desc { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    public class ClassDTO : IDataTransferObject
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("hit_die")] public int HitDie { get; set; } = 0;
        // proficiency_choices
        [JsonPropertyName("proficiencies")] public List<UrlDTO> Proficiencies { get; set; } = new();
        [JsonPropertyName("saving_throws")] public List<UrlDTO> SavingThrows { get; set; } = new();
        // starting_equipment
        // starting_equipment_options
        // class_levels
        // multi_classing
        [JsonPropertyName("subclasses")] public List<UrlDTO> Subclasses { get; set; } = new();
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    public class EquipmentDTO : IDataTransferObject
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("image")] public string Image { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
        [JsonPropertyName("armor_category")] public string ArmorCategory { get; set; } = string.Empty;
        [JsonPropertyName("weapon_category")] public string WeaponCategory { get; set; } = string.Empty;
        [JsonPropertyName("weapon_range")] public string WeaponRange { get; set; } = string.Empty;
        [JsonPropertyName("category_range")] public string CategoryRange { get; set; } = string.Empty;
        [JsonPropertyName("vehicle_category")] public string VehicleCategory { get; set; } = string.Empty;
        [JsonPropertyName("capacity")] public string Capacity { get; set; } = string.Empty;
        [JsonPropertyName("stealth_disadvantage")] public bool StealthDisadvantage { get; set; } = false;
        [JsonPropertyName("weight")] public float Weight { get; set; } = 0.0f;
        [JsonPropertyName("str_minimum")] public int StrMinimum { get; set; } = 0;
        [JsonPropertyName("quantity")] public int Quantity { get; set; } = 0;
        [JsonPropertyName("armor_class")] public Equipment_ArmorClassDTO ArmorClass { get; set; } = new();
        [JsonPropertyName("cost")] public AmountDTO Cost { get; set; } = new();
        [JsonPropertyName("speed")] public AmountDTO Speed { get; set; } = new();
        [JsonPropertyName("range")] public DistanceDTO Range { get; set; } = new();
        [JsonPropertyName("throw_range")] public DistanceDTO ThrowRange { get; set; } = new();
        [JsonPropertyName("equipment_category")] public UrlDTO EquipmentCategory { get; set; } = new();
        [JsonPropertyName("equipment_categoryDetail")] public DescriptionDTO EquipmentCategoryDetail { get; set; } = new();
        [JsonPropertyName("gear_category")] public UrlDTO GearCategory { get; set; } = new();
        [JsonPropertyName("gear_categoryDetail")] public DescriptionDTO GearCategoryDetail { get; set; } = new();
        [JsonPropertyName("damage")] public Equipment_DamageDTO Damage { get; set; } = new();
        [JsonPropertyName("two_handed_damage")] public Equipment_DamageDTO TwoHandedDamage { get; set; } = new();
        [JsonPropertyName("desc")] public string[] Desc { get; set; }
        [JsonPropertyName("special")] public string[] Special { get; set; }
        [JsonPropertyName("contents")] public List<Equipment_ContentDTO> Contents { get; set; } = new();
        [JsonPropertyName("properties")] public List<UrlDTO> Properties { get; set; } = new();
        [JsonPropertyName("propertiesDetail")] public List<DescriptionsDTO> PropertiesDetail { get; set; } = new();
    }

    public class Equipment_ArmorClassDTO : IDataTransferObject
    {
        [JsonPropertyName("base")] public int Base { get; set; } = 0;
        [JsonPropertyName("dex_bonus")] public bool DexBonus { get; set; } = false;
        [JsonPropertyName("max_bonus")] public int MaxBonus { get; set; } = 0;
    }

    public class Equipment_DamageDTO : IDataTransferObject
    {
        [JsonPropertyName("damage_dice")] public string DamageDice { get; set; } = string.Empty;
        [JsonPropertyName("damage_type")] public UrlDTO DamageType { get; set; } = new();
        [JsonPropertyName("damage_typeDetail")] public DescriptionsDTO DamageTypeDetail { get; set; } = new();
    }

    public class Equipment_ContentDTO : IDataTransferObject
    {
        [JsonPropertyName("item")] public UrlDTO Item { get; set; } = new();
        [JsonPropertyName("quantity")] public int Quantity { get; set; } = 0;
    }

    public class LanguageDTO : IDataTransferObject
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
        [JsonPropertyName("typical_speakers")] public string[] TypicalSpeakers { get; set; }
        [JsonPropertyName("script")] public string Script { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    public class SkillDTO : IDataTransferObject
    {
        [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("desc")] public string[] Desc { get; set; }
        [JsonPropertyName("ability_score")] public UrlDTO AbilityScore { get; set; } = new();
        [JsonPropertyName("abilityScoreDetailed")] public DescriptionsDTO AbilityScoreDetailed { get; set; } = new();
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    #endregion
}