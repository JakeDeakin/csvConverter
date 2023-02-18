// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using System.Data.Common;

String columnName;
String sqlInsert;
String filePath;
String filePathtoOpen;
String fileNametoSave;
String dbTableName;
List<String> dbTableColumns;

Console.WriteLine("File path of csv file?");
filePathtoOpen = Console.ReadLine();

Console.WriteLine("Table Name?");
dbTableName = Console.ReadLine();

//dbTableColumns = new List<String>();
//Console.WriteLine("How many columns?");
//int dbColumnCount = Int32.Parse(Console.ReadLine());

//for (int i = 0; i < dbColumnCount; i++)
//{
//    Console.WriteLine("Name of column " + (i + 1) + "?");
//    columnName = Console.ReadLine();
//    dbTableColumns.Add(columnName);
//}

string firstLine = File.ReadLines(filePathtoOpen).First();
sqlInsert = "Insert into " + dbTableName + " (" + firstLine + "),\n";

//foreach (string cn in dbTableColumns)
//{
//    sqlInsert += cn + ",";
//}

//sqlInsert += "),\n";
//string firstLine = File.ReadLines(filePathtoOpen).First();
//dbTableColumns = new List<String>();
//dbTableColumns = firstLine.Split(',').ToList();
//int num = dbTableColumns.Count;
//Console.WriteLine(num);

foreach (string csvLine in File.ReadLines(filePathtoOpen).Skip(0))
{
    sqlInsert += "(" + csvLine + ")," + "\n";
}
sqlInsert = sqlInsert.Remove(sqlInsert.Length - 2) + ";";
sqlInsert = sqlInsert.Replace("\"", "");

Console.WriteLine(sqlInsert);
Console.WriteLine("What would you like to name the query?");
fileNametoSave = Console.ReadLine();
Console.WriteLine("Where would you like to save the query?");
filePath = Console.ReadLine();
File.WriteAllText(filePath + "\\" + fileNametoSave, sqlInsert);

