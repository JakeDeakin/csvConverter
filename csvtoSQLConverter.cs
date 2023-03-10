using System.Windows.Markup;

namespace MyConverter
{
    public class MainController
    {
        static readonly String? filePath = "C:\\Users\\Jake\\Documents\\sqlOutput";
        static readonly String? filePathtoOpen = "C:\\Users\\Jake\\Documents\\test2.csv";
        //static readonly String? filePath;
        //static readonly String? filePathtoOpen;

        public static void Main()
        {
            CSVToSQLConverter converter = new();
            converter.TransposeColumns(filePathtoOpen, filePath);
            //converter.ConvertCSVToSQLFile(filePathtoOpen, filePath);
        }
    }

    public class CSVToSQLConverter
    {
        String sqlInsert = "Insert into ";
        String dbTableName = "Table";
        Boolean validFileName = false;
        Boolean validFilePath = false;

        public void ConvertCSVToSQLFile(String? filePathtoOpen, String? filePathtoSave)
        {
            do
            {
                try
                {
                    if (filePathtoOpen == String.Empty || filePathtoOpen == null)
                    {
                        Console.WriteLine("File path of csv file? (including name of file)");
                        filePathtoOpen = Console.ReadLine() ?? String.Empty;
                    }

                    if (filePathtoOpen == null)
                    {
                        throw new Exception("File path is null");
                    }

                    string firstLine = File.ReadLines(filePathtoOpen).First();

                    validFilePath = true;

                    Console.WriteLine("Table Name?");
                    dbTableName = Console.ReadLine() ?? String.Empty;

                    sqlInsert = "Insert into " + dbTableName + " (" + firstLine + "),\n";

                    foreach (string csvLine in File.ReadLines(filePathtoOpen).Skip(1))
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
                    filePathtoOpen = String.Empty;
                }

            } while (!validFilePath);

            saveOutput(sqlInsert,filePathtoSave);
        }

        private void saveOutput(string output, string? filePathtoSave)
        {
            do
            {
                try
                {
                    if (filePathtoSave == String.Empty || filePathtoSave == null)
                    {
                        Console.WriteLine("Where would you like to save the query? (file name inclusive)");

                        filePathtoSave = Console.ReadLine() ?? String.Empty;
                    }

                    if (filePathtoSave == null)
                    {
                        throw new Exception("File path is null");
                    }
                    File.WriteAllText(filePathtoSave, output);
                    validFileName = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("You must give a valid file path for the query to be saved.");
                    filePathtoSave = String.Empty;
                }

            } while (!validFileName);

            Console.WriteLine("File saved to " + filePathtoSave);
        }

        public void TransposeColumns(String? filePathtoOpen, String? filePathtoSave)
        {
            List<List<String>> columns = new ();
            List<String> rows = new ();
            String values = "";
            List<String> tempRows;
            List<List<String>> tempColumns = new();
            

            foreach (string line in File.ReadLines(filePathtoOpen))
            {
                rows = line.Split(',').ToList();
                columns.Add(rows);
            }

            for (int i = 0; i < rows.Count; i++)
            {
                tempRows = new();
                foreach (List<String> list in columns)
                {
                    tempRows.Add(list[i]);
                }
                tempColumns.Add(tempRows);
            }

            columns = tempColumns;

            foreach (List<String> list in columns)
            {
                string firstValue = list[0];
                foreach (String value in list.Skip(1))
                {
                    values += firstValue + "," + value + "\n";
                }
                values = values.Remove(values.Length - 1) + "\n";
            }
            Console.WriteLine(values);

            saveOutput(values, filePathtoSave);
            ConvertCSVToSQLFile(filePathtoSave, filePathtoSave);
        }
    }

}



