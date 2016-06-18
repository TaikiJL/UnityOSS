public struct Stringf
{

	/// <summary>
	/// Returns the length of the given string without counting Rich Text tags.
	/// </summary>
	/// <returns>The text length.</returns>
	/// <param name="str">String.</param>
	public static int RichTextLength(string str)
	{
		if (string.IsNullOrEmpty (str))
			return 0;

		int length = 0;
		int i = 0;

		while (i < str.Length)
		{
			while (i < str.Length && !str[i].Equals('<'))
			{
				++i;
				++length;
			}
			++i;

			while (i < str.Length && !str[i].Equals('>'))
				++i;
			++i;
		}

		return length;
	}

}
