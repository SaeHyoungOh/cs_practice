namespace console_calc.Class
{
	/*
	 * Class to hold user information
	 */

	public class User
	{
		private string userName;

		// default constructor
		public User() { }

		// initializing constructor
		public User(string name)
		{
			userName = name;
		}

		// set userName
		public void SetUserName(string name)
		{
			userName = name;
		}

		// get userName
		public string GetUserName()
		{
			return userName;
		}
	}
}