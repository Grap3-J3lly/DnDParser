// The DTOs are in their own folder. I have them under a basic Input vs. Output file structure, 
// but I could see as this scaled up maybe having Input and Output be folders with each category 
// (Ability Scores vs. Races vs. Classes etc.) having their own files.

using System.Text.Json;

namespace DndParser
{

    public class Parser
    {
        public const string dndBaseUrl = "https://www.dnd5eapi.co";
        public const string apiYear2014 = "/api/2014/";
        public const string apiYear2024 = "/api/2024/";

        public static async Task StartParser(int categoryIndex = -1)
        {
            // If there were more than one step to this parser, it would be included sequentially here
            // I'm hardcoding in the starting point for the ability scores, but this could easily be configured and sent in as an additional parameter
            
            // Known Missing Categories for 2014:
            // - Backgrounds (1)
            // - Feats (1)

            // Known Missing Categories for 2024:
            // - Backgrounds (4)
            // - Levels (0)
            // - Monsters (3)
            // - Rule-sections (0)
            // - Rules (0)
            // - Spells (0)

            CategoryDTO categoryDTO = await GetDTOAtUrl<CategoryDTO>(apiYear2014);
            // await TryParseAbilityScores(categoryDTO.AbilityScores); // ---DONE--- //
            // await TryParseAlignments(categoryDTO.Alignments); // ---DONE--- //
            // await TryParseBackgrounds(categoryDTO.Backgrounds); // TBD
            // await TryParseClasses(categoryDTO.Classes);
            await TryParseConditions(categoryDTO.Conditions);
            // await TryParseDamageTypes(categoryDTO.DamageTypes);
            // await TryParseEquipment(categoryDTO.Equipment);
            // await TryParseEquipmentCategories(categoryDTO.EquipmentCategories);
            // await TryParseFeats(categoryDTO.Feats); // TBD
            // await TryParseFeatures(categoryDTO.Features);
            // await TryParseLanguages(categoryDTO.Languages);
            // await TryParseMagicItems(categoryDTO.MagicItems);
            // await TryParseMagicSchools(categoryDTO.MagicSchools);
            // await TryParseMonsters(categoryDTO.Monsters);
            // await TryParseProficiencies(categoryDTO.Proficiencies);
            // await TryParseRaces(categoryDTO.Races);
            // await TryParseRuleSections(categoryDTO.RuleSections);
            // await TryParseRules(categoryDTO.Rules);
            // await TryParseSkills(categoryDTO.Skills); // ---DONE--- //
            // await TryParseSpells(categoryDTO.Spells);
            // await TryParseSubclasses(categoryDTO.Subclasses);
            // await TryParseSubraces(categoryDTO.Subraces);
            // await TryParseTraits(categoryDTO.Traits);
            // await TryParseWeaponProperties(categoryDTO.WeaponProperties);

            // ResultsDTO results_backgrounds = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Backgrounds);
            // ResultsDTO results_classes = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Classes);
            // ResultsDTO results_damageTypes = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.DamageTypes);
            // ResultsDTO results_equipment = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Equipment);
            // ResultsDTO results_equipmentCategories = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.EquipmentCategories);
            // ResultsDTO results_feats = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Feats);
            // ResultsDTO results_features = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Features);
            // ResultsDTO results_languages = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Languages);
            // ResultsDTO results_magicItems = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.MagicItems);
            // ResultsDTO results_magicSchools = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.MagicSchools);
            // ResultsDTO results_monsters = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Monsters);
            // ResultsDTO results_proficiencies = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Proficiencies);
            // ResultsDTO results_races = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Races);
            // ResultsDTO results_ruleSections = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.RuleSections);
            // ResultsDTO results_rules = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Rules);
            // ResultsDTO results_spells = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Spells);
            // ResultsDTO results_subclasses = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Subclasses);
            // ResultsDTO results_subraces = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Subraces);
            // ResultsDTO results_traits = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Traits);
            // ResultsDTO results_weaponProperties = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.WeaponProperties);

            // ResultsDTO focusDTO = results_alignments;

            // foreach(UrlDTO urlObj in focusDTO.Results)
            // {
            //     Console.WriteLine($"Results: {urlObj.Name}");
            // }

        }

        // This might be rate locked
        public static async Task TryParseAll(CategoryDTO categoryDTO)
        {
            List<Task> tasks = new List<Task> {
                TryParseAbilityScores(categoryDTO.AbilityScores)
            };
            await Task.WhenAll(tasks);
        }

        // --------------------------------
        //	    PARSE - ABILITY SCORE
	    // --------------------------------
        #region Parse_AbilityScore

        /// <summary>
        /// Attempts to connect to Ability-Score specific URL and tries to process data as such.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private static async Task TryParseAbilityScores(string abilityScoresUrl)
        {
            try
            {
                // Import DTOs for later mapping
                List<AbilityScoreDTO> abilityScoreDTOs = new();

                // Get initial list of scores
                ResultsDTO resultsDTO = await GetDTOAtUrl<ResultsDTO>(abilityScoresUrl);

                // Load and store the full score details
                await AddDTOToList(resultsDTO.Results, abilityScoreDTOs);

                // Load and store all skill details
                foreach(AbilityScoreDTO scoreDTO in abilityScoreDTOs)
                {
                    await AddDTOToList(scoreDTO.Skills, scoreDTO.SkillsDetailed);
                }

                // Map to Schema
                SchemaRoot_AbilityScoreDTO exportDTO = MapToSchemaDTOs_AbilityScores(abilityScoreDTOs);
                
                // Prepare to Print                
                ExportData(exportDTO);
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //	    PARSE - ALIGNMENTS
	    // --------------------------------
        #region Parse_Alignments

        private static async Task TryParseAlignments(string alignmentsUrl)
        {
            try
            {
                List<AlignmentDTO> alignmentDTOs = new();

                ResultsDTO results_alignments = await GetDTOAtUrl<ResultsDTO>(alignmentsUrl);
                await AddDTOToList(results_alignments.Results, alignmentDTOs);

                SchemaRoot_AlignmentDTO exportDTO = MapToSchemaDTOs_Alignments(alignmentDTOs);
                ExportData(exportDTO);
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //	    PARSE - CONDITIONS
	    // --------------------------------
        #region Parse_Conditions

        private static async Task TryParseConditions(string conditionsUrl)
        {
            try
            {
                List<DescriptionDTO> conditionDTOs = new();

                ResultsDTO results_conditions = await GetDTOAtUrl<ResultsDTO>(conditionsUrl);
                await AddDTOToList(results_conditions.Results, conditionDTOs);

                SchemaRoot_ConditionsDTO exportDTO = MapToSchemaDTOs_Conditions(conditionDTOs);
                ExportData(exportDTO);
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //	    PARSE - SKILLS
	    // --------------------------------
        #region Parse_Skills

        private static async Task TryParseSkills(string skillsUrl)
        {
            try
            {
                List<SkillDTO> skillDTOs = new();

                ResultsDTO results_skills = await GetDTOAtUrl<ResultsDTO>(skillsUrl);
                await AddDTOToList(results_skills.Results, skillDTOs);
                await AddSkillAbilityScore(skillDTOs);

                SchemaRoot_SkillDTO exportDTO = MapToSchemaDTOs_Skills(skillDTOs);
                ExportData(exportDTO);
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        private static async Task AddSkillAbilityScore(List<SkillDTO> skillDTOs)
        {
            foreach(SkillDTO skillDTO in skillDTOs)
            {
                DescriptionDTO? descriptionDTO = await GetDTOAtUrl<DescriptionDTO>(skillDTO.AbilityScore.Url);
                skillDTO.AbilityScoreDetailed = descriptionDTO;
            }
        }

        #endregion

        // --------------------------------
        //	    SCHEMA MAPPING
	    // --------------------------------

        #region SchemaMapping

        /// <summary>
        /// Part of what would be a family of Mapping functions, this one focused on Ability Scores. Assumption: List of Ability Scores and List of Skills are Unsorted
        /// </summary>
        /// <param name="scores"></param>
        /// <param name="skills"></param>
        private static SchemaRoot_AbilityScoreDTO MapToSchemaDTOs_AbilityScores(List<AbilityScoreDTO> scores)
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
                foreach(DescriptionDTO skillDetail in abScoreDTO.SkillsDetailed)
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
        
        private static SchemaRoot_AlignmentDTO MapToSchemaDTOs_Alignments(List<AlignmentDTO> alignments)
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

        private static SchemaRoot_ConditionsDTO MapToSchemaDTOs_Conditions(List<DescriptionDTO> conditions)
        {
            SchemaRoot_ConditionsDTO exportDTO = new();

            foreach(DescriptionDTO conditionDTO in conditions)
            {
                SchemaDescriptionDTO newCondition = new();
                newCondition.Name = conditionDTO.Name;
                newCondition.UpdatedAt = conditionDTO.UpdatedAt;

                newCondition.Description = string.Join(" ", conditionDTO.Desc);
                exportDTO.Conditions.Add(newCondition);
            }

            return exportDTO;
        }

        private static SchemaRoot_SkillDTO MapToSchemaDTOs_Skills(List<SkillDTO> skills)
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

        #endregion
        
        // --------------------------------
        //	    UNIVERSAL FUNCTIONS
	    // --------------------------------
        #region UniversalFunctions

        private static async Task<T> GetDTOAtUrl<T>(string url)
        {
            string response = await WebClient.GetDataAtURL(dndBaseUrl, url);

            if(response == string.Empty)
            {
                Console.WriteLine($"No Initial Response Found from Client");
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(response);
        }

        /// <summary>
        /// Loads in DTO details at url in UrlDTO, serializes and adds them to list of specified type
        /// </summary>
        /// <param name="urlsToLoad"></param>
        /// <param name="dtoList"></param>
        private static async Task AddDTOToList<T>(List<UrlDTO> urlsToLoad, List<T> dtoList)
        {
            foreach(UrlDTO urlDTO in urlsToLoad)
            {
                T? newDTO = await GetDTOAtUrl<T>(urlDTO.Url);
                dtoList.Add(newDTO);
            }
        }

        private static void ExportData<T>(T exportDTO)
        {
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonOutput = JsonSerializer.Serialize(exportDTO, jsonOptions);

            Console.WriteLine("\nFinal Payload:");
            Console.WriteLine(jsonOutput);
        }

        #endregion

        // --------------------------------
        //	    DEBUG TOOLS
	    // --------------------------------

        #region DebugTools

        // Sole purpose for this is to help me debug
        private static void PrintDTOData(string response)
        {
            Console.WriteLine($"Printing DTO Data: {response}");
        }

        #endregion
    }
}