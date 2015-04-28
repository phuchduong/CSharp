using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tweetinvi;
using System.Threading;
using Microsoft.ServiceBus.Messaging;

namespace TweetStreamA
{
    class Program
    {
        static void Main(string[] args)
        {   
            // Event Hub connection string
            String eventHubName = "tweethub";
            String connectionString = "Endpoint=sb://tweetstream.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=zekWmi6kZwRGgWukTWin4Fu3oFhutUthxDkR71W/XaU=";
            var myEventHub = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);

            // Twitter variables
            String aToken = "1318985240-yYIz4hVWNmbvbpNeMvRkPNPIwxjCO2XqFOb4leQ";
            String aTokenSecret = "FBteOpkARK8kN3dpy6aVk3qgQMaKA7OC2xDsyDxU5pmjz";
            String APIKey = "JqyH4BF9JozhYgAXYoljbWw8H";
            String APISecret = "At0dRtyjqHIvJJtI32nggqwzAiWG25TEVHfe7QsR9LBIvs425c";

            // Set twitter credentials
            TwitterCredentials.SetCredentials(aToken, aTokenSecret, APIKey, APISecret);

            //Initialize a stream object
            var stream = Stream.CreateSampleStream();
            stream.TweetReceived += (s, TweetReceived) =>
            {
                try
                {
                    String tweetPayload = TweetReceived.Tweet.Text.ToString();
                    Console.WriteLine(tweetPayload);
                    myEventHub.Send(new EventData(Encoding.UTF8.GetBytes(tweetPayload)));
                    //// Filter by English tweets.
                    //String tweetLanguage = TweetReceived.Tweet.Language.ToString();
                    //if (tweetLanguage.Equals("English"))
                    //{
                    //    Console.WriteLine(TweetReceived.Tweet.Text);
                    //}
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
                    Console.ResetColor();
                }
            };
            stream.StartStream();
        }
    }
}
