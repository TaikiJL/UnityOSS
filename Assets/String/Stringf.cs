/*
﻿The MIT License (MIT)

Copyright (c) 2016 Taiki Hagiwara

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

https://github.com/TaikiJL/UnityOSS 
*/

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
