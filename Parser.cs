// The DTOs are in their own folder. I have them under a basic Input vs. Output file structure, 
// but I could see as this scaled up maybe having Input and Output be folders with each category 
// (Ability Scores vs. Races vs. Classes etc.) having their own files.

using System.Text.Json;
using System.Diagnostics;

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

            // TODO: Replace All Results Processing with Bulk Load Call

            CategoryDTO categoryDTO = await GetDTOAtUrl<CategoryDTO>(apiYear2014);

            long startTime = Stopwatch.GetTimestamp();

            // await TryParseAbilityScores(categoryDTO.AbilityScores);                                      // ---DONE--- //
            // await TryParseAlignments(categoryDTO.Alignments);                                            // ---DONE--- //
            // await TryParseBackgrounds(categoryDTO.Backgrounds);                                          // TBD
            // ResultsDTO results_backgrounds = await GetDTOAtUrl<ResultsDTO>(categoryDTO.Backgrounds);

            // await TryParseClasses(categoryDTO.Classes);
            
            // await TryParseConditions(categoryDTO.Conditions);                                            // ---DONE--- //
            // await TryParseDamageTypes(categoryDTO.DamageTypes);                                          // ---DONE--- //

            // await TryParseEquipment(categoryDTO.Equipment);                                              // ---DONE--- //
            // await TryParseEquipmentCategories(categoryDTO.EquipmentCategories);
            
            // await TryParseFeats(categoryDTO.Feats);                                                      // TBD
            // ResultsDTO results_feats = await GetDTOAtUrl<ResultsDTO>(categoryDTO.Feats);
            
            // await TryParseFeatures(categoryDTO.Features);

            // await TryParseLanguages(categoryDTO.Languages);                                              // ---DONE--- //

            // await TryParseMagicItems(categoryDTO.MagicItems);

            // await TryParseMagicSchools(categoryDTO.MagicSchools);                                        // ---DONE--- //

            // await TryParseMonsters(categoryDTO.Monsters);
            // await TryParseProficiencies(categoryDTO.Proficiencies);
            // await TryParseRaces(categoryDTO.Races);
            
            // await TryParseRuleSections(categoryDTO.RuleSections);                                        // ---DONE--- //
            
            // await TryParseRules(categoryDTO.Rules);
            
            // await TryParseSkills(categoryDTO.Skills);                                                    // ---DONE--- //
            
            // await TryParseSpells(categoryDTO.Spells);
            // await TryParseSubclasses(categoryDTO.Subclasses);
            // await TryParseSubraces(categoryDTO.Subraces);
            // await TryParseTraits(categoryDTO.Traits);
            
            await TryParseWeaponProperties(categoryDTO.WeaponProperties);                                // ---DONE--- //

            long endTime = Stopwatch.GetTimestamp();
            TimeSpan elapsed = Stopwatch.GetElapsedTime(startTime, endTime);
            Console.WriteLine($"Execution Time: {elapsed.TotalMilliseconds} ms");
            Console.WriteLine($"Execution Time: {elapsed.TotalSeconds} s");
            Console.WriteLine($"Execution Time: {elapsed.TotalMinutes} minutes");

            // Previous Runtime for all Equipment Loading:
            // MS: 295395.3763
            // Seconds: 295.3953763
            // Minutes: 4.9232562717

            // Current Runtime for all Equipment Loading:
            // MS: 7578.653
            // Seconds: 7.578653
            // Minutes: 0.12631088333333335

            // Previous Runtime for EquipmentDTO Assignment:
            // MS: 233056.2671
            // Seconds: 233.0562671
            // Minutes: 3.8842711183333334

            // Current Runtime for EquipmentDTO Assignment:
            // MS: 7889.0775
            // Seconds: 7.8890775
            // Minutes: 0.131484625
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
        private static async Task TryParseAbilityScores(string abilityScoresUrl) // Execution Time: ~.64s
        {
            try
            {
                // Import DTOs for later mapping
                List<AbilityScoreDTO> abilityScoreDTOs = new();

                // Get initial list of scores
                ResultsDTO results_abilityScores = await GetDTOAtUrl<ResultsDTO>(abilityScoresUrl);

                // Load and store the full score details
                await BulkLoadDTOToList(results_abilityScores.Results, abilityScoreDTOs);

                // Load and store all skill details
                await AddAbilityScoreSkills(abilityScoreDTOs);

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

        private static async Task AddAbilityScoreSkills(List<AbilityScoreDTO> abilityScoreDTOs)
        {
            List<(Action<IDataTransferObject> assignmentResult, Type dtoType, string url)> dtosToLoad = new();
            foreach(AbilityScoreDTO scoreDTO in abilityScoreDTOs)
                {
                    // await AddDTOToList(scoreDTO.Skills, scoreDTO.SkillsDetailed);

                    string focusUrl;
                    for(int index = 0; index < scoreDTO.Skills.Count; index++)
                    {
                        focusUrl = scoreDTO.Skills[index].Url;
                        dtosToLoad.Add((
                            result => scoreDTO.SkillsDetailed.Add((DescriptionsDTO)result),
                            typeof(DescriptionsDTO),
                            focusUrl
                        ));
                    }
                }
                await BulkLoadDTOToTuple(dtosToLoad);
        }

        #endregion

        // --------------------------------
        //	    PARSE - ALIGNMENTS
	    // --------------------------------
        #region Alignments

        private static async Task TryParseAlignments(string alignmentsUrl) // Execution Time: ~.38s
        {
            try
            {
                List<AlignmentDTO> alignmentDTOs = new();
                ResultsDTO results_alignments = await GetDTOAtUrl<ResultsDTO>(alignmentsUrl);

                await BulkLoadDTOToList(results_alignments.Results, alignmentDTOs);

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

        private static async Task TryParseConditions(string conditionsUrl) // Execution Time: ~.41s
        {
            try
            {
                List<DescriptionsDTO> conditionDTOs = new();
                ResultsDTO results_conditions = await GetDTOAtUrl<ResultsDTO>(conditionsUrl);

                await BulkLoadDTOToList(results_conditions.Results, conditionDTOs);

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

        private static async Task TryParseDamageTypes(string damageTypesUrl) // Execution Time: ~.42s
        {
            try
            {
                List<DescriptionsDTO> damageTypeDTOs = new();
                ResultsDTO results_damageTypes = await GetDTOAtUrl<ResultsDTO>(damageTypesUrl);

                await BulkLoadDTOToList(results_damageTypes.Results, damageTypeDTOs);

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

        private static async Task TryParseEquipment(string equipmentUrl) // Execution Time: ~7.43s
        {
            try
            {
                List<EquipmentDTO> equipmentDTOs = new();
                ResultsDTO results_equipment = await GetDTOAtUrl<ResultsDTO>(equipmentUrl);
                await BulkLoadDTOToList(results_equipment.Results, equipmentDTOs);

                await AddEquipmentDTODetails(equipmentDTOs);
                
                SchemaRoot_EquipmentDTO exportDTO = SchemaMapper.MapToSchemaDTOs_Equipment(equipmentDTOs);
                ExportData(exportDTO, "Equipment.txt");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        private static async Task AddEquipmentDTODetails(List<EquipmentDTO> equipmentDTOs)
        {
            List<(Action<IDataTransferObject> assignmentResult, Type dtoType, string url)> dtosToLoad = new();
            foreach(EquipmentDTO equipmentDTO in equipmentDTOs)
            {
                // Equipment Category
                string focusUrl = equipmentDTO.EquipmentCategory?.Url;
                if(!string.IsNullOrEmpty(focusUrl))
                {
                    dtosToLoad.Add((
                        result => equipmentDTO.EquipmentCategoryDetail = (DescriptionDTO)result, 
                        typeof(DescriptionDTO), 
                        focusUrl
                        ));
                }

                // Gear Category
                focusUrl = equipmentDTO.GearCategory?.Url;
                if(!string.IsNullOrEmpty(focusUrl))
                {
                    dtosToLoad.Add((
                        result => equipmentDTO.GearCategoryDetail = (DescriptionDTO)result, 
                        typeof(DescriptionDTO), 
                        focusUrl
                        ));
                }

                // Damage
                focusUrl = equipmentDTO.Damage.DamageType?.Url;
                if(!string.IsNullOrEmpty(focusUrl))
                {
                    dtosToLoad.Add((
                        result => equipmentDTO.Damage.DamageTypeDetail = (DescriptionsDTO)result,
                        typeof(DescriptionsDTO),
                        focusUrl
                        ));
                }

                // Two Handed Damage
                focusUrl = equipmentDTO.TwoHandedDamage.DamageType?.Url;
                if(!string.IsNullOrEmpty(focusUrl))
                {
                    dtosToLoad.Add((
                        result => equipmentDTO.TwoHandedDamage.DamageTypeDetail = (DescriptionsDTO)result,
                        typeof(DescriptionsDTO),
                        focusUrl
                        ));
                }
                // Properties
                for(int index = 0; index < equipmentDTO.Properties.Count; index++)
                {
                    focusUrl = equipmentDTO.Properties[index].Url;
                    dtosToLoad.Add((
                        result => equipmentDTO.PropertiesDetail.Add((DescriptionsDTO)result),
                        typeof(DescriptionsDTO),
                        focusUrl
                    ));
                }

            }
            await BulkLoadDTOToTuple(dtosToLoad);
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

        private static async Task TryParseLanguages(string languageUrl) // Execution Time: ~.40s
        {
            try
            {
                List<LanguageDTO> languageDTOs = new();

                ResultsDTO results_languages = await GetDTOAtUrl<ResultsDTO>(languageUrl);

                await BulkLoadDTOToList(results_languages.Results, languageDTOs);

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

        private static async Task TryParseMagicSchools(string magicSchoolsUrl) // Execution Time: ~.42s
        {
            try
            {
                List<DescriptionDTO> magicSchoolDTOs = new();

                ResultsDTO results_magicSchools = await GetDTOAtUrl<ResultsDTO>(magicSchoolsUrl);

                await BulkLoadDTOToList(results_magicSchools.Results, magicSchoolDTOs);
                
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

        private static async Task TryParseRuleSections(string ruleSectionsUrl) // Execution Time: ~.48s
        {
            try
            {
                List<DescriptionDTO> ruleSectionDTOs = new();
                ResultsDTO results_ruleSections = await GetDTOAtUrl<ResultsDTO>(ruleSectionsUrl);

                await BulkLoadDTOToList(results_ruleSections.Results, ruleSectionDTOs);
                
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

        private static async Task TryParseSkills(string skillsUrl) // Execution Time: ~.81s
        {
            try
            {
                List<SkillDTO> skillDTOs = new();

                ResultsDTO results_skills = await GetDTOAtUrl<ResultsDTO>(skillsUrl);
                await BulkLoadDTOToList(results_skills.Results, skillDTOs);
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
            List<(Action<IDataTransferObject> assignmentResult, Type dtoType, string url)> dtosToLoad = new();
            foreach(SkillDTO skillDTO in skillDTOs)
            {
                string focusUrl = skillDTO.AbilityScore?.Url;
                if(!string.IsNullOrEmpty(focusUrl))
                {
                    dtosToLoad.Add((
                        result => skillDTO.AbilityScoreDetailed = (DescriptionsDTO)result, 
                        typeof(DescriptionsDTO), 
                        focusUrl
                        ));
                }
            }
            await BulkLoadDTOToTuple(dtosToLoad);
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

        private static async Task TryParseWeaponProperties(string weaponPropertiesUrl) // Execution Time: ~.41s
        {
            try
            {
                List<DescriptionsDTO> weaponPropertiesDTO = new();
                ResultsDTO results_weaponProperties = await GetDTOAtUrl<ResultsDTO>(weaponPropertiesUrl);
                await BulkLoadDTOToList(results_weaponProperties.Results, weaponPropertiesDTO);
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

        private static async Task BulkLoadDTOToList<T>(List<UrlDTO> urlDTOs, List<T> dtoList)
        {
            List<string> urlsToLoad = new();
            foreach(UrlDTO urlDTO in urlDTOs)
            {
                urlsToLoad.Add(urlDTO.Url);
            }
            string[] results = await WebClient.GetDataAtURLBulk(dndBaseUrl, urlsToLoad);
            
            foreach(string result in results)
            {
                dtoList.Add(JsonSerializer.Deserialize<T>(result));
            }
        }

        private static async Task BulkLoadDTOToTuple(List<(Action<IDataTransferObject> assignResult, Type dtoType, string url)> tupleList)
        {
            List<string> urlsToLoad = new();

            foreach((Action<IDataTransferObject> assignmentResult, Type dtoType, string url) in tupleList)  // Separates the URLs for bulk loading
            {
                urlsToLoad.Add(url);
            }
            string[] results = await WebClient.GetDataAtURLBulk(dndBaseUrl, urlsToLoad);                    // Bulk loads URLs

            for(int index = 0; index < results.Length; index++) // Loops through results, deserializes using given type, assigns result using callback action
            {
                (Action<IDataTransferObject> assignResult, Type dtoType, string url) currentTuple = tupleList[index];
                
                IDataTransferObject deserializedResult = (IDataTransferObject)JsonSerializer.Deserialize(results[index], currentTuple.dtoType);        
                currentTuple.assignResult(deserializedResult);
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
    }
}