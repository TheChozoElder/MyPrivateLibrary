using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using DinnerHandler.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace DinnerHandler.ViewModel {
	/// <summary>
	/// This class contains properties that the main View can data bind to.
	/// <para>
	/// See http://www.galasoft.ch/mvvm
	/// </para>
	/// </summary>
	public class MainViewModel : ViewModelBase
	{

		#region Commands
		public ICommand ValidateBookInfo
		{
			get;
			private set;
		}
		public ICommand DeleteBookCommand
		{
			get;
			private set;
		}
		#endregion

		#region BindedVariables

		private Book activeBook { get; set; }
		public string ActiveBookName = "ActiveBook";
		public Book ActiveBook
		{
			get
			{
				return activeBook;
			}
			set
			{
				if (activeBook != value)
				{
					activeBook = value;
				}
				RaisePropertyChanged(ActiveBookName);
			}
		}
		#endregion

		private int timesAddedBookWhoAllreadyExists = 0;

		public string LibraryName = "library";
		public List<Book> Library
		{
			get
			{
				return library.Library;
			}
			set
			{
				if (library.Library != value)
				{
					library.Library = value;
				}
				RaisePropertyChanged(LibraryName);
			}
		}

		private BookList library { get; set; }

		private const string libPath = "Library.json";

		public MainViewModel()
		{
			library = new BookList {Library = new List<Book>()};

			activeBook = new Book("", "", "", "", 0, 0, "", "", 0);

			try {
				var jsonStream = new StreamReader(libPath);
				var jsonString = jsonStream.ReadToEnd();
				library.Deserialize(jsonString);
				jsonStream.Close();
			} catch(FileNotFoundException fnf){
				Console.Write(fnf.FileName + " not found. Creating new file.");
				using (var writer = new StreamWriter(libPath))
				{
					writer.Write((library.Serialize()));
				}
			}
				CreateCommands();
		}

		private void CreateCommands()
		{
			ValidateBookInfo = new RelayCommand(ValidateBook);
			DeleteBookCommand = new RelayCommand(DeleteBook);
		}

		private void ValidateBook()
		{

			bool foundAndEdited = false;
			if (activeBook.Author == "" || activeBook.Title == "")
			{
				DisplayMessage("Min mor sier alltid 'For å ha orden i biblioteket ditt må alle bøker ha både navn og tittel, Karl!'. Det burde du også følge..");
				return;
			}

			for (int i = 0; i < Library.Count; i++)
			{

				if (Library.ElementAt(i).Author.ToLower() == activeBook.Author.ToLower()
					&& Library.ElementAt(i).Title.ToLower() == activeBook.Title.ToLower())
				{
					Library.ElementAt(i).Format = activeBook.Format;
					Library.ElementAt(i).Genre = activeBook.Genre;
					Library.ElementAt(i).ISBN = activeBook.ISBN;
					Library.ElementAt(i).Language = activeBook.Language;
					Library.ElementAt(i).Location = activeBook.Location;
					Library.ElementAt(i).Publisher = activeBook.Publisher;
					Library.ElementAt(i).Series = activeBook.Series;

					foundAndEdited = true;
				}
			}

			if (!foundAndEdited)
			{
				Library.Add(activeBook);
			}

			activeBook = new Book("", "", "", "", 0, 0, "", "", 0);

			RaisePropertyChanged(ActiveBookName);

			LibraryUpdated();
		}

		private void DeleteBook()
		{
			for (int i = 0; i < Library.Count; i++)
			{

				if (Library.ElementAt(i).Author.ToLower() == activeBook.Author.ToLower()
					&& Library.ElementAt(i).Title.ToLower() == activeBook.Title.ToLower())
				{
					library.RemoveBook(Library.ElementAt(i));
				}
			}

			LibraryUpdated();
		}

		private void LibraryUpdated()
		{
			var newList = Library.ToList();
			library.Library = newList;

			using (var writer = new StreamWriter(libPath))
			{
				writer.Write((library.Serialize()));
			}

			RaisePropertyChanged(LibraryName);
		}

		private void DisplayMessage(String message)
		{
			Debug.Print(message);
		}

		private void LibraryChanged()
		{
			var newList = library.Library.ToList();

			using (var writer = new StreamWriter(libPath))
			{
				writer.Write((library.Serialize()));
			}

			library.Library = newList;
			RaisePropertyChanged(LibraryName);
		}
	}
}