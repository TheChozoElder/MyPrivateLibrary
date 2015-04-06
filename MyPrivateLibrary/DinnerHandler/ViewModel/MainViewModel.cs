using System;
using System.Collections.Generic;
using System.ComponentModel;
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

//			var oki = new Book("Wicca - den gamle religion i det nye årtusen", "Aartun, Jorun Sofie Fallmo", "Debatt/samfunn",
//				"Den mørke materien:1,2,3", Language.Norsk, Format.Innbundet, "6445186437645", "Vigmostad & Bjørke", Location.Hjemme);
//			var doli = new Book("fewa", "ee", "Alfabet", "ABC", Language.Norsk, Format.Box, "5754427619665",
//				"Guriland productions", Location.Loftet);
//			library.AddBook(oki);
//			library.AddBook(doli);

			try {
				var jsonStream = new StreamReader(libPath);
				var jsonString = jsonStream.ReadToEnd();
				library.Deserialize(jsonString);
				jsonStream.Close();
			} catch(FileNotFoundException fnf){

				using (var writer = new StreamWriter(libPath))
				{
					writer.Write((library.Serialize()));
				}
			}
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