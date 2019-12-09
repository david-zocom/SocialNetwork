namespace SocialNetworkApp
{
	public class Post
	{
		public string content;
		public User poster;

		public Post(string content, User poster)
		{
			this.content = content;
			this.poster = poster;
		}
	}
}