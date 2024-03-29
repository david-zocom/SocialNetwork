﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetworkApp
{
	public class SocialNetwork
	{
		// properties: all users, all posts
		// CreateUser, FindUser, DoPost, (GetTopPosts)

		public List<User> allUsers = new List<User>();
		public List<Post> allPosts = new List<Post>();

		public bool CreateUser(string firstName, string lastName)
		{
			User user = new User(firstName, lastName);

			// do not add a user that already exist in the list
			foreach (var person in allUsers)
			{
				if (person.firstName == firstName && person.lastName == lastName)
				{
					return false;
				}
			}

			// do not add if names are invalid
			if (IsInvalidName(firstName) || IsInvalidName(lastName))
			{
				return false;
			}

			allUsers.Add(user);
			return true;
		}

		private bool IsInvalidName(string name)
		{
			// null
			if (name == null)
				return true;

			// empty string ""
			if (name.Length == 0)
				return true;

			// string with numbers
			for (int i = 0; i < name.Length; i++)
			{
				if (Char.IsDigit(name[i]))
					return true;
			}

			return false;
		}


		// 1 return the User object
		// 2 do not return anything
		// 3 return true or false
		// 4 maybe it can throw exceptions
		//public bool CreateUser(string firstName, string lastName)
		// User: string firstName, lastName, ... (date of birth, profile picture)


		public List<User> FindUsers(string search)
		{
			List<User> matches = new List<User>();
			foreach (var user in allUsers)
			{
				if (user.firstName == search || user.lastName == search)
					matches.Add(user);
			}
			return matches;
		}

		public bool DoPost(string content, User user)
		{
			if (content == null || content == "")
				return false;

			if (user == null)
				return false;

			allPosts.Add(new Post(content, user));
			return true;
		}

	}
}
