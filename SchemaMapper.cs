namespace DndParser
{
    public class SchemaMapper
    {
        // --------------------------------
        //	    GENERIC SCHEMA MAPPING
	    // --------------------------------

        #region Generic Schema Mapping

        /// This variation is for DTOs with single line descriptions
        internal static void MapToDescriptionSchema(List<DescriptionDTO> generalDTOs, List<SchemaDescriptionDTO> schemaDTOs)
        {
            foreach(DescriptionDTO generalDTO in generalDTOs)
            {
                SchemaDescriptionDTO newCondition = new();
                newCondition.Name = generalDTO.Name;
                newCondition.UpdatedAt = generalDTO.UpdatedAt;

                newCondition.Description = generalDTO.Desc;
                schemaDTOs.Add(newCondition);
            }
        }

        /// This variation is for DTOs with multi-line descriptions
        internal static void MapToDescriptionsSchema(List<DescriptionsDTO> generalDTOs, List<SchemaDescriptionDTO> schemaDTOs)
        {
            foreach(DescriptionsDTO generalDTO in generalDTOs)
            {
                SchemaDescriptionDTO newCondition = new();
                newCondition.Name = generalDTO.Name;
                newCondition.UpdatedAt = generalDTO.UpdatedAt;

                newCondition.Description = string.Join(" ", generalDTO.Desc);
                schemaDTOs.Add(newCondition);
            }
        }

        #endregion

        // --------------------------------
        //  SCHEMA MAPPING BY CATEGORY
	    // --------------------------------
        #region Schema Mapping By Category

        /// <summary>
        /// Part of what would be a family of Mapping functions, this one focused on Ability Scores. Assumption: List of Ability Scores and List of Skills are Unsorted
        /// </summary>
        /// <param name="scores"></param>
        /// <param name="skills"></param>
        internal static SchemaRoot_AbilityScoreDTO MapToSchemaDTOs_AbilityScores(List<AbilityScoreDTO> scores)
        {
            SchemaRoot_AbilityScoreDTO exportDTO = new();

            foreach(AbilityScoreDTO abScoreDTO in scores)
            {
                // Creating the new ability object
                SchemaAbilityScoreDTO newAbility = new();
                
                // Basic 1-1 Mappings
                newAbility.Name = abScoreDTO.Name;
                newAbility.FullName = abScoreDTO.FullName;
                newAbility.UpdatedAt = abScoreDTO.UpdatedAt;

                // Custom Rule for Description: Concatenating array together with space delimiter
                newAbility.Description = string.Join(" ", abScoreDTO.Desc);

                // Creating and adding proper skill objects
                newAbility.Skills = new();
                foreach(DescriptionsDTO skillDetail in abScoreDTO.SkillsDetailed)
                {                    
                    SchemaDescriptionDTO newSkill = new();
                    newSkill.Name = skillDetail.Name;
                    newSkill.UpdatedAt = skillDetail.UpdatedAt;

                    // Same Custom Rule for Description: Concatenating array together with space delimiter
                    newSkill.Description = string.Join(" ", skillDetail.Desc);
                    newAbility.Skills.Add(newSkill);
                }
                exportDTO.AbilityScores.Add(newAbility);
            }
            return exportDTO;
        }
        
        internal static SchemaRoot_AlignmentDTO MapToSchemaDTOs_Alignments(List<AlignmentDTO> alignments)
        {
            SchemaRoot_AlignmentDTO exportDTO = new();

            foreach(AlignmentDTO alignmentDTO in alignments)
            {
                SchemaAlignmentDTO newAlignment = new();
                
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
            MapToDescriptionsSchema(conditions, exportDTO.Conditions);
            return exportDTO;
        }

        internal static SchemaRoot_DamageTypeDTO MapToSchemaDTOs_DamageTypes(List<DescriptionsDTO> damageTypes)
        {
            SchemaRoot_DamageTypeDTO exportDTO = new();
            MapToDescriptionsSchema(damageTypes, exportDTO.DamageTypes);
            return exportDTO;
        }

        internal static SchemaRoot_EquipmentDTO MapToSchemaDTOs_Equipment(List<EquipmentDTO> equipment)
        {
            SchemaRoot_EquipmentDTO exportDTO = new();

            foreach(EquipmentDTO equipmentDTO in equipment)
            {
                SchemaEquipmentDTO newEquipment = new();

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

                // Equipment Category Assignment
                newEquipment.EquipmentCategory.Name = equipmentDTO.EquipmentCategoryDetail.Name;
                newEquipment.EquipmentCategory.UpdatedAt = equipmentDTO.EquipmentCategoryDetail.UpdatedAt;

                // Gear Category Assignment
                newEquipment.GearCategory.Name = equipmentDTO.GearCategoryDetail.Name;
                newEquipment.GearCategory.UpdatedAt = equipmentDTO.GearCategoryDetail.UpdatedAt;

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
                foreach(Equipment_ContentDTO contentDTO in equipmentDTO.Contents)
                {
                    SchemaEquipment_ContentDTO newContentDTO = new();
                    newContentDTO.Name = contentDTO.Item.Name;
                    newContentDTO.Quantity = contentDTO.Quantity;
                    newEquipment.Content.Add(newContentDTO);
                }

                // Properties Assignment
                MapToDescriptionsSchema(equipmentDTO.PropertiesDetail, newEquipment.Properties);
                exportDTO.Equipment.Add(newEquipment);
            }

            return exportDTO;
        }

        internal static SchemaRoot_LanguageDTO MapToSchemaDTOs_Languages(List<LanguageDTO> languages)
        {
            SchemaRoot_LanguageDTO exportDTO = new();

            foreach(LanguageDTO languageDTO in languages)
            {
                SchemaLanguageDTO newLanguage = new();
                newLanguage.Name = languageDTO.Name;
                newLanguage.Type = languageDTO.Type;
                newLanguage.Script = languageDTO.Script;
                newLanguage.UpdatedAt = languageDTO.UpdatedAt;

                newLanguage.TypicalSpeakers = string.Join(", ", languageDTO.TypicalSpeakers);
                exportDTO.Languages.Add(newLanguage);
            }

            return exportDTO;
        }

        internal static SchemaRoot_MagicSchoolDTO MapToSchemaDTOs_MagicSchools(List<DescriptionDTO> magicSchools)
        {
            SchemaRoot_MagicSchoolDTO exportDTO = new();
            MapToDescriptionSchema(magicSchools, exportDTO.MagicSchools);
            return exportDTO;
        }

        internal static SchemaRoot_RuleSectionDTO MapToSchemaDTOs_RuleSections(List<DescriptionDTO> ruleSections)
        {
            SchemaRoot_RuleSectionDTO exportDTO = new();
            MapToDescriptionSchema(ruleSections, exportDTO.RuleSections);
            return exportDTO;
        }

        internal static SchemaRoot_SkillDTO MapToSchemaDTOs_Skills(List<SkillDTO> skills)
        {
            SchemaRoot_SkillDTO exportDTO = new();

            foreach(SkillDTO skillDTO in skills)
            {
                SchemaSkillDTO newSkill = new();
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
            MapToDescriptionsSchema(weaponProperties, exportDTO.WeaponProperties);
            return exportDTO;            
        }

        #endregion
        
    }
}