
using BL.OpenAI;
using OpenAI_API;
using OpenAI_API.Models;
using System.Reflection;
using System.Text.Json;


namespace BL
{
    public class OpenAi
    {
        private static OpenAi instance = null;
        private string apiKey;
        private OpenAIAPI api = null;


        private OpenAi(string apiKey)
        {
            this.apiKey = apiKey;

            api = new OpenAIAPI(apiKey);

        }

        public static OpenAi GetInstance(string apiKey)
        {

            if (instance == null)
            {
                instance = new OpenAi(apiKey);
            }
            return instance;

        }

        public async Task<PilotageInfo> AnalyzePilotageReq(string rawEmail)
        {
            //create a chat conversation
            var chat = api.Chat.CreateConversation();
            chat.Model = Model.GPT4_Turbo;

            chat.RequestParameters.Temperature = 0;


            /// give instruction as System
            chat.AppendSystemMessage(FileManager.ReadFile("files/pilotage_request/promtInstructions.txt"));

            // give a few examples as user and assistant
            chat.AppendUserInput(FileManager.ReadFile("files/pilotage_request/examples/1/input.txt"));
            chat.AppendExampleChatbotOutput(FileManager.ReadFile("files/pilotage_request/examples/1/output.txt"));

            // now let's ask it a question
            chat.AppendUserInput(rawEmail);
            // and get the response
            string response = await chat.GetResponseFromChatbotAsync();
            Console.WriteLine(response);




            PilotageInfo pilotageInfo = new PilotageInfo(response);




            return pilotageInfo;
        }
        public async Task<CategorizedEmail> CategorizeEmail(Email email)
        {
            //create a chat conversation
            var chat = api.Chat.CreateConversation();
            chat.Model = Model.GPT4_Turbo;

            chat.RequestParameters.Temperature = 0;


            /// give instruction as System
            chat.AppendSystemMessage(FileManager.ReadFile("files/categorizer/promtInstructions.txt"));

            // give a few examples as user and assistant
            //chat.AppendUserInput(FileManager.ReadFile("files/categorizer/examples/1/input.txt"));
            //chat.AppendExampleChatbotOutput(FileManager.ReadFile("files/categorizer/examples/1/output.txt"));

            // now let's ask it a question
            //make json object
            var input = new
            {
                body = email.Body,
                subject = email.Subject,
                from = email.From
            };

            chat.AppendUserInput(JsonSerializer.Serialize(input));
            // and get the response
            string response = await chat.GetResponseFromChatbotAsync();

            CategorizeEmailResponse category = new(response);



            CategorizedEmail categorizedEmail = new CategorizedEmail(email, category);




            return categorizedEmail;
        }
    }
}
