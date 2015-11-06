using System;
using System.IO;

using Mono.Data.Sqlite;
using System.Data;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using Android.OS;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB
{
	[Activity (Label = "DB", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			new DatabaseCopy(this); // ВЫЗОВ КОПИРОВАНИЯ БД ИЗ ПАПКИ Assets

			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			Button button = FindViewById<Button> (Resource.Id.myButton);
			ListView peopleList = FindViewById<ListView>(Resource.Id.listView1);




			button.Click += delegate {
				List<string> tempList = new List<string> ();
				SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath); // СОЗДАНИЕ ПЕРЕМЕННОЙ ПОДКЛЮЧЕНИЕ К БД
				SqliteCommand command; // ПЕРЕМЕННАЯ КОМАНД
				SqliteDataReader reader;// ПЕРЕМЕННАЯ ЧТЕНИЯ ИЗ ПОТОКА ПОЛУЧЕННЫХ МАССИВОВ

				connection.Open(); // ОТКРЫТИЕ ПОДКЛЮЧЕНИЯ

				command = connection.CreateCommand(); // СОЗДАНИЕ КОМАНД

				command.CommandText = "SELECT name,surname FROM students"; // СОЗДАНИЕ ЗАПРОСА ПОЛУЧЕНИЯ ИНФОРМАЦИИ О СТУДЕНТАХ

				reader = command.ExecuteReader(); // ПОЛУЧЕНИЕ ДАННЫХ СОГЛАСНО ЭТОМУ ЗАПРОСУ
				while(reader.Read()) // ЦИКЛ ПРОХОЖДЕНИЯ ПО ВСЕМ ПОЛУЧЕННЫМ МАССИВАМ
				{
					tempList.Add(reader[0].ToString() + " " + reader[1].ToString()); //СОХРАНЕНИЕ ПОЛУЧЕННОЙ ИНФОРМАЦИИ
				}
				reader.Close(); // ЗАКРЫТИЕ ПОТОКА ЧТЕНИЯ

				connection.Close(); // ЗАКРЫТИЕ ПОДКЛЮЧЕНИЯ К БД

				peopleList.Adapter = new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleListItem1,tempList); // ПЕРЕДАЧА НА ИНТЕРФЕЙС ПОЛУЧЕННЫХ ДАННЫХ
			};
		}
	}
}


