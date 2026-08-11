using System.Text.Json.Serialization;
namespace DndParser
{
    // Naming Convention: 
    // For Output Categories with matching, but slightly different, Input DTOs, start the category with:
    // Output[Rest of the name]

    // TODO: May need to look into making Universal DTOs - DTOs shared by Input *and* Output
    // TODO: May also need to look into keeping as hash of loaded URLs to 
    // prevent future double loading and increase application speed
    // UPDATE: This was addressed by refactoring how URLs are processed. They're now loaded in in bulk. 

    // --------------------------------
    //	    ABILITY-SCORE DTOs
    // --------------------------------
    #region Ability-Score DTOs
    public class SchemaRoot_AbilityScoreDTO : IDataTransferObject
    {
        [JsonPropertyName("ability-scores")] public List<OutputAbilityScoreDTO> AbilityScores { get; set; } = new();
    }

    public class OutputAbilityScoreDTO : IDataTransferObject
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("full_name")] public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
        [JsonPropertyName("skills")] public List<DescriptionDTO> Skills { get; set; } = new();

    }
    #endregion

    // --------------------------------
    //	    ALIGNMENT DTOs
    // --------------------------------
    #region Alignment DTOs

    public class SchemaRoot_AlignmentDTO : IDataTransferObject
    {
        [JsonPropertyName("alignments")] public List<OutputAlignmentDTO> Alignments { get; set; } = new();
    }

    public class OutputAlignmentDTO : IDataTransferObject
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

    public class SchemaRoot_ConditionsDTO : IDataTransferObject
    {
        [JsonPropertyName("conditions")] public List<DescriptionDTO> Conditions { get; set; } = new();
    }

    #endregion

    // --------------------------------
    //	    DAMAGE TYPE DTOs
    // --------------------------------
    #region Damage Type DTOs

    public class SchemaRoot_DamageTypeDTO : IDataTransferObject
    {
        [JsonPropertyName("damage-types")] public List<DescriptionDTO> DamageTypes { get; set; } = new();
    }

    #endregion

    // --------------------------------
    //	    EQUIPMENT DTOs
    // --------------------------------
    #region Equipment DTOs

    public class SchemaRoot_EquipmentDTO : IDataTransferObject
    {
        [JsonPropertyName("equipment")] public List<OutputEquipmentDTO> Equipment { get; set; } = new();
    }

    public class OutputEquipmentDTO : IDataTransferObject
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
        [JsonPropertyName("armor_class")] public ArmorClassDTO ArmorClass { get; set; } = new();
        [JsonPropertyName("cost")] public AmountDTO Cost { get; set; } = new();
        [JsonPropertyName("speed")] public AmountDTO Speed { get; set; } = new();
        [JsonPropertyName("range")] public DistanceDTO Range { get; set; } = new();
        [JsonPropertyName("throw_range")] public DistanceDTO ThrowRange { get; set; } = new();
        [JsonPropertyName("equipment_category")] public DescriptionDTO EquipmentCategory { get; set; } = new();
        [JsonPropertyName("gear_category")] public DescriptionDTO GearCategory { get; set; } = new();
        [JsonPropertyName("damage")] public OutputDamageDTO Damage { get; set; }
        [JsonPropertyName("two_handed_damage")] public OutputDamageDTO TwoHandedDamage { get; set; }
        [JsonPropertyName("content")] public List<OutputContentDTO> Content { get; set; } = new();
        [JsonPropertyName("properties")] public List<DescriptionDTO> Properties { get; set; } = new();
    }

    #endregion

    // --------------------------------
    //  EQUIPMENT CATEGORY DTOs
    // --------------------------------

    #region Equipment Category DTOs

    public class SchemaRoot_EquipmentCategoryDTO : IDataTransferObject
    {
        [JsonPropertyName("equipment-categories")] public List<OutputEquipmentCategoryDTO> EquipmentCategories { get; set; } = new();
    }

    public class OutputEquipmentCategoryDTO : IDataTransferObject
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
        [JsonPropertyName("equipment")] public List<OutputEquipmentDTO> Equipment { get; set; } = new();
    }

    #endregion

    // --------------------------------
    //	    LANGUAGE DTOs
    // --------------------------------

    #region Language Type DTOs

    public class SchemaRoot_LanguageDTO : IDataTransferObject
    {
        [JsonPropertyName("languages")] public List<OutputLanguageDTO> Languages { get; set; } = new();
    }

    public class OutputLanguageDTO : IDataTransferObject
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
        [JsonPropertyName("typical_speakers")] public string TypicalSpeakers { get; set; } = string.Empty;
        [JsonPropertyName("script")] public string Script { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
    }

    #endregion

    // --------------------------------
    //	    MAGIC ITEM DTOs
    // --------------------------------

    #region Magic Item DTOs

    public class SchemaRoot_MagicItemDTO : IDataTransferObject
    {
        [JsonPropertyName("magic-items")] public List<OutputMagicItemDTO> MagicItems { get; set; } = new();
    }

    public class OutputMagicItemDTO : IDataTransferObject
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
        [JsonPropertyName("image")] public string Image { get; set; } = string.Empty;
        [JsonPropertyName("variant")] public bool Variant { get; set; } = false;
        [JsonPropertyName("rarity")] public NameDTO Rarity { get; set; } = new();
        [JsonPropertyName("equipment_category")] public DescriptionDTO EquipmentCategory { get; set; } = new();
        [JsonPropertyName("variants")] public List<DescriptionDTO> Variants { get; set; } = new();
    }

    #endregion

    // --------------------------------
    //	    MAGIC SCHOOL DTOs
    // --------------------------------
    #region Magic School DTOs

    public class SchemaRoot_MagicSchoolDTO : IDataTransferObject
    {
        [JsonPropertyName("magic-schools")] public List<DescriptionDTO> MagicSchools { get; set; } = new();
    }

    #endregion

    // --------------------------------
    //	    RULE SECTION DTOs
    // --------------------------------
    #region Rule Section DTOs

    public class SchemaRoot_RuleSectionDTO : IDataTransferObject
    {
        [JsonPropertyName("rule-sections")] public List<DescriptionDTO> RuleSections { get; set; } = new();
    }

    #endregion

    // --------------------------------
    //	    RULE SECTION DTOs
    // --------------------------------
    #region Rule DTOs

    public class SchemaRoot_RuleDTO : IDataTransferObject
    {
        [JsonPropertyName("rules")] public List<OutputRuleDTO> Rules { get; set; } = new();
    }

    public class OutputRuleDTO : IDataTransferObject
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
        [JsonPropertyName("subsections")] public List<DescriptionDTO> Subsections { get; set; } = new();
    }

    #endregion

    // --------------------------------
    //	    SKILL DTOs
    // --------------------------------
    #region Skill DTOs

    public class SchemaRoot_SkillDTO : IDataTransferObject
    {
        [JsonPropertyName("skills")] public List<OuputSkillDTO> Skills { get; set; } = new();
    }

    public class OuputSkillDTO : IDataTransferObject
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
        [JsonPropertyName("ability_score")] public FullNameDescriptionDTO AbilityScore { get; set; } = new();
    }

    #endregion

    // --------------------------------
    //	    WEAPON PROPERTIES DTOs
    // --------------------------------   
    #region Weapon Property DTOs

    public class SchemaRoot_WeaponPropertyDTO : IDataTransferObject
    {
        [JsonPropertyName("weapon-properties")] public List<DescriptionDTO> WeaponProperties { get; set; } = new();
    }

    #endregion

    // --------------------------------
    //	    GENERALIZED DTOs
    // --------------------------------

    #region Generalized DTOs

    public class OutputDamageDTO : IDataTransferObject
    {
        [JsonPropertyName("damage_dice")] public string DamageDice { get; set; } = string.Empty;
        [JsonPropertyName("damage_type")] public DescriptionDTO DamageType { get; set; } = new();
    }

    public class OutputContentDTO : IDataTransferObject
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("quantity")] public int Quantity { get; set; } = 0;
    }

    #endregion
}