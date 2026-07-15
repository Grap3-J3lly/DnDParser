// The DTOs are in their own folder. I have them under a basic Input vs. Output file structure, 
// but I could see as this scaled up maybe having Input and Output be folders with each category 
// (Ability Scores vs. Races vs. Classes etc.) having their own files.

using System.Text.Json;

namespace DndParser
{

    public class Parser
    {
        public const string dndBaseUrl = "https://www.dnd5eapi.co";
        public const string filePath = "output/";
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

            // TODO: Immediate Queue:
            // - Equipment Categories
            // - Equipment
            // - Traits
            // - Subraces
            // - Races

            CategoryDTO categoryDTO = await GetDTOAtUrl<CategoryDTO>(apiYear2014);
            // await TryParseAbilityScores(categoryDTO.AbilityScores); // ---DONE--- //
            // await TryParseAlignments(categoryDTO.Alignments); // ---DONE--- //
            // await TryParseBackgrounds(categoryDTO.Backgrounds); // TBD
            // ResultsDTO results_backgrounds = await GetDTOAtUrl<ResultsDTO>(categoryDTO.Backgrounds);

            // await TryParseClasses(categoryDTO.Classes);
            
            // await TryParseConditions(categoryDTO.Conditions); // ---DONE--- //
            // await TryParseDamageTypes(categoryDTO.DamageTypes); // ---DONE--- //

            await TryParseEquipment(categoryDTO.Equipment);
            // await TryParseEquipmentCategories(categoryDTO.EquipmentCategories);
            
            // await TryParseFeats(categoryDTO.Feats); // TBD
            // ResultsDTO results_feats = await GetDTOAtUrl<ResultsDTO>(categoryDTO.Feats);
            
            // await TryParseFeatures(categoryDTO.Features);

            // await TryParseLanguages(categoryDTO.Languages); // ---DONE--- //

            // await TryParseMagicItems(categoryDTO.MagicItems);

            // await TryParseMagicSchools(categoryDTO.MagicSchools); // ---DONE--- //

            // await TryParseMonsters(categoryDTO.Monsters);
            // await TryParseProficiencies(categoryDTO.Proficiencies);
            // await TryParseRaces(categoryDTO.Races);
            
            // await TryParseRuleSections(categoryDTO.RuleSections); // ---DONE--- //
            
            // await TryParseRules(categoryDTO.Rules);
            
            // await TryParseSkills(categoryDTO.Skills); // ---DONE--- //
            
            // await TryParseSpells(categoryDTO.Spells);
            // await TryParseSubclasses(categoryDTO.Subclasses);
            // await TryParseSubraces(categoryDTO.Subraces);
            // await TryParseTraits(categoryDTO.Traits);
            
            // await TryParseWeaponProperties(categoryDTO.WeaponProperties); // ---DONE--- //
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
        #region AbilityScores

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
                SchemaRoot_AbilityScoreDTO exportDTO = SchemaMapper.MapToSchemaDTOs_AbilityScores(abilityScoreDTOs);
                
                // Prepare to Print                
                ExportData(exportDTO, "AbilityScores.txt");
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
        #region Alignments

        private static async Task TryParseAlignments(string alignmentsUrl)
        {
            try
            {
                List<AlignmentDTO> alignmentDTOs = new();

                ResultsDTO results_alignments = await GetDTOAtUrl<ResultsDTO>(alignmentsUrl);
                await AddDTOToList(results_alignments.Results, alignmentDTOs);

                SchemaRoot_AlignmentDTO exportDTO = SchemaMapper.MapToSchemaDTOs_Alignments(alignmentDTOs);
                ExportData(exportDTO, "Alignments.txt");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //	    PARSE - CLASSES
	    // --------------------------------
        #region Classes

        private static async Task TryParseClasses(string classesUrl)
        {
            try
            {
                ResultsDTO results_classes = await GetDTOAtUrl<ResultsDTO>(classesUrl);

                string response = await WebClient.GetDataAtURL(dndBaseUrl, results_classes.Results[0].Url);
                Console.WriteLine($"Class Details: {response}");
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
        #region Conditions

        private static async Task TryParseConditions(string conditionsUrl)
        {
            try
            {
                List<DescriptionsDTO> conditionDTOs = new();

                ResultsDTO results_conditions = await GetDTOAtUrl<ResultsDTO>(conditionsUrl);
                await AddDTOToList(results_conditions.Results, conditionDTOs);

                SchemaRoot_ConditionsDTO exportDTO = SchemaMapper.MapToSchemaDTOs_Conditions(conditionDTOs);
                ExportData(exportDTO, "Conditions.txt");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //	    PARSE - DAMAGE TYPES
	    // --------------------------------
        #region DamageTypes

        private static async Task TryParseDamageTypes(string damageTypesUrl)
        {
            try
            {
                List<DescriptionsDTO> damageTypeDTOs = new();
                ResultsDTO results_damageTypes = await GetDTOAtUrl<ResultsDTO>(damageTypesUrl);
                await AddDTOToList(results_damageTypes.Results, damageTypeDTOs);

                SchemaRoot_DamageTypeDTO exportDTO = SchemaMapper.MapToSchemaDTOs_DamageTypes(damageTypeDTOs);
                ExportData(exportDTO, "DamageTypes.txt");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //	    PARSE - EQUIPMENT
	    // --------------------------------
        #region Equipment

        private static async Task TryParseEquipment(string equipmentUrl)
        {
            try
            {
                ResultsDTO results_equipment = await GetDTOAtUrl<ResultsDTO>(equipmentUrl);

                foreach(UrlDTO urlDTO in results_equipment.Results)
                {
                    string response = await WebClient.GetDataAtURL(dndBaseUrl, urlDTO.Url);

                    using (StreamWriter writer = new StreamWriter(filePath + "temp.txt", append: true))
                    {
                        writer.WriteLine(response);
                    }

                    Console.WriteLine($"Equipment Details: {response}");
                }
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //  PARSE - EQUIPMENT CATEGORIES
	    // --------------------------------
        #region Equipment Categories

        private static async Task TryParseEquipmentCategories(string equipmentCategoryUrl)
        {
            try
            {
                ResultsDTO results_equipmentCategories = await GetDTOAtUrl<ResultsDTO>(equipmentCategoryUrl);
                string response = await WebClient.GetDataAtURL(dndBaseUrl, results_equipmentCategories.Results[0].Url);
                Console.WriteLine($"Equipment Category Details: {response}");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //      PARSE - FEATURES
	    // --------------------------------
        #region Features

        private static async Task TryParseFeatures(string featuresUrl)
        {
            try
            {
                ResultsDTO results_features = await GetDTOAtUrl<ResultsDTO>(featuresUrl);
                string response = await WebClient.GetDataAtURL(dndBaseUrl, results_features.Results[0].Url);
                Console.WriteLine($"Features Details: {response}");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //	    PARSE - LANGUAGES
	    // --------------------------------
        #region Languages

        private static async Task TryParseLanguages(string languageUrl)
        {
            try
            {
                List<LanguageDTO> languageDTOs = new();

                ResultsDTO results_languages = await GetDTOAtUrl<ResultsDTO>(languageUrl);
                await AddDTOToList(results_languages.Results, languageDTOs);

                SchemaRoot_LanguageDTO exportDTO = SchemaMapper.MapToSchemaDTOs_Languages(languageDTOs);
                ExportData(exportDTO, "Languages.txt");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //      PARSE - MAGIC ITEMS
	    // --------------------------------
        #region Magic Items

        private static async Task TryParseMagicItems(string magicItemsUrl)
        {
            try
            {
                ResultsDTO results_magicItems = await GetDTOAtUrl<ResultsDTO>(magicItemsUrl);
                string response = await WebClient.GetDataAtURL(dndBaseUrl, results_magicItems.Results[0].Url);
                Console.WriteLine($"Magic Items Details: {response}");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //      PARSE - MAGIC SCHOOLS
	    // --------------------------------
        #region Magic Schools

        private static async Task TryParseMagicSchools(string magicSchoolsUrl)
        {
            try
            {
                List<DescriptionDTO> magicSchoolDTOs = new();

                ResultsDTO results_magicSchools = await GetDTOAtUrl<ResultsDTO>(magicSchoolsUrl);
                await AddDTOToList(results_magicSchools.Results, magicSchoolDTOs);
                SchemaRoot_MagicSchoolDTO exportDTO = SchemaMapper.MapToSchemaDTOs_MagicSchools(magicSchoolDTOs);
                ExportData(exportDTO, "MagicSchools.txt");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //      PARSE - MONSTERS
	    // --------------------------------
        #region Monsters

        private static async Task TryParseMonsters(string monstersUrl)
        {
            try
            {
                ResultsDTO results_monsters = await GetDTOAtUrl<ResultsDTO>(monstersUrl);
                string response = await WebClient.GetDataAtURL(dndBaseUrl, results_monsters.Results[0].Url);
                Console.WriteLine($"Monsters Details: {response}");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //	    PARSE - PROFICIENCIES
	    // --------------------------------
        #region Proficiencies

        private static async Task TryParseProficiencies(string proficienciesUrl)
        {
            try
            {
                ResultsDTO results_proficiencies = await GetDTOAtUrl<ResultsDTO>(proficienciesUrl);

                string response = await WebClient.GetDataAtURL(dndBaseUrl, results_proficiencies.Results[0].Url);
                Console.WriteLine($"Proficiencies Detailed: {response}");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //      PARSE - RACES
	    // --------------------------------
        #region Races

        private static async Task TryParseRaces(string racesUrl)
        {
            try
            {
                ResultsDTO results_races = await GetDTOAtUrl<ResultsDTO>(racesUrl);
                string response = await WebClient.GetDataAtURL(dndBaseUrl, results_races.Results[0].Url);
                Console.WriteLine($"Races Details: {response}");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //      PARSE - RULE SECTIONS
	    // --------------------------------
        #region Rule Sections

        private static async Task TryParseRuleSections(string ruleSectionsUrl)
        {
            try
            {
                List<DescriptionDTO> ruleSectionDTOs = new();
                ResultsDTO results_ruleSections = await GetDTOAtUrl<ResultsDTO>(ruleSectionsUrl);
                await AddDTOToList(results_ruleSections.Results, ruleSectionDTOs);
                SchemaRoot_RuleSectionDTO exportDTO = SchemaMapper.MapToSchemaDTOs_RuleSections(ruleSectionDTOs);
                ExportData(exportDTO, "RuleSections.txt");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //      PARSE - RULES
	    // --------------------------------
        #region Rules

        private static async Task TryParseRules(string rulesUrl)
        {
            try
            {
                ResultsDTO results_rules = await GetDTOAtUrl<ResultsDTO>(rulesUrl);
                string response = await WebClient.GetDataAtURL(dndBaseUrl, results_rules.Results[0].Url);
                Console.WriteLine($"Rules Details: {response}");
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
        #region Skills

        private static async Task TryParseSkills(string skillsUrl)
        {
            try
            {
                List<SkillDTO> skillDTOs = new();

                ResultsDTO results_skills = await GetDTOAtUrl<ResultsDTO>(skillsUrl);
                await AddDTOToList(results_skills.Results, skillDTOs);
                await AddSkillAbilityScore(skillDTOs);

                SchemaRoot_SkillDTO exportDTO = SchemaMapper.MapToSchemaDTOs_Skills(skillDTOs);
                ExportData(exportDTO, "Skills.txt");
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
                DescriptionsDTO? descriptionDTO = await GetDTOAtUrl<DescriptionsDTO>(skillDTO.AbilityScore.Url);
                skillDTO.AbilityScoreDetailed = descriptionDTO;
            }
        }

        #endregion

        // --------------------------------
        //      PARSE - SPELLS
	    // --------------------------------
        #region Spells

        private static async Task TryParseSpells(string spellsUrl)
        {
            try
            {
                ResultsDTO results_spells = await GetDTOAtUrl<ResultsDTO>(spellsUrl);
                string response = await WebClient.GetDataAtURL(dndBaseUrl, results_spells.Results[0].Url);
                Console.WriteLine($"Spell Details: {response}");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //      PARSE - SUBCLASSES
	    // --------------------------------
        #region Subclasses

        private static async Task TryParseSubclasses(string subclassesUrl)
        {
            try
            {
                ResultsDTO results_subclasses = await GetDTOAtUrl<ResultsDTO>(subclassesUrl);
                string response = await WebClient.GetDataAtURL(dndBaseUrl, results_subclasses.Results[0].Url);
                Console.WriteLine($"Subclasses Details: {response}");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //      PARSE - SUBRACES
	    // --------------------------------
        #region Subraces

        private static async Task TryParseSubraces(string subracesUrl)
        {
            try
            {
                ResultsDTO results_subraces = await GetDTOAtUrl<ResultsDTO>(subracesUrl);
                string response = await WebClient.GetDataAtURL(dndBaseUrl, results_subraces.Results[0].Url);
                Console.WriteLine($"Subraces Details: {response}");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //      PARSE - TRAITS
	    // --------------------------------
        #region Traits

        private static async Task TryParseTraits(string traitsUrl)
        {
            try
            {
                ResultsDTO results_traits = await GetDTOAtUrl<ResultsDTO>(traitsUrl);
                string response = await WebClient.GetDataAtURL(dndBaseUrl, results_traits.Results[0].Url);
                Console.WriteLine($"Traits Details: {response}");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        #endregion

        // --------------------------------
        //  PARSE - WEAPON PROPERTIES
	    // --------------------------------
        #region Weapon Properties

        private static async Task TryParseWeaponProperties(string weaponPropertiesUrl)
        {
            try
            {
                List<DescriptionsDTO> weaponPropertiesDTO = new();
                ResultsDTO results_weaponProperties = await GetDTOAtUrl<ResultsDTO>(weaponPropertiesUrl);
                await AddDTOToList(results_weaponProperties.Results, weaponPropertiesDTO);
                SchemaRoot_WeaponPropertyDTO exportDTO = SchemaMapper.MapToSchemaDTOs_WeaponProperties(weaponPropertiesDTO);
                ExportData(exportDTO, "WeaponProperties.txt");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
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

        private static void ExportData<T>(T exportDTO, string fileName)
        {
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonOutput = JsonSerializer.Serialize(exportDTO, jsonOptions);

            using (StreamWriter writer = new StreamWriter(filePath + fileName, append: true))
            {
                writer.WriteLine(jsonOutput);
            }

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