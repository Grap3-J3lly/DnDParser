namespace DndParser
{
    public class SchemaMapper
    {
        // --------------------------------
        //  SCHEMA MAPPING BY CATEGORY
	    // --------------------------------
        #region Schema Mapping By Category

        /// <summary>
        /// Part of what would be a family of Mapping functions, this one focused on Ability Scores. Assumption: List of Ability Scores and List of Skills are Unsorted
        /// </summary>
        /// <param name="scores"></param>
        /// <param name="skills"></param>
        internal static SchemaRoot_AbilityScoreDTO MapToSchemaDTOs_AbilityScores(List<InputAbilityScoreDTO> scores)
        {
            SchemaRoot_AbilityScoreDTO exportDTO = new();

            foreach(InputAbilityScoreDTO abScoreDTO in scores)
            {
                // Creating the new ability object
                OutputAbilityScoreDTO newAbility = new();
                
                // Basic 1-1 Mappings
                newAbility.Name = abScoreDTO.Name;
                newAbility.FullName = abScoreDTO.FullName;
                newAbility.UpdatedAt = abScoreDTO.UpdatedAt;

                // Custom Rule for Description: Concatenating array together with space delimiter
                newAbility.Description = string.Join(" ", abScoreDTO.Desc);

                // Creating and adding proper skill objects
                newAbility.Skills = new();
                MapDescToDescription(abScoreDTO.SkillsDetailed, newAbility.Skills);
                exportDTO.AbilityScores.Add(newAbility);
            }
            return exportDTO;
        }
        
        internal static SchemaRoot_AlignmentDTO MapToSchemaDTOs_Alignments(List<InputAlignmentDTO> alignments)
        {
            SchemaRoot_AlignmentDTO exportDTO = new();

            foreach(InputAlignmentDTO alignmentDTO in alignments)
            {
                OutputAlignmentDTO newAlignment = new();
                
                newAlignment.Name = alignmentDTO.Name;
                newAlignment.Abbreviation = alignmentDTO.Abbreviation;
                newAlignment.Description = alignmentDTO.Desc;
                newAlignment.UpdatedAt = alignmentDTO.UpdatedAt;

                exportDTO.Alignments.Add(newAlignment);
            }

            return exportDTO;
        }

        internal static SchemaRoot_ConditionsDTO MapToSchemaDTOs_Conditions(List<DescriptionsDTO> conditions)
        {
            SchemaRoot_ConditionsDTO exportDTO = new();
            MapDescToDescription(conditions, exportDTO.Conditions);
            return exportDTO;
        }

        internal static SchemaRoot_DamageTypeDTO MapToSchemaDTOs_DamageTypes(List<DescriptionsDTO> damageTypes)
        {
            SchemaRoot_DamageTypeDTO exportDTO = new();
            MapDescToDescription(damageTypes, exportDTO.DamageTypes);
            return exportDTO;
        }

        internal static SchemaRoot_EquipmentDTO MapToSchemaDTOs_Equipment(List<InputEquipmentDTO> equipment)
        {
            SchemaRoot_EquipmentDTO exportDTO = new();
            exportDTO.Equipment = EquipmentMapping(equipment);
            return exportDTO;
        }

        internal static SchemaRoot_EquipmentCategoryDTO MapToSchemaDTOs_EquipmentCategories(List<InputEquipmentCategoryDTO> equipmentCategories)
        {
            SchemaRoot_EquipmentCategoryDTO exportDTO = new();

            foreach(InputEquipmentCategoryDTO equipmentCategoryDTO in equipmentCategories)
            {
                OutputEquipmentCategoryDTO newEquipmentCategory = new();
                newEquipmentCategory.Name = equipmentCategoryDTO.Name;
                newEquipmentCategory.UpdatedAt = equipmentCategoryDTO.UpdatedAt;

                newEquipmentCategory.Equipment = EquipmentMapping(equipmentCategoryDTO.EquipmentDetails, mapCategoryObjects: false);
                exportDTO.EquipmentCategories.Add(newEquipmentCategory);
            }

            return exportDTO;
        }

        internal static SchemaRoot_LanguageDTO MapToSchemaDTOs_Languages(List<InputLanguageDTO> languages)
        {
            SchemaRoot_LanguageDTO exportDTO = new();

            foreach(InputLanguageDTO languageDTO in languages)
            {
                OutputLanguageDTO newLanguage = new();
                newLanguage.Name = languageDTO.Name;
                newLanguage.Type = languageDTO.Type;
                newLanguage.Script = languageDTO.Script;
                newLanguage.UpdatedAt = languageDTO.UpdatedAt;

                newLanguage.TypicalSpeakers = string.Join(", ", languageDTO.TypicalSpeakers);
                exportDTO.Languages.Add(newLanguage);
            }

            return exportDTO;
        }

        internal static SchemaRoot_MagicItemDTO MapToSchemaDTOs_MagicItems(List<InputMagicItemDTO> magicItems)
        {
            SchemaRoot_MagicItemDTO exportDTO = new();

            foreach(InputMagicItemDTO magicItemDTO in magicItems)
            {
                OutputMagicItemDTO newMagicItemDTO = new();
                newMagicItemDTO.Name = magicItemDTO.Name;
                newMagicItemDTO.UpdatedAt = magicItemDTO.UpdatedAt;
                newMagicItemDTO.Variant = magicItemDTO.Variant;
                newMagicItemDTO.Image = magicItemDTO.Image;

                newMagicItemDTO.Description = string.Join(" ", magicItemDTO.Desc);

                newMagicItemDTO.Rarity.Name = magicItemDTO.Rarity.Name;

                newMagicItemDTO.EquipmentCategory.Name = magicItemDTO.EquipmentCategoryDetail.Name;
                newMagicItemDTO.EquipmentCategory.UpdatedAt = magicItemDTO.EquipmentCategoryDetail.UpdatedAt;

                MapDescToDescription(magicItemDTO.VariantsDetailed, newMagicItemDTO.Variants);

                exportDTO.MagicItems.Add(newMagicItemDTO);
            }

            return exportDTO;
        }

        internal static SchemaRoot_MagicSchoolDTO MapToSchemaDTOs_MagicSchools(List<DescriptionDTO> magicSchools)
        {
            SchemaRoot_MagicSchoolDTO exportDTO = new();
            exportDTO.MagicSchools = magicSchools;
            return exportDTO;
        }

        internal static SchemaRoot_RuleSectionDTO MapToSchemaDTOs_RuleSections(List<DescriptionDTO> ruleSections)
        {
            SchemaRoot_RuleSectionDTO exportDTO = new();
            exportDTO.RuleSections = ruleSections;
            return exportDTO;
        }

        internal static SchemaRoot_RuleDTO MapToSchemaDTOs_Rules(List<InputRuleDTO> rules)
        {
            SchemaRoot_RuleDTO exportDTO = new();

            foreach(InputRuleDTO ruleDTO in rules)
            {
                OutputRuleDTO newRuleDTO = new();

                newRuleDTO.Name = ruleDTO.Name;
                newRuleDTO.Description = ruleDTO.Desc;
                newRuleDTO.UpdatedAt = ruleDTO.UpdatedAt;
                newRuleDTO.Subsections = ruleDTO.SubsectionsDetail;

                exportDTO.Rules.Add(newRuleDTO);
            }

            return exportDTO;
        }

        internal static SchemaRoot_SkillDTO MapToSchemaDTOs_Skills(List<InputSkillDTO> skills)
        {
            SchemaRoot_SkillDTO exportDTO = new();

            foreach(InputSkillDTO skillDTO in skills)
            {
                OuputSkillDTO newSkill = new();
                newSkill.Name = skillDTO.Name;
                newSkill.UpdatedAt = skillDTO.UpdatedAt;

                newSkill.Description = string.Join(" ", skillDTO.Desc);

                newSkill.AbilityScore.Name = skillDTO.AbilityScoreDetailed.Name;
                newSkill.AbilityScore.FullName = skillDTO.AbilityScoreDetailed.FullName;
                newSkill.AbilityScore.UpdatedAt = skillDTO.AbilityScoreDetailed.UpdatedAt;
                newSkill.AbilityScore.Description = string.Join(" ", skillDTO.AbilityScoreDetailed.Desc);

                exportDTO.Skills.Add(newSkill);
            }

            return exportDTO;
        }

        internal static SchemaRoot_WeaponPropertyDTO MapToSchemaDTOs_WeaponProperties(List<DescriptionsDTO> weaponProperties)
        {
            SchemaRoot_WeaponPropertyDTO exportDTO = new();
            MapDescToDescription(weaponProperties, exportDTO.WeaponProperties);
            return exportDTO;            
        }

        #endregion

        // --------------------------------
        //  HELPER MAPPING FUNCTIONS
        // --------------------------------

        #region Helper Mapping Functions

        /// This variation is for DTOs with multi-line descriptions
        internal static void MapDescToDescription(List<DescriptionsDTO> generalDTOs, List<DescriptionDTO> schemaDTOs)
        {
            foreach(DescriptionsDTO generalDTO in generalDTOs)
            {
                DescriptionDTO newCondition = new();
                newCondition.Name = generalDTO.Name;
                newCondition.UpdatedAt = generalDTO.UpdatedAt;

                newCondition.Description = string.Join(" ", generalDTO.Desc);
                schemaDTOs.Add(newCondition);
            }
        }

        internal static List<OutputEquipmentDTO> EquipmentMapping(List<InputEquipmentDTO> equipment, bool mapCategoryObjects = true)
        {
            List<OutputEquipmentDTO> schemaEquipmentDTOs = new();

            foreach(InputEquipmentDTO equipmentDTO in equipment)
            {
                OutputEquipmentDTO newEquipment = new();

                // 1-to-1 assignments
                newEquipment.Name = equipmentDTO.Name;
                newEquipment.Image = equipmentDTO.Image;
                newEquipment.UpdatedAt = equipmentDTO.UpdatedAt;
                newEquipment.ArmorCategory = equipmentDTO.ArmorCategory;
                newEquipment.WeaponCategory = equipmentDTO.WeaponCategory;
                newEquipment.WeaponRange = equipmentDTO.WeaponRange;
                newEquipment.CategoryRange = equipmentDTO.CategoryRange;
                newEquipment.VehicleCategory = equipmentDTO.VehicleCategory;
                newEquipment.Capacity = equipmentDTO.Capacity;
                newEquipment.StealthDisadvantage = equipmentDTO.StealthDisadvantage;
                newEquipment.Weight = equipmentDTO.Weight;
                newEquipment.StrMinimum = equipmentDTO.StrMinimum;
                newEquipment.Quantity = equipmentDTO.Quantity;

                // Array to String Concatenation Assignment
                newEquipment.Description = string.Join(" ", equipmentDTO.Desc);

                if(equipmentDTO.Special != null)
                newEquipment.Special = string.Join(" ", equipmentDTO.Special);
                
                // Armor Class Assignment
                newEquipment.ArmorClass.Base = equipmentDTO.ArmorClass.Base;
                newEquipment.ArmorClass.DexBonus = equipmentDTO.ArmorClass.DexBonus;
                newEquipment.ArmorClass.MaxBonus = equipmentDTO.ArmorClass.MaxBonus;

                // Cost Assignment
                newEquipment.Cost.Quantity = equipmentDTO.Cost.Quantity;
                newEquipment.Cost.Unit = equipmentDTO.Cost.Unit;

                // Speed Assignment
                newEquipment.Speed.Quantity = equipmentDTO.Speed.Quantity;
                newEquipment.Speed.Unit = equipmentDTO.Speed.Unit;

                // Range Assignment
                newEquipment.Range.Normal = equipmentDTO.Range.Normal;
                newEquipment.Range.Long = equipmentDTO.Range.Long;

                // Throw Range Assignment
                newEquipment.ThrowRange.Normal = equipmentDTO.ThrowRange.Normal;
                newEquipment.ThrowRange.Long = equipmentDTO.ThrowRange.Long;

                if(mapCategoryObjects)
                {
                    // Equipment Category Assignment
                    newEquipment.EquipmentCategory.Name = equipmentDTO.EquipmentCategoryDetail.Name;
                    newEquipment.EquipmentCategory.UpdatedAt = equipmentDTO.EquipmentCategoryDetail.UpdatedAt;

                    // Gear Category Assignment
                    newEquipment.GearCategory.Name = equipmentDTO.GearCategoryDetail.Name;
                    newEquipment.GearCategory.UpdatedAt = equipmentDTO.GearCategoryDetail.UpdatedAt;
                }

                // Damage Assignment
                string damageUpdatedAt = equipmentDTO.Damage.DamageTypeDetail.UpdatedAt;
                if(!string.IsNullOrEmpty(damageUpdatedAt))
                {
                    if(equipmentDTO.Damage.DamageTypeDetail.Desc == null)
                    {
                        Console.WriteLine($"SchemaMapper.cs: UpdatedAt for Null Desc: {equipmentDTO.Damage.DamageTypeDetail.UpdatedAt}");
                    }
                    newEquipment.Damage = new();
                    newEquipment.Damage.DamageDice = equipmentDTO.Damage.DamageDice;
                    newEquipment.Damage.DamageType.Name = equipmentDTO.Damage.DamageTypeDetail.Name;
                    newEquipment.Damage.DamageType.UpdatedAt = equipmentDTO.Damage.DamageTypeDetail.UpdatedAt;
                    newEquipment.Damage.DamageType.Description = string.Join(" ", equipmentDTO.Damage.DamageTypeDetail.Desc);
                }

                // Two Handed Damage Assignment
                string twoHandedDamageUpdatedAt = equipmentDTO.TwoHandedDamage.DamageTypeDetail.UpdatedAt;
                if(!string.IsNullOrEmpty(twoHandedDamageUpdatedAt))
                {
                    newEquipment.TwoHandedDamage = new();
                    newEquipment.TwoHandedDamage.DamageDice = equipmentDTO.TwoHandedDamage.DamageDice;
                    newEquipment.TwoHandedDamage.DamageType.Name = equipmentDTO.TwoHandedDamage.DamageTypeDetail.Name;
                    newEquipment.TwoHandedDamage.DamageType.UpdatedAt = equipmentDTO.TwoHandedDamage.DamageTypeDetail.UpdatedAt;
                    newEquipment.TwoHandedDamage.DamageType.Description = string.Join(" ", equipmentDTO.TwoHandedDamage.DamageTypeDetail.Desc);
                }

                // Content Assignment
                foreach(InputContentDTO contentDTO in equipmentDTO.Contents)
                {
                    OutputContentDTO newContentDTO = new();
                    newContentDTO.Name = contentDTO.Item.Name;
                    newContentDTO.Quantity = contentDTO.Quantity;
                    newEquipment.Content.Add(newContentDTO);
                }

                // Properties Assignment
                MapDescToDescription(equipmentDTO.PropertiesDetail, newEquipment.Properties);
                schemaEquipmentDTOs.Add(newEquipment);
            }

            return schemaEquipmentDTOs;
        }

        #endregion

    }
}