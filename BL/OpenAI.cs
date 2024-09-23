using OpenAI_API;
using OpenAI_API.Models;
using System.Reflection;


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
            chat.AppendSystemMessage(FileManager.ReadFile("files/promtInstructions.txt"));

            // give a few examples as user and assistant
            chat.AppendUserInput(FileManager.ReadFile("files/examples/1/input.txt"));
            chat.AppendExampleChatbotOutput(FileManager.ReadFile("files/examples/1/output.txt"));

            // now let's ask it a question
            chat.AppendUserInput(rawEmail);
            // and get the response
            string response = await chat.GetResponseFromChatbotAsync();
            Console.WriteLine(response);




            PilotageInfo pilotageInfo = new PilotageInfo(response);




            return pilotageInfo;
        }
    }
}
