using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DinnerHandler.Model
{
	class BookList
	{

		public List<Book> Library { get; set; }


		public void AddBook(Book BookToAdd)
		{
			if (Library.Any(book => BookToAdd.Title.ToLower() == book.Title.ToLower()))
			{
				return;
			}

			Library.Add(BookToAdd);
		}
		public void RemoveIngredient(Book bookToRemove)
		{
			Book removeThis = null;
			foreach (var book in Library)
			{
				if (bookToRemove.Title.ToLower() == book.Title.ToLower())
				{
					removeThis = book;
				}
			}

			if (removeThis != null)
			{
				Library.Remove(removeThis);
			}
		}

		public string Serialize()
		{
			var settings = new JsonSerializerSettings();

			// Convert object to json string.
			return JsonConvert.SerializeObject(this.Library, Formatting.Indented, settings);
		}

		/// <summary>
		/// Deserializes JSON data file and updates itself based on the deserialized data
		/// </summary>
		public void Deserialize(string jsonData)
		{
			var statsObjectList = JsonConvert.DeserializeObject<List<Book>>(jsonData);
			Library = statsObjectList;
		}
	}
}
