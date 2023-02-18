// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using System.Data.Common;

String sqlInsert = "Insert into ";
String filePath = "C:\\Users\\Jake\\Documents";
String filePathtoOpen = "C:\\Users\\Jake\\Documents\\TestCSv.csv";
String fileNametoSave;
String filePathtoSave = "Path has not been set.";
String dbTableName;
Boolean validFileName = false;
Boolean validFilePath = false;

do
{
    try
    {
        //Uncomment this section to change the filepath to the csv file.
       // Console.WriteLine("File path of csv file? (including name of file");
      //  filePathtoOpen = Console.ReadLine();
        string firstLine = File.ReadLines(filePathtoOpen).First();
        validFilePath = true;

        Console.WriteLine("Table Name?");
        dbTableName = Console.ReadLine();

        sqlInsert = "Insert into " + dbTableName + " (" + firstLine + "),\n";

        foreach (string csvLine in File.ReadLines(filePathtoOpen).Skip(0))
        {
            sqlInsert += "(" + csvLine + ")," + "\n";
        }
        sqlInsert = sqlInsert.Remove(sqlInsert.Length - 2) + ";";
        sqlInsert = sqlInsert.Replace("\"", "");

        Console.WriteLine(sqlInsert);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine("Please enter a valid file path.");
    }

} while (!validFilePath);

do
{
    try
    {
        //Uncomment 2 lines below to not use the hard coded filepath
        //Console.WriteLine("Where would you like to save the query? (path only, not the name)");
        //filePath = Console.ReadLine();

        Console.WriteLine("What would you like to name the query?");
        fileNametoSave = Console.ReadLine();
        filePathtoSave = filePath + "\\" + fileNametoSave;
        File.WriteAllText(filePathtoSave, sqlInsert);
        validFileName = true;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine("You must give a file name for the query to be saved.");
    }
   
} while (!validFileName);

Console.WriteLine("File saved to " + filePathtoSave);



