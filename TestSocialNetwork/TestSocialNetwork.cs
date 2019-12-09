using System;
using Xunit;
using SocialNetworkApp;
using System.Collections.Generic;

namespace TestSocialNetwork
{
	public class TestSocialNetwork
	{
		// CreateUser, FindUser, DoPost, GetTopPost

		#region CreateUser test cases
		// When CreateUser is called it should add the new user to the list of all users
		[Fact]
		public void ShouldCreateUser_ForCorrectParameters()
		{
			// arrange
			string firstName = "Nils", lastName = "Holgersson";
			SocialNetwork socialNetwork = new SocialNetwork();

			// act
			bool result = socialNetwork.CreateUser(firstName, lastName);

			// assert
			Assert.True(result, "Did not return true");
			// assert that the created User actually is in the list allUsers
			bool exist = UserExist(socialNetwork.allUsers, firstName, lastName);
			Assert.True(exist, "No user with that name exist in allUsers");
		}
		public bool UserExist(List<User> list, string firstName, string lastName)
		{
			foreach (var user in list)
			{
				if (user.firstName == firstName && user.lastName == lastName)
				{
					return true;
				}
			}
			return false;
		}

		// - inte om användaren redan finns
		[Fact]
		public void ShouldNotCreateUser_WhenUserAlreadyExists()
		{
			// arrange
			SocialNetwork socialNetwork = new SocialNetwork();
			string firstName = "Gordon", lastName = "Brown";
			User existingUser = new User(firstName, lastName);
			socialNetwork.allUsers.Add(existingUser);

			// act
			bool result = socialNetwork.CreateUser(firstName, lastName);

			// assert
			Assert.False(result, "Function returned true");
			Assert.Single(socialNetwork.allUsers);
		}
		// - inte om namnet innehåller otillåtna tecken, tom sträng "" eller null
		[Theory]
		[InlineData("aaaaaa5dddddd", "Ice")]
		[InlineData("Vanilla", "pppp33pppp")]
		[InlineData("", "Nilsson")]
		[InlineData("Herr", "")]
		[InlineData(null, "Jdkfmdmf")]
		[InlineData("Poji", null)]
		public void ShouldNotCreateUser_IfNameisInvalid(string firstName, string lastName)
		{
			// arrange
			SocialNetwork socialNetwork = new SocialNetwork();
			//string firstName = "aaaaa5ddddd", lastName = "Ice";

			// act
			bool result = socialNetwork.CreateUser(firstName, lastName);

			// assert
			Assert.False(result, "CreateUser returned true");
			Assert.Empty(socialNetwork.allUsers);
		}
		#endregion

		// FindUsers should return an empty list if there are no matching users
		// FindUsers should return a list of matching users if at least one matches
		// - in a real-world application, we should explain what we mean with "matches" - exact match? Or approximate? (Per=Persson etc.)
		// ???? which users are already in the network ????

		[Fact]
		public void ShouldReturnEmptyList_IfNoMatchingUsers()
		{
			// arrange
			SocialNetwork socialNetwork = new SocialNetwork();

			// act
			List<User> matches = socialNetwork.FindUsers("Michael");

			// assert
			Assert.Empty(matches);
		}

		[Fact]
		public void ShouldReturnMatchingUsers_IfAnyMatch()
		{
			// arrange
			string search = "Godzilla";
			SocialNetwork socialNetwork = new SocialNetwork();
			User u1 = new User(search, "Persson");
			User u2 = new User("Nisse", search);
			User u3 = new User("Pelle", "Svanslös");
			socialNetwork.allUsers.Add(u1);
			socialNetwork.allUsers.Add(u2);

			// act
			List<User> matches = socialNetwork.FindUsers(search);

			// assert
			Assert.Equal(2, matches.Count);
		}
	}
}
