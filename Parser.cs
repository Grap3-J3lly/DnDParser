// The DTOs are in their own folder. I have them under a basic Input vs. Output file structure, 
// but I could see as this scaled up maybe having Input and Output be folders with each category 
// (Ability Scores vs. Races vs. Classes etc.) having their own files.

using System.Text.Json;
using System.Text.Json.Nodes;

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

            CategoryDTO categoryDTO = await GetSpecificDTOAtUrl<CategoryDTO>(apiYear2014);
            await TryParseAbilityScores(categoryDTO.AbilityScores);

            // ResultsDTO results_alignments = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Alignments);
            // ResultsDTO results_backgrounds = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Backgrounds);
            // ResultsDTO results_classes = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Classes);
            // ResultsDTO results_conditions = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Conditions);
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
            // ResultsDTO results_skills = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Skills);
            // ResultsDTO results_spells = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Spells);
            // ResultsDTO results_subclasses = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Subclasses);
            // ResultsDTO results_subraces = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Subraces);
            // ResultsDTO results_traits = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Traits);
            // ResultsDTO results_weaponProperties = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.WeaponProperties);

            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Alignments);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Backgrounds);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Classes);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Conditions);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.DamageTypes);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Equipment);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.EquipmentCategories);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Feats);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Features);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Languages);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.MagicItems);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.MagicSchools);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Monsters);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Proficiencies);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Races);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.RuleSections);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Rules);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Skills);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Spells);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Subclasses);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Subraces);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.Traits);
            // ResultsDTO focusDTO = await GetSpecificDTOAtUrl<ResultsDTO>(categoryDTO.WeaponProperties);

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
                List<UrlDTO> abilityScoreGeneralDTOs = new();
                List<AbilityScoreDTO> abilityScoreDTOs = new();
                List<DescriptionDTO> skillDTOs = new();

                // Get initial list of scores
                ResultsDTO resultsDTO = await GetSpecificDTOAtUrl<ResultsDTO>(abilityScoresUrl);

                // Load and store the full score details
                await AddAbilityScores(resultsDTO.Results, abilityScoreDTOs);

                // Load and store all skill details
                await AddSkills(abilityScoreDTOs, skillDTOs);

                // Map to Schema
                SchemaRootDTO exportDTO = MapToSchemaDTOs_AbilityScores(abilityScoreDTOs, skillDTOs);
                
                // Prepare to Print
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string jsonOutput = JsonSerializer.Serialize(exportDTO, jsonOptions);

                Console.WriteLine("\nFinal Payload:");
                Console.WriteLine(jsonOutput);

            }
            catch(Exception exception)
            {
                Console.WriteLine($"Caught unexpected exception: {exception}");
            }
        }

        /// <summary>
        /// Loads in ability score details based on info from smaller input DTOs, serializes and adds them to list of the more detailed abilityScoreDTOs
        /// </summary>
        /// <param name="client"></param>
        /// <param name="abilityScoreGeneralDTOs"></param>
        /// /// <param name="abilityScoreDTOs"></param>
        private static async Task AddAbilityScores(List<UrlDTO> abilityScoreGeneralDTOs, List<AbilityScoreDTO> abilityScoreDTOs)
        {
            foreach(UrlDTO abilityScoreSmall in abilityScoreGeneralDTOs)
            {
                // string response = await WebClient.GetDataAtURL(dndBaseUrl, abilityScoreSmall.Url);

                // if(response == string.Empty)
                // {
                //     Console.WriteLine("No Response from Client. Attempting next Iteration");
                //     continue;
                // }

                // // PrintDTOData("AbilityScores: " + response);

                // // Store the score details per ability score
                // AbilityScoreDTO? newAbilityScoreDTO = JsonSerializer.Deserialize<AbilityScoreDTO>(response);

                AbilityScoreDTO? newAbilityScoreDTO = await GetSpecificDTOAtUrl<AbilityScoreDTO>(abilityScoreSmall.Url);

                abilityScoreDTOs.Add(newAbilityScoreDTO);

            }
        }

        /// <summary>
        /// Runs through each abilityScoreDTO, grabs their list of Skills, and loads/stores the associated detailed SkillDTO
        /// </summary>
        /// <param name="client"></param>
        /// <param name="abilityScoreDTOs"></param>
        /// <param name="skillDTOs"></param>
        private static async Task AddSkills(List<AbilityScoreDTO> abilityScoreDTOs, List<DescriptionDTO> skillDTOs)
        {
            foreach(AbilityScoreDTO scoreDTO in abilityScoreDTOs)
            {
                foreach(UrlDTO skillGeneralDTO in scoreDTO.Skills)
                {
                    if(skillGeneralDTO.Url == string.Empty)
                    {
                        Console.WriteLine($"No Url Stored in skillGeneralDTO {skillGeneralDTO}, moving to next iteration");
                        continue;
                    }

                    string response = await WebClient.GetDataAtURL(dndBaseUrl, skillGeneralDTO.Url);

                    if(response == string.Empty)
                    {
                        Console.WriteLine("No Response from Client. Attempting next Iteration");
                        continue;
                    }

                    // PrintDTOData("Skill Info: " + response);

                    DescriptionDTO? newSkillDTO = JsonSerializer.Deserialize<DescriptionDTO>(response);

                    skillDTOs.Add(newSkillDTO);
                }
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
        private static SchemaRootDTO MapToSchemaDTOs_AbilityScores(List<AbilityScoreDTO> scores, List<DescriptionDTO> skills)
        {
            SchemaRootDTO exportDTO = new();

            // If Scores and Skills are unsorted, then the skills list will be populated with skill details in an order relative to the ability score list, since that's how they were loaded in originally.
            // This means that the first subset of skills in the list will be Deception, Intimidation, Performance, and Persuasion, because the first ability score will be Charisma, and so on.
            // This means that the list of skill GeneralDTOs on each ability score DTO, when put together, will be the same length and order of the list of Skills
            // This allows us to distribute the list of SkillDTOs to their assigned ability score without running a triple loop
            foreach(AbilityScoreDTO abScore in scores)
            {
                for(int index = 0; index < abScore.Skills.Count; index++)
                {
                    abScore.SkillsDetailed.Add(skills[index]);
                }
                skills.RemoveRange(0, abScore.Skills.Count);
            }

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
                    SchemaSkillDTO newSkill = new();
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
        
        #endregion
        
        // --------------------------------
        //	    UNIVERSAL FUNCTIONS
	    // --------------------------------
        #region UniversalFunctions

        private static async Task<T> GetSpecificDTOAtUrl<T>(string url)
        {
            string response = await WebClient.GetDataAtURL(dndBaseUrl, url);

            if(response == string.Empty)
            {
                Console.WriteLine($"No Initial Response Found from Client");
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(response);
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