using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinnerHandler.Model
{

	public enum Language
	{
		[Description("Norsk")]
		Norsk,
		[Description("Engelsk")]
		Engelsk,
		[Description("Dansk")]
		Dansk,
		[Description("Svensk")]
		Svensk
	}

	public enum Format
	{
		[Description("Innbundet")]
		Innbundet,
		[Description("Pocket")]
		Pocket,
		[Description("Box")]
		Box,
		[Description("Lese eksemplar")]
		LeseEksemplar
	}

	public enum Location
	{
		[Description("Hjemme")]
		Hjemme,
		[Description("Rommet")]
		Rommet,
		[Description("Loftet")]
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
