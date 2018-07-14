using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//GContacts
using Google.Contacts;
using Google.GData.Contacts;
using Google.GData.Client;
using Google.GData.Extensions;

namespace ContactsNamedays
{

    class GoogleConnection
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        string[] Scopes = { CalendarService.Scope.CalendarReadonly, @"https://www.googleapis.com/auth/contacts.readonly" };
        string ApplicationName = "GContactsNamedayDotNET";
            //"Google Calendar API .NET Quickstart";

        UserCredential credential;

        public void start()
        {

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/client_secret.json");//calendar - dotnet - quickstart

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
        }

        public void calendar() { 
            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }
            Console.Read();

        }

        public void loadcontacts()
        {
            OAuth2Parameters parameters = new OAuth2Parameters();
            parameters.AccessToken = credential.Token.AccessToken;
            parameters.RefreshToken = credential.Token.RefreshToken;

            RequestSettings settings = new RequestSettings(ApplicationName, parameters);

            // Add authorization token.
            // ...
            ContactsRequest cr = new ContactsRequest(settings);
            PrintAllContacts(cr);


        }

        public HashSet<string> PrintAllContacts(ContactsRequest cr)
        {
            HashSet<string> contacts = new HashSet<string>();
            Feed<Contact> f = cr.GetContacts();
            foreach (Contact entry in f.Entries)
            {
                if (entry.Name != null)
                {
                    Name name = entry.Name;
                    if (!string.IsNullOrEmpty(name.GivenName))
                    {
                        Console.WriteLine("\t\t" + name.GivenName);
                        contacts.Add(name.GivenName.ToString());
                    }
                    else
                        Console.WriteLine("\t\t (no given name found)");                                        
                }
                else
                    Console.WriteLine("\t (no name found)");                
            }
            return contacts;
        }


    }
}