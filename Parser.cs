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

        public static async Task StartParser()
        {
            // If there were more than one step to this parser, it would be included sequentially here
            // I'm hardcoding in the starting point for the ability scores, but this could easily be configured and sent in as an additional parameter
            
            // Missing Categories for 2014:
            // - Backgrounds (1)
            // - Feats (1)

            // Missing Categories for 2024:
            // - Backgrounds (4)
            // - Monsters (3)
            // - Rule-sections (0)
            // - Rules (0)
            // - Spells (0)

            await TryParseAbilityScores();
        }

        public static async Task TryParseAll()
        {
            List<Task> tasks = new List<Task> {
                TryParseAbilityScores()
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
        private static async Task TryParseAbilityScores(string abilityScoresUrl = "/api/2014/ability-scores")
        {
            try
            {
                // Import DTOs for later mapping
                List<GeneralInfoDTO> abilityScoreGeneralDTOs = new();
                List<AbilityScoreDTO> abilityScoreDTOs = new();
                List<SkillDTO> skillDTOs = new();

                // Get initial list of scores
                ResultsDTO resultsDTO = await GetResultsDTO(abilityScoresUrl);

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
        private static async Task AddAbilityScores(List<GeneralInfoDTO> abilityScoreGeneralDTOs, List<AbilityScoreDTO> abilityScoreDTOs)
        {
            foreach(GeneralInfoDTO abilityScoreSmall in abilityScoreGeneralDTOs)
            {
                string response = await WebClient.GetDataAtURL(dndBaseUrl, abilityScoreSmall.Url);

                if(response == string.Empty)
                {
                    Console.WriteLine("No Response from Client. Attempting next Iteration");
                    continue;
                }

                // PrintDTOData("AbilityScores: " + response);

                // Store the score details per ability score
                AbilityScoreDTO? newAbilityScoreDTO = JsonSerializer.Deserialize<AbilityScoreDTO>(response);
                abilityScoreDTOs.Add(newAbilityScoreDTO);

            }
        }

        /// <summary>
        /// Runs through each abilityScoreDTO, grabs their list of Skills, and loads/stores the associated detailed SkillDTO
        /// </summary>
        /// <param name="client"></param>
        /// <param name="abilityScoreDTOs"></param>
        /// <param name="skillDTOs"></param>
        private static async Task AddSkills(List<AbilityScoreDTO> abilityScoreDTOs, List<SkillDTO> skillDTOs)
        {
            foreach(AbilityScoreDTO scoreDTO in abilityScoreDTOs)
            {
                foreach(GeneralInfoDTO skillGeneralDTO in scoreDTO.Skills)
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

                    SkillDTO? newSkillDTO = JsonSerializer.Deserialize<SkillDTO>(response);

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
        private static SchemaRootDTO MapToSchemaDTOs_AbilityScores(List<AbilityScoreDTO> scores, List<SkillDTO> skills)
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
                foreach(SkillDTO skillDetail in abScoreDTO.SkillsDetailed)
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

        private static async Task<ResultsDTO> GetResultsDTO(string categoryUrl)
        {
            string response = await WebClient.GetDataAtURL(dndBaseUrl, categoryUrl);

            if(response == string.Empty)
            {
                Console.WriteLine($"No Initial Response Found from Client");
                return null;
            }

            return JsonSerializer.Deserialize<ResultsDTO>(response);
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