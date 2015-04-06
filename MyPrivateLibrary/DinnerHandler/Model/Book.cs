using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinnerHandler.Model
{

	public enum Language
	{
		Norsk,
		Engelsk,
		Dansk,
		Svensk
	}

	public enum Format
	{
		Innbundet,
		Pocket,
		Box,
		LeseEksemplar
	}

	public enum Location
	{
		Hjemme,
		Rommet,
		Loftet
	}

	public class Book
	{

		public string Title { get; set; }
		public string Author { get; set; }
		public string Genre { get; set; }
		public string Series { get; set; }
		public Language Language { get; set; }
		public Format Format { get; set; }
		public string ISBN { get; set; }
		public string Publisher { get; set; }
		public Location Location { get; set; }

		public Book(string title, string author, string genre, string series, Language language, Format format, string isbn, string publisher, Location location)
		{
			Title = title;
			Author = author;
			Genre = genre;
			Series = series;
			Language = language;
			Format = format;
			ISBN = isbn;
			Publisher = publisher;
			Location = location;
		}

	}
}
