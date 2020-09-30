/*

using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration

namespace UAMmodule
{
    class CRUD
    {

    const string clientId = "Enter clientId from Portal";
    const string tenant = "<yourtenant>.onmicrosoft.com";
    const string redirectUri = "https://localhost";

    // Change the following between each call to create/update user if not deleting the user
    private static string givenName = "test99";
    private static string surname = "user99";

    private static void Main(string[] args)
    {
        // Initialize and prepare MSAL
        string[] scopes = new string[] { "user.read", "user.readwrite.all" };

        IPublicClientApplication app = PublicClientApplicationBuilder.Create(clientId)
            .WithAuthority(new Uri($"https://login.microsoftonline.com/{tenant}"))
            .WithRedirectUri(redirectUri)
            .Build();

        // Initialize the Graph SDK authentication provider
        InteractiveAuthenticationProvider authenticationProvider = new InteractiveAuthenticationProvider(app, scopes);
        GraphServiceClient graphServiceClient = new GraphServiceClient(authenticationProvider);

        // Get information from Graph about the currently signed-In user
        Console.WriteLine("--Fetching details of the currently signed-in user--");
        GetMeAsync(graphServiceClient).GetAwaiter().GetResult();
        Console.WriteLine("---------");

        // Create a new user
        Console.WriteLine($"--Creating a new user in the tenant '{tenant}'--");
        User newUser = CreateUserAsync(graphServiceClient).Result;
        PrintUserDetails(newUser);
        Console.WriteLine("---------");

        // Update an existing user
        if (newUser != null)
        {
            Console.WriteLine("--Updating the detail of an existing user--");
            User updatedUser = UpdateUserAsync(graphServiceClient, userId: newUser.Id, jobTitle: "Program Manager").Result;
            PrintUserDetails(updatedUser);
            Console.WriteLine("---------");
        }

        // List existing users
        Console.WriteLine("--Listing all users in the tenant--");
        List<User> users = GetUsersAsync(graphServiceClient).Result;
        users.ForEach(u => PrintUserDetails(u));
        Console.WriteLine("---------");

        // Delete this user
        Console.WriteLine("--Deleting a user in the tenant--");
        if (newUser != null)
        {
            DeleteUserAsync(graphServiceClient, newUser?.Id).GetAwaiter().GetResult(); ;
        }

        Console.WriteLine("---------");

        // List existing users after deletion
        Console.WriteLine("--Listing all users in the tenant after deleting a user.--");
        users = GetUsersAsync(graphServiceClient).Result;
        users.ForEach(u => PrintUserDetails(u));
        Console.WriteLine("---------");

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }



    private static async Task GetMeAsync(GraphServiceClient graphServiceClient)
    {
        // Call /me Api
        var me = await graphServiceClient.Me.Request().GetAsync();
        Console.WriteLine($"Display Name from /me->{me.DisplayName}");

        var directreports = await graphServiceClient.Me.DirectReports.Request().GetAsync();

        foreach (User user in directreports.CurrentPage)
        {
            Console.WriteLine($"Report's Display Name ->{user.DisplayName}");
        }
    }


    private static async Task<User> CreateUserAsync(GraphServiceClient graphServiceClient)
    {
        User newUserObject = null;

        string displayname = $"{givenName} {surname}";
        string mailNickName = $"{givenName}{surname}";
        string upn = $"{mailNickName}{tenant}";
        string password = "p@$$w0rd!";

        try
        {

            newUserObject = await graphServiceClient.Users.Request().AddAsync(new User
            {
                AccountEnabled = true,
                DisplayName = displayname,
                MailNickname = mailNickName,
                GivenName = givenName,
                Surname = surname,
                PasswordProfile = new PasswordProfile
                {
                    Password = password
                },
                UserPrincipalName = upn
            });
        }
        catch (ServiceException e)
        {
            Console.WriteLine("We could not add a new user: " + e.Error.Message);
            return null;
        }

        return newUserObject;
    }


    private static void PrintUserDetails(User user)
    {
        if (user != null)
        {
            Console.WriteLine($"DisplayName-{user.DisplayName}, MailNickname- {user.MailNickname}, GivenName-{user.GivenName}, Surname-{user.Surname}, Upn-{user.UserPrincipalName}, JobTitle-{user.JobTitle}, Id-{user.Id}");
        }
        else
        {
            Console.WriteLine("The provided User is null!");
        }
    }



    private static async Task<User> UpdateUserAsync(GraphServiceClient graphServiceClient, string userId, string jobTitle)
    {
        User updatedUser = null;
        try
        {

            // Update the user.
            updatedUser = await graphServiceClient.Users[userId].Request().UpdateAsync(new User
            {
                JobTitle = jobTitle
            });
        }
        catch (ServiceException e)
        {
            Console.WriteLine($"We could not update details of the user with Id {userId}: " + $"{e}");
        }

        return updatedUser;
    }


    private static async Task<List<User>> GetUsersAsync(GraphServiceClient graphServiceClient)
    {
        List<User> allUsers = new List<User>();

        try
        {

            IGraphServiceUsersCollectionPage users = await graphServiceClient.Users.Request().Top(5).GetAsync();

            // When paginating
            //while(users.NextPageRequest != null)
            //{
            //    users = await users.NextPageRequest.GetAsync();
            //}

            if (users?.CurrentPage.Count > 0)
            {
                foreach (User user in users)
                {
                    allUsers.Add(user);
                }
            }
        }
        catch (ServiceException e)
        {
            Console.WriteLine("We could not retrieve the user's list: " + $"{e}");
            return null;
        }

        return allUsers;
    }


    private static async Task DeleteUserAsync(GraphServiceClient graphServiceClient, string userId)
    {
        try
        {
            await graphServiceClient.Users[userId].Request().DeleteAsync();
        }
        catch (ServiceException e)
        {
            Console.WriteLine($"We could not delete the user with Id-{userId}: " + $"{e}");
        }
    }



}
}
*/