using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.IO;

namespace Connection_Monitor
{
    internal class Monitor
    {
        public static void Main(string[] args)
        {
            Console.Title = ("Connection Monitor");
            Console.WriteLine("");

            Console.WriteLine("Connection Monitor");
            Console.WriteLine("");
            Console.WriteLine("");


            var currentDate = DateTime.Now.ToString("ddd M\\-dd\\-yy");

            int pingNum = 0;
            string fileName = @"C:\Users\metro_int_user\Documents\InternetLogs\Connection-Log " + ".txt";
            string exitMessage = "Press ESC to exit to main menu.";

            // Check if a file with the same name exists if so, delete it
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Create a new file     
            using (FileStream fs = File.Create(fileName))
            {
                sw.WriteLine("Done! ");
            }

                //Create log file
                Console.WriteLine("");
            Console.WriteLine("===STARTED CHECKING INTERNET SERVICE===");
            Console.WriteLine("");
            Console.WriteLine("File Succesfully Created In Documents");

            using (StreamWriter recorder = new StreamWriter(fileName))
            {
                recorder.WriteLine("===INTERNET SERVICE LOG FOR " + currentDate + "===");
                recorder.WriteLine("");
                Ping serviceChecker = new Ping();

                // create timer to send ping to google every 30 seconds
                var mainTimer = new System.Timers.Timer();
                mainTimer.Interval = 5000;
                mainTimer.Enabled = true;
                mainTimer.AutoReset = true;
                mainTimer.Start();

                //while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                //{
                    mainTimer.Elapsed += onTimedEvent;
                    void onTimedEvent(object source, System.Timers.ElapsedEventArgs elapsedEventArgs)
                    {
                        Console.WriteLine("!Sending ping attempt to google.com");
                        Console.Read();
                        try
                        {
                            Console.WriteLine("!Sending ping attempt to google.com");
                            PingReply reply = serviceChecker.Send("172.217.1.132", 2);
                            if (reply != null)
                            {
                                var currentTime = DateTime.Now.ToString("hh:mm tt");
                                string successMessage = "#PING " + pingNum + ": Status Return = SUCCESSFULL / Time Recorded = [" + currentTime + "]";
                                pingNum++;
                                Console.WriteLine(successMessage);
                                //recorder.WriteLine(successMessage);
                                Console.WriteLine(exitMessage);
                                mainTimer.Stop();
                                mainTimer.Start();
                            }
                            else
                            {
                                var currentTime = DateTime.Now.ToString("hh:mm tt");
                                string failMessage = "#PING " + pingNum + ": Status Return = FAILURE / Time Recorded = [" + currentTime + "]";
                                pingNum++;
                                Console.WriteLine(failMessage);
                                recorder.WriteLine(failMessage);
                                Console.WriteLine(exitMessage);
                                mainTimer.Stop();
                                mainTimer.Start();
                            }
                        }
                        catch
                        {
                            Console.WriteLine("!ERROR ENCOUNTERED WHILE ATTEMPTING TO CONTACT GOOGLE SERVER");
                            Console.WriteLine("!PRESS ANY KEY TO RETURN TO THE MAIN MENU");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }

                    }

                //}

            }

            string ans = Console.ReadLine();
        }

        //private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        //{
        //    //some business logic goes here
        //    ReCheckConnection();
        //}

        //private static void ReCheckConnection()
        //{
        //    var mainTimer = new System.Timers.Timer();
        //    mainTimer.Enabled = false;
        //    if (false)
        //    {
        //        mainTimer.Interval = 60000; //run after 60 seconds
        //        mainTimer.Start();

        //    }

        //}

    }
}
