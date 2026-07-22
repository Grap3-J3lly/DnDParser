using System.Text.Json.Serialization;
namespace DndParser
{
    // --------------------------------
    //	    GENERALIZED DTOs
    // --------------------------------

    // TODO: May need to look into making Universal DTOs - DTOs shared by Input *and* Output
    // TODO: May also need to look into keeping as hash of loaded URLs to 
    // prevent future double loading and increase application speed

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

    public class SchemaAmountDTO
    {
        [JsonPropertyName("quantity")] public float Quantity { get; set; } = 0.0f;
        [JsonPropertyName("unit")] public string Unit { get; set; } = string.Empty;
    }

    public class SchemaDistanceDTO
    {
        [JsonPropertyName("normal")] public int Normal { get; set; } = 0;
        [JsonPropertyName("long")] public int Long { get; set; } = 0;
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
    //	    EQUIPMENT DTOs
    // --------------------------------
    #region Equipment DTOs

    public class SchemaRoot_EquipmentDTO
    {
        [JsonPropertyName("equipment")] public List<SchemaEquipmentDTO> Equipment { get; set; } = new();
    }

    public class SchemaEquipmentDTO
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("image")] public string Image { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("special")] public string Special { get; set; } = string.Empty;
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
        [JsonPropertyName("armor_class")] public SchemaEquipment_ArmorClassDTO ArmorClass { get; set; } = new();
        [JsonPropertyName("cost")] public SchemaAmountDTO Cost { get; set; } = new();
        [JsonPropertyName("speed")] public SchemaAmountDTO Speed { get; set; } = new();
        [JsonPropertyName("range")] public SchemaDistanceDTO Range { get; set; } = new();
        [JsonPropertyName("throw_range")] public SchemaDistanceDTO ThrowRange { get; set; } = new();
        [JsonPropertyName("equipment_category")] public SchemaDescriptionDTO EquipmentCategory { get; set; } = new();
        [JsonPropertyName("gear_category")] public SchemaDescriptionDTO GearCategory { get; set; } = new();
        [JsonPropertyName("damage")] public SchemaEquipment_DamageDTO Damage { get; set; }
        [JsonPropertyName("two_handed_damage")] public SchemaEquipment_DamageDTO TwoHandedDamage { get; set; }
        [JsonPropertyName("content")] public List<SchemaEquipment_ContentDTO> Content { get; set; } = new();
        [JsonPropertyName("properties")] public List<SchemaDescriptionDTO> Properties { get; set; } = new();
    }

    public class SchemaEquipment_ArmorClassDTO
    {
        [JsonPropertyName("base")] public int Base { get; set; } = 0;
        [JsonPropertyName("dex_bonus")] public bool DexBonus { get; set; } = false;
        [JsonPropertyName("max_bonus")] public int MaxBonus { get; set; } = 0;
    }

    public class SchemaEquipment_DamageDTO
    {
        [JsonPropertyName("damage_dice")] public string DamageDice { get; set; } = string.Empty;
        [JsonPropertyName("damage_type")] public SchemaDescriptionDTO DamageType { get; set; } = new();
    }

    public class SchemaEquipment_ContentDTO
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("quantity")] public int Quantity { get; set; } = 0;
    }

    #endregion

    // --------------------------------
    //	    LANGUAGE DTOs
    // --------------------------------

    #region Language Type DTOs

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

    #endregion

    // --------------------------------
    //	    MAGIC SCHOOL DTOs
    // --------------------------------
    #region Magic School DTOs

    public class SchemaRoot_MagicSchoolDTO
    {
        [JsonPropertyName("magic-schools")] public List<SchemaDescriptionDTO> MagicSchools { get; set; } = new();
    }

    #endregion

    // --------------------------------
    //	    RULE SECTION DTOs
    // --------------------------------
    #region Rule Section DTOs

    public class SchemaRoot_RuleSectionDTO
    {
        [JsonPropertyName("rule-sections")] public List<SchemaDescriptionDTO> RuleSections { get; set; } = new();
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

    // --------------------------------
    //	    WEAPON PROPERTIES DTOs
    // --------------------------------   
    #region Weapon Property DTOs

    public class SchemaRoot_WeaponPropertyDTO
    {
        [JsonPropertyName("weapon-properties")] public List<SchemaDescriptionDTO> WeaponProperties { get; set; } = new();
    }

    #endregion
}